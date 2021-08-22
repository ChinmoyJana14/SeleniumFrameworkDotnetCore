using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
    class NCTLogInPage
    {
        private readonly IWebDriver driver;

        private ExcelDataAccess excelDataAccess = new ExcelDataAccess();

        ConfigurationCreater configurationCreater = new ConfigurationCreater();

        [FindsBy(How = How.XPath, Using = "//login-app/login-component//login-identifier//input")]
        private IWebElement UserName { get; set; }

        [FindsBy(How = How.XPath, Using = "//login-app/login-component//login-identifier/form//button")]
        private IWebElement Next { get; set; }

        [FindsBy(How = How.XPath, Using = "//login-app/login-component//login-password/form//input")]

        private IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//login-app/login-component//login-password/form//button")]

        private IWebElement Login { get; set; }

        public NCTLogInPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void LoginToApplication(string testName, string UserNameFromDataSheet)
        {
            var userData = excelDataAccess.GetTestData(testName,UserNameFromDataSheet);
            UserName.SendKeys(userData);
            Next.Click();
            configurationCreater.WaitUntil(Password,driver);
           var password = excelDataAccess.GetTestData(testName, "Password");
            // Password.SendKeys(password);
             new Actions(driver).SendKeys(password).Perform();
            configurationCreater.WaitUntil(Login, driver);
          
            Login.Submit();
        }
    }
}

