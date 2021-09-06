using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;
using System.Text;

namespace SeleniumCSharpSpecflowProject
{
    class BrowserClass
    {
        static IWebDriver driver;

        public static IWebDriver GetBrowserInstanceCreated(string browser)
        {
            if (browser.ToLower().Equals("chrome"))
            {
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
                return driver;
            }
            else
            {
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                driver = new FirefoxDriver();
                return driver;
            }
        }
    }
}
