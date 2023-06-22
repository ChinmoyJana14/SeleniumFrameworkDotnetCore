# SeleniumFrameworkDotnetCore
# Quick Pro Tips
namespace is package maintaining the Classes
Same class can be there in two different name space, can be called inside another creating namespace.class.object

var is a dynamic data type, can assign any value, evaluate run time not compile time.
cannot reassign different data type
var a = 1;
a = "str" // Compile error
But for dynamic data type, data type can be changed unlike var
       dynamic b = 1.1;
            b = "str";

Above c# 8 i.e >.NET core 3.x, don't need to write code inside class,namespace and method
The main method will added automatically and globally, this takes priority over declared Main
To change the priority -> Class->Property->build Action ->None
For multiple declared Main -> Runtime error

Contructor:
Intinilization
class Program
    {
        String name;
        public Program(String name)
        {
            this.name = name;
        }
static void Main(string[] args)
        {
            Program p = new Program("Priya");
            p.GetName();
        }
public void GetName()
        {
            Console.WriteLine($"Name is {name}");
        }
    }

Inheritence of Parametized Constructor:
Need to inheritate the parent constructor using base keyword
using System;
namespace Fundas
{
class ParentClass
{
int mark;
public ParentClass(int mark)
{
this.mark = mark;
}
public void ParentClassMeth()
{
int perc = this.mark / 100;
Console.WriteLine("The Percentage Obtained is : {0}", perc);
}
}

class ChildInheritedClass : ParentClass
{
public ChildInheritedClass(int a) : base(a) // have to inherit parent constructor using base
{
}    
static void Main(string[] args)
{
ChildInheritedClass obj = new ChildInheritedClass(600);
obj.ParentClassMeth();
}
}
}

Selenium: NUnit Test Project

FirefoxDriver()/ChromeDriver() inheriting the RemoteDriver class which implements the WebDriver interface
If your main tests and other classes define the commonly shared driver as simply WebDriver instead of specifically being tied to ChromeDriver,
then the same tests can be run without change to the test code itself simply by initializing the shared driver object with a different driver extended class.

Architecture
Selenium cannot talk with browser directly, so
Selenium WebDriver launches browser specific server -> Selenium WebDriver Client Libraries / Language Bindings ->
Create HTTP requests(RESTful web service of JSON using Wire Protocol ) -> Exclusive browser executable files get the commands ->
communicate with browser via same protocol over the HTTP server ->
Response from browser after execution of command is also sent back to Selenium WebDriver API through the same server.

Set Up Driver
Recent version
new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
Specific version
   new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), "104.0.5112.79");

For Firefox
new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
    driver = new FirefoxDriver();

Implicit Wait
driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

Explicit Wait
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
WebDriverWait wait;
wait.Until(ExpectedConditions.ElementIsVisible
                    (By.XPath("//h1[contains(text(),'Welcome to Frame')]")));

Alert - Window based popup
driver.SwitchTo().Alert().Accept();
driver.SwitchTo().Alert().Dismiss();
driver.SwitchTo().Alert().Sendkeys("hello");

Scroll
IWebElement webElement = driver.FindElement(By.Id("Frame-ID"));
    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
    js.ExecuteScript("argument[0].scrollIntoView(true);", webElement);

Frame
driver.SwitchTo().Frame("Id/Name/Index of the iFrame");  
driver.SwitchTo().DefaultContent();

MultiWindow:
    string parentWindow = driver.CurrentWindowHandle;
driver.FindElement(By.Id("LinkOfNewWindow"));
    string childWindow = driver.WindowHandles[1];
    driver.SwitchTo().Window(childWindow);
driver.SwitchTo().Window(parentWindow);

Page Objects
public class FinalPage
    {
        private By deliveryLocationIndia = By.LinkText("India");

        public FinalPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement deliveryLocationBox;

        public IWebElement getdeliveryLocationBox()
        {
            return deliveryLocationBox;
        }
public By getdeliveryLocationIndia()
        {
            return deliveryLocationIndia;
        }
}

Data Driven
   [Test]
        [TestCase("sanujana8@gmail.com", "Practice12345")]
        [TestCase("sanujana7@gmail.com", "Practice1234567")]
        public void LogInTest(string username, string password)
        {
            homePage = new HomePage(getDriver());
            homePage.getSignInButton().Click();
            logInPage = new LogInPage(getDriver());
            productsPage = logInPage.ValidLogIn(username, password);
        }

For multiple test data
[Test, TestCaseSource("AddTestDataConfig")]
public void LogInTest(string username, string password)
        {
            homePage = new HomePage(getDriver());
            homePage.getSignInButton().Click();
            logInPage = new LogInPage(getDriver());
            productsPage = logInPage.ValidLogIn(username, password);
        }
public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData("sanujana8@gmail.com", "Practice12345");
            yield return new TestCaseData("sanujana7@gmail.com", "Practice1234567");
            yield return new TestCaseData("sanujana5@gmail.com", "Practice1234589");
        }

Json Driven
To get current Dir->  {Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}

1. Create Json file and save in project level
Right Click -> Properties-> Copy to Output Directory -> Always

2.     var myJsonString = File.ReadAllText("Utilities//testData.json");
           var jsonObject =  JToken.Parse(myJsonString);
           return jsonObject.SelectToken(tokenName).Value<string>();
 
3.   public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"));
            yield return new TestCaseData(getDataParser().extractData("wrong_username"), getDataParser().extractData("wrong_password"));
            yield return new TestCaseData(getDataParser().extractData("wrong_username_2"), getDataParser().extractData("wrong_password_2"));
        }

4. For Array
        public string[] extractDataArray(string tokenName)
        {
            var myJsonString = File.ReadAllText("Utilities//testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectTokens(tokenName).Values<string>().ToList().ToArray();
        }

public static IEnumerable<TestCaseData> Test_02()
        {
            yield return new TestCaseData(new[] { getDataParser().extractDataArray("products") });
        }

Parallel Execution:
To do that, we need to create a pool of driver to become thread safe
public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
and to access driver from the pool for each thread
driver.Value instead of driver
//Run all data sets of Test method in parallel
Add below annotation over the method
[Parallelizable(ParallelScope.All)]
//Run all test methods in one class parallel
Add below annotation over the class
[Parallelizable(ParallelScope.Children)]
//Run all test files in project parallel
Add below annotation over all the class
[Parallelizable(ParallelScope.Self)]

Run Seleted TestCase - Category
To run all test from cmd -> Go to project level and run "dotnet test SeleniumFramework.csproj"
[Test, TestCaseSource("Test_02"), Category("Smoke")]
[Test, TestCaseSource("AddTestDataConfig"),Category("Regression")]
dotnet test SeleniumFramework.csproj --filter TestCategory=Regression

Set Variable From Outside:
Set a variable expecting from terminal
Cmd Command:    dotnet test SeleniumFramework.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\", value=\"Chrome\")
Powershell Command: dotnet test SeleniumFramework.csproj --filter TestCategory=Regression --% -- TestRunParameters.Parameter(name=\"browserName\", value=\"Chrome\")

To run the build dll file:
Set patha variable with C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow
Go to the dll folder location after build - like C:\Users\a140614\source\repos\SeleniumPractice\FrameUI\bin\Debug\net5.0
run vstest.console.exe <file name>.dll

Extent Report:
Install nuget
[OneTimeSetUp] Annotation runs only once in a project where [SetUp] method runs before every method

ExtentReports extent;
        ExtentTest test;
   [OneTimeSetUp]
        public void SetUp()
        {
            string reportDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
                                + "//reports//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportDir);
            extent.AttachReporter(htmlReporter);//Assign html reporter object to the object of ExtentReports(main guy)
            extent.AddSystemInfo("Host Name","Local Host");
            extent.AddSystemInfo("Environement", "QA");
            extent.AddSystemInfo("User", "Chinmoy Jana");
        }

Inside the [SetUp] method
test = extent.CreateTest(TestContext.CurrentContext.Test.Name);//For every test the test method name will be assign to the the object of ExtentReports dynamically

Inside the [TearDown] method
public void Close()
        {
            var resultStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var logTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime dt = DateTime.Now;
            String fileName = "Screenshot_" + dt.ToString("hh_mm_ss") + ".png";
            if(resultStatus == TestStatus.Failed)
            {
                test.Fail("Test Failed", captureScreenshot(driver.Value, fileName));
                test.Log(Status.Fail, "Test Failed with LogTrace" + logTrace);
            }
            else if (resultStatus == TestStatus.Passed)
            {

            }
            extent.Flush();
            driver.Value.Quit();
        }

Another method for taking screenshot:
public MediaEntityModelProvider captureScreenshot(IWebDriver driver,String screenshotName)
{
  ITakesScreenshot ts = (ITakesScreenshot)driver;
  var screenshot = ts.GetScreenshot().AsBase64EncodedString;
return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
}

Jenkins:
Download jenkins.war
go to the patha -> java -jar jenkins.war -httpPort=9090
open local host and log in with set username password
New Item->FreeStyleProject-> Custom Workspace for local else git-> add patha
Build->Add build step -> Execute Shell or Execute windows batch
provide the command like
dotnet test SeleniumFramework.csproj --filter TestCategory=Regression -- TestRunParameters.Parameter(name=\"browserName\", value=\"Chrome\")
save -> build now
Jenkins Parameterization:
General->This project is Parametized->Add Parameter->Choice parameter
Name->browserName
Choices->
Chrome
Firefox
Edge
Save
Build with parameter-> dropdown will be there
in the command replace the value with "$browserName"

handle calendar
String mon = "Nov 2023";
String day = "23";
driver.findElement.By.xpath("xpath of date picker").click();
WebElement currentMonth = driver.findElement.By.xpath("xpath of current month displayed");
while(true){
if (currentMonth.getText().equals(mon)){
break;
} else{
driver.findElement.By.xpath("xpath of next button").click();
}
}
driver.findElement.By.xpath("//[@class='datepicker']/div[1]/table/tbody/tr/td[contains(text(),'" + day + "')]").click();


------------------------- ---------------
else if (DateTime.Parse(currentMonthFromPage.Text.Trim()) > DateTime.Parse(providedDate))

- $exception {"Title: Is Merge Date disabled?. Expected: . Actual: disabled"} Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException
            DateTime dt1 = DateTime.Parse("1 Apr 2022");
            DateTime dt2 = DateTime.Parse("Oct 2022");
            if (DateTime.Parse("1 Apr 2022") > DateTime.Parse("Oct 2022"))
            {
                Console.WriteLine("aaaa");
            } else
            {
                Console.WriteLine("hhhh");
   
scroll:
            var element = driver.Value.FindElement(finForecastScPage.getContractMarktoMarketCollapseLInk());
            Actions actions = new Actions(driver.Value);
            actions.MoveToElement(element);
            actions.Perform();

            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver.Value;
            //js.ExecuteScript("window.scrollBy(0,250)", "");

//div[@id='toast-container']//following-sibling::div[@class='toast-message'][text()='Storage cleanig started.']
//div[@id='toast-container']//following-sibling::div[@class='toast-message'][text()='Checking for any running job.']

Improvements :
1. publish all validation points in the report
2. share report over email
3. trigger DB validation after finishing UI - 1.5 hr wait
4. cross env test
