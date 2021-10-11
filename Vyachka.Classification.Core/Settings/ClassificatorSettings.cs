using System;
using System.Collections.Generic;
using System.Linq;

namespace Vyachka.Classification.Core.Settings
{
    public class ClassificatorSettings : IClassificatorSettings
    {
        private readonly Dictionary<string, ParameterSetting> _innerParameterSettings;

        public IReadOnlyCollection<string> ParamNames
        {
            get { return _innerParameterSettings.Keys.ToList(); }
        }

        public ClassificatorSettings()
        {
            _innerParameterSettings = new Dictionary<string, ParameterSetting>();
        }

        public ClassificatorSettings(List<ParameterSetting> parameterSettingsCollection)
        {
            if (parameterSettingsCollection == null)
            {
                throw new ArgumentNullException(nameof(parameterSettingsCollection));
            }

            _innerParameterSettings = new Dictionary<string, ParameterSetting>();

            foreach (var parameterSetting in parameterSettingsCollection)
            {
                _innerParameterSettings.Add(parameterSetting.ParamName, parameterSetting);
            }
        }

        public void AddParameterSetting(string paramName, int measureFreq, double maxDeviation)
        {
            var parameterSetting = new ParameterSetting(paramName, measureFreq, maxDeviation);
            _innerParameterSettings.Add(paramName, parameterSetting);
        }

        public ParameterSetting this[string paramName]
        {
            get
            {
                return _innerParameterSettings.ContainsKey(paramName) ? _innerParameterSettings[paramName] : null;
            }
        }
    }
}
