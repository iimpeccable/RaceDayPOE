using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RaceDay.API.Controllers;
using RaceDay.API.Data;
using RaceDay.API.DTOs;
using RaceDay.API.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class AuthAndEventsTests
    {
        private RaceDayDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new RaceDayDbContext(options);

            // Manual seeding without calling EnsureCreated() prevents primary key collisions
            context.Roles.AddRange(
                new Role { RoleId = 1, Name = "Organiser" },
                new Role { RoleId = 2, Name = "Participant" }
            );
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task Register_ShouldCreateUser_WhenValidDataProvided()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var config = new ConfigurationBuilder().Build();
            var controller = new AuthController(context, config);

            var registerDto = new RegisterDto
            {
                Name = "Mpendulo",
                Email = "mpendulo@example.com",
                Password = "Password123!",
                Age = 25,
                Contact = "0821234567",
                RoleId = 2
            };

            // Act
            var result = await controller.Register(registerDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, objectResult.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnConflict_WhenEmailAlreadyExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Users.Add(new User
            {
                Name = "Existing User",
                Email = "duplicate@example.com",
                PasswordHash = "hashed_pw",
                RoleId = 2
            });
            await context.SaveChangesAsync();

            var config = new ConfigurationBuilder().Build();
            var controller = new AuthController(context, config);

            var registerDto = new RegisterDto
            {
                Name = "New User",
                Email = "duplicate@example.com",
                Password = "Password123!",
                RoleId = 2
            };

            // Act
            var result = await controller.Register(registerDto);

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task GetEvents_ShouldReturnAllEvents()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Events.Add(new Event
            {
                Name = "Spring 10km",
                Description = "Road race",
                Date = DateTime.Now.AddDays(10),
                Location = "Joburg",
                Distance = 10.0m,
                EventType = "Run",
                OrganizerId = 1
            });
            await context.SaveChangesAsync();

            var controller = new EventsController(context);

            // Act
            var result = await controller.GetEvents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}