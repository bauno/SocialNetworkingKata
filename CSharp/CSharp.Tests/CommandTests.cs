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
        private Mock<Display> _display;
        private Mock<SocialNetwork> _socialNetwork;

        [SetUp]
        public void Init()
        {
            _display = new Mock<Display>();
            _socialNetwork = new Mock<SocialNetwork>();
        }
        
        [Test]
        public void CanExecPostCommand()
        {
            var cmd = new PostCommand("pippo", "pluto");
            
            cmd.SendTo(_socialNetwork.Object)
                .ShowOn(_display.Object);
            _socialNetwork.Verify(s => s.Post("pippo", "pluto"), Times.Once);
            _display.Verify(d => d.Show(It.IsAny<WallView>()), Times.Never);
        }

        [Test]
        public void CanExecReadCommand()
        {
            var wallView = new WallView();
            _socialNetwork.Setup(s => s.ReadWall("pippo"))
                .Returns(wallView);
            
            var cmd = new ReadCommand("pippo");
            cmd.SendTo(_socialNetwork.Object)
                .ShowOn(_display.Object);
 
            _socialNetwork.Verify(s => s.ReadWall("pippo"));
            _display.Verify(d => d.Show(wallView), Times.Once);
        }

        [Test]
        public void CanExecFollowCommand()
        {
            var cmd = new FollowCommand("pippo", "pluto");
            
            cmd.SendTo(_socialNetwork.Object)
                .ShowOn(_display.Object);
            _socialNetwork.Verify(s => s.Follow("pippo", "pluto"), Times.Once);
            _display.Verify(d => d.Show(It.IsAny<WallView>()), Times.Never);
        }
        
        
        [Test]
        public void CanExecWallCommand()
        {
            var pippoWall = new WallView();
            var plutoWall = new WallView();
            _socialNetwork.Setup(s => s.ReadWall("pippo"))
                .Returns(pippoWall);
            _socialNetwork.Setup(s => s.ReadWall("pluto"))
                .Returns(plutoWall);
            
            var cmd = new FollowCommand("pluto", "pippo");
            cmd.SendTo(_socialNetwork.Object)
                .ShowOn(_display.Object);
 
            _socialNetwork.Verify(s => s.ReadWall("pippo"));
            _display.Verify(d => d.Show(pippoWall), Times.Once);            
        }  
    }
}