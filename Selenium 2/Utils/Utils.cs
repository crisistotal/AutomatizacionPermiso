using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Threading;
using NPOI.SS.Util;
using Microsoft.OData.Edm;

namespace Selenium_2
{
    class Utils
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;

        public Utils(IWebDriver driver)
        {
            this.driver = driver;
            acciones = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        #region Componentes
        #endregion


        internal void FocusElement(IWebElement button)
        {
            acciones.MoveToElement(button);
            acciones.Perform();
        }

        internal void WaitForClickeable(IWebElement button)
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.
                ElementToBeClickable(button));
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException("El elemento no se encuentra clickeable");
            }
        }

        internal void WaitForClickeableJavaScript()
        {
            //Todo: Mejorar a futuro
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.
                ElementToBeClickable((IWebElement)
                ((IJavaScriptExecutor)driver).ExecuteScript(
                "return document.querySelector('ion-toast.md.hydrated').shadowRoot.querySelector" +
                "('div.toast-wrapper.toast-bottom').querySelector('div.toast-container').querySelector" +
                "('div.toast-button-group.toast-button-group-end').querySelector('button.toast-button.ion-focusable.ion-activatable')"))).Click();
        }

        internal void WaitForVisible(By by)
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.
                    ElementIsVisible(by));
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException("El elemento no se encuentra visible");
            }
        }
        public void NewTab(String url2)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)  driver;
            js.ExecuteScript("window.open()");
            ArrayList tabs = new ArrayList(driver.WindowHandles);
            driver.SwitchTo().Window((string)tabs[1]);
            driver.Navigate().GoToUrl(url2);

        }

        public void OldTab()
        {
            ArrayList tabs = new ArrayList(driver.WindowHandles);
            driver.SwitchTo().Window((string)tabs[0]);
        }

        public void SeleccionarOpcionDeLista(IList<IWebElement> combo, String opcion)
        {

            for (int i = 0; i < combo.Count; i++)
            {
                if (combo.ElementAt(i).Text.Contains(opcion))
                {
                    combo.ElementAt(i).Click();
                    break;
                }
            }
        }

        public String GenerateStringFechaHora(int tamMaximo)
        {
            String r = "Test Automatizado " + getFechaYHoraActual() + " ";
            int tamano = tamMaximo - 44;
            if (tamano < 2)
            {
                r = "TA ";
                tamano = tamMaximo - 3;
            }
            else
            {
                r += Guid.NewGuid().ToString("n").Substring(0, tamano-1);
            }    
            return r;
        }
        public String getFechaYHoraActual()
        {            
            String today = DateTime.Now.ToString("G");            
            return today;
        }
    }

  
}
