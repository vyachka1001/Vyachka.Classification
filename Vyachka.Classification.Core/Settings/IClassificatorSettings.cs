using System.Collections.Generic;

namespace Vyachka.Classification.Core.Settings
{
    public interface IClassificatorSettings
    {
        void AddParameterSetting(string paramName, int measureFreq, double maxDeviation);
        ParameterSetting this[string paramName] { get; }
        IReadOnlyCollection<string> ParamNames { get; }
    }
}
