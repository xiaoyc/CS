using CommonSpider.DBHelper;
using NetworkCrawlerManager.Core;
//using CommonSpider.JVideo;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CommonSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            //JVideoSpider.Run();

            //PostService api = new PostService();
            //api.GetAllPosts();
            //api.InsertPost(null);
            //Console.WriteLine(HashPassword("admin"));

            Console.WriteLine("aa");

            List<string> list_lines = new List<string>();

          
                JiraSpider.Run();
         

           

            Console.Read();
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
