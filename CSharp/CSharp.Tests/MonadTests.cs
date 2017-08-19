using System.Runtime.InteropServices.ComTypes;
using LanguageExt;
using NUnit.Framework;
using static LanguageExt.Prelude;


namespace CSharp.Tests
{
    [TestFixture]
    public class MonadTests
    {
        [Test]
        public void CanUseOption()
        {
            var one = Some(1);

            var res = one.Some(i => i*3)
                .None(() => 0);

            Assert.AreEqual(1, res);

            var two = one.Bind(i => Some(i * 2));

            var three = two.Map(i => i * 5.0);

        }

        [Test]
        public void CanUseEither()
        {
            
        }
    }
}