using System;

namespace TfsDeploymentChecker.Models.Exceptions
{
    public class CustomBlazorException : Exception
    {
        public CustomBlazorException(string errorMessage) : base(errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }
}
