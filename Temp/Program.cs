using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace ConsoleApp4
{
    public static class class2
    {
        public static IWebElement WaitUntilGetElementEnabled(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until((d) =>
            {
                IWebElement element = d.FindElement(by);
                if (element.Displayed &&
                    element.Enabled &&
                    element.GetAttribute("disabled") == null)
                {
                    return element;
                }

                return null;
            });
        }
        public static IWebElement WaitUntilGetElement2(this IWebDriver driver, By by)
        {
            IWebElement remElement = null;

            do
            {
                try
                {
                    remElement = driver.FindElement(by);
                    var text = remElement.Text;
                    var ttt = remElement.GetAttribute("class");
                }
                catch (Exception ex)
                {
                    Thread.Sleep(200);
                }

            } while (remElement == null);


            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10000));
            //wait.Until<IWebElement>(d => { return d.FindElement(by);});

            return remElement;
        }

        public static IWebElement WaitUntilGetElement(this IWebDriver driver, By by, int timeoutInSeconds = 30)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        //public static IWebElement WaitUntilGetElementEnabled(this IWebDriver driver, By by, int timeoutInSeconds = 30)
        //{
        //    if (timeoutInSeconds > 0)
        //    {
        //        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        //        return wait.Until(drv => drv.FindElement(by));
        //    }
        //    return driver.FindElement(by);
        //}

        public static void WaitForAjax(this IWebDriver driver)
        {
            //    while (true) // Handle timeout somewhere
            //    {
            //        //string jqueryAjaxReadyScript = "return jQuery.active == 0";
            //        string angularReadyScript = "return angular.element(document).injector().get('$http').pendingRequests.length === 0";
            //        //var jsAjaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").ToString().Equals("complete");

            //        var angularComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript(angularReadyScript);
            //        if (angularComplete)
            //            break;
            //        Thread.Sleep(100);
            //    }



            var pageLoadWait = new WebDriverWait(driver, TimeSpan.FromSeconds(3000));
            pageLoadWait.Until<bool>(
                (d) =>
                {
                    return (bool)(d as IJavaScriptExecutor).ExecuteScript(
                                                                    @"
                                                                    try {
                                                                      if (document.readyState !== 'complete') {
                                                                        return false; // Page not loaded yet
                                                                      }
                                                                      if (window.jQuery) {
                                                                        if (window.jQuery.active) {
                                                                          return false;
                                                                        } else if (window.jQuery.ajax && window.jQuery.ajax.active) {
                                                                          return false;
                                                                        }
                                                                      }
                                                                      if (window.angular) {
                                                                        if (!window.qa) {
                                                                          // Used to track the render cycle finish after loading is complete
                                                                          window.qa = {
                                                                            doneRendering: false
                                                                          };
                                                                        }
                                                                        // Get the angular injector for this app (change element if necessary)
                                                                        var injector = window.angular.element('body').injector();
                                                                        // Store providers to use for these checks
                                                                        var $rootScope = injector.get('$rootScope');
                                                                        var $http = injector.get('$http');
                                                                        var $timeout = injector.get('$timeout');
                                                                        // Check if digest
                                                                        if ($rootScope.$$phase === '$apply' || $rootScope.$$phase === '$digest' || $http.pendingRequests.length !== 0) {
                                                                          window.qa.doneRendering = false;
                                                                          return false; // Angular digesting or loading data
                                                                        }
                                                                        if (!window.qa.doneRendering) {
                                                                          // Set timeout to mark angular rendering as finished
                                                                          $timeout(function() {
                                                                            window.qa.doneRendering = true;
                                                                          }, 0);
                                                                          return false;
                                                                        }
                                                                      }
                                                                      return true;
                                                                    } catch (ex) {
                                                                      return false;
                                                                    }");
                });
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                string url = "https://w3038.azurewebsites.net/wp-login.php";
                driver.Navigate().GoToUrl(url);
                //string url = "https://w3038.azurewebsites.net/wp-admin/";

                IWebElement userLoginElement = driver.WaitUntilGetElement(By.Id("user_login"));
                userLoginElement.Clear();
                userLoginElement.SendKeys("chen303847233");

                IWebElement userpassElement = driver.WaitUntilGetElement(By.Id("user_pass"));
                userpassElement.Clear();
                userpassElement.SendKeys("chen_3038!QA");

                IWebElement submitButton = driver.WaitUntilGetElement(By.Id("wp-submit"));

                new Actions(driver).MoveToElement(submitButton).Click().Perform();

                string postUrl = "https://w3038.azurewebsites.net/wp-admin/post-new.php";

                driver.Navigate().GoToUrl(postUrl);

                driver.WaitForAjax();

                //            
                //
                string image = @"C:\Users\ecghikv\Desktop\1453443404707750.jpg";
                UploadThumbnail(driver,image);
            }


            Console.Read();
        }

        public static void UploadThumbnail(IWebDriver driver, String imgPath)
        {
            //parent current window
           // String currentWindow = driver.getw();

            //System.out.println(imgPath);
            try
            {
                driver.WaitUntilGetElement(By.Id("set-post-thumbnail")).Click();
                driver.WaitForAjax();
                driver.WaitUntilGetElement(By.LinkText("Upload Files")).Click();

                //Select Files
                driver.WaitForAjax();
                //driver.WaitUntilGetElement(By.Id("__wp-uploader-id-1")).Click();
                //upload
                driver.WaitUntilGetElement(By.XPath("//div[7]/input")).SendKeys(imgPath);
                driver.WaitForAjax();
                var btnSetFeatureImage = driver.WaitUntilGetElementEnabled(By.XPath("//div[@id='__wp-uploader-id-0']/div[5]/div/div[2]/button"));
              
                //String buttonStatus = "disabled";
                //while (buttonStatus == "disabled")
                //    buttonStatus = btnSetFeatureImage.GetAttribute("disabled");

                btnSetFeatureImage.Click();


                driver.WaitForAjax();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

       


    }
}
