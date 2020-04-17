using Microsoft.EntityFrameworkCore;
using PCHUBStore.Services;
using PCHUBStore.Tests.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PCHUBStore.Tests.ForumServicesTests
{
    public class ForumServiceTest
    {

        [Theory]
        [InlineData("Id1", "Title1", "Content1")]
        [InlineData("Id2", "Title2", "Content2")]
        [InlineData("Id3", "Title3", "Content3")]
        [InlineData("Id4", "Title4", "Content4")]
        [InlineData("Id5", "Title5", "Content5")]
        public async Task TestIfReturnsCorrectPage(string id, string title, string content)
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var forumService = new ForumServices(context);

            await context.ForumPosts.AddAsync(new Data.Models.ForumPost
            {
                Id = id,
                Title = title,
                Content = content,
                CreatedOn = DateTime.UtcNow,
            });

            
            await context.SaveChangesAsync();
            //Act
            var result = await forumService.GetForumPostAsync(id);

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(title, result.Title);
            Assert.Equal(content, result.Content);
        }


        [Fact]
        public async Task TestIfReturnsCorrectPages()
        {
            // Arrange
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var forumService = new ForumServices(context);

            await context.ForumPosts.AddAsync(new Data.Models.ForumPost
            {
                Id = "One",
                Title = "One",
                Content = "One",
                CreatedOn = DateTime.UtcNow,
            });

            await context.ForumPosts.AddAsync(new Data.Models.ForumPost
            {
                Id = "Two",
                Title = "Two",
                Content = "Two",
                CreatedOn = DateTime.UtcNow,
            });

            await context.ForumPosts.AddAsync(new Data.Models.ForumPost
            {
                Id = "Three",
                Title = "Three",
                Content = "Three",
                CreatedOn = DateTime.UtcNow,
            });

            await context.SaveChangesAsync();
            //Act
            var result = await forumService.GetAllForumPostsAsync();

            // Assert
            Assert.Equal(3, result.Count);
        }
    }

}
