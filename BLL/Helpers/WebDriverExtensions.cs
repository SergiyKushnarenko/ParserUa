﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace BLL.Helpers
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            var counter = 0;

            do
            {
                try
                {
                    return driver.FindElement(by);


                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    Task.Delay(1000).Wait();
                    counter++;
                    continue;
                }

            }
            while (counter < timeoutInSeconds);

            return driver.FindElement(by);

            //if (timeoutInSeconds > 0)
            //{
            //    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            //    Task.Delay(3500).Wait();
            //    return wait.Until(drv => drv.FindElement(by));
            //}
            //return driver.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElements(by));
            }
            return driver.FindElements(by);
        }
    }
}
