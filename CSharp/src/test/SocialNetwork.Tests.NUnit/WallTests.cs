using System;
using System.CodeDom;
using System.Linq;
using System.Runtime.CompilerServices;
using SocialNetwork.Core;
using SocialNetwork.Core.Dtos;
using SocialNetwork.Core.Entities;
using SocialNetwork.Core.Repositories.Interfaces;
using SocialNetwork.Core.Services;
using SocialNetwork.Core.Values;
using NUnit.Framework;

namespace CSharp.Tests
{
    [TestFixture]
    public class WallTests
    {
        private PostDto _firstPost;
        private PostDto _secondPost;
        private WallDto _wallDto;
        private Wall _sut;

        [SetUp]
        public void Init()
        {
            _firstPost = new PostDto {Content = "first"};
            _secondPost = new PostDto {Content = "Second"};
            
            _wallDto = new WallDto
            {
                User = "pippo",
                Posts = new[]
                {
                    _firstPost,
                    _secondPost
                },
                Follows = new[]{"Pluto", "Topolino"}
                
            };
            
            _sut = new Wall("pippo");
            
        }
        
        [Test]
        public void CanAddPostToWall()
        {
            var now = DateTime.Now;
            TimeService.TestNow = now;
            _sut.AddPost(new Post ("pluto", now));
            var wDto = (Dto<WallDto, Wall>) _sut;
            var res = wDto.ToDto();
            Assert.AreEqual(_wallDto.User, res.User);
            Assert.AreEqual("pluto", res.Posts.Single().Content);
            Assert.AreEqual(now, res.Posts.Single().TimeStamp);
            
            
        }

        [Test]
        public void CanFollowUser()
        {
            _sut.Follow("pippo");
            _sut.Follow("pluto");
            var wDto = (Dto<WallDto, Wall>) _sut;
            var res = wDto.ToDto();
            Assert.AreEqual(_wallDto.User, res.User);
            Assert.AreEqual("pippo", res.Follows.First());            
            Assert.AreEqual("pluto", res.Follows.Last());            
        }
        

        [Test]
        public void CannotCreateWallWithEmptyUser()
        {
            Assert.Throws<ArgumentNullException>(() => new Wall(""));
        }


        [Test]
        public void CanCrudWall()
        {                       
            var wDto = (Dto<WallDto, Wall>) _sut;
            wDto.Load(_wallDto);

            var res = wDto.ToDto();
            
            Assert.AreEqual(_wallDto.User, res.User);
            Assert.AreEqual(_wallDto.Posts.First().Content, res.Posts.First().Content);
            Assert.AreEqual(_wallDto.Posts.Last().Content, res.Posts.Last().Content);
            Assert.AreEqual("Pluto", res.Follows.First());
            Assert.AreEqual("Topolino", res.Follows.Last());
                                    
        }
        
    }
}