using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace roomstogoseleniumframework.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        // Define elements using PageFactory
        [FindsBy(How = How.CssSelector, Using = "a[title='Living Rooms']")]
        private IWebElement livingRoomsNavLink;

        [FindsBy(How = How.CssSelector, Using = "a[title='Dining Rooms']")]
        private IWebElement diningRoomsNavLink;

        [FindsBy(How = How.CssSelector, Using = "a[title='Sales']")]
        private IWebElement salesNavLink;

        [FindsBy(How = How.CssSelector, Using = "img[alt='Shop Mattress Sale']")]
        private IWebElement shopMattressSaleImage;

        [FindsBy(How = How.CssSelector, Using = ".swiper-wrapper")]
        private IWebElement swiperWrapper;

        public void ClickLivingRooms()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(livingRoomsNavLink));
            livingRoomsNavLink.Click();
        }

        public void ClickDiningRooms()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(diningRoomsNavLink));
            diningRoomsNavLink.Click();
        }

        public void ClickSales()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(salesNavLink));
            salesNavLink.Click();
        }

        public void ScrollToShopMattressSale()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", shopMattressSaleImage);
        }

        public void ClickShopKidsSale()
        {
            ScrollToShopMattressSale();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".swiper-wrapper")));
            var swiperItems = swiperWrapper.FindElements(By.CssSelector(".swiper-slide"));

            foreach (var item in swiperItems)
            {
                var titleElement = item.FindElement(By.CssSelector(".css-7g1can"));
                string titleText = titleElement.Text;

                if (titleText == "Shop Kids Sale")
                {
                    // Verify the name matches "Shop Kids Sale"
                    if (titleText == "Shop Kids Sale")
                    {
                        // Scroll to the element (if necessary)
                        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                        js.ExecuteScript("arguments[0].scrollIntoView(true);", item);

                        // Click on the element
                        item.Click();
                        break;
                    }
                }
            }
        }
    }
}
