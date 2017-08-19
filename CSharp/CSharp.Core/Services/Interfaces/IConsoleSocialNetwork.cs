using CSharpFunctionalExtensions;
using LanguageExt;

namespace CSharp.Core.Services.Interfaces
{
    public interface IConsoleSocialNetwork
    {
        Option<string> Enter(string cmdString);
    }
}