using System;
using System.CodeDom;
using System.Linq;
using CSharp.Core;
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
                }
            };
            
            _sut = new Wall("pippo");
            
        }
        
        [Test]
        public void CanAddPostToWall()
        {
            _sut.AddPost(new Post {Content = "pluto"});
            var wDto = (Dto<WallDto, Wall>) _sut;
            var res = wDto.ToDto();
            Assert.AreEqual(_wallDto.User, res.User);
            Assert.AreEqual("pluto", res.Posts.Single().Content);
            
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
                                    
        }
        
    }
}