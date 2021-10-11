using System;

namespace Vyachka.Classification.Core.Models
{
    public class ValueRange
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public ValueRange(int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
            {
                throw new ArgumentException("StartIndex is greater than EndIndex.");
            }

            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}