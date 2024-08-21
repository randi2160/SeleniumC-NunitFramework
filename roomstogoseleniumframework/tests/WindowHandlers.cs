using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using roomstogoseleniumframework.Utilities;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class WindowHandlers:Base
    {
       
        
        public void WindowHandle()

        {
            String email = "mentor@rahulshettyacademy.com";
            String parentWindowId = driver.Value.CurrentWindowHandle;
            driver.Value.FindElement(By.ClassName("blinkingText")).Click();

            Assert.AreEqual(2, driver.Value.WindowHandles.Count);//1

            String childWindowName = driver.Value.WindowHandles[1];

            driver.Value.SwitchTo().Window(childWindowName);

            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector(".red")).Text);
            String text = driver.Value.FindElement(By.CssSelector(".red")).Text;

            // Please email us at mentor @rahulshettyacademy.com with below template to receive response

            String[] splittedText = text.Split("at");

            String[] trimmedString = splittedText[1].Trim().Split(" ");

            Assert.AreEqual(email, trimmedString[0]);

            driver.Value.SwitchTo().Window(parentWindowId);

            driver.Value.FindElement(By.Id("username")).SendKeys(trimmedString[0]);

        }


    }
}
