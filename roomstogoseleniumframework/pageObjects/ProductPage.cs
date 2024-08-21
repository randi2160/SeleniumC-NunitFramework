using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace roomstogoseleniumframework.PageObjects
{
    public class ProductPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        // Define elements
        [FindsBy(How = How.CssSelector, Using = "h1[title='Kids Labor Day Sale']")]
        private IWebElement kidsLaborDaySaleHeader;

        [FindsBy(How = How.CssSelector, Using = "a[title='Sales']")]
        private IWebElement salesTab;

        [FindsBy(How = How.CssSelector, Using = "h2[title='Kids Modern Colors Slate Blue 5 Pc Full Panel Bedroom']")]
        private IWebElement kidsModernColorsSlateBlueBedroom;

        [FindsBy(How = How.CssSelector, Using = "button[aria-label='Add To Cart for Kids Modern Colors Slate Blue 5 Pc Full Panel Bedroom']")]
        private IWebElement addToCartButton;

        [FindsBy(How = How.CssSelector, Using = "svg[data-testid='CloseOutlinedIcon']")]
        private IWebElement closeCartButton;

        public void ValidateKidsLaborDaySaleHeader()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1[title='Kids Labor Day Sale']")));
            Assert.IsNotNull(kidsLaborDaySaleHeader, "The 'Kids Labor Day Sale' header does not exist.");
            Assert.AreEqual("Kids Labor Day Sale", kidsLaborDaySaleHeader.Text, "The header text does not match 'Kids Labor Day Sale'.");
        }

        public void ValidateSalesTabIsActive()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("a[title='Sales']")));
            bool isActive = salesTab.GetAttribute("class").Contains("active");
            Assert.IsTrue(isActive, "The 'Sales' tab is not active.");
        }

        public void ScrollToKidsModernColorsSlateBlueBedroom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", kidsModernColorsSlateBlueBedroom);
        }

        public void AddToCart()
        {
            addToCartButton.Click();
        }

        public void CloseCart()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(closeCartButton)).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("svg[data-testid='CloseOutlinedIcon']")));
        }
    }
}
