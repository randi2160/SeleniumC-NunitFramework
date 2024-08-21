using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using roomstogoseleniumframework.Utilities;
using roomstogoseleniumframework.pageObjects;

namespace roomstogoseleniumframework.tests
{
    public class Tests : Base
    {
        [Ignore("This test is currently disabled because of a known issue.")]
        [Test]
        public void TestGetSwiperItems()
        {
            CloseAnyPopupOrDialog();

            // Scroll to the image element and ensure it's in view
            IWebElement imageElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("img[alt='Shop Mattress Sale']")));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", imageElement);

            // Wait until the swiper wrapper is visible
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".swiper-wrapper")));

            // Get the list of all swiper slides
            IList<IWebElement> swiperItems = driver.Value.FindElements(By.CssSelector(".swiper-slide"));

            foreach (var item in swiperItems)
            {
                // Find the title of the item
                IWebElement titleElement = item.FindElement(By.CssSelector(".css-7g1can"));
                string titleText = titleElement.Text;

                if (titleText == "Shop Kids Sale")
                {
                    // Verify the name matches "Shop Kids Sale"
                    Assert.AreEqual("Shop Kids Sale", titleText, "The title does not match 'Shop Kids Sale'.");

                    // Scroll to the element (if necessary)
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", item);

                    // Click on the element
                    item.Click();

                    // Wait for the next page to load
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("/sales/labor-day/kids"));

                    // Optionally, verify that the correct page loaded
                    Assert.IsTrue(driver.Value.Url.Contains("/sales/labor-day/kids"), "The URL does not contain the expected path after clicking 'Shop Kids Sale'.");

                    // Validate that the header exists and its text is correct
                    IWebElement headerElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h1[title='Kids Labor Day Sale']")));
                    Assert.IsNotNull(headerElement, "The 'Kids Labor Day Sale' header does not exist.");
                    Assert.AreEqual("Kids Labor Day Sale", headerElement.Text, "The header text does not match 'Kids Labor Day Sale'.");

                    // Validate that the Sales tab is active by checking the presence of the 'active' class
                    IWebElement salesTab = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("a[title='Sales']")));
                    bool isActive = salesTab.GetAttribute("class").Contains("active");
                    Assert.IsTrue(isActive, "The 'Sales' tab is not active.");

                    // Break the loop as the item has been found and clicked
                    break;
                }
            }

            // Locate the item and then click on add to cart button
            IWebElement itemElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("h2[title='Kids Modern Colors Slate Blue 5 Pc Full Panel Bedroom']")));

            // Scroll the page until the item element is in view
            js.ExecuteScript("arguments[0].scrollIntoView(true);", itemElement);

            // Ensure the page is fully scrolled and the element is in view
            System.Threading.Thread.Sleep(1000); // Optional: Wait for smooth scrolling

            // Locate the "Add to Cart" button using its aria-label attribute
            IWebElement addToCartButton = driver.Value.FindElement(By.CssSelector("button[aria-label='Add To Cart for Kids Modern Colors Slate Blue 5 Pc Full Panel Bedroom']"));

            // Click the "Add to Cart" button
            addToCartButton.Click();

            // Optionally, you can wait for the cart update or the next page load
            // Wait until the SVG close button is visible and clickable
            IWebElement closeButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("svg[data-testid='CloseOutlinedIcon']")));

            // Click the close button
            closeButton.Click();

            // Optionally, wait to ensure the cart is closed
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("svg[data-testid='CloseOutlinedIcon']")));
        }

        [Test]
        [Ignore("This test is currently disabled because of a known issue.")]
        public void EndToEndFlow()
        {
            CloseAnyPopupOrDialog();

            // Click on the 'Living Rooms' navigation link
            IWebElement livingRoomsNavLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[title='Living Rooms']")));
            livingRoomsNavLink.Click();

            // Click on the 'Dining Rooms' navigation link
            IWebElement diningRoomsNavLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[title='Dining Rooms']")));
            diningRoomsNavLink.Click();

            // Handling the popup or dialog if it appears
            CloseAnyPopupOrDialog();

            // Click on the 'Sales' navigation link
            IWebElement salesNavLink = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("a[title='Sales']")));
            salesNavLink.Click();

            // Example login steps
            IWebElement loginMenuButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Login or Create Account')]")));
            loginMenuButton.Click();

            IWebElement emailField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("email")));
            emailField.SendKeys("your_email@example.com");

            IWebElement passwordField = driver.Value.FindElement(By.Id("password"));
            passwordField.SendKeys("your_password");

            IWebElement loginSubmitButton = driver.Value.FindElement(By.XPath("//button[contains(text(),'LOG IN')]"));
            loginSubmitButton.Click();
        }

        private void CloseAnyPopupOrDialog1()
        {
            try
            {
                IWebElement closeModalButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".close-modal-selector")));
                closeModalButton.Click();
            }
            catch (WebDriverTimeoutException) { }
            catch (NoSuchElementException) { }

            try
            {
                IAlert alert = driver.Value.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException) { }

            try
            {
                IWebElement cookieConsentCloseButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".onetrust-close-btn-handler")));
                try
                {
                    cookieConsentCloseButton.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", cookieConsentCloseButton);
                }
            }
            catch (WebDriverTimeoutException) { }
            catch (NoSuchElementException) { }

            try
            {
                IWebElement abtCloseButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".abt-close-icon")));
                try
                {
                    abtCloseButton.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", abtCloseButton);
                }
            }
            catch (WebDriverTimeoutException) { }
            catch (NoSuchElementException) { }

            try
            {
                if (driver.Value.FindElements(By.CssSelector(".banner-close-button")).Count > 0)
                {
                    IWebElement bannerCloseButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".banner-close-button")));
                    try
                    {
                        bannerCloseButton.Click();
                    }
                    catch (ElementClickInterceptedException)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", bannerCloseButton);
                    }
                }
            }
            catch (WebDriverTimeoutException) { }
            catch (NoSuchElementException) { }
        }
    }
}