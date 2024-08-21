using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using System;

namespace roomstogoseleniumframework.PageObjects
{
    public class SearchPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SearchPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement SearchBox => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-testid='search-desktop-1'] input")));
        private IWebElement SearchButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-testid='searchButton-desktop-1']")));
        private IList<IWebElement> ResultTitles => wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("h2.MuiTypography-root.MuiTypography-body1.css-p834i9")));

        public void SearchForTerm(string searchTerm)
        {
            CloseAnyPopupOrDialog();
            ActivateSearchComponent();

            SearchBox.Clear();
            SearchBox.SendKeys(searchTerm);

            ScrollToElement(SearchButton); // Scroll to the search button before clicking
            ClickElementWithRetry(SearchButton); // Retry mechanism for clicking the search button
        }

        public void CloseAnyPopupOrDialog()
        {
            try
            {
                IWebElement closeModalButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(".close-modal-selector")));
                closeModalButton?.Click();
            }
            catch (WebDriverTimeoutException) { }
            catch (NoSuchElementException) { }

            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException) { }
        }

        public void ActivateSearchComponent()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", SearchBox);
                ClickElementWithRetry(SearchBox);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error activating search component: " + ex.Message);
            }
        }

        private void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        private void ClickElementWithRetry(IWebElement element, int retries = 3)
        {
            for (int attempt = 0; attempt < retries; attempt++)
            {
                try
                {
                    element.Click();
                    return;
                }
                catch (ElementClickInterceptedException)
                {
                    if (attempt == retries - 1) throw;
                    Console.WriteLine("Retrying click due to interception...");
                    Thread.Sleep(500); // Adding a small delay before retrying
                }
            }
        }

        public IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData("Kids bedroom");
            yield return new TestCaseData("Kids tables");
            yield return new TestCaseData("Kids sofa");
        }

        public void VerifySearchResultsContainAnyWord(string searchTerm)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h2.MuiTypography-root.MuiTypography-body1.css-p834i9")));

            string[] searchWords = searchTerm.Split(' ');

            foreach (var title in ResultTitles)
            {
                string titleText = title.GetAttribute("title");
                bool wordFound = false;

                foreach (string word in searchWords)
                {
                    if (titleText.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        wordFound = true;
                        break;
                    }
                }

                Assert.IsTrue(wordFound, $"The title '{titleText}' does not contain any of the expected words from the search term '{searchTerm}'.");
            }
        }
    }
}
