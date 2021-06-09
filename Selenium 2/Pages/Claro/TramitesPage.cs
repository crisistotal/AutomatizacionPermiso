using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium_2
{
    class TramitesPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public TramitesPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);

        }

        [FindsBy(How = How.XPath, Using = "//ion-toast[@id='undefined']")]
        private IWebElement toast;

        [FindsBy(How = How.XPath, Using = "//ion-item[@id='procedures-btn-prospecto']")]
        private IWebElement btnProspecto;

        [FindsBy(How = How.Id, Using = "prospectos-btn-new")]
        private IWebElement btnNuevoProspecto;

        private bool ElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        internal void ControlTramites()
        {
            //Faltaria agregar el wait de cargando tramites
            //Agarra el toast cuando no esta trayendo los tramites en la grilla
            Thread.Sleep(4000);
            if (ElementPresent(By.XPath("//ion-toast[@id='undefined']")))
            {
                throw new NoSuchElementException("No trae tramites");
            }
        }
        internal void ValidateLogInTramites()
        {
            utiles.WaitForClickeable(toast);
            Assert.IsTrue(toast.Text.Contains("El usuario ingresó con éxito."));
            utiles.WaitForClickeableJavaScript();
        }
    }
}
