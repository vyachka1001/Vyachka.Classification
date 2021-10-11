using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Vyachka.DataFileGeneration
{    
    public class Program
    {
        private const int NumberOfValues = 10;
        private const int CoreMinValue = 4;
        private const int CoreMaxValue = 6;
        private const string ParamName1 = "Velocity";
        private const string ParamName2 = "Altitude";
        private const string ParamName3 = "Horizontal_velocity";
        private static readonly Random Generator = new Random();
        private const string Path = "parameter_values";

        private static void Main(string[] args)
        {
            CreateTextDataFile();
            CreateBinaryDataFile();
        }

        private static void CreateBinaryDataFile()
        {
            var header = CreateHeader();
            var dictionary = new Dictionary<string, double[]>
            {
                {ParamName1, CreateDataArray()},
                {ParamName2, CreateDataArray()},
                {ParamName3, CreateDataArray()}
            };
            using (var bw = new BinaryWriter(File.Open($"{Path}.bin", FileMode.Create))) 
            {
                bw.Write(header);
                foreach (var param in dictionary)
                {
                    bw.Write(param.Key);
                    bw.Write(param.Value.Length);
                    foreach (var value in param.Value)
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

        private static void CreateTextDataFile()
        {
            var lines = new string[4];
            lines[0] = CreateHeader();
            lines[1] = CreateDataLine(ParamName1);
            lines[2] = CreateDataLine(ParamName2);
            lines[3] = CreateDataLine(ParamName3);
            File.WriteAllLines($"{Path}.txt", lines);
        }

        private static string CreateHeader()
        {
            const string dateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
            var todayDate = DateTime.Now;
            var header = todayDate.ToString(dateTimeFormat);

            return header;
        }

        private static string CreateDataLine(string valueName)
        {
            var line = valueName + " ";
            for (var i = 0; i < NumberOfValues; i++)
            {
                var value = Generator.NextDouble() * (CoreMaxValue - CoreMinValue) + CoreMinValue;

                line += value.ToString("F", CultureInfo.InvariantCulture);
                if (i + 1 < NumberOfValues)
                {
                    line += ',';
                }
            }

            return line;
        }
    }
}
