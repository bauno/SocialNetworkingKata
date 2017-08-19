using CSharp.Core.Services.Interfaces;
using LanguageExt;

namespace CSharp.Core.Services
{
    public class Nothing : Displayable
    {
        public override Unit ShowOn(Display display)
        {
            return Unit.Default;
        }
    }
}