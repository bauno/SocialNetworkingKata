using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using CSharp.Core;
using Microsoft.Win32;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class PostRepositoryTests
    {
        [SetUp]
        public void Init()
        {
            
        }

        [Test]

        public void ConstructorTests()
        {
            Assert.Throws<ArgumentNullException>(() => new MemoryPostRepository(null));
        }
             
        [Test]
        public void CanSavePosts()
        {
            var data = new Dictionary<string, Post>();
            var sut = new MemoryPostRepository(data);
            var post = new Post {User = "pippo", Content = "pluto"};
            sut.Save(post);
            var savedPost = data.Single();
            Assert.AreEqual("pippo", savedPost.Key);
            Assert.AreEqual(post, savedPost.Value);

        }

        [Test]
        public void CanReadSavedPosts()
        {
            Assert.Fail();
        }
    }
}