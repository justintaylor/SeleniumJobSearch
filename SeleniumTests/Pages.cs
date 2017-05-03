using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
        private IWebElement element { get; set; }

        public AdvantageCreditUnion(IWebDriver driver)
        {
            this.driver = driver;

            PageFactory.InitElements(driver, this);

            // wait for the page to load
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => element.Displayed);
        }

        public void CheckForJobs()
        {
            if (element.Text == noOpenings)
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
        private IWebElement Category  { get; set; }
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
}