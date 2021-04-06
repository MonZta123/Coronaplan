using System;

namespace BeastSources.Coronaplan.Api.Exceptions
{
    public class ExceptionBase : Exception
    {
        public string ErrorText { get; }
        public long ErrorCode { get; }

        public ExceptionBase(string errorText, long errorCode)
        {
            this.ErrorCode = errorCode;
            this.ErrorText = errorText;
        }
    }
}