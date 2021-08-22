using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TestSelenium.TestData;

namespace TestSeleniumWithDotnetCore.PageObjects
{
    public class PhptravelsFlightPage
    {
        private readonly IWebDriver driver;

        private ExcelDataAccess excelDataAccess = new ExcelDataAccess();

        ConfigurationCreater configurationCreater = new ConfigurationCreater();


        [FindsBy(How = How.XPath, Using = "//div[@class='custom-control custom-radio  custom-control-inline']")]
        private IWebElement JourneyTypeRadioButtons { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='form-icon-left flightclass']")]
        private IWebElement CabinClassDropdownBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@class='chosen-results']/li")]
        private IList<IWebElement> CabinClassDropdownList { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='s2id_autogen5']")]
        private IWebElement FromField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='s2id_autogen6']")]
        private IWebElement ToField { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@name='cabinclass']")]
        private IWebElement CabinClassDropdownList1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='FlightsDateStart']")]
        private IWebElement DepartureCalendar { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='FlightsDateEnd']")]
        private IWebElement ReturnCalendar { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='datepicker--nav-action' and @data-action='next']//*[local-name()='path']")]
        private IWebElement DatePickerNext { get; set; }

        [FindsBy(How = How.XPath, Using = "(//button[contains(.,'+')])[3]")]
        private IWebElement AdultsPlusButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//button[contains(.,'+')])[4]")]
        private IWebElement ChildPlusButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//button[contains(.,'+')])[5]")]
        private IWebElement InfantPlusButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//button[@type='submit'])[2]")]
        private IWebElement SearchButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[contains(.,'Child')]")]
        private IWebElement ChildLabel { get; set; }

        public PhptravelsFlightPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void SelectJourneyType(string testName)
        {
            configurationCreater.WaitUntil(JourneyTypeRadioButtons, driver);
            string journeyType = excelDataAccess.GetTestData(testName, "JourneyType");
            IWebElement radioBtn = driver.FindElement(By.XPath
                ("//label[contains(.,'"+ journeyType + "')]"));
            radioBtn.Click();
        }

        public void SelectCabinClass(string testName)
        {
            configurationCreater.WaitUntil(CabinClassDropdownList1, driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5000);
            CabinClassDropdownBox.Click();
            string cabinClass = excelDataAccess.GetTestData(testName, "CabinClass");
            IWebElement travelClass = driver.FindElement(By.XPath("//ul[@class='chosen-results']/li[contains(text(),'"+ cabinClass +"')]"));
            travelClass.Click();
        }

        public void SelectOrginDestination(string testName)
        {
            var from = excelDataAccess.GetTestData(testName, "From");
            FromField.SendKeys(from);
            Thread.Sleep(4000);
            Actions act = new Actions(driver);
            act.SendKeys(Keys.Tab).Build().Perform();
            Thread.Sleep(1000);
            var to = excelDataAccess.GetTestData(testName, "To");
            ToField.SendKeys(to);
            Thread.Sleep(4000);
            act.SendKeys(Keys.Tab).Build().Perform();           
        }

        public void SelectDepartureDate(string testName)
        {
            var depdate = excelDataAccess.GetTestData(testName, "Depart");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='"+ depdate + "';", DepartureCalendar);
        }

        public void SelectReturnDate(string testName)
        {
            var retdate = excelDataAccess.GetTestData(testName, "Return");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='" + retdate + "';", ReturnCalendar);
        }

        public void SelectAdultsPassengers(string testName)
        {
            int noOfAdults = Int32.Parse(excelDataAccess.GetTestData(testName, "Adults"));
            if (noOfAdults > 1)
            {
                for (int i = 1; i < noOfAdults; i++)
                {
                    AdultsPlusButton.SendKeys(Keys.Enter);
                    AdultsPlusButton.SendKeys(Keys.Tab);
                }

            }           
        }

        public void SelectChildPassengers(string testName)
        {
            int noOfChildren = Int32.Parse(excelDataAccess.GetTestData(testName, "Child"));
            for (int i = 1; i <= noOfChildren; i++)
            {
                ChildPlusButton.Click();
                //ChildPlusButton.SendKeys(Keys.Enter);

                //ChildPlusButton.SendKeys(Keys.Tab);
                Actions builder = new Actions(driver);
                builder.Release(InfantPlusButton);
               ChildPlusButton.SendKeys(Keys.Tab);
            }
        }

        public void SelectInfantPassengers(string testName)
        {
            int noOfInfant = Int32.Parse(excelDataAccess.GetTestData(testName, "Infant"));
            for (int i = 1; i <= noOfInfant; i++)
            {
                InfantPlusButton.Click();
                //InfantPlusButton.SendKeys(Keys.Enter);
                //InfantPlusButton.Clear();
                //ChildLabel.Click();
                Actions builder = new Actions(driver);
                builder.Release(InfantPlusButton);
            }
        }

        public void SubmitFlightSearch()
        {
            SearchButton.Click();
        }
    }
    
}
