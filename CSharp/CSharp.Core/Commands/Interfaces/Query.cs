using System.Collections.Generic;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;

namespace CSharp.Core.Commands.Interfaces
{
    public interface Query : Message
    {
        IEnumerable<WallView> Exec(SocialNetwork socialNetwork);
    }
}