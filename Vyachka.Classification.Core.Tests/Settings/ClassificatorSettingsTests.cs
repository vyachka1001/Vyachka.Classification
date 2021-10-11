using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Vyachka.Classification.Core.Settings;

namespace Vyachka.Classification.Core.Tests.Settings
{
    [TestFixture]
    internal class ClassificatorSettingsTests
    {
        [Test]
        public void ClassificatorSettingsConstructorThrowsArgumentNullExceptionWhenInputCollectionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ClassificatorSettings(null));
        }

        [Test]
        public void ParamNamesReturnsValidParameterNames()
        {
            var testCollection = new List<ParameterSetting>
            {
                new ParameterSetting("Velocity", 10, 23.21),
                new ParameterSetting("Altitude", 12, 20.12)
            };

            var classificatorSettings = new ClassificatorSettings(testCollection);
            var actualParamNames = classificatorSettings.ParamNames;

            for (var i = 0; i < actualParamNames.Count; i++)
            {
                Assert.AreEqual(testCollection[i].ParamName, actualParamNames.ToList()[i]);
            }
        }

        [Test]
        public void ParameterSettingIndexerReturnsValidParameterSettingWhenItExists()
        {
            var testCollection = new List<ParameterSetting>
            {
                new ParameterSetting("Velocity", 10, 23.21),
                new ParameterSetting("Altitude", 12, 20.12)
            };

            var classificatorSettings = new ClassificatorSettings(testCollection);

            foreach (var parameterSetting in testCollection)
            {
                AssertParameterSettingsAreEqual(
                    parameterSetting, classificatorSettings[parameterSetting.ParamName]);
            }
        }

        [Test]
        public void ParameterSettingIndexerReturnsNullWhenCollectionDoesNotContainsRequiredParamName()
        {
            var testCollection = new List<ParameterSetting>
            {
                new ParameterSetting("Velocity", 10, 23.21),
                new ParameterSetting("Altitude", 12, 20.12)
            };
            var classificatorSettings = new ClassificatorSettings(testCollection);

            Assert.IsNull(classificatorSettings["not_exist"]);
        }

        [Test]
        public void ClassificatorSettingsConstructorThrowsArgumentExceptionWhenUserTryToAddParametersWithEqualNames()
        {
            var testCollection = new List<ParameterSetting>
            {
                new ParameterSetting("Velocity", 10, 23.21),
                new ParameterSetting("Velocity", 12, 20.12)
            };
            Assert.Throws<ArgumentException>((() => new ClassificatorSettings(testCollection)));
        }

        [Test]
        public void ClassificatorSettingsCanBeCreatedFromParameterSettingCollection()
        {
            var expectedCollection = new List<ParameterSetting>
            {
                new ParameterSetting("Velocity", 10, 23.21),
                new ParameterSetting("Altitude", 12, 20.12)
            };
            
            var settings = new ClassificatorSettings(expectedCollection);
            Assert.AreEqual(expectedCollection.Count, settings.ParamNames.Count);

            for (var index = 0; index < expectedCollection.Count; index++)
            {
                var expectedSetting = expectedCollection[index];
                var actualSetting = settings[expectedCollection[index].ParamName];
                AssertParameterSettingsAreEqual(expectedSetting, actualSetting);
            }
        }

        [Test]
        public void ParameterSettingCanBeAddedCorrectly()
        {
            var parameterCollection = new List<ParameterSetting>
            {
                new ParameterSetting("Velocity", 10, 23.21),
                new ParameterSetting("Altitude", 12, 20.12)
            };

            var settings = new ClassificatorSettings(parameterCollection);
            var countBeforeAdding = settings.ParamNames.Count;

            var parameterToAdd = new ParameterSetting("Horizontal Velocity", 20, 21.02);
            settings.AddParameterSetting(parameterToAdd.ParamName, parameterToAdd.MeasureFreq, parameterToAdd.MaxDeviation);

            var countAfterAdding = settings.ParamNames.Count;

            Assert.AreEqual(countBeforeAdding + 1, countAfterAdding);

            var lastElement = settings[parameterToAdd.ParamName];
            AssertParameterSettingsAreEqual(parameterToAdd, lastElement);

            var expectedCollection = parameterCollection;

            for (var index = 0; index < expectedCollection.Count; index++)
            {
                var expectedSetting = expectedCollection[index];
                var actualSetting = settings[expectedCollection[index].ParamName];
                AssertParameterSettingsAreEqual(expectedSetting, actualSetting);
            }
        }

        private static void AssertParameterSettingsAreEqual(ParameterSetting expected, ParameterSetting actual)
        {
            Assert.AreEqual(expected.ParamName, actual.ParamName);
            Assert.AreEqual(expected.MeasureFreq, actual.MeasureFreq);
            Assert.AreEqual(expected.MaxDeviation, actual.MaxDeviation);
        }
    }
}
     
