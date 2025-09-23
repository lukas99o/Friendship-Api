using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vänskap_Api.Data;
using Vänskap_Api.Models;
using Vänskap_Api.Models.Dtos.Conversation;
using Vänskap_Api.Service;
using Vänskap_Api.Service.IService;
using Xunit.Sdk;

namespace Vänskap_Api_Tests
{
    public class TestConversation
    {
        [Fact]
        public async Task Returns_Correct_Conversation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            using var context = new ApplicationDbContext(options);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, "test1")
            }, "mock"));

            mockHttpContext.Setup(ctx => ctx.User).Returns(mockUser);
            mockHttpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(mockHttpContext.Object);    

            var user1 = new ApplicationUser { Id = "test1", FirstName = "Test", LastName = "Test" };
            var user2 = new ApplicationUser { Id = "test2", FirstName = "Test", LastName = "Test", UserName = "Test" };

            user1.Friendships.Add(new Friendship { UserId = user1.Id, FriendId = user2.Id });
            user2.Friendships.Add(new Friendship { UserId = user2.Id, FriendId = user1.Id });

            await context.AddAsync(user1);
            await context.AddAsync(user2);
            await context.SaveChangesAsync();

            var conversation = new Conversation { Title = "Test" };
            var conversationEvent = new Conversation { Title = "Test" };

            var evnt = new Event { CreatedByUserId = "test", Title = "Test", 
                Conversation = conversationEvent };

            conversation.ConversationParticipants.Add(new ConversationParticipant { UserId = user1.Id });
            conversation.ConversationParticipants.Add(new ConversationParticipant { UserId = user2.Id });

            conversationEvent.ConversationParticipants.Add(new ConversationParticipant { UserId = user1.Id });
            conversationEvent.ConversationParticipants.Add(new ConversationParticipant { UserId = user2.Id });
            conversationEvent.Event = evnt;

            await context.AddAsync(conversation);
            await context.AddAsync(conversationEvent);
            await context.SaveChangesAsync();

            var service = new ConversationService(context, mockHttpContextAccessor.Object);

            // Act
            var conversationDto = await service.StartPrivateConversation("Test");

            // Assert
            mockHttpContextAccessor.Verify(accessor => accessor.HttpContext, Times.AtLeastOnce());
            service.Should().NotBeNull();
            service.UserId.Should().Be(user1.Id);
            conversationDto!.ConversationId.Should().Be(1);
        }
    }
}