using System;

namespace CSharp.Core.Services
{
    public static class TimeService
    {
        internal static DateTime? TestNow { get; set; }
        
        public static DateTime Now()
        {
            return TestNow ?? DateTime.Now;
        }
    }
}