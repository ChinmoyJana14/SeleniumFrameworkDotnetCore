using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using TestSelenium.TestData;
using TestSeleniumWithDotnetCore;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;

namespace TestSelenium.PageObjects
{

    public class NCTDashboardPage
    {
        private readonly IWebDriver driver;
        ConfigurationCreater configurationCreater = new ConfigurationCreater();
        [FindsBy(How = How.XPath, Using = "//sidenav/a[3]")]
        private IWebElement AddNewSiteLink { get; set; }

        public NCTDashboardPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void AddNewSite()
        {
            configurationCreater.WaitUntil(AddNewSiteLink, driver);
            AddNewSiteLink.Click();
        }
    }
}

