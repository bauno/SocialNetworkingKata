using System;
using CSharp.Core.Services.Interfaces;

namespace CSharp.Core.Services
{
    public class PostTsStringFormatter : PostTsFormatter
    {
        public string NiceTs(DateTime now, DateTime postTs)
        {
            var delta = now.Subtract(postTs);
            if (delta.TotalMinutes < 1)
                return $"{delta.TotalSeconds} seconds ago";
            return $"{(int)delta.TotalMinutes} minutes ago";
        }
    }
}