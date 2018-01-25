using System;
using System.Net;
using System.Net.Http;
using CommonSpider.DBHelper.Models;
using Dapper;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommonSpider.DBHelper
{
    public class API
    {

        public int InsertOrUpdatePost(Post post)
        {

            Post p = new Post() { Title = "test2", CategoryName = "testcategory2", Content = "test", Image = new byte[1], CreateTime = DateTime.Now, Description = "test", VideoUrl = "testurl" };

            var client = new RestClient("http://localhost:5366");

            var request = new RestRequest("api/post", Method.POST);
          
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(p);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return 1;
            }

            return -1;

        }

        public IList<Post> GetAllPosts()
        {
            var client = new RestClient("http://localhost:5366");

            var request = new RestRequest("api/post/getallposts", Method.GET);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);

            var ttt = response.Content;
            var posts = JsonConvert.DeserializeObject<List<Post>>(response.Content);// response.Content;

            return posts;


          
        }
    }
}
