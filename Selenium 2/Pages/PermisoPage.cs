using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_2.Pages
{
    class PermisoPage
    {

        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public PermisoPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }



        [FindsBy(How = How.XPath, Using =
           "//div[@id='block-system-main']/section/article/div/div[5]/div/div/div/a/div")]
        private IWebElement btnTramite;

        [FindsBy(How= How.XPath,Using = "")]
        public void Goto()
        {
            driver.Navigate().GoToUrl("https://www.argentina.gob.ar/circular");

        }

        public void clickTramite()
        {
            btnTramite.Click();
        }

    }
}
