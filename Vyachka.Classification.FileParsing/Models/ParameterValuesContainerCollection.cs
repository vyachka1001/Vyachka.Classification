using System;
using System.Collections.Generic;
using System.Linq;
using Vyachka.Classification.Core.Models;

namespace Vyachka.Classification.FileParsing.Models
{
    public class ParameterValuesContainerCollection
    {
        private readonly Dictionary<string, ParameterValuesContainer> _parameterValues;

        public IReadOnlyCollection<string> ParamNames
        {
            get { return (_parameterValues.Keys.ToList()); }
        }
        public ParameterValuesContainerCollection()
        {
            _parameterValues = new Dictionary<string, ParameterValuesContainer>();
        }

        public ParameterValuesContainerCollection(Dictionary<string, ParameterValuesContainer> parameterValues)
        {
            if (parameterValues == null)
            {
                throw new ArgumentNullException(nameof(parameterValues));
            }

            _parameterValues = new Dictionary<string, ParameterValuesContainer>();

            foreach (var parameterValue in parameterValues)
            {
                _parameterValues.Add(parameterValue.Key, parameterValue.Value);
            }
        }

        public ParameterValuesContainer this[string paramName]
        {
            get
            {
                return _parameterValues.ContainsKey(paramName) ? _parameterValues[paramName] : null;
            }
        }
    }
}
