using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core;
using CSharp.Core.Entities.Interfaces;
using CSharp.Core.Repositories.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Values;
using CSharp.Core.Views;
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
            var now = DateTime.Now;
            TimeService.TestNow = now;
            
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
            Assert.AreEqual(now, posts.First().TimeStamp);
          
            Assert.AreEqual("second", posts.Last().Content);
            Assert.AreEqual(now, posts.Last().TimeStamp);
                                    
                                    
        }


        [Test]
        public void CanSendMessage()
        {
            
            var wall = new Mock<IWall>();

            var message = new UserMessage {From = "Alice", Message = "ciao"};
            var messages = new List<UserMessage>();


            wall.Setup(w => w.SendMessage(It.IsAny<UserMessage>()))
                .Callback<UserMessage>(m => messages.Add(m));
            

            _repository.Setup(r => r.LoadOrCreateWallOf("Bob"))
                .Returns(wall.Object);
                
            _sut.SendMessage("Alice","Bob","ciao");    
          
            
            
            Assert.AreEqual("Alice", messages.Single().From);
            Assert.AreEqual("ciao",messages.Single().Message);
            
            
           
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