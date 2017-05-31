using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core;
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
            Assert.Throws<ArgumentNullException>(() => new MemoryPostRepository(null, new Dictionary<string, WallDto>() ));
            Assert.Throws<ArgumentNullException>(() => new MemoryPostRepository(new Dictionary<string, Post>(), null));
        }
                     
        

        [Test]
        public void CanLoadAWall()
        {
            var data = new Dictionary<string, Post>();
            var walls = new Dictionary<string, WallDto>{
                {"pippo", new WallDto{User = "pippo", Posts = new[]{new PostDto{Content = "pluto"}}}}};
            var sut = new MemoryPostRepository(data, walls);
            var wall = sut.LoadOrCreateWallOf("pippo");
            var wDto = (Dto<WallDto,Wall>)wall;
            var dto = wDto.ToDto();
            Assert.AreEqual("pippo", dto.User);
            Assert.AreEqual("pluto", dto.Posts.Single().Content);


        }

        [Test]
        public void CanLoadAReadWall()
        {
            var now = DateTime.Now;
            TimeService.TestNow = now;
            var data = new Dictionary<string, Post>();
            var walls = new Dictionary<string, WallDto>
            {
                {
                    "pippo",
                    new WallDto {User = "pippo", Posts = new[] {new PostDto {Content = "pluto", TimeStamp = now}}}
                }
            };
            var sut = new MemoryPostRepository(data, walls);
            var wall = sut.ReadWallOf("pippo");            
            Assert.AreEqual("pippo", wall.User);
            Assert.AreEqual("pluto", wall.Posts.Single().Content);
            Assert.AreEqual(now, wall.Posts.Single().TimeStamp);
            
            
        }

        [Test]
        public void CanCreateAWall()
        {
            var data = new Dictionary<string, Post>();
            var walls = new Dictionary<string, WallDto>();
            var sut = new MemoryPostRepository(data, walls);
            var wall = sut.LoadOrCreateWallOf("pippo");
            var wDto = (Dto<WallDto,Wall>)wall;
            var dto = wDto.ToDto();
            Assert.AreEqual("pippo", dto.User);
            Assert.IsEmpty(dto.Posts);
        }

        [Test]
        public void CanSaveANewWall()
        {
            var data = new Dictionary<string, Post>();
            var walls = new Dictionary<string, WallDto>();
            var sut = new MemoryPostRepository(data, walls);
            var dto = new WallDto {User = "pippo", Posts = new[] {new PostDto {Content = "pluto"}}};
            var wall = new Wall("pippo");
            ((Dto<WallDto, Wall>)wall).Load(dto);
            sut.Save(wall);
            
            Assert.AreEqual("pippo", walls.Values.Single().User);
            Assert.AreEqual("pluto", walls.Values.Single().Posts.Single().Content);
            
        }
        
        [Test]
        public void CanSaveAnExistingWall()
        {
            var data = new Dictionary<string, Post>();
            
            var walls = new Dictionary<string, WallDto>
            {
                {"pippo",
                new WallDto {User = "pippo", Posts = new[] {new PostDto {Content = "topolino"}}}}
            };
            var sut = new MemoryPostRepository(data, walls);
            var dto = new WallDto {User = "pippo", Posts = new[] {new PostDto {Content = "pluto"}}};
            var wall = new Wall("pippo");
            ((Dto<WallDto, Wall>)wall).Load(dto);
            sut.Save(wall);
            
            Assert.AreEqual("pippo", walls.Values.Single().User);
            Assert.AreEqual("pluto", walls.Values.Single().Posts.Single().Content);
            
        }
    }
}