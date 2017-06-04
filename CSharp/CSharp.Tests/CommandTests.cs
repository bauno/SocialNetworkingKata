using System.CodeDom;
using System.Linq;
using CSharp.Core;
using CSharp.Core.Commands;
using CSharp.Core.Services;
using CSharp.Core.Services.Interfaces;
using CSharp.Core.Views;
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

            var res = cmd.SendTo(_socialNetwork.Object);                
            _socialNetwork.Verify(s => s.Post("pippo", "pluto"), Times.Once);
            Assert.IsAssignableFrom<Nothing>(res);
        }


        [Test]
        public void CanExecFollowCommand()
        {
            var cmd = new FollowCommand("pippo", "pluto");

            var res = cmd.SendTo(_socialNetwork.Object);                
            _socialNetwork.Verify(s => s.Follow("pippo", "pluto"), Times.Once);  
            Assert.IsAssignableFrom<Nothing>(res);
            
        }
        
        
        [Test]
        public void CanExecWallCommand()
        {
            var pippoWall = new WallView {Follows = new[] {"pluto"}};
            var plutoWall = new WallView();
            var walls = new[] {pippoWall, plutoWall};
            _socialNetwork.Setup(s => s.ReadWall("pippo"))
                .Returns(pippoWall);
            _socialNetwork.Setup(s => s.ReadWall("pluto"))
                .Returns(plutoWall);

            var cmd = new WallCommand("pippo");
            var res = cmd.SendTo(_socialNetwork.Object);
 
            _socialNetwork.Verify(s => s.ReadWall("pippo"));
            _socialNetwork.Verify(s => s.ReadWall("pluto"));
            
            Assert.AreEqual(new WallsDisplay(walls), res);
                        
                        
        }  
        
        [Test]
        public void CanExecReadCommand()
        {
            var wallView = new WallView();
            _socialNetwork.Setup(s => s.ReadWall("pippo"))
                .Returns(wallView);
            
            var cmd = new ReadCommand("pippo");
            var res = cmd.SendTo(_socialNetwork.Object);
                
 
            _socialNetwork.Verify(s => s.ReadWall("pippo"));
            Assert.AreEqual(new WallDisplay(wallView), res);
        }
    }
}