using System;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories;
using CSharp.Core.Factories.Interfaces;
using CSharpFunctionalExtensions;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandParserTests
    {
        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new StringCommandParser(null));
        }

        [Test]
        public void WillCallFactoriesToParseCommand()
        {
            var cmdString = "pippo";
            var cmd = new Mock<Command>(); 
            var fac1 = new Mock<CommandFactory>();
            fac1.Setup(f1 => f1.Parse(cmdString))
                .Returns<Command>(null);
            
            var fac2 = new Mock<CommandFactory>();
            fac2.Setup(f2 => f2.Parse(cmdString))
                .Returns(Maybe<Command>.From(cmd.Object));
            
            var sut = new StringCommandParser(new[]{fac1.Object, fac2.Object});
            
            Assert.AreEqual(cmd.Object, sut.Parse(cmdString).Value);
            
        }

        [Test]
        public void WillThrowInvalidCommandIfAllFactoriesReturnNull()
        {
            var cmdString = "pippo";             
            var fac1 = new Mock<CommandFactory>();
            fac1.Setup(f1 => f1.Parse(cmdString))
                .Returns<Command>(null);
            
            var fac2 = new Mock<CommandFactory>();
            fac2.Setup(f2 => f2.Parse(cmdString))
                .Returns<Command>(null);
            
            var sut = new StringCommandParser(new[]{fac1.Object, fac2.Object});
            
            Assert.IsTrue(sut.Parse(cmdString).HasNoValue);
            
            fac1.Verify(f1 => f1.Parse(cmdString), Times.Once);
            fac1.Verify(f2 => f2.Parse(cmdString), Times.Once);
        }                 
    }
}