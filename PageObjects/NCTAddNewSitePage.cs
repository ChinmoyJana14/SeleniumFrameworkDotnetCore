using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Threading;
using TestSelenium.TestData;

namespace TestSeleniumWithDotnetCore.PageObjects
{
    class NCTAddNewSitePage
    {
        private readonly IWebDriver driver;

        private ExcelDataAccess excelDataAccess = new ExcelDataAccess();

        private IConfiguration config;

        [FindsBy(How = How.XPath, Using = "//mat-select[@id='mat-select-5']")]
        private IWebElement ConsentDropdown { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[@class='file-upload-button__label']")]
        private IWebElement UploadSignedFormButton { get; set; }
        
        public NCTAddNewSitePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void AddSignedPaperConsent(string testName, string UserNameFromDataSheet)
        {
            SelectElement select = new SelectElement(ConsentDropdown);
            select.SelectByText("Signed paper consent");
            UploadSignedFormButton.Click();

            config = ConfigurationCreater.SetConfiguration();
            var filePath = config["MyConfig:LocalFilePath"];
            Thread.Sleep(2000);
            UploadSignedFormButton.SendKeys(filePath);
            ////System.Windows.Forms.SendKeys.SendWait(filePath);
            ////System.Windows.Forms.SendKeys.SendWait(@"{Enter}");
            //var filedialogOverlay = driver.SwitchTo().ActiveElement();
            //filedialogOverlay.SendKeys.SendWait(filePath);
            ////Input "Enter" key
            //filedialogOverlay.SendKeys.SendWait(@"{Enter}");
        }
    }
}
