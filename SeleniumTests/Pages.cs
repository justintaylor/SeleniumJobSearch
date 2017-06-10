using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace SeleniumTests
{

    public class AdvantageCreditUnion
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public string url = "https://www.advantagepluscreditunion.com/about-us/contact/careers.html";

        private string noOpenings = "No Current Job Openings";

        [FindsBy(How = How.CssSelector, Using = ".content div h1")]
        [CacheLookup]
        private IWebElement Element { get; set; }

        public AdvantageCreditUnion(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => Element.Displayed);
        }

        public void CheckForJobs()
        {
            if (Element.Text == noOpenings)
            {
                // no openings, close page
                driver.Close();

                Assert.Inconclusive("no openings at this time");
            }
        }
    }

    public class IdahoCentralCreditUnion
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public string url = "https://careers-iccu.icims.com/";

        public string frame = "icims_content_iframe";
        private string infoSysValue = "8730";
        private string infoTechValue = "55143";
        private string locationValue = "-12796-Chubbuck";
        private string title = "Careers Center - Welcome";
        private string jobList = "Careers Center - Job Listings";
        private const string error = "Error: Sorry, no jobs were found that match your search criteria. Please try other selections.";
        private const string jobs = "Here are our current job openings. For more information or to apply for a position, please click on the job title.";

        [FindsBy(How = How.Id, Using = "jsb_f_position_s")]
        [CacheLookup]
        private IWebElement Category { get; set; }
        // selectElement implementation from: http://stackoverflow.com/questions/31613763/how-to-initialize-selectelements-while-using-pagefactory-findsby-in-selenium-c/31615591#31615591
        public SelectElement CategorySelect
        {
            get { return new SelectElement(Category); }
        }

        [FindsBy(How = How.Id, Using = "jsb_f_positiontype_s")]
        [CacheLookup]
        private IWebElement Position { get; set; }
        public SelectElement PositionSelect
        {
            get { return new SelectElement(Position); }
        }

        [FindsBy(How = How.Id, Using = "jsb_f_location_s")]
        [CacheLookup]
        private IWebElement Location { get; set; }
        public SelectElement LocationSelect
        {
            get { return new SelectElement(Location); }
        }

        [FindsBy(How = How.Id, Using = "jsb_form_submit_i")]
        [CacheLookup]
        private IWebElement Search { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".iCIMS_Header + div")]
        [CacheLookup]
        private IWebElement Message { get; set; }

        public IdahoCentralCreditUnion(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));
        }

        public void SearchInfoSys()
        {
            SearchCategory(infoSysValue);
        }

        public void SearchInfoTech()
        {
            SearchCategory(infoTechValue);
        }

        public void SearchCategory(string selection)
        {
            // focus to the iframe
            driver.SwitchTo().Frame(frame);

            CategorySelect.SelectByValue(selection);
            LocationSelect.SelectByValue(locationValue);

            Search.Click();

            wait.Until(d => d.Title.StartsWith(jobList, StringComparison.OrdinalIgnoreCase));

            // get text from this element to determine if jobs are shown
            switch (Message.Text)
            {
                case error:
                    driver.Close();
                    Assert.Inconclusive("no openings at this time");
                    break;
                case jobs:
                    // there are jobs available, don't close the window
                    break;
                default:
                    Assert.Fail("An unexpected string was encountered: " + Message.Text);
                    break;
            }
        }
    }

    public class CityOfPocatello
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public string url = "https://id-pocatello.civicplushrms.com/CPExternal/Jobs.aspx";

        private string locationsValue = "844";
        private string noOpenings = "No job openings currently available.";

        [FindsBy(How = How.Id, Using = "ddlLocations")]
        [CacheLookup]
        private IWebElement Locations { get; set; }
        public SelectElement LocationsSelect
        {
            get { return new SelectElement(Locations); }
        }

        [FindsBy(How = How.Id, Using = "Searchresult")]
        [CacheLookup]
        private IWebElement SearchResult { get; set; }

        [FindsBy(How = How.ClassName, Using = "prev")]
        [CacheLookup]
        private IWebElement Previous { get; set; }

        public CityOfPocatello(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => Previous.Displayed);
        }

        public void SearchInfoTech()
        {
            LocationsSelect.SelectByValue(locationsValue);

            if (SearchResult.Text == noOpenings)
            {
                driver.Close();

                Assert.Inconclusive("no openings at this time");
            }
        }
    }

    public class LDS
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LDS(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => Search.Displayed);
        }

        private string zeroResults = "0 results";

        [FindsBy(How = How.Id, Using = "ctl00_m_g_3f46ec34_e362_4676_8aa8_2b87e4a9900b_btnSearchRight")]
        [CacheLookup]
        private IWebElement Search { get; set; }


        [FindsBy(How = How.CssSelector, Using = "[data-id=ctl00_m_g_3f46ec34_e362_4676_8aa8_2b87e4a9900b_ddlCountry]")]
        [CacheLookup]
        private IWebElement Country { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.ddlcountry .dropdown-menu.open ul li:nth-child(1) a")]
        [CacheLookup]
        private IWebElement CountryDropdown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.ddlcountry .dropdown-menu.open ul li:nth-child(123) a")]
        [CacheLookup]
        private IWebElement CountryUSA { get; set; }

        // --------------------------------------------------

        [FindsBy(How = How.CssSelector, Using = "[data-id=ddlLocation]")]
        [CacheLookup]
        private IWebElement Location { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#recruitingLocationDiv .dropdown-menu")]
        [CacheLookup]
        private IWebElement LocationDropdown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#recruitingLocationDiv .dropdown-menu ul li:nth-child(21) a")]
        [CacheLookup]
        private IWebElement LocationInputIdaho { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#recruitingLocationDiv .dropdown-menu ul li:nth-child(55) a")]
        [CacheLookup]
        private IWebElement LocationInputUtah { get; set; }

        // --------------------------------------------------

        [FindsBy(How = How.CssSelector, Using = "[data-id=ctl00_m_g_3f46ec34_e362_4676_8aa8_2b87e4a9900b_ddlJobFamily]")]
        [CacheLookup]
        private IWebElement JobFamily { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#ctl00_m_g_3f46ec34_e362_4676_8aa8_2b87e4a9900b > div.row.search-area > div.col-lg-3.col-md-4.col-sm-4 > div:nth-child(3) > div:nth-child(2) > div > div > div > ul > li:nth-child(1) > a")]
        [CacheLookup]
        private IWebElement JobFamilyDropdown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#ctl00_m_g_3f46ec34_e362_4676_8aa8_2b87e4a9900b > div.row.search-area > div.col-lg-3.col-md-4.col-sm-4 > div:nth-child(3) > div:nth-child(2) > div > div > div > ul > li:nth-child(15) > a")]
        [CacheLookup]
        private IWebElement JobFamilyInfoTech { get; set; }

        // --------------------------------------------------

        [FindsBy(How = How.Id, Using = "ctl00_m_g_3f46ec34_e362_4676_8aa8_2b87e4a9900b_lblNumResults")]
        [CacheLookup]
        private IWebElement ResultsCount { get; set; }
        
        public void SearchIdahoInfoTech()
        {
            Country.Click();
            wait.Until(d => CountryDropdown.Displayed);
            CountryUSA.Click();

            Location.Click();
            wait.Until(d => LocationDropdown.Displayed);
            LocationInputIdaho.SendKeys(Keys.Space);

            JobFamily.Click();
            wait.Until(d => JobFamilyDropdown.Displayed);
            JobFamilyInfoTech.SendKeys(Keys.Space);

            Search.Click();

            wait.Until(d => ResultsCount.Text.Contains("results"));

            if (ResultsCount.Text == zeroResults)
            {
                driver.Close();

                Assert.Inconclusive("no openings at this time");
            }
        }

        public void SearchUtahInfoTech()
        {
            Country.Click();
            wait.Until(d => CountryDropdown.Displayed);
            CountryUSA.Click();

            wait.Until(d => Location.Displayed);

            Location.Click();
            wait.Until(d => LocationDropdown.Displayed);
            LocationInputUtah.SendKeys(Keys.Space);

            JobFamily.Click();
            wait.Until(d => JobFamilyDropdown.Displayed);
            JobFamilyInfoTech.SendKeys(Keys.Space);

            Search.Click();

            wait.Until(d => ResultsCount.Text.Contains("results"));

            if (ResultsCount.Text == zeroResults)
            {
                driver.Close();

                Assert.Inconclusive("no openings at this time");
            }
        }
    }

    public class Intermountain
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Intermountain(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => Expertise.Displayed);
        }

        private string noJobs = "There were no jobs matching this query.";

        [FindsBy(How = How.Id, Using = "res_newjoblist__res_candcriteria-a1ExpertisegcC__Field")]
        [CacheLookup]
        private IWebElement Expertise { get; set; }
        public SelectElement ExpertiseSelect
        {
            get { return new SelectElement(Expertise); }
        }

        [FindsBy(How = How.CssSelector, Using = ".cc-content .clearfix .primary")]
        [CacheLookup]
        private IWebElement Search { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".cc-content .search-summary")]
        [CacheLookup]
        private IWebElement SearchSummary { get; set; }

        public void SearchInfoTech()
        {
            ExpertiseSelect.SelectByValue("10000007"); // Information Technology

            Search.Click();
            wait.Until(d => SearchSummary.Displayed);
            
            // determine if results were shown
            if (SearchSummary.Text == noJobs)
            {
                Assert.Inconclusive("no openings at this time");
                driver.Close();
            }            
        }
    }

    public class Simplot
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Simplot(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // Switch to the iframe
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
            wait.Until(d => d.FindElement(By.Id(frame)).Displayed);
            driver.SwitchTo().Frame(frame);

            // wait for the page to load
            wait.Until(d => FirstRow.Displayed);
        }

        private string frame = "parentIframe";
        private string infoTech = "Information Technology";
        private string noRecords = "No Records Found";

        [FindsBy(How = How.CssSelector, Using = ".grid-canvas .slick-row:first-of-type")]
        [CacheLookup]
        private IWebElement FirstRow { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#uxcategoryContainer input")]
        //[CacheLookup]
        private IWebElement Category { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#uxcategoryContainer button")]
        [CacheLookup]
        private IWebElement CategoryButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#ui-id-4 ul")]
        [CacheLookup]
        private IWebElement Categories { get; set; }
        
        protected virtual By CategoriesListSelector
        {
            get { return By.CssSelector("#ui-id-4 ul li"); }
        }

        [FindsBy(How = How.ClassName, Using = "slick-pager-status")]
        [CacheLookup]
        private IWebElement Status { get; set; }

        [FindsBy(How = How.Id, Using = "searchBtn")]
        [CacheLookup]
        private IWebElement Search { get; set; }

        [FindsBy(How = How.Id, Using = "SearchResult")]
        [CacheLookup]
        private IWebElement SearchResult { get; set; }

        public void SearchInfoTech()
        {
            // wait for results to show
            wait.Until(d => FirstRow.Displayed);

            // filter results
            CategoryButton.Click();
            wait.Until(d => Categories.Displayed);

            // highlight info tech
            IList<IWebElement> categories = driver.FindElements(CategoriesListSelector);
            categories.First(e => e.Text.Contains(infoTech)).Click();

            Search.Click();

            // wait for page to load results
            wait.Until(d => SearchResult.Displayed);

            // determine if results were shown
            if (Status.Text == noRecords)
            {
                Assert.Inconclusive("no openings at this time");
                driver.Close();
            }
        }
    }

    public class LabCorp
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LabCorp(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => SearchResults.Displayed);
        }

        [FindsBy(How = How.Id, Using = "search-results")]
        [CacheLookup]
        private IWebElement SearchResults { get; set; }

        [FindsBy(How = How.Id, Using = "region-toggle")]
        [CacheLookup]
        private IWebElement StateExpander { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#search-filters section:nth-of-type(2) ul")]
        [CacheLookup]
        private IWebElement StatesList { get; set; }

        protected virtual By StatesListSelector
        {
            get { return By.CssSelector("#search-filters section:nth-of-type(2) ul li"); }
        }

        [FindsBy(How = How.Id, Using = "applied-filters-label")]
        [CacheLookup]
        private IWebElement FilterLabel { get; set; }

        [FindsBy(How = How.Id, Using = "#search-results-list ul")]
        [CacheLookup]
        private IWebElement ResultsList { get; set; }

        public void SearchStates()
        {
            // close the window if UT and ID are not shown in the states list

            StateExpander.Click();
            wait.Until(d => StatesList.Displayed);

            // get list of states
            IList<IWebElement> states = driver.FindElements(StatesListSelector);
            var idaho = states.FirstOrDefault(e => e.Text.Contains("Idaho"));
            var utah = states.FirstOrDefault(e => e.Text.Contains("Utah"));

            if(idaho == null && utah == null)
            {
                // no results exist within Idaho and Utah, close window
                driver.Close();

                Assert.Inconclusive("No jobs exist within Idaho and Utah");
            }

            // narrow list results
            if (idaho != null) idaho.Click();

            if (utah != null) utah.Click();

            wait.Until(d => FilterLabel.Displayed);

            Assert.IsTrue(ResultsList.Displayed, "No results were shown");
        }
    }

    public class Maverik
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Maverik(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => Submit.Displayed) ;
        }

        private string IT = "21";
        private string Tech = "31";

        [FindsBy(How = How.Id, Using = "Submit")]
        [CacheLookup]
        private IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "Req_JobFamilyFK")]
        [CacheLookup]
        private IWebElement JobFamily { get; set; }
        public SelectElement JobFamilySelect
        {
            get { return new SelectElement(JobFamily); }
        }

        [FindsBy(How = How.Id, Using = "lblPageTitle")]
        [CacheLookup]
        private IWebElement BrowseJobsHeader { get; set; }

        [FindsBy(How = How.CssSelector, Using = "table.GridTable")]
        [CacheLookup]
        private IWebElement ResultsTable { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".PrintMedium")]
        [CacheLookup]
        private IWebElement ResultsMessage { get; set; }

        public void SearchTech()
        {
            Search(Tech);
        }

        public void SearchIT()
        {
            Search(IT);
        }

        private void Search(string category)
        {
            JobFamilySelect.SelectByValue(category);

            Submit.Click();

            wait.Until(d => BrowseJobsHeader.Displayed);

            // check for most likely element, which in this case is a no results message
            if(ResultsMessage.Displayed)
            {
                driver.Close();

                Assert.Inconclusive("No results were shown");
            }
        }
    }

    public class ONSemi
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ONSemi(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(d => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));
            wait.Until(d => JobsAvailable.Displayed);
        }

        [FindsBy(How = How.Id, Using = "JOB_LOCALE-select")]
        [CacheLookup]
        private IWebElement JobsAvailable { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".no-results-info")]
        [CacheLookup]
        private IWebElement NoResults { get; set; }

        [FindsBy(How = How.Id, Using = "LOCATION")]
        [CacheLookup]
        private IWebElement Location { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul[style*=\"block\"]")]
        [CacheLookup]
        private IWebElement LocationSuggestions { get; set; } 

        [FindsBy(How = How.CssSelector, Using = "ul[style*=\"block\"] li:first-of-type")]
        [CacheLookup]
        private IWebElement LocationSuggestionsFirstRow { get; set; } // First search suggestion in Location field 

        public void SearchState(string state)
        {
            Assert.IsTrue(state.Length > 0, "Please specify a state");

            Location.SendKeys(state);
            wait.Until(d => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));
            wait.Until(d => LocationSuggestions.Displayed);
            wait.Until(d => LocationSuggestionsFirstRow.Displayed);

            LocationSuggestionsFirstRow.Click();
            wait.Until(d => (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0"));

            if(NoResults.Displayed)
            {
                driver.Close();

                Assert.Inconclusive("no openings at this time");
            }
        }
    }
    /*
       public class <site>
       {
           private IWebDriver driver;
           private WebDriverWait wait;

           public <site>(IWebDriver driver)
           {
               this.driver = driver;

               PageFactory.InitElements(driver, this);

               // wait for the page to load
               wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
               wait.Until(d => <element_to_watch_for>.Displayed);
           }

           [FindsBy(How = How, Using = "")]
           [CacheLookup]
           private IWebElement <element> { get; set; }

           [FindsBy(How = How, Using = "")]
           [CacheLookup]
           private IWebElement <element> { get; set; }
           public SelectElement <element>Select
           {
               get { return new SelectElement(<element>); }
           }
       }
   */
}