using System;

namespace TaNoMenu.Exceptions
{
    public class HttpException : Exception
    {

        public int StatusCode { get; }

        public override string Message { get; }

        public HttpException(string message, int statusCode)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}