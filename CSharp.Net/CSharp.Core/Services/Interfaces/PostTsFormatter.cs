using System;

namespace CSharp.Core.Services.Interfaces
{
    public interface PostTsFormatter
    {
        string NiceTs(DateTime now, DateTime postTs);
    }
}