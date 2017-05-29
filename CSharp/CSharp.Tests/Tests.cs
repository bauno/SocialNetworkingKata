using System;
using CSharp.Core;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var sut = new Social();
            Assert.IsEmpty(sut.Read("pippo"));
        }
    }
}