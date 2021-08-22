using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TestSelenium.PageObjects;


namespace TestSeleniumWithDotnetCore
{
    public class ConfigurationCreater
    {
  
        public static IConfiguration SetConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            return config;
        }
        public void WaitUntil(IWebElement element, IWebDriver driver)
        {
            //if (wait == null)
            //    wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            //wait.Until(driver => element);
            //int timeoutInSeconds;
            //if (timeoutInSeconds > 0)
            //{
            //    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            //    return wait.Until(drv => drv.FindElement(by));
            //}

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement myDynamicElement = wait.Until<IWebElement>(d => element);
        }

}
}
