using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NLog;

namespace Selenium_2.Pages.Multicuenta
{
    class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        #region Componentes

        [FindsBy(How = How.Id, Using = "logo")]
        private IWebElement logo;

        [FindsBy(How = How.Id, Using = "title")]
        private IWebElement titulo;

        [FindsBy(How = How.Id, Using = "iuserName")]
        private IWebElement txtUsuario;

        [FindsBy(How = How.Id, Using = "ipassword")]
        private IWebElement txtContrasena;

        [FindsBy(How = How.Id, Using = "btn_login")]
        private IWebElement btnIniciarSesion;

        [FindsBy(How = How.Id, Using = "btn_recover_password")]
        private IWebElement btnOlvideContrasena;

        [FindsBy(How = How.Id, Using = "btn_register")]
        private IWebElement btnRegistrate;

        [FindsBy(How = How.Id, Using = "alert_message_log_in")]
        private IWebElement lblMjeLoginFallido;

        [FindsBy(How = How.Id, Using = "alert_message_log_in")]
        private IWebElement spinner;
        #endregion

        public void Go()
        {
            driver.Navigate().GoToUrl("https://dev1.bitsion.com:61054/portal/UserPortal/Webforms/Account/Views/Index.aspx");
            driver.Manage().Window.Maximize();
        }
        public Boolean ExisteUsuario()
        {
            utiles.WaitForClickeable(txtUsuario);
            return txtUsuario.Displayed;
        }
        public void EnviarUsuario(String VarUsuario)
        {
            txtUsuario.SendKeys(VarUsuario);
        }
        public Boolean ExisteContrasena()
        {
            return txtContrasena.Displayed;
        }
        public void enviarContrasena(String VarContrasena)
        {
            _logger.Info("Ingresar contraseña");
            txtContrasena.SendKeys(VarContrasena);
        }
        public Boolean ExisteIniciarSesion()
        {
            return btnIniciarSesion.Displayed;
        }
        public void ReintentarIniciarSesion()
        {
            MenuPage menu = new MenuPage(driver);
            _logger.Info("Presionar iniciar sesión");
            do
            {
                btnIniciarSesion.Click();
            }
            while (menu.ExisteOpcionActividad()==false);
        }
        public void clickIniciarSesión()
        {
            _logger.Info("Presionar iniciar sesión");
            btnIniciarSesion.Click();
        }
        public void ExisteRecuperarContrasena()
        {
            Assert.IsTrue(btnOlvideContrasena.Displayed);
        }
        public void RecuperarContrasena()
        {
            btnOlvideContrasena.Click();
        }
        public void ExisteRegistrate()
        {
            Assert.IsTrue(btnRegistrate.Displayed);
        }
        public void Registrate()
        {
            btnRegistrate.Click();
        }
        public String MensajeLoginFallido()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("alert_message_log_in")));
            return lblMjeLoginFallido.Text;
        }

        public void IniciarSesion(String usu, String con)
        {
            #region Codigo en Java
            /*
            Reporter.log("Ir a la pagina de login");
            go();
            Reporter.log("Ingresar usuario valido");
            enviarUsuario(usu);
            Reporter.log("Ingresar contrasena valida");
            enviarContrasena(con);
            Reporter.log("Hacer click en iniciar sesion");
            presionarIniciarSesion();
            */
            #endregion
            Go();
            Assert.IsTrue(ExisteUsuario(), "No existe el campo usuario");
            _logger.Info("Ingresar usuario");
            EnviarUsuario(usu);
            Assert.IsTrue(ExisteContrasena(), "No existe el campo contraseña");
            _logger.Info("Ingresar contraseña");
            enviarContrasena(con);
            Assert.IsTrue(ExisteIniciarSesion(), "No existe el boton iniciar sesion");
            _logger.Info("Presionar iniciar sesión");
            MenuPage menu = new MenuPage(driver);
            ReintentarIniciarSesion();

        }

        public Boolean ValidarSpinner()
        {
          
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id("alert_message_log_in")));
        }
    }
}

