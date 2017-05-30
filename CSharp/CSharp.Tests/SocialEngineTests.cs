using System;
using System.CodeDom;
using CSharp.Core;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class SocialEngineTests
    {
        private SocialEngine _sut;
        private Mock<PostRepository> _repository;
        private Post _savedPost;
        private WallDto _savedWallDto;

        [SetUp]
        public void Init()
        {
            _repository = new Mock<PostRepository>();
            _repository.Setup(r => r.Save(It.IsAny<Post>()))
                .Callback<Post>(p => _savedPost = p);

            _sut = new SocialEngine(_repository.Object);
        }

        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new SocialEngine(null));
        }

        [Test]
        public void CanPostToWall()
        {
            var user = "Alice";
            var message = "I love the weather today";
            _sut.Post(user, message);

            Assert.AreEqual(user, _savedPost.User);
            Assert.AreEqual(message, _savedPost.Content);

        }

        [Test]
        public void CanPostToWallTwice()
        {
            var user = "Bob";
            var firstPost = "first";
            var secondPost = "second";
            _sut.Post(user, firstPost);
            _sut.Post(user, secondPost);
            Assert.Fail();                        
        }

        [Test]
        public void CanReadWall()
        {
            var user = "Alice";
            var message = "the quick brown fox";
            _repository.Setup(r => r.ReadPostFrom(user))
                .Returns(new Post {User = user, Content = message});
            var post = _sut.ReadWall(user);
            Assert.AreEqual(user, post.User);
            Assert.AreEqual(message, post.Content);

        }

        
    }
}