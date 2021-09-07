using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Remote;

namespace SeleniumCSharpSpecflowProject
{
    class BrowserClass
    {
        static IWebDriver driver;

        public static IWebDriver GetBrowserInstanceCreated(string browser)
        {
            switch(browser.ToLower().Trim())
            {
                case "chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--start-maximized");
                    chromeOptions.AddArguments("--disable-extensions");
                    chromeOptions.AddArgument("--ignore-certificate-errors");
                    chromeOptions.AddArguments("--disable-popup-blocking");
                    chromeOptions.AddArgument("--incognito");
                    chromeOptions.AddArguments("--enable-automation");
                    driver = new ChromeDriver(chromeOptions);
                    return driver;
                case "firefox":
                case "mozilla firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    FirefoxProfile ffProfile = new FirefoxProfile();
                    firefoxOptions.AddAdditionalCapability(FirefoxDriver.ProfileCapabilityName, ffProfile);
                    driver = new FirefoxDriver(firefoxOptions);
                    return driver;
            }
            return driver;
        }
    }
}
