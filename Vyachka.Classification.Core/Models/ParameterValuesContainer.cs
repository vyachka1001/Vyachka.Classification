namespace Vyachka.Classification.Core.Models
{
    public class ParameterValuesContainer
    {
        public string ParamName { get; }
        public double[] Values { get; }

        public ParameterValuesContainer(string paramName, double[] values)
        {
            ParamName = paramName;
            Values = values;
        }
    }
}
