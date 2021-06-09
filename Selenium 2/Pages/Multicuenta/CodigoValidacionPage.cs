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
    class CodigoValidacionPage 
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public CodigoValidacionPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        #region Componentes
        [FindsBy(How = How.Id, Using = "ModalOTP")]
        private IWebElement otp;

        [FindsBy(How = How.Id, Using = "codigoVeficacionOTP")]
        private IWebElement codigo;

        [FindsBy(How = How.Id, Using = "continuarOTP")]
        private IWebElement btnVerificarOTP;

        [FindsBy(How = How.Id, Using = "resendOTP")]
        private IWebElement btnReenviarOTP;

        [FindsBy(How = How.Id, Using = "modalCancelButton")]
        private IWebElement btnCancelar;

        #endregion

        public Boolean ExisteModalIngresarCodigo()
        {
            return otp.Displayed;
        }
        // _logger.Info("Seleccionar opción cerrar sesión");
        public Boolean ExisteBtnEnviarCodigoAlEmail()
        {
            //El boton btnReenviarOTP se muestra después de N segundos. Este valor se configura en la variable Portal.Multiaccount.OTP.Wait.Time
            utiles.WaitForVisible(By.Id("resendOTP"));
            return btnReenviarOTP.Displayed;
        }

        public void PresionarOpcionEnviarCodigoAlEmail()
        {
            btnReenviarOTP.Click();
        }

        public void IngresarOTP()
        {
            codigo.SendKeys(BuscarCodigo());
	    }

        public void ConfirmarOperacion()
        {
            btnVerificarOTP.Click();
        }

        public String BuscarCodigo()
        {
            GmailPage gmail = new GmailPage(driver);
            gmail.LoginGmail("testingbitsion@gmail.com", "!QAZxsw2");
            String otp = gmail.ObtenerOTPMail();
            return otp;
	    }

    }
}
