using System.CodeDom;
using System.Linq;
using SocialNetwork.Core;
using SocialNetwork.Core.Commands;
using SocialNetwork.Core.Services;
using SocialNetwork.Core.Services.Interfaces;
using SocialNetwork.Core.Views;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandTests
    {
        private Mock<Display> _display;
        private Mock<ISocialNetwork> _socialNetwork;

        [SetUp]
        public void Init()
        {
            _display = new Mock<Display>();
            _socialNetwork = new Mock<ISocialNetwork>();
        }
        
        [Test]
        public void CanExecPostCommand()
        {
            var cmd = new PostCommand("pippo", "pluto");

            var res = cmd.SendTo(_socialNetwork.Object);                
            _socialNetwork.Verify(s => s.Post("pippo", "pluto"), Times.Once);
            Assert.IsTrue(res.IsNone);
        }
        
        [Test]
        public void CanExecMessageCommand()
        {
            var cmd = new MesssageCommand("pippo", "pluto", "Messaggio!!");
            var res = cmd.SendTo(_socialNetwork.Object);
            _socialNetwork.Verify(s => s.SendMessage("pippo","pluto", "Messaggio!!"));
            Assert.IsTrue(res.IsNone);            
        }


        [Test]
        public void CanExecFollowCommand()
        {
            var cmd = new FollowCommand("pippo", "pluto");

            var res = cmd.SendTo(_socialNetwork.Object);                
            _socialNetwork.Verify(s => s.Follow("pippo", "pluto"), Times.Once);  
            Assert.IsTrue(res.IsNone);
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

            res.IfSome(d => Assert.AreEqual(new WallsDisplay(walls), d));            
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
            var display = new WallDisplay(wallView);
            res.IfSome(d => Assert.AreEqual(display, d));
            
        }

     
    }
}