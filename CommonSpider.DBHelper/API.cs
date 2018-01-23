using System;
using System.Net;
using System.Net.Http;
using CommonSpider.DBHelper.Models;
using Dapper;
using RestSharp;

namespace CommonSpider.DBHelper
{
    public class API
    {

        public int InsertPost(Post post)
        {
            //var customer = new Customer();
            //customer.Name = "Joe";
            //customer.Email = "joe@smith.com";



            Post p = new Post() { Title = "test2", CategoryName = "testcategory2", Content = "test", Image = new byte[1], CreateTime = DateTime.Now, Description = "test", VideoUrl = "testurl" };
            //http.Post("http://localhost:5366/api/post", p, HttpContentTypes.ApplicationJson);

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
    }
}
