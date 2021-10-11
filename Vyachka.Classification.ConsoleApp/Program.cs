using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Vyachka.Classification.Core;
using Vyachka.Classification.Core.Settings;
using Vyachka.Classification.FileParsing;
using Vyachka.Classification.FileParsing.Models;

namespace Vyachka.Classification.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parameterCollection = EnterParameterCollection();

            if (parameterCollection != null)
            {
                var parameterValues = ReadParameterValues();
                var paramContainer =
                    parameterValues.ParametersCollection[
                        parameterValues.ParametersCollection.ParamNames.FirstOrDefault()];
                if (paramContainer != null)
                {
                    var classificator = CreateClassificator(parameterCollection);
                    var processingResult = classificator.Process(paramContainer);
                }
            }
            //var values = new double[] {2, 2, 3, 5, 7, 8, 2, 2};
            //var expectedIndexes = new int[] {0, 2, 4, 5, 6, 7}; // 2, 2, 3 - good; 3, 5, 7 - bad; 7, 8 - good; 8, 2 - bad; 2, 2 - good;

            Console.ReadKey();
        }

        private static DataReadingResult ReadParameterValues()
        {
            var parameterValuesPath = ConfigurationManager.AppSettings["ParameterValuesFilePath"];
            var parameterValues = ParameterValuesFileReader.ReadParamsFromBinaryFile(parameterValuesPath);

            return parameterValues;
        }

        private static IReadOnlyCollection<ParameterSetting> EnterParameterCollection()
        {
            var path = ConfigurationManager.AppSettings["ParameterSettingsFilePath"];
            IReadOnlyCollection<ParameterSetting> parameterCollection = null;
            try
            {
                parameterCollection = FileSettingParser.ParseFileToParameterCollection(path);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return parameterCollection;
        }

        private static Classificator CreateClassificator(IReadOnlyCollection<ParameterSetting> parameterCollection)
        {
            var classificatorSettings = new ClassificatorSettings(parameterCollection.ToList());
            var classificator = new Classificator(classificatorSettings);

            return classificator;
        }

        private static bool AreEqual(int[] expectedIndexes, List<int> switchIndexes)
        {
            if (expectedIndexes.Length != switchIndexes.Count)
            {
                return false;
            }

            for (var i = 0; i < expectedIndexes.Length; i++)
            {
                if (expectedIndexes[i] != switchIndexes[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
