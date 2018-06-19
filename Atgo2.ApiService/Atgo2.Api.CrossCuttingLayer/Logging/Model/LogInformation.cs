using System;

namespace Atgo2.Api.CrossCuttingLayer.Logging.Model
{
    public class LogInformation
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Module { get; set; }
        public string Data { get; set; }
        public Exception Exception { get; set; }
    }
}
