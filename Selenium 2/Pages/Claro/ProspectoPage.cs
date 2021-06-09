using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Selenium_2.Pages.Claro
{
    class ProspectoPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public ProspectoPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        internal void LLenarFormulario()
        {

        }
    }
   

}