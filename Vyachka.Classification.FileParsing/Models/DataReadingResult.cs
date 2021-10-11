using System;

namespace Vyachka.Classification.FileParsing.Models
{
    public class DataReadingResult
    {
        public DateTime RecordingDateTimeUtc { get; set; }
        public ParameterValuesContainerCollection ParametersCollection { get; set; }
    }
}
