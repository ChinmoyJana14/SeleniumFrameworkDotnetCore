using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TestSelenium.TestData;

namespace TestSeleniumWithDotnetCore.PageObjects
{
    public class PhptravelsAccountPage
    {
        private readonly IWebDriver driver;

        private ExcelDataAccess excelDataAccess = new ExcelDataAccess();

        ConfigurationCreater configurationCreater = new ConfigurationCreater();

        [FindsBy(How = How.XPath, Using = "//a[@class='text-center hotels active']")]
        private IWebElement HotelsTab { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='text-center flights ']")]
        private IWebElement FlightsTab { get; set; }
        public PhptravelsAccountPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void SelectHotel()
        {
            configurationCreater.WaitUntil(HotelsTab, driver);
            HotelsTab.Click();
        }

        public void SelectFlight()
        {
            configurationCreater.WaitUntil(FlightsTab, driver);
            Thread.Sleep(4000);
           // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            FlightsTab.Click();
        }
    }
}
