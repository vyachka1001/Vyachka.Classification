using System;

namespace Vyachka.Classification.FileParsing.Exceptions
{
    public class InvalidFileFormatException : Exception
    {
        public InvalidFileFormatException() : base()
        {

        }

        public InvalidFileFormatException(string message) : base(message)
        {

        }
    }
}