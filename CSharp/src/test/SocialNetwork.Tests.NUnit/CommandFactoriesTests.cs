using SocialNetwork.Core.Factories;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class CommandFactoriesTests
    {
        [Test]
        public void PostCommandFactoryCanParsePostCommand()
        {
            var cmdString = "Alice -> I love the weather today";
            var expected = "Type: Post; User: Alice; Post: I love the weather today";
            var sut = new PostCommandFactory();
            
            sut.Parse(cmdString).IfSome(cmd => Assert.AreEqual(expected, cmd.ToString()));
        }

        [Test]
        public void ReadCommandFactoryCanParseReadCommand()
        {
            var cmdString = "Alice";            
            var expected = "Type: Read; User: Alice";
            var sut = new ReadCommandFactory();
            sut.Parse(cmdString).IfSome(cmd => Assert.AreEqual(expected, cmd.ToString()));
        }

        [Test]
        public void  WallCommandFactoryCanParseWallCommand()
        {
            var cmdString = "Charlie wall";
            var expected = "Type: Wall; User: Charlie";
            var sut = new WallCommandFactory();
            sut.Parse(cmdString)
                .IfSome(cmd => Assert.AreEqual(expected, cmd.ToString()));
        }
        
        [Test]
        public void FollowCommandFactoryCanParseFollowCommand()
        {
            var cmdString = "Charlie follows Alice";
            var expected = "Type: Follow; User: Charlie; Who: Alice";
            var sut = new FollowCommandFactory();
            var res = sut.Parse(cmdString);
            
            sut.Parse(cmdString).IfSome(cmd => Assert.AreEqual(expected, cmd.ToString()));
        }
        
        [TestCase("Alice")]
        [TestCase("Charlie wall")]
        [TestCase("Alice -> I love the weather today")]
        public void FollowCommandFactoryReturnsNullIfCannotParseCommand(string cmdString)
        {

            var sut = new FollowCommandFactory();
            Assert.IsTrue(sut.Parse(cmdString).IsNone);
        }
        
        [TestCase("Alice")]
        [TestCase("Charlie follows Alice")]
        [TestCase("Alice -> I love the weather today")]
        public void WallCommandFactoryReturnsNullIfCannotParseCommand(string cmdString)
        {

            var sut = new WallCommandFactory();
            Assert.IsTrue(sut.Parse(cmdString).IsNone);
        }

        [TestCase("Alice")]
        [TestCase("Charlie follows Alice")]
        [TestCase("Charlie wall")]
        public void PostCommandFactoryReturnsNullIfCannotParseCommand(string cmdString)
        {

           var sut = new PostCommandFactory();
           Assert.IsTrue(sut.Parse(cmdString).IsNone);
        }
        
        [TestCase("Alice -> I love the weather today")]
        [TestCase("Charlie follows Alice")]
        [TestCase("Charlie wall")]
        public void ReadCommandFactoryReturnsNullIfCannotParseCommand(string cmdString)
        {

            var sut = new ReadCommandFactory();
            Assert.IsTrue(sut.Parse(cmdString).IsNone);
        }
    }
}