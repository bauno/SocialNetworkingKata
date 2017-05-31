using System.CodeDom;
using System.Linq;
using CSharp.Core;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandTests
    {
        private Mock<SocialNetwork> _socialNetwork;

        [SetUp]
        public void Init()
        {
            _socialNetwork = new Mock<SocialNetwork>();
        }
        
        [Test]
        public void CanExecPostCommand()
        {
            var cmd = new PostCommand("pippo", "pluto");
            
            cmd.SendTo(_socialNetwork.Object);
            _socialNetwork.Verify(s => s.Post("pippo", "pluto"), Times.Once);
        }

        [Test]
        public void CanExecReadCommand()
        {
            _socialNetwork.Setup(s => s.ReadWall("pippo"))
                .Returns(new[]{new PostView{User = "pippo", Content = "pluto"}});
            
            var cmd = new ReadCommand("pippo");
            var res = cmd.SendTo(_socialNetwork.Object);
            
            Assert.AreEqual("pippo", res.Single().User);
            Assert.AreEqual("pluto", res.Single().Content);
            
            
            _socialNetwork.Verify(s => s.ReadWall("pippo"));
        }
          
    }
}