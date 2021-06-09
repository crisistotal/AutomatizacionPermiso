using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Remote;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using CreatingReports;
using AventStack.ExtentReports;
using NLog;

namespace Selenium_2.Pages.Claro
{
    class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        #region Componentes
        [FindsBy(How = How.XPath, Using =
            "//ion-input[@id='login-phone-number']/input[1]")]
        private IWebElement txtNumTel;

        [FindsBy(How = How.XPath, Using =
            "//ion-input[@id='login-pos-number']/input[1]")]
        private IWebElement txtNumPos;

        [FindsBy(How = How.XPath, Using =
            "//ion-input[@id='login-password-pin']/input[1]")]
        private IWebElement txtNumPin;

        [FindsBy(How = How.Id, Using = "login-btn-ingress")]
        private IWebElement btnIniciarSesion;

        [FindsBy(How = How.Id, Using = "login-item-pda")]
        private IWebElement rbtPuntoActivacion;

        [FindsBy(How = How.Id, Using = "login-item-pdv")]
        private IWebElement rbtPuntoVenta;

        [FindsBy(How = How.Id, Using = "login-item-pdg")]
        private IWebElement rbtPuntoGiro;

        [FindsBy(How = How.Id, Using = "login-item-vd")]
        private IWebElement rbtVendedorDirecta;

        [FindsBy(How = How.Id, Using = "login-btn-next-slide")]
        private IWebElement ingresarPy;

        #endregion

        internal void Goto(int i)
        {
            switch (i)
            {
                case 1:
                    driver.Navigate().GoToUrl
                ("https://test-doc.claro.com.ar/psr/login");
                    Reporter.LogPassingTestStepToBugLogger("");
                    break;
                case 2:
                    driver.Navigate().GoToUrl
                ("https://test-doc.claro.com.py/psr/login");
                    Reporter.LogPassingTestStepToBugLogger("Ingresa a Argentina");
                    break;
                case 3:
                    driver.Navigate().GoToUrl
                ("https://test-doc.claro.com.uy/psr/login");
                    Reporter.LogPassingTestStepToBugLogger("Ingresa a Uruguay");
                    break;
                default:
                    break;
            }
        }
        internal void LoginNuevoUsuarioArUy(String initial, String numPos, String numPin)
        {
            try
            {
                Reporter.LogTestStepForBugLogger(Status.Info, "Ingresa credenciales");
                utiles.WaitForClickeable(txtNumTel);
                txtNumTel.SendKeys(CreateRandomInteger(initial, 8));
                txtNumPos.SendKeys(numPos);
                txtNumPin.SendKeys(numPin);
                utiles.FocusElement(btnIniciarSesion);
                btnIniciarSesion.Click();
            }
            catch (Exception e)
            {
                Reporter.LogTestStepForBugLogger(Status.Fail, e.ToString());
                throw;
            }
           
        }

        internal void LoginNuevoUsuarioPy(String initial, String numPos, String numPin, int tipoKiosko)
        {
            switch (tipoKiosko)
            {
                case 1:
                    utiles.WaitForClickeable(rbtPuntoActivacion);
                    rbtPuntoActivacion.Click();
                    ingresarPy.Click();
                    break;
                case 2:
                    utiles.WaitForClickeable(rbtPuntoVenta);
                    rbtPuntoVenta.Click();
                    ingresarPy.Click();
                    break;
                case 3:
                    utiles.WaitForClickeable(rbtPuntoGiro);
                    rbtPuntoGiro.Click();
                    ingresarPy.Click();
                    break;
                case 4:
                    utiles.WaitForClickeable(rbtVendedorDirecta);
                    rbtVendedorDirecta.Click();
                    ingresarPy.Click();
                    break;
                default:
                    break;
            }
            utiles.WaitForClickeable(txtNumTel);
            txtNumTel.SendKeys(CreateRandomInteger(initial, 9));
            txtNumPos.SendKeys(numPos);
            txtNumPin.SendKeys(numPin);
            utiles.FocusElement(btnIniciarSesion);
            btnIniciarSesion.Click();
        }

        internal String CreateRandomInteger(String inicial, int size)
        {
            Random random = new Random();
            string r = "";
            for (int i = 0; i < size; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            r = inicial + r;
            return r;
        }
    }
}