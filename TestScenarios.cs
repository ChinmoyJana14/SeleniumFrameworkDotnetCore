using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Data;
using TestSelenium.PageObjects;
using TestSeleniumWithDotnetCore;
using TestSeleniumWithDotnetCore.PageObjects;

namespace Tests
{
    [TestFixture]
    public class TestScenarios
    {

       private IConfiguration config;
       public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            //driver = new ChromeDriver(@"C:\\Repos");
            //driver = new FirefoxDriver(@"C:\\Repos");
            driver = new InternetExplorerDriver(@"C:\\Repos");
            //var options = new ChromeOptions();
            //options.AddArgument("-no-sandbox");
            //driver = new ChromeDriver(@"C:\\Repos", options,
            //TimeSpan.FromMinutes(2));
             
            config = ConfigurationCreater.SetConfiguration();
            var url = config["MyConfig:Environment:SapUrl"];
            
            object cookies = ((IJavaScriptExecutor)driver).ExecuteScript("return document.cookie");
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }
            
        [Test]
        // This scenario will not work as NCT portal is developed in Angular, need to use protractor
        public void NCT_Scenario_01()
        {
            var loginPage = new NCTLogInPage(driver);
            loginPage.LoginToApplication(TestContext.CurrentContext.Test.Name, "UserName");
            var dashboardPage = new NCTDashboardPage(driver);
            dashboardPage.AddNewSite();
        }

        [Test]
        public void Phptravels_Scenario_01()
        {
            var homePage = new PhptravelsHomePage(driver);
            homePage.GoToLogIn();

            var loginPage = new PhptravelsLoginPage(driver);
            loginPage.PerformLogIn(TestContext.CurrentContext.Test.Name);

            var accountPage = new PhptravelsAccountPage(driver);
            accountPage.SelectFlight();

            var phptravelsFlightPage = new PhptravelsFlightPage(driver);
            phptravelsFlightPage.SelectJourneyType(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectCabinClass(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectOrginDestination(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectDepartureDate(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectReturnDate(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectAdultsPassengers(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectChildPassengers(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectInfantPassengers(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SubmitFlightSearch();
        }


        [Test]
        public void Phptravels_Scenario_02()
        {
            var homePage = new PhptravelsHomePage(driver);
            homePage.GoToLogIn();

            var loginPage = new PhptravelsLoginPage(driver);
            loginPage.PerformLogIn(TestContext.CurrentContext.Test.Name);

            var accountPage = new PhptravelsAccountPage(driver);
            accountPage.SelectFlight();

            var phptravelsFlightPage = new PhptravelsFlightPage(driver);
            phptravelsFlightPage.SelectJourneyType(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectCabinClass(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectOrginDestination(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectDepartureDate(TestContext.CurrentContext.Test.Name);
            //phptravelsFlightPage.SelectReturnDate(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectAdultsPassengers(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectChildPassengers(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SelectInfantPassengers(TestContext.CurrentContext.Test.Name);
            phptravelsFlightPage.SubmitFlightSearch();
        }
    }
}