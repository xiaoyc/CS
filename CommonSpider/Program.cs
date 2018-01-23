using CommonSpider.DBHelper;
//using CommonSpider.JVideo;
using System;

namespace CommonSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            //JVideoSpider.Run();

            API api = new API();
            api.InsertPost(null);

            Console.Read();
        }
    }
}
