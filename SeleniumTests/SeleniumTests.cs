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

        [TestMethod]
        public void CityOfPocatello()
        {
            Driver.Url = "https://id-pocatello.civicplushrms.com/CPExternal/Jobs.aspx";

            var page = new CityOfPocatello(Driver);

            page.SearchInfoTech();
        }

        [TestMethod]
        public void LDSIdaho()
        {
            Driver.Url = "https://careers.lds.org/search/Public/Search.aspx";

            var page = new LDS(Driver);

            page.SearchIdahoInfoTech();
        }

        [TestMethod]
        public void LDSUtah()
        {
            Driver.Url = "https://careers.lds.org/search/Public/Search.aspx";

            var page = new LDS(Driver);

            page.SearchUtahInfoTech();
        }

        [TestMethod]
        public void Intermountain()
        {
            Driver.Url = "https://jobs.intermountainhealthcare.org/res_joblist.html";

            var page = new Intermountain(Driver);

            page.SearchInfoTech();
        }

        [TestMethod]
        public void Simplot()
        {
            Driver.Url = "https://jrsext.simplot.com/prodhcm/CandidateSelfService/controller.servlet?context.dataarea=prodhcm&webappname=CandidateSelfService&context.session.key.HROrganization=JRS&context.session.key.JobBoard=EXTERNAL2&_saveKeys=true&context.session.key.noheader=true";

            var page = new Simplot(Driver);

            page.SearchInfoTech();
        }

        [TestMethod]
        public void LabCorp()
        {
            Driver.Url = "https://jobs.labcorp.com/category/information-technology-jobs/668/4482/1";

            var page = new LabCorp(Driver);

            page.SearchStates();
        }

        [TestMethod]
        public void MaverikTechnical()
        {
            Driver.Url = "https://rn21.ultipro.com/FJM1000/JobBoard/SearchJobs.aspx?Page=Search";

            var page = new Maverik(Driver);

            page.SearchTech();
        }

        [TestMethod]
        public void MaverikIT()
        {
            Driver.Url = "https://rn21.ultipro.com/FJM1000/JobBoard/SearchJobs.aspx?Page=Search";

            var page = new Maverik(Driver);

            page.SearchIT();
        }

        [TestMethod]
        public void ONSemiIdaho()
        {
            Driver.Url = "https://onsemi.taleo.net/careersection/on_ex/jobsearch.ftl?lang=en&src=CWS-1";

            var page = new ONSemi(Driver);

            page.SearchState("Idaho");
        }

        [TestMethod]
        public void ONSemiUtah()
        {
            Driver.Url = "https://onsemi.taleo.net/careersection/on_ex/jobsearch.ftl?lang=en&src=CWS-1";

            var page = new ONSemi(Driver);

            page.SearchState("Utah");
        }

        /* TO ADD:
         * https://apply.fbijobs.gov/ // FBI 
         * https://employment.wellsfargo.com/psp/PSEA/APPLICANT_NW/HRMS/c/HRS_HRAM.HRS_APP_SCHJOB.GBL?FOCUS=Applicant // Wells Fargo
         */
    }
}
