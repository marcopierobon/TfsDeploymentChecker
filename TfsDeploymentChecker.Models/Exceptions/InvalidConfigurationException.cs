using System;

namespace TfsDeploymentChecker.Models.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string errorCode, string errorMessage) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }

        public InvalidConfigurationException(Exception exception, string errorCode, string errorMessage) : base(errorMessage, exception)
        {
            ErrorCode = errorCode;
            Exception = exception;
        }
        
        public string ErrorCode { get; set; }

        public Exception Exception { get; set; }
    }
}
