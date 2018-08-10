using System;
using CSharp.Core;
using CSharp.Core.Services;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class PostTimeStampFormatterTests
    {
        [TestCase(2)]
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(40)]
        [TestCase(50)]
        public void WillShowSecondsIfLessThanAMinute(int seconds)
        {
            var now = DateTime.Now;
            var postTs = now.AddSeconds(-seconds);
            var sut = new PostTsStringFormatter();
            var res = $"{seconds} seconds ago";
            Assert.AreEqual(res, sut.NiceTs(now, postTs));
        }

        [TestCase(61, 1)]
        [TestCase(121, 2)]
        [TestCase(350, 5)]
        [TestCase(599, 9)]
        [TestCase(600, 10)]
        [TestCase(601, 10 )]
        public void WilShowMinutesIfMoreThan60Seconds(int seconds, int minutes)
        {
            var now = DateTime.Now;
            var postTs = now.AddSeconds(-seconds);
            var sut = new PostTsStringFormatter();
            var res = $"{minutes} minutes ago";
            Assert.AreEqual(res, sut.NiceTs(now, postTs));
        }
    }
}