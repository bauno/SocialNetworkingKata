using System.Runtime.InteropServices.ComTypes;
using System.Xml.Schema;
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

        Either<string, int> Pippo(Option<int> value)
        {
            return value.ToEither("Error: no value");
        }
        
        [Test]
        public void CanUseEither()
        {
            var some = Some(1);
            var none = None;

            var x = some.ToEither("Error: no value");
            var y = x.Bind<double>(i => i * 5.0);
        }
    }
}