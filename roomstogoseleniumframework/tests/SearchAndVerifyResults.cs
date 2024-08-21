using NUnit.Framework;
using roomstogoseleniumframework.PageObjects;
using roomstogoseleniumframework.Utilities;
using System.Collections.Generic;

namespace roomstogoseleniumframework.tests
{
    public class SearchAndVerifyResults : Base
    {
        [Test, TestCaseSource(nameof(AddTestDataConfig))]
        [Parallelizable(ParallelScope.All)]
        public void SearchForKidsBedroomAndVerifyResults(string searchTerm)
        {
            // Close any popups or dialogs if present
            CloseAnyPopupOrDialog();

            // Initialize the SearchPage
            SearchPage searchPage = new SearchPage(driver.Value);

            // Perform the search
            searchPage.SearchForTerm(searchTerm);

            // Verify the results
            searchPage.VerifySearchResultsContainAnyWord(searchTerm);
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            var searchTerms = getDataParser().ExtractSearchTerms();

            foreach (var searchTerm in searchTerms)
            {
                yield return new TestCaseData(searchTerm);
            }
        }
    }
}
