using System;
using System.Collections.Generic;
using Vyachka.Classification.Core.Models;
using Vyachka.Classification.Core.Settings;

namespace Vyachka.Classification.Core
{
    public class Classificator
    {
        private readonly IClassificatorSettings _classificatorSettings;
        private double _delta;

        public Classificator(IClassificatorSettings classificatorSettings)
        {
            _classificatorSettings = classificatorSettings;
        }

        public ClassificationResult Process(ParameterValuesContainer valuesContainer)
        {
            if (valuesContainer == null)
            {
                throw new ArgumentNullException(nameof(valuesContainer));
            }

            if (valuesContainer.Values.Length < 2)
            {
                return new ClassificationResult()
                {
                    GoodRanges = new List<ValueRange>()
                    {
                        new ValueRange(0, 0)
                    }
                };
            }
            
            var values = valuesContainer.Values;
            var switchIndexes = new List<int>();
            var i = 0;
            var iMax = values.Length - 1;
            SetDelta(valuesContainer.ParamName);

            var isGoodRange = AreNeighborsGood(values[0], values[1]);
            if (!isGoodRange)
            {
                switchIndexes.Add(i);
            }

            while (i < iMax)
            {
                while (i < iMax && !AreNeighborsGood(values[i], values[i + 1]))
                {
                    i++;
                }
                switchIndexes.Add(i);

                while (i < iMax && AreNeighborsGood(values[i], values[i + 1]))
                {
                    i++;
                }
                switchIndexes.Add(i);
            }

            var badRanges = new List<ValueRange>();
            var goodRanges = new List<ValueRange>();

            for (i = 1; i < switchIndexes.Count; i++)
            {
                var startIndex = switchIndexes[i - 1];
                var endIndex = switchIndexes[i];

                if (isGoodRange)
                {
                    var goodRange = new ValueRange(startIndex, endIndex);
                    goodRanges.Add(goodRange);
                }
                else
                {
                    var badRange = new ValueRange(startIndex, endIndex);
                    badRanges.Add(badRange);
                }

                isGoodRange = !isGoodRange;
            }

            var classificationResult = new ClassificationResult
            {
                BadRanges = badRanges,
                GoodRanges = goodRanges
            };

            return classificationResult;
        }

        private void SetDelta(string paramName)
        {
            var parameterSetting = _classificatorSettings[paramName];
            _delta = parameterSetting.MaxDeviation / parameterSetting.MeasureFreq;
        }

        private bool AreNeighborsGood(double value1, double value2)
        {
            return Math.Abs(value1 - value2) <= _delta;
        }
    }
}