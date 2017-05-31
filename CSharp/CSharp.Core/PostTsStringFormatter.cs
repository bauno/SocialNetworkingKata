using System;

namespace CSharp.Core
{
    public class PostTsStringFormatter : PostTsFormatter
    {
        public string NiceTs(DateTime now, DateTime postTs)
        {
            var delta = now.Subtract(postTs).TotalSeconds;
            if (delta < 60)
                return $"{delta} seconds ago";
            var minutes = (int) delta / 60;
            return $"{minutes} minutes ago";
        }
    }
}