using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "http://localhost/Meeting%20about%20MF%20scale%20perf%20testing%20a.%20.%20.%20-%20Thursday,%20June%2015,%202017%2012.01.33%20PM.mp4";

            url = "https://localhost:44398/api/values";
            DownloadFile(url,"test.txt");

            //Console.WriteLine(r);

            Console.Read();
        }

        public static void DownloadFile(string url,string fileName)
        {

            WebClient webClient = null;
         

            Retry:
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                webClient = new WebClient();

                DateTime dt = DateTime.Now;
                CancellationTokenSource source = new CancellationTokenSource();

                var task = webClient.DownloadFileTaskAsync(url, fileName, source.Token);

                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;

                while (!task.IsCompleted)
                {
                    if ((DateTime.Now - dt).TotalSeconds > 3)
                    {
                        //Console.WriteLine("Cancled");
                        source.Cancel();
                        throw new Exception("Cancled");

                    }
                    else
                    {
                        //Console.WriteLine("Delay...");
                        Task.Delay(1000).Wait();
                    }
                }

                Console.WriteLine("Cancle:" + task.IsCanceled);

                if (task.IsCompleted)
                    Console.WriteLine("Done");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                goto Retry;                
            }
            finally
            {
                if (webClient != null)
                    webClient.Dispose();
            }


           // source.Cancel();
            //cancelToken.c

           // webClient.CancelAsync();
        }

        private static void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine($"%{e.ProgressPercentage}");
        }
    }

    public static class E
    {
       public static Task DownloadFileTaskAsync(this WebClient client, string address, string filename,
                           CancellationToken token)
        {
            token.Register(() => client.CancelAsync());

            return client.DownloadFileTaskAsync(address, filename);
        }
    }
}
