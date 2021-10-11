using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Vyachka.Classification.Core.Settings;

namespace Vyachka.Classification.FileParsing
{
    public static class FileSettingParser
    {
        public static char LineSeparator { get; set; } = ',';

        public static IReadOnlyCollection<ParameterSetting> ParseFileToParameterCollection(string path)
        {
            var parameterSettingCollection = new List<ParameterSetting>();
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parameterSetting = CreateParameterSettingFromLine(line);
                parameterSettingCollection.Add(parameterSetting);
            }

            return parameterSettingCollection;
        }

        private static ParameterSetting CreateParameterSettingFromLine(string line)
        {
            var paramArr = line.Split(LineSeparator);
            if (paramArr.Length != 3)
            {
                throw new ArgumentException($"Invalid line '{line}' : expected 3 elements.", nameof(line));
            }

            var paramName = paramArr[0];
            var isCorrectParsing = int.TryParse(paramArr[1], out var measureFreq);
            if (!isCorrectParsing)
            {
                throw new FormatException($"Can not parse measure freq '{paramArr[1]}'.");
            }

            isCorrectParsing = double.TryParse(paramArr[2], NumberStyles.Number, CultureInfo.InvariantCulture,
                out var maxDeviation);
            if (!isCorrectParsing)
            {
                throw new FormatException($"Can not parse max deviation '{paramArr[2]}'.");
            }

            var setting = new ParameterSetting(paramName, measureFreq, maxDeviation);
            return setting;
        }
    }
}



