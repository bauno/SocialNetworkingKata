using System;
using System.CodeDom;
using CSharp.Core;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Exceptions;
using CSharp.Core.Factories;
using CSharp.Core.Factories.Interfaces;
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
                .Returns(cmd.Object);
            
            var sut = new StringCommandParser(new[]{fac1.Object, fac2.Object});
            
            Assert.AreEqual(cmd.Object, sut.Parse(cmdString));
            
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
            
            Assert.Throws<InvalidCommandException>(() => sut.Parse(cmdString));
            
            fac1.Verify(f1 => f1.Parse(cmdString), Times.Once);
            fac1.Verify(f2 => f2.Parse(cmdString), Times.Once);
        }

                 
    }
}