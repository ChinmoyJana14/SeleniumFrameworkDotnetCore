using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using TestSelenium.TestData;

namespace TestSeleniumWithDotnetCore.PageObjects
{
    public class PhptravelsHomePage
    {
        private readonly IWebDriver driver;

        private ExcelDataAccess excelDataAccess = new ExcelDataAccess();

        ConfigurationCreater configurationCreater = new ConfigurationCreater();

        [FindsBy(How = How.XPath, Using = "//div[@class='dropdown dropdown-login dropdown-tab']/a[@id='dropdownCurrency']")]
        private IWebElement MyAccountButton { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Login')]")]
        private IWebElement LoginLink { get; set; }
        public PhptravelsHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void GoToLogIn()
        {
            configurationCreater.WaitUntil(MyAccountButton, driver);
            MyAccountButton.Click();
            LoginLink.Click();
        }
    }
}
