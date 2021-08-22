using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;
using TestSelenium.TestData;

namespace TestSeleniumWithDotnetCore.PageObjects
{
    class PhptravelsLoginPage
    {
        private readonly IWebDriver driver;

        private ExcelDataAccess excelDataAccess = new ExcelDataAccess();

        ConfigurationCreater configurationCreater = new ConfigurationCreater();

        [FindsBy(How = How.XPath, Using = "//input[@type='email']")]
        private IWebElement EmailField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement PasswordField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement LoginButton { get; set; }

        public PhptravelsLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
         public void PerformLogIn(string testName)
            {
                var userEmail = excelDataAccess.GetTestData(testName, "UserName");
                EmailField.SendKeys(userEmail);
                var userPassword = excelDataAccess.GetTestData(testName, "Password");
                PasswordField.SendKeys(userPassword);
                LoginButton.Click();
          }
        
    }
}
