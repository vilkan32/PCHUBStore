using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Areas.Support.Services;
using PCHUBStore.Tests.Common;
using Xunit;


namespace PCHUBStore.Tests.SupportServicesTests.SupportForumServicesTests
{
    public class SupportForumServiceTests
    {
        [Theory]
        [InlineData("TestTitle", "TestContent", "TestUrlPicture")]
        [InlineData("NewNameTitle", "new content asdasd <p> dasds hellp</p>", "PictureDemo")]
        [InlineData("Acer Asipre 123", "video content from youtube for example", "Picture Url tested")]
        public async Task TestIfCreateForumPostWorksCorrectly(string title, string content, string url)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var forumService = new SupportForumServices(context);


            await forumService.CreateForumPost(title, content, url);

            var result = await context.ForumPosts.FirstOrDefaultAsync(x => x.Title == title);

            Assert.NotNull(result);

            Assert.Equal(title, result.Title);
            Assert.Equal(content, result.Content);
            Assert.Equal(url, result.PictureUrl);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("Invalid Test")]
        [InlineData("Invalid")]
        public async Task TestIfEditForumPostThrowsError(string id)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var forumService = new SupportForumServices(context);
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var form = new EditForumPostViewModel();

                form.Id = id;

                await forumService.EditForumPostAsync(form);
            });
        }


        [Theory]
        [InlineData("TestTitle", "TestContent", "TestUrlPicture")]
        [InlineData("NewNameTitle", "new content asdasd <p> dasds hellp</p>", "PictureDemo")]
        [InlineData("Acer Asipre 123", "video content from youtube for example", "Picture Url tested")]
        public async Task TestIfEditForumPostWorksAccordingly(string title, string content, string url)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var forumService = new SupportForumServices(context);

            await forumService.CreateForumPost(title, content, url);

            var id = await context.ForumPosts.FirstOrDefaultAsync(x => x.Title == title);

            var form = new EditForumPostViewModel();

            form.Id = id.Id;
            form.Title = "new Title";
            form.Content = "new Content";

            await forumService.EditForumPostAsync(form);

            var result = await context.ForumPosts.FirstOrDefaultAsync(x => x.Title == "new Title");

            Assert.NotNull(result);

            Assert.Equal("new Title", result.Title);

            Assert.Equal("new Content", result.Content);
        }

        [Theory]
        [InlineData("TestTitle", "TestContent", "TestUrlPicture")]
        [InlineData("NewNameTitle", "new content asdasd <p> dasds hellp</p>", "PictureDemo")]
        [InlineData("Acer Asipre 123", "video content from youtube for example", "Picture Url tested")]
        public async Task TestIfDeleteForumPostWorksAccordingly(string title, string content, string url)
        {
            var context = PCHUBDbContextInMemoryInitializer.InitializeContext();

            var forumService = new SupportForumServices(context);

            await forumService.CreateForumPost(title, content, url);

            var post = await context.ForumPosts.FirstOrDefaultAsync();


            await forumService.DeleteForumPostAsync(post.Id);

            var result = await context.ForumPosts.AnyAsync();

            Assert.False(result);
        }
    }
}
