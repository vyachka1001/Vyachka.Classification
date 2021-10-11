using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Vyachka.Classification.FileParsing.Exceptions;
using Vyachka.Classification.FileParsing.Tests.TestHelpers;

namespace Vyachka.Classification.FileParsing.Tests
{
    [TestFixture]
    internal class ParameterValuesFileReaderTests
    {
        private readonly Dictionary<string, string> _fileNamesForTests = new Dictionary<string, string>
        {
            {"EmptyFile", "empty_file.bin"},
            {"InvalidFirstStringType", "invalid_1st_string_type.bin"},
            {"InvalidFirstStringFormat", "invalid_1st_string_format.bin"},
            {"FileWithOnlyValidHeader", "valid_header_file.bin" },
            {"ValidFile", "valid_file.bin" }
        };

        private static readonly Random Generator = new Random();
        private const int NumberOfValues = 10;
        private const int CoreMinValue = 4;
        private const int CoreMaxValue = 6;
        private const string ParamName1 = "Velocity";
        private const string ParamName2 = "Altitude";

        [Test]
        public void ReadParamsFromBinaryFileThrowsFileNotFoundExceptionWhenPathDoesNotExists()
        {
            Assert.Throws<FileNotFoundException>(() => 
                ParameterValuesFileReader.ReadParamsFromBinaryFile("path"));
        }

        [Test]
        public void ReadParamsFromBinaryFileReturnsEmptyDataReadingResultWhenFileIsEmpty()
        {
            Assert.Throws<InvalidFileFormatException>(() =>
                ParameterValuesFileReader.ReadParamsFromBinaryFile(
                    CommonTestHelper.BuildFilePath(_fileNamesForTests["EmptyFile"])));
        }

        [Test]
        public void ReadParamsFromBinaryFileThrowsExceptionWhenItIsIntInTheFirstString()
        {
            var path = CommonTestHelper.BuildFilePath(_fileNamesForTests["InvalidFirstStringType"]);
            using (var bw = new BinaryWriter(File.Create(path)))
            {
                bw.Write(124);
                WriteValidDataInFile(bw, ParamName1);
            }

            Assert.Throws<InvalidFileFormatException>(() => ParameterValuesFileReader.ReadParamsFromBinaryFile(path));
        }

        [Test]
        public void ReadParamsFromBinaryFileThrowsExceptionWhenItIsInvalidDateTimeFormat()
        {
            var path = CommonTestHelper.BuildFilePath(_fileNamesForTests["InvalidFirstStringFormat"]);
            using (var bw = new BinaryWriter(File.Create(path)))
            {
                bw.Write("Not DateTime format!");
                WriteValidDataInFile(bw, ParamName1);
            }

            Assert.Throws<InvalidFileFormatException>(() =>
                ParameterValuesFileReader.ReadParamsFromBinaryFile(path));
        }
        
        [Test]
        public void ReadParamsFromBinaryFileReturnsEmptyNotNullDictionaryAndValidDateTimeWhenFileHasOnlyValidHeader()
        {
            var path = CommonTestHelper.BuildFilePath(_fileNamesForTests["FileWithOnlyValidHeader"]);
            var header = CreateHeader();

            using (var bw = new BinaryWriter(File.Create(path)))
            {
                bw.Write(header);
            }

            var parameterValues = ParameterValuesFileReader.ReadParamsFromBinaryFile(path);
            Assert.IsNotNull(parameterValues.ParametersCollection);
            Assert.IsEmpty(parameterValues.ParametersCollection.ParamNames);
            Assert.AreEqual(DateTime.Parse(header), parameterValues.RecordingDateTimeUtc);
        }
        
        [Test]
        public void ReadParamsFromBinaryFileReturnsCorrectlyParsedDataReadingResult()
        {
            var expectedCollection = new Dictionary<string, double[]>()
            {
                {ParamName1, CreateDataArray()},
                {ParamName2, CreateDataArray()}
            };
            var header = CreateHeader();

            var path = CommonTestHelper.BuildFilePath(_fileNamesForTests["ValidFile"]);
            CreateDataFile(header, path, expectedCollection);
            var actualCollection = ParameterValuesFileReader.ReadParamsFromBinaryFile(path);

            Assert.AreEqual(DateTime.Parse(header), actualCollection.RecordingDateTimeUtc);
            var actualCollectionParamNames = actualCollection.ParametersCollection.ParamNames;
            Assert.AreEqual(expectedCollection.Keys.Count, actualCollectionParamNames.Count);

            for (var i = 0; i < actualCollection.ParametersCollection.ParamNames.Count; i++)
            {
                var expected = expectedCollection.Keys.ToList()[i];
                var actual = actualCollectionParamNames.ToList()[i];
                var actualValues = actualCollection.ParametersCollection[actual].Values;
                var expectedValues = expectedCollection[expected];
                
                AssertParameterValuesAreEqual(actualValues, expectedValues);
            }
        }

        private void AssertParameterValuesAreEqual(double[] actual, double[] expected)
        {
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        private static void WriteValidDataInFile(BinaryWriter bw, string paramName)
        {
            var data = CreateDataArray();
            bw.Write(paramName);
            bw.Write(data.Length);
            foreach (var value in data)
            {
                bw.Write(value);
            }
        }

        private void CreateDataFile(string header, string path, Dictionary<string, double[]> expectedCollection)
        {
            using (var bw = new BinaryWriter(File.Create(path)))
            {
                bw.Write(header);
                foreach (var parameterValue in expectedCollection)
                {
                    bw.Write(parameterValue.Key);
                    bw.Write(parameterValue.Value.Length);
                    foreach (var value in parameterValue.Value)
                    {
                        bw.Write(value);
                    }
                }
            }
        }

        private static double[] CreateDataArray()
        {
            var dataArray = new double[NumberOfValues];
            for (var i = 0; i < NumberOfValues; i++)
            {
                var value = Generator.NextDouble() * (CoreMaxValue - CoreMinValue) + CoreMinValue;
                dataArray[i] = value;
            }

            return dataArray;
        }

        private static string CreateHeader()
        {
            const string dateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
            var todayDate = DateTime.Now;
            var header = todayDate.ToString(dateTimeFormat);

            return header;
        }
    }
}