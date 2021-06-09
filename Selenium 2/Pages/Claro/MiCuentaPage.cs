using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_2.Pages.Claro
{
    class MiCuentaPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public MiCuentaPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        [FindsBy(How = How.Id, Using = "tab-button-account")]
        private IWebElement btnMiCuenta;

        [FindsBy(How = How.XPath, Using = "//ion-button[contains(text(),'Cerrar sesión')]")]
        private IWebElement btnCerrarSesion;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Aceptar')]")]
        private IWebElement btnAceptarCerrarSesion;

        [FindsBy(How = How.XPath, Using = "//p[1]")]
        private IWebElement labelUsrMiCuenta;

        internal void LogOut()
        {
            utiles.WaitForClickeable(btnMiCuenta);
            btnMiCuenta.Click();
            utiles.WaitForClickeable(btnCerrarSesion);
            btnCerrarSesion.Click();
            utiles.WaitForClickeable(btnAceptarCerrarSesion);
            btnAceptarCerrarSesion.Click();
        }

        internal void ValidarUsrEnrolado()
        {
            utiles.WaitForClickeable(btnMiCuenta);
            btnMiCuenta.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//p[1]")));
            if(labelUsrMiCuenta.Text.Contains("Selenium Automatica"))
            {
                Console.WriteLine("El usuario esta correctamente loggeado");
            }
            else
            {
                throw new NoSuchElementException("No se registro correctamente el nombre de kiosko");
            }
        }
    }
}
