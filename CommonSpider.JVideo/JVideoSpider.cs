//using DotnetSpider.Core;
//using DotnetSpider.Core.Infrastructure;
//using DotnetSpider.Core.Pipeline;
//using DotnetSpider.Core.Processor;
//using DotnetSpider.Core.Scheduler;
//using DotnetSpider.Core.Selector;
//using DotnetSpider.Extension.Downloader;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Interactions;
//using OpenQA.Selenium.Remote;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;

//namespace CommonSpider.JVideo
//{
//    public class JVideoSpider
//    {
//        public static void Run()
//        {
//            // Config encoding, header, cookie, proxy etc... 定义采集的 Site 对象, 设置 Header、Cookie、代理等
//            var site = new Site { EncodingName = "UTF-8" };
//            //for (int i = 1; i < 5; ++i)
//            //{
//            // Add start/feed urls. 添加初始采集链接
//            site.AddStartUrl($"https://hpjav.com/tw/category/censored");
//            //}
//            Spider spider = Spider.Create(site,
//                // use memoery queue scheduler. 使用内存调度
//                new QueueDuplicateRemovedScheduler(),
//                // use custmize processor for youku 为优酷自定义的 Processor
//                new JavPageProcessor())
//                // use custmize pipeline for youku 为优酷自定义的 Pipeline
//                .AddPipeline(new JavPipeline());
//            spider.Downloader = new WebDriverDownloader(Browser.Chrome);
//            spider.ThreadNum = 1;
//            spider.EmptySleepTime = 3000;

//            // Start crawler 启动爬虫
//            spider.Run();
//        }
//    }
//    public class JavPipeline : BasePipeline
//    {
//        private static long count = 0;

//        public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
//        {
//            foreach (var resultItem in resultItems)
//            {
//                StringBuilder builder = new StringBuilder();
//                foreach (Video entry in resultItem.Results["VideoResult"])
//                {
//                    count++;
//                    builder.AppendLine($" [Video {count}] {entry.Url}");

//                    var site = new Site { EncodingName = "UTF-8" };
//                    site.AddStartUrl(entry.Url);
//                    JavDetailPageSpider.Run(site);
//                }
//                Console.WriteLine(builder);
//            }
//            // Other actions like save data to DB. 可以自由实现插入数据库或保存到文件
//        }

      
//    }

//    public class JavPageProcessor : BasePageProcessor
//    {
//        protected override void Handle(Page page)
//        {
//            // 利用 Selectable 查询并构造自己想要的数据对象
//            var videos = page.Selectable.SelectList(Selectors.XPath("//div[@class='entry-title']")).Links().Nodes();

//            List<Video> results = new List<Video>();

//            foreach (var item in videos)
//            {
//                var video = new Video();
//                video.Url = item.GetValue();
//                results.Add(video);
//            }

//            // Save data object by key. 以自定义KEY存入page对象中供Pipeline调用
//            page.AddResultItem("VideoResult", results);

//            // Add target requests to scheduler. 解析需要采集的URL
//            foreach (var url in page.Selectable.SelectList(Selectors.XPath("//ul[@class='pagination']")).Links().Nodes())
//            {
//                //page.AddTargetRequest(new Request(url.GetValue(), null));
//            }
//        }
//    }

//    public class Video
//    {
//        public string Url { get; set; }
//        public string Title { get; set; }
//    }

//    public class JavDetailPageSpider
//    {
//        public static void Run(Site site)
//        {
//            // Config encoding, header, cookie, proxy etc... 定义采集的 Site 对象, 设置 Header、Cookie、代理等
//            //var site = new Site { EncodingName = "UTF-8" };
//            //for (int i = 1; i < 5; ++i)
//            //{
//            // Add start/feed urls. 添加初始采集链接
//            //  site.AddStartUrl($"https://hpjav.com/tw/category/censored");
//            //}
//            Spider spider = Spider.Create(site,
//                // use memoery queue scheduler. 使用内存调度
//                new QueueDuplicateRemovedScheduler(),
//                // use custmize processor for youku 为优酷自定义的 Processor
//                new JavDetailPageProcessor())
//                // use custmize pipeline for youku 为优酷自定义的 Pipeline
//                .AddPipeline(new JavDetailPagePipeline());


//            var downloader = new WebDriverDownloader(Browser.Chrome);
//            //downloader.NavigateCompeleted += Download_NavigateCompeleted;
//            List<IWebDriverHandler> webDriverHandler = new List<IWebDriverHandler>();

//            webDriverHandler.Add(new JDetailedPageWebDriverHandler());

//            downloader.WebDriverHandlers = webDriverHandler;

//            spider.Downloader = downloader;

//            spider.ThreadNum = 1;
//            spider.EmptySleepTime = 3000;

//            // Start crawler 启动爬虫
//            spider.Run();
//        } 
//    }

//    public class JDetailedPageWebDriverHandler : IWebDriverHandler
//    {
//        public bool Handle(RemoteWebDriver driver)
//        {
//            IWebElement playButton = WaitUntilGetElement(driver, By.ClassName("play-button"));
//            new Actions(driver).MoveToElement(playButton).Click().Perform();

//            driver.SwitchTo().Frame("server0");

//            playButton = WaitUntilGetElement(driver, By.XPath("//button[@class='vjs-big-play-button']"));

//            //double click
//            new Actions(driver).MoveToElement(playButton).Click().Perform();

//            new Actions(driver).MoveToElement(playButton).Click().Perform();


//            return true;
//        }

//        public static IWebElement WaitUntilGetElement(IWebDriver driver, By by)
//        {
//            IWebElement remElement = null;
//            do
//            {
//                try
//                {
//                    remElement = driver.FindElement(by);
//                }
//                catch (Exception ex)
//                {
//                    Thread.Sleep(200);
//                }

//            } while (remElement == null);


//            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));
//            //wait.Until<IWebElement>(d => { return d.FindElement(by);});

//            return remElement;
//        }
//    }

//    public class JavDetailPagePipeline : BasePipeline
//    {
//        private static long count = 0;

//        public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
//        {
//            foreach (var resultItem in resultItems)
//            {
//                StringBuilder builder = new StringBuilder();
//                foreach (Video entry in resultItem.Results["VideoDetailResult"])
//                {
//                    count++;
//                    builder.AppendLine($" [Video {count}] {entry.Url}");
//                }
//                Console.WriteLine(builder);
//            }
//            // Other actions like save data to DB. 可以自由实现插入数据库或保存到文件
//        }
//    }

//    public class JavDetailPageProcessor : BasePageProcessor
//    {
//        protected override void Handle(Page page)
//        {
//            // 利用 Selectable 查询并构造自己想要的数据对象
//            //var videos = page.Selectable.SelectList(Selectors.XPath("//div[@class='entry-title']")).Links().Nodes();

//            //IWebElement videoElement = WaitUntilGetElement(obj, By.Id("olvideo_html5_api"));

//            //var streamUrl = videoElement.GetAttribute("src");

//            //Console.WriteLine(streamUrl);

//            var url = page.Selectable.Select(Selectors.XPath("//video[@id='olvideo_html5_api']/@src"));

//            List<Video> results = new List<Video>();


//            var video = new Video();
//            video.Url = url.GetValue();
//            results.Add(video);


//            // Save data object by key. 以自定义KEY存入page对象中供Pipeline调用
//            page.AddResultItem("VideoDetailResult", results);


//        }


//    }
//}
