using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roomstogoseleniumframework.pageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver) 
        {
            this.driver = driver;
            //Once it get the object and register the driver and all the finds by annotation...This is the page factory
            PageFactory.InitElements(driver, this);

        }
        //PageOject Factory (Encapsulation) 
        // driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username; // you should not store as a public because someone can modify this....we can expose it to the method. 
        public IWebElement getUserName() 
        {
            return username;
        }


    }
}
