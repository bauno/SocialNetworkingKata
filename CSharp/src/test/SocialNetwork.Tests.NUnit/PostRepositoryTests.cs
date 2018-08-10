using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core.Dtos;
using CSharp.Core.Entities;
using CSharp.Core.Repositories;
using CSharp.Core.Repositories.Interfaces;
using CSharp.Core.Services;
using CSharp.Core.Values;
using NUnit.Framework;
using static LanguageExt.Prelude;

namespace CSharp.Tests
{
    [TestFixture]
    public class PostRepositoryTests
    {
        private WallDto _wallDto;

        [SetUp]
        public void Init()
        {
            _wallDto = new WallDto
            {
                User = "pippo",
                Posts = new[] {new PostDto {Content = "pluto"}},
                Follows = new[] {"qui", "quo"}
            };
        }

        [Test]
        public void CanLoadAWall()
        {
            var walls = Map(("pippo", _wallDto));
            var sut = new MemoryPostRepository(walls);
            var wall = sut.LoadOrCreateWallOf("pippo");
            var wDto = (Dto<WallDto, Wall>) wall;
            var dto = wDto.ToDto();
            Assert.AreEqual("pippo", dto.User);
            Assert.AreEqual("pluto", dto.Posts.Single().Content);
        }

        [Test]
        public void CanLoadAReadWall()
        {
            var now = DateTime.Now;
            TimeService.TestNow = now;
            var data = Map<string, Post>();
            var walls = Map<string, WallDto>
            (
                (
                "pippo",
                new WallDto
                {
                    User = "pippo",
                    Posts = new[] {new PostDto {Content = "pluto", TimeStamp = now}},
                    Follows = new[] {"alice"}
                }
                )
            );
            var sut = new MemoryPostRepository(walls);
            var wall = sut.ReadWallOf("pippo");
            Assert.AreEqual("pippo", wall.User);
            Assert.AreEqual("pluto", wall.Posts.Single().Content);
            Assert.AreEqual(now, wall.Posts.Single().TimeStamp);
            Assert.AreEqual("alice", wall.Follows.Single());
        }

        [Test]
        public void CanCreateAWall()
        {            
            var walls = Map<string, WallDto>();
            var sut = new MemoryPostRepository(walls);
            var wall = sut.LoadOrCreateWallOf("pippo");
            var wDto = (Dto<WallDto, Wall>) wall;
            var dto = wDto.ToDto();
            Assert.AreEqual("pippo", dto.User);
            Assert.IsEmpty(dto.Posts);
        }

        [Ignore("The wall is now immutable")]
        public void CanSaveANewWall()
        {
            var walls = Map<string, WallDto>();
            var sut = new MemoryPostRepository(walls);

            var wall = new Wall("pippo");
            ((Dto<WallDto, Wall>) wall).Load(_wallDto);
            sut.Save(wall);

            Assert.AreEqual("pippo", walls.Values.Single().User);
            Assert.AreEqual("pluto", walls.Values.Single().Posts.Single().Content);
        }

        [Test]
        public void CanSaveAnExistingWall()
        {
            var data = Map<string, Post>();

            var walls = Map
            (
                (
                    "pippo",
                    _wallDto
                )
            );
            var sut = new MemoryPostRepository(walls);
            var dto = new WallDto
            {
                User = "pippo",
                Posts = new[] {new PostDto {Content = "pluto"}},
                Follows = new[] {"topolino"}
            };
            var wall = new Wall("pippo");
            ((Dto<WallDto, Wall>) wall).Load(dto);
            sut.Save(wall);

            Assert.AreEqual("pippo", walls.Values.Single().User);
            Assert.AreEqual("pluto", walls.Values.Single().Posts.Single().Content);
        }
    }
}