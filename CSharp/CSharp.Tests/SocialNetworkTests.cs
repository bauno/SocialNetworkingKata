using System;
using CSharp.Core;
using Moq;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class SocialNetworkTests
    {
        private Social _sut;
        private Mock<PostRepository> _repository;

        [SetUp]
        public void Init()
        {
            _repository = new Mock<PostRepository>();
            _sut = new Social(_repository.Object);
        }

       [Test]
       public void ConstructorTests()
       {           
           Assert.Throws<ArgumentNullException>(() => new Social(null));
       }  

        [Test]
        public void CanPostToWall()
        {
            var user = "Alice";
            var message = "I love the weather today";
            _sut.Post(user, message);
            Assert.Fail();
            
        }
    }
}