using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumTests
{
    [TestClass]
    public class SeleniumTests
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            Driver = new ChromeDriver();
            //Driver = new FirefoxDriver();

            Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(10));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            /* 
             * Usually you would want to close the browser window(s) when the test suite is done running, 
             * for this project they should stay open when the tests are done. Though each test will close 
             * the window if no job results are found.
            */
            // Driver.Quit();
        }

        [TestMethod]
        public void AdvantageCreditUnion()
        {
            Driver.Url = "https://www.advantagepluscreditunion.com/about-us/contact/careers.html";
            var page = new AdvantageCreditUnion(Driver);

            page.CheckForJobs();
        }

        [TestMethod]
        public void IdahoCentralCreditUnionInfoSys()
        {
            // search for Information Systems jobs

            Driver.Url = "https://careers-iccu.icims.com/";

            var page = new IdahoCentralCreditUnion(Driver);

            page.SearchInfoSys();            
        }

        [TestMethod]
        public void IdahoCentralCreditUnionInfoTech()
        {
            // search for Information Technology jobs

            Driver.Url = "https://careers-iccu.icims.com/";

            var page = new IdahoCentralCreditUnion(Driver);

            page.SearchInfoTech();            
        }
    }
}
