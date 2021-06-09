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
    class EnrolamientoPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public EnrolamientoPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);

        }

        #region Componentes

        [FindsBy(How = How.XPath, Using = "//ion-input[@id='register-phone-number']/input[1]")]
        private IWebElement telUsuario;

        [FindsBy(How = How.XPath, Using = "//ion-input[@id='register-code-agent']/input[1]")]
        private IWebElement codAgente;

        [FindsBy(How = How.XPath, Using = "//ion-input[@id='register-name']/input[1]")]
        private IWebElement nombreUsr;

        [FindsBy(How = How.XPath, Using = "//ion-input[@id='register-lastname']/input[1]")]
        private IWebElement apellidoUsr;

        [FindsBy(How = How.XPath, Using = "//ion-input[@id='register-document-number']/input[1]")]
        private IWebElement documentUsr;

        [FindsBy(How = How.Id, Using = "register-btn-continue")]
        private IWebElement btnRegistrarUsr;

        [FindsBy(How = How.XPath, Using = "//ion-item[@id='procedures-btn-prospecto']")]
        private IWebElement btnProspecto;

        #endregion

        internal void Enrolamiento()
        {
            //obliga al usuario a enrolarse haciendo click en prospecto
            utiles.WaitForClickeable(btnProspecto);
            btnProspecto.Click();
        }

        internal void FormularioIngreso()
        {
            //Completa el formulario de ingreso
            utiles.WaitForClickeable(nombreUsr);
            nombreUsr.SendKeys("Selenium");
            apellidoUsr.SendKeys("Automatica");
            documentUsr.SendKeys("000001");
            utiles.FocusElement(btnRegistrarUsr);
            utiles.WaitForClickeable(btnRegistrarUsr);
            btnRegistrarUsr.Click();
        }
    }
}
