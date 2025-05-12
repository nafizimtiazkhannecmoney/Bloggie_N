using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _dbContext;

        public BlogPostRepository(BloggieDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _dbContext.BlogPosts.AddAsync(blogPost);
            await _dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _dbContext.BlogPosts.FindAsync(id);
            if(existingBlogPost != null)
            {
                _dbContext.Remove(existingBlogPost);
                await _dbContext.SaveChangesAsync();
                return existingBlogPost;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var blogPosts = await _dbContext.BlogPosts.Include(t => t.Tags).ToListAsync();
            return blogPosts;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var exitingBlogPost = await _dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (exitingBlogPost != null)
            {
                exitingBlogPost.Heading = blogPost.Heading;
                exitingBlogPost.PageTitle = blogPost.PageTitle;
                exitingBlogPost.Content = blogPost.Content;
                exitingBlogPost.ShortDescription = blogPost.ShortDescription;
                exitingBlogPost.Author = blogPost.Author;
                exitingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                exitingBlogPost.UrlHandle = blogPost.UrlHandle;
                exitingBlogPost.Visible = blogPost.Visible;
                exitingBlogPost.PublishedDate = blogPost.PublishedDate;
                exitingBlogPost.Tags = blogPost.Tags;
                await _dbContext.SaveChangesAsync();
                return exitingBlogPost;
            }
            else
            {
                return null;
            }
        }
    }
}
