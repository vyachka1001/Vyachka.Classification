using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Vyachka.Classification.Core.Settings;
using Vyachka.Classification.FileParsing.Tests.TestHelpers;

namespace Vyachka.Classification.FileParsing.Tests
{
    [TestFixture]
    internal class FileSettingParserTests
    {
        private readonly Dictionary<string, string> _fileNamesForTests = new Dictionary<string, string>
        {
            {"InvalidNumberOfParamValuesTest", "invalid_number_of_param_values_in_one_line.txt"} ,
            {"InvalidMeasureFreqTest", "invalid_measure_freq_format.txt"} ,
            {"InvalidMaxDeviationFormat", "invalid_max_deviation_format.txt"},
            {"EmptyFile", "empty_file.txt"},
            {"ProperParsingTest", "valid_file.txt"}
        };

        [Test]
        public void ParseFileToParameterCollectionThrowsArgumentExceptionWhenLineHasInvalidNumberOfParamValues()
        {
            Assert.Throws<ArgumentException>(() =>
                FileSettingParser.ParseFileToParameterCollection(
                    CommonTestHelper.BuildFilePath(_fileNamesForTests["InvalidNumberOfParamValuesTest"])));
        }

        [Test]
        public void ParseFileToParameterCollection_TakesFileWithLineThatHasInvalidMeasureFreqFormat_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() =>
                FileSettingParser.ParseFileToParameterCollection(
                    CommonTestHelper.BuildFilePath(_fileNamesForTests["InvalidMeasureFreqTest"])));
        }

        [Test]
        public void ParseFileToParameterCollectionThrowsFormatExceptionWhenLineHasInvalidMaxDeviationFormat()
        {
            Assert.Throws<FormatException>(() =>
                FileSettingParser.ParseFileToParameterCollection(
                    CommonTestHelper.BuildFilePath(_fileNamesForTests["InvalidMaxDeviationFormat"])));
        }

        [Test]
        public void ParseFileToParameterCollectionThrowsFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() =>
                FileSettingParser.ParseFileToParameterCollection("not_found"));
        }

        [Test]
        public void ParseFileToParameterCollectionReturnsEmptyAndNotNullCollectionWhenFileIsEmpty()
        {
            var emptyCollection =
                FileSettingParser.ParseFileToParameterCollection(
                    CommonTestHelper.BuildFilePath(_fileNamesForTests["EmptyFile"]));
            Assert.IsNotNull(emptyCollection);
            Assert.IsEmpty(emptyCollection);
        }

        [Test]
        public void ParseFileToParameterCollectionReturnsCorrectlyFilledCollection()
        {
            var expectedCollection = new List<ParameterSetting>();
            var lines = File.ReadAllLines(CommonTestHelper.BuildFilePath(_fileNamesForTests["ProperParsingTest"]));
            foreach (var line in lines)
            {
                var parameterSetting = CreateParameterSettingFromLine(line);
                expectedCollection.Add(parameterSetting);
            }

            var actualCollection = FileSettingParser
                .ParseFileToParameterCollection(CommonTestHelper.BuildFilePath(_fileNamesForTests["ProperParsingTest"])).ToList();

            Assert.AreEqual(expectedCollection.Count, actualCollection.Count, "Collection.Count does not match");
            for (var index = 0; index < expectedCollection.Count; index++)
            {
                var actual = actualCollection[index];
                var expected = expectedCollection[index];
                Assert.AreEqual(expected.ParamName, actual.ParamName, nameof(actual.ParamName));
                Assert.AreEqual(expected.MeasureFreq, actual.MeasureFreq, nameof(actual.MeasureFreq));
                Assert.AreEqual(expected.MaxDeviation, actual.MaxDeviation, nameof(actual.MaxDeviation));
            }
        }

        private static ParameterSetting CreateParameterSettingFromLine(string line)
        {
            var paramArr = line.Split(FileSettingParser.LineSeparator);
            var paramName = paramArr[0];
            var measureFreq = int.Parse(paramArr[1]);
            var maxDeviation = double.Parse(paramArr[2], NumberStyles.Number, CultureInfo.InvariantCulture);
            
            var setting = new ParameterSetting(paramName, measureFreq, maxDeviation);
            return setting;
        }
    }
}
