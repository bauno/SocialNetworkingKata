﻿using CSharp.Core.Services.Interfaces;

namespace CSharp.Core.Commands.Interfaces
{
    public interface Command : Message
    {
        void SendTo(SocialNetwork socialNetwork);
    }
}