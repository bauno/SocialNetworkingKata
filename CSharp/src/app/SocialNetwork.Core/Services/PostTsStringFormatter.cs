using System;
using SocialNetwork.Core.Services.Interfaces;

namespace SocialNetwork.Core.Services
{
    public class PostTsStringFormatter : PostTsFormatter
    {
        public string NiceTs(DateTime now, DateTime postTs)
        {
            var delta = now.Subtract(postTs);
            return delta.TotalMinutes < 1 ? $"{(int)delta.TotalSeconds} seconds ago" : 
                $"{(int)delta.TotalMinutes} minutes ago";
        }
    }
}