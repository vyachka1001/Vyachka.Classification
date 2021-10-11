using System;
using System.Collections.Generic;
using System.IO;
using Vyachka.Classification.Core.Models;
using Vyachka.Classification.FileParsing.Exceptions;
using Vyachka.Classification.FileParsing.Models;

namespace Vyachka.Classification.FileParsing
{
    public static class ParameterValuesFileReader
    {
        public static DataReadingResult ReadParamsFromBinaryFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            var parameterValues = new Dictionary<string, ParameterValuesContainer>();
            DateTime dateTime;

            try
            {
                using (var br = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    var dateTimeString = br.ReadString();
                    dateTime = DateTime.Parse(dateTimeString);
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        var paramName = br.ReadString();
                        var numberOfValues = br.ReadInt32();
                        var values = new double[numberOfValues];
                        for (var i = 0; i < numberOfValues; i++)
                        {
                            values[i] = br.ReadDouble();
                        }

                        var container = new ParameterValuesContainer(paramName, values);
                        parameterValues.Add(paramName, container);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidFileFormatException(ex.Message);
            }
            
            var readingResult = new DataReadingResult
            {
                RecordingDateTimeUtc = dateTime,
                ParametersCollection = new ParameterValuesContainerCollection(parameterValues)
            };

            return readingResult;
        }

    /*    public void ReadParamNamesAndValuesFromTextFile(string path)
        {
            if (!File.Exists($"{path}.txt"))
            {
                throw new FileNotFoundException();
            }

            using (var sr = new StreamReader($"{path}.txt"))
            {
                sr.ReadLine();
                while (sr.Peek() != -1)
                {
                    var line = sr.ReadLine();
                    var valuesArr = line.Split(FileSettingParser.LineSeparator);
                    var paramName = valuesArr[0];
                    var values = CreateArrOfValuesFromStringArr(valuesArr, valuesArr.Length);
                    AddParameterValuesContainer(paramName, values);
                }
            }
        }

        private static double[] CreateArrOfValuesFromStringArr(string[] valuesArr, int numberOfValues)
        {
            if (valuesArr.Length != numberOfValues + 1)
            {
                throw new ArgumentException($"Invalid line '{valuesArr}' : expected {numberOfValues + 1} elements.",
                    nameof(valuesArr));
            }

            var values = new double[numberOfValues];
            for (var i = 1; i < valuesArr.Length; i++)
            {
                var isCorrectParsing = double.TryParse(valuesArr[i], out values[i - 1]);
                if (!isCorrectParsing)
                {
                    throw new FormatException($"Can not parse value '{valuesArr[i]}'.");
                }
            }

            return values;
        }*/
    }
}


/*
 1) в 1-й строке - строка(сделать валидный файл, кроме первой строки(число туда))
 2) проверить на правильность DateTime(если формат неправильный, бросается Ex.)
 3) header без данных(вернется валидный dateTime и пустой не null словарь)
 4) на правильность результата
 */