using System;

namespace CSharp.Core
{
    public interface PostTsFormatter
    {
        string NiceTs(DateTime now, DateTime postTs);
    }
}