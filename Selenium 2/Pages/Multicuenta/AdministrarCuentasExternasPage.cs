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
    class AdministrarCuentasExternasPage 
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public AdministrarCuentasExternasPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        #region Componentes
        [FindsBy(How = How.XPath, Using = "//h3")]
        private IWebElement lblTitulo;

        [FindsBy(How = How.XPath, Using = "//p/a")]
        private IWebElement linkDescargarArchivo;

        [FindsBy(How = How.XPath, Using = "//div/span[1]/i")]
        private IWebElement btnAgendarCBUCVU;

        [FindsBy(How = How.XPath, Using = "//label/span[1]/i")]
        private IWebElement btnAgendarCBUCVUMasivamente;

        [FindsBy(How = How.Id, Using = "filterAccountField")]
        private IWebElement txtBuscador;
        
        [FindsBy(How = How.XPath, Using = "//nz-card/div[1]/div/div[1]")]
        private IWebElement lblNombreCtaExt; 

        [FindsBy(How = How.XPath, Using = "//div[1]/nz-card//i")]
        private IWebElement btnEliminar;

        #region ModalAsociarCuentaExterna
        [FindsBy(How = How.Id, Using = "CbuCvuField")]
        private IWebElement txtCuentaAAgendar;

        [FindsBy(How = How.XPath, Using = "//span/div")]
        private IWebElement btnBuscarCuenta;
        
        [FindsBy(How = How.XPath, Using = "//form//nz-card/div[1]")]
        private IWebElement lblNombreCuentaAAsociar;

        [FindsBy(How = How.XPath, Using = "//form//div[2]/span[1]")]
        private IWebElement lblCBUAAsociar;

        [FindsBy(How = How.XPath, Using = "//form//div[2]/span[3]")]
        private IWebElement lblTipoCuentaAAsociar;

        [FindsBy(How = How.Id, Using = "continuar")]
        private IWebElement btnContinuar;

        [FindsBy(How = How.XPath, Using = "//nz-result/div[2]")]
        private IWebElement lblMjeConfirmacion;

        [FindsBy(How = How.Id, Using = "respuesta")]
        private IWebElement lblRespuesta;
        #endregion
        #region ModalEliminarAsociacionCuenta
        [FindsBy(How = How.Id, Using = "ModalConfirmation")]
        private IWebElement modalConfirmacionEliminar;

        [FindsBy(How = How.XPath, Using = "//div[2]/p[1]")]
        private IWebElement mjeEliminacion;

        [FindsBy(How = How.XPath, Using = "//div[2]/p[2]")]
        private IWebElement CBUEliminacion;

        [FindsBy(How = How.XPath, Using = "//div[2]/p[3]")]
        private IWebElement NombreCuentaEliminacion;

        [FindsBy(How = How.Id, Using = "modalConfirmButton")]
        private IWebElement btnConfirmarEliminacion;

        [FindsBy(How = How.Id, Using = "modalCancelButton")]
        private IWebElement btnCancelarEliminacion;
        #endregion
        #endregion

        public Boolean ExisteTitulo()
        {
            _logger.Info("Se muestra título?");
            utiles.WaitForVisible(By.XPath("//h3"));
            return lblTitulo.Displayed;
        }
        public String GetTitulo()
        {
            _logger.Info("Mostrar nombre del título");
            return lblTitulo.Text;
        }
        // _logger.Info("");
        public Boolean ExisteLinkDescargaArchivoEjemplo()
        {
            utiles.WaitForVisible(By.XPath("//p/a"));
            return linkDescargarArchivo.Displayed;
        }

        public Boolean ExisteOpcionAgendarCBU()
        {
            utiles.WaitForVisible(By.XPath("//div/span[1]/i"));
            return btnAgendarCBUCVU.Displayed;
        }
        public Boolean ExisteOpcionAgendarCBUMasivamente()
        {
            utiles.WaitForVisible(By.XPath("//label/span[1]/i"));
            return btnAgendarCBUCVUMasivamente.Displayed;
        }
        public void ClickOpcionAgendarCBUCVU()
        {
            btnAgendarCBUCVU.Click();
        }
        public void ClickOpcionAgendarCBUCVUMasivamente()
        {
            btnAgendarCBUCVUMasivamente.Click();
        }
        public void IngresarCuentaExternaABuscar(string ctaExt)
        {
            txtBuscador.SendKeys(ctaExt);
	    }
        public Boolean ValidarCuentaExterna()
        {
            utiles.WaitForVisible(By.XPath("//nz-card/div[1]/div/div[1]"));
            return lblNombreCtaExt.Displayed;
        }
        public String GetNombreCuentaExterna()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return lblNombreCtaExt.Text;
        }
        public Boolean ExisteOpcionEliminarCuenta()
        {
            utiles.WaitForVisible(By.XPath("//div[1]/nz-card//i"));
            return btnEliminar.Displayed;
        }
        public void ClickEliminarCuenta()
        {
            btnEliminar.Click();
        }
        
        public Boolean ExisteModalEliminar()
        {
            utiles.WaitForVisible(By.Id("ModalConfirmation"));
            return modalConfirmacionEliminar.Displayed;
        }
        public String GetMensajeEliminar()
        {
            return mjeEliminacion.Text;
        }
        public String GetCBUEliminacion()
        {
            return CBUEliminacion.Text;
        }
        public String GetNombreCuentaEliminacion()
        {
            return NombreCuentaEliminacion.Text;
        }
        public void ClickConfirmarEliminar()
        {
            btnConfirmarEliminacion.Click();
        }
        public void ClickCancelarEliminar()
        {
            btnCancelarEliminacion.Click();
        }
        public void IngresarCuentaExternaAAsociar(string ctaExt)
        {
            txtCuentaAAgendar.SendKeys(ctaExt);
        }
        public void ClickBuscarCuentaAAgendar()
        {
            btnBuscarCuenta.Click();
        }
        public String GetNombreCuentaAAgendar()
        {
            utiles.WaitForVisible(By.XPath("//form//nz-card/div[1]"));
            return lblNombreCuentaAAsociar.Text;
        }
        public String GetCBUCuentaAAgendar()
        {
            return lblCBUAAsociar.Text;
        }
        public String GetTipodeCuentaAAgendar()
        {
            return lblTipoCuentaAAsociar.Text;
        }
   
        public void ClickContinuar()
        {
            btnContinuar.Click();
        }
        public String GetTituloMjeConfirmacion()
        {
            utiles.WaitForVisible(By.XPath("//nz-result/div[2]"));
            return lblMjeConfirmacion.Text;
        }
        public String GetMjeConfirmacion()
        {
            return lblRespuesta.Text;
        }
    }
}
