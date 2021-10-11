using System.Collections.Generic;

namespace Vyachka.Classification.Core.Models
{
    public class ClassificationResult
    {
        public List<ValueRange> GoodRanges { get; set; }
        public List<ValueRange> BadRanges { get; set; }

        public ClassificationResult()
        {
            GoodRanges = new List<ValueRange>();
            BadRanges = new List<ValueRange>();
        }
    }
}