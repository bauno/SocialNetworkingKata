using System;

namespace SocialNetwork.Core.Services.Interfaces
{
    public interface PostTsFormatter
    {
        string NiceTs(DateTime now, DateTime postTs);
    }
}