using NUnit.Framework;
using Vyachka.Classification.Core.Settings;

namespace Vyachka.Classification.Core.Tests.Settings
{
    [TestFixture]
    internal class ParameterSettingTests
    {
        [Test]
        public void ParameterSettingCanBeCreated()
        {
            const string paramName = "velocity";
            const int measureFreq = 10;
            const double maxDeviation = 256.2;
            var setting = new ParameterSetting(paramName, measureFreq, maxDeviation);

            Assert.AreEqual(paramName, setting.ParamName, nameof(setting.ParamName));
            Assert.AreEqual(measureFreq, setting.MeasureFreq, nameof(setting.MeasureFreq));
            Assert.AreEqual(maxDeviation, setting.MaxDeviation, nameof(setting.MaxDeviation));
        }
    }
}
