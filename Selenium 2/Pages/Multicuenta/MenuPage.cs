using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Selenium_2.Pages.Multicuenta
{
    class MenuPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public MenuPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        #region Componentes
        [FindsBy(How = How.Id, Using = "actividad")]
        private IWebElement LinkActividad;

        [FindsBy(How = How.Id, Using = "transferir_dinero")]
        private IWebElement linkTransferirDinero;

        [FindsBy(How = How.Id, Using = "administrar_cuentas_externas")]
        private IWebElement linkAdmCuentasExternas;

        [FindsBy(How = How.Id, Using = "cmb_my_account")]
        private IWebElement btnNombreUsuario;

        [FindsBy(How = How.Id, Using = "label_cmb_username")]
        private IWebElement lblNombreUsuario;

        [FindsBy(How = How.Id, Using = "btn_change_password")]
        private IWebElement btnCambiarContrasena;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"bs-example-navbar-collapse-1\"]//li[2]/a")]
        private IWebElement btnCerrarSesion;
        #endregion

        public Boolean existeComboMiCuenta()
        {
            return btnNombreUsuario.Displayed;
        }

        public void CerrarSesion()
        {
            _logger.Info("Seleccionar opción cerrar sesión");
            utiles.WaitForClickeable(lblNombreUsuario);
            lblNombreUsuario.Click();
            utiles.WaitForClickeable(btnCerrarSesion);
            btnCerrarSesion.Click();
        }

        public void IngresarActividad()
        {
            LinkActividad.Click();
        }
        public void IngresarAdmCtasExternas()
        {
            linkAdmCuentasExternas.Click();
        }
        public Boolean ExisteOpcionTransferirDinero()
        {
            utiles.WaitForClickeable(linkTransferirDinero);
            return linkTransferirDinero.Displayed;
        }

        public Boolean ExisteOpcionActividad()
        {            
            utiles.WaitForClickeable(LinkActividad);
            return LinkActividad.Displayed;
        }

        public void CambiarAIframeFormResponse()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("formResponse")));
            driver.SwitchTo().Frame("formResponse");
        }
        public void CambiarAIframeMenu()
        {
            driver.SwitchTo().DefaultContent();
        }
        public void IngresarATransferirDinero()
        {
            linkTransferirDinero.Click();
        }
        public String ValidarUsuario(String text)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(lblNombreUsuario, text));
            return lblNombreUsuario.Text;
        }



    }
}
