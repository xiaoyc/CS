using CommonSpider.DBHelper;
using System;

namespace CommonSpider.JVideoSinglePage
{
    public class SinglePage
    {
        public int ProcessSinglePage()
        {
            PostService api = new PostService();
            var posts = api.GetDraftPosts();

            if (posts != null && posts.Count > 0)
            {

            }

            return 1;
        }
    }
}
