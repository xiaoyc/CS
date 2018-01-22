using System;
using System.Net.Http;
using CommonSpider.DBHelper.Models;
using Dapper;
using EasyHttp.Http;

namespace CommonSpider.DBHelper
{
    public class API
    {

        public void InsertPost(Post post)
        {
            //var customer = new Customer();
            //customer.Name = "Joe";
            //customer.Email = "joe@smith.com";
            var http = new EasyHttp.Http.HttpClient();


            Post p = new Post() { Title = "test2", CategoryName = "testcategory2", Content = "test", Image = new byte[1], CreateTime = DateTime.Now, Description = "test", VideoUrl = "testurl" };
            http.Post("http://localhost:5366/api/post", p, HttpContentTypes.ApplicationJson);
        }
    }
}
