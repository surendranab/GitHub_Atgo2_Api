using Atgo2.Api.CrossCuttingLayer.Logging.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Atgo2.Api.CrossCuttingLayer.Logging.Interfaces
{
    public interface IServiceLogger 
    {
        void Error([Localizable(false)] string message, [Localizable(false)] string argument);
        void Error(Exception exception, [Localizable(false)] string message, params object[] args);
        void Error([Localizable(false)] string message);
        void Info([Localizable(false)] string message);
        void Log(LogInformation logInformation, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
    }
}
