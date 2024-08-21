using OpenQA.Selenium;
using roomstogoseleniumframework.PageObjects;
using roomstogoseleniumframework.Utilities;
using NUnit.Framework;
using System;

namespace roomstogoseleniumframework.tests
{
   
    public class NavToSaleAndAddToCartWorkFlow : Base
    {
        [Test,Category("Regression")]
        
        public void TestGetSwiperItems()
        {
            HomePage homePage = new HomePage(driver.Value);
            ProductPage productPage = new ProductPage(driver.Value);
            CloseAnyPopupOrDialog();

            // Scroll to the 'Shop Mattress Sale' section
            homePage.ScrollToShopMattressSale();

            // Click on 'Shop Kids Sale' in the swiper items
            homePage.ClickShopKidsSale();

            // Validate the page and product details
            productPage.ValidateKidsLaborDaySaleHeader();
            productPage.ValidateSalesTabIsActive();

            // Scroll to a specific product and add it to the cart
            productPage.ScrollToKidsModernColorsSlateBlueBedroom();
            productPage.AddToCart();
            productPage.CloseCart();
        }

        [Test, Category("Smoke")]
        public void EndToEndFlow()
        {
            HomePage homePage = new HomePage(driver.Value);
            CloseAnyPopupOrDialog();

            // Click through Living Rooms, Dining Rooms, and Sales
            homePage.ClickLivingRooms();
            homePage.ClickDiningRooms();
            CloseAnyPopupOrDialog();
            homePage.ClickSales();

            // Add login steps here if required using a LoginPage object.
        }
    }
}
