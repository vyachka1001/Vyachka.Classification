namespace Vyachka.Classification.Core.Settings
{
    public class ParameterSetting
    {
        public string ParamName { get; }
        public int MeasureFreq { get; }
        public double MaxDeviation { get; }

        public ParameterSetting(string paramName, int measureFreq, double maxDeviation)
        {
            ParamName = paramName;
            MeasureFreq = measureFreq;
            MaxDeviation = maxDeviation;
        }
    }
}
