using System.CodeDom;
using CSharp.Core;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void CanExecPostCommand()
        {
            var cmd = new PostCommand("pippo", "pluto");
            var socialNetwork = new Mock<ISocial>();
            cmd.SendTo(socialNetwork.Object);
            socialNetwork.Verify(s => s.Post("pippo", "pluto"), Times.Once);
        }
          
    }
}