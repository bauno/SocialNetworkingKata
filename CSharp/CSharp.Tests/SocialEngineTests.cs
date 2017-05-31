using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

        [SetUp]
        public void Init()
        {
            _repository = new Mock<PostRepository>();            
            _sut = new SocialEngine(_repository.Object);
        }

        [Test]
        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new SocialEngine(null));
        }
       

        [Test]
        public void CanPostToWallTwice()
        {
            var wall = new Mock<IWall>();
            var posts = new List<Post>();
            wall.Setup(w => w.AddPost(It.IsAny<Post>()))
                .Callback<Post>(p => posts.Add(p));

            _repository.Setup(r => r.LoadOrCreateWallOf("Bob"))
                .Returns(wall.Object);
                
                
            
            var user = "Bob";
            var firstPost = "first";
            var secondPost = "second";
            _sut.Post(user, firstPost);
            _sut.Post(user, secondPost); 
            
            Assert.AreEqual(2, posts.Count);
            
            Assert.AreEqual("first", posts.First().Content);
          
            Assert.AreEqual("second", posts.Last().Content);
                                    
        }

        [Test]
        public void CanReadWall()
        {
            var user = "Alice";
            var message = "the quick brown fox";
            var wall = new WallView {User = "Alice", Posts = new[] {new PostView {Content = message}}};
            _repository.Setup(r => r.ReadWallOf("Alice"))
                .Returns(wall);
            var outputWall = _sut.ReadWall(user);
            Assert.AreEqual(user, outputWall.User);
            Assert.AreEqual(message, outputWall.Posts.Single().Content);

        }

        
    }
}