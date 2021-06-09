using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using NLog;
using System.Globalization;

namespace Selenium_2.Pages.Multicuenta
{
    class TransferirDineroPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public TransferirDineroPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        #region Componentes
        [FindsBy(How = How.Id, Using = "transferMainTitle")]
        private IWebElement lblTitulo;

        [FindsBy(How = How.Id, Using = "accountsSelect")]
        private IList<IWebElement> lstCuentaOrigen;

        [FindsBy(How = How.Id, Using = "destinationAccountField")]
        private IList<IWebElement> lstCuentaDestino;

        [FindsBy(How = How.Id, Using = "cheSearch")]
        private IWebElement lblSaldoActual;

        [FindsBy(How = How.Id, Using = "transferAmountField")]
        private IWebElement txtMonto;

        [FindsBy(How = How.Id, Using = "transferConceptField")]
        private IList<IWebElement> lstConceptoTransferencia;

        [FindsBy(How = How.Id, Using = "transferEmailField")]
        private IWebElement txtEmailComprobante;

        [FindsBy(How = How.Id, Using = "transferMessageField")]
        private IWebElement txtMjeDestinatario;

        [FindsBy(How = How.Id, Using = "continuar")]
        private IWebElement btnContinuar;

        //Modal Estás transfiriendo dinero a...Datos: Nombre o RS, CUIT, CBU/CVU, Monto, Concepto, Confirmar y Cancelar. COMPLETAR
        [FindsBy(How = How.Id, Using = "ModalConfirmTransfer")]
        private IWebElement detalleTransaccion;

        [FindsBy(How = How.Id, Using = "destinationAccountName")]
        private IWebElement lblCuentaDestinoNombre;

        [FindsBy(How = How.Id, Using = "destinationAccountCUIT")]
        private IWebElement lblCuentaDestinoCUIT;

        [FindsBy(How = How.Id, Using = "destinationAccountCbuCvu")]
        private IWebElement lblCuentaDestinoCBU;

        [FindsBy(How = How.Id, Using = "destinationAccountConcept")]
        private IWebElement lblConcepto;

        [FindsBy(How = How.Id, Using = "modalConfirmButton")]
        private IWebElement btnConfirmar;

        //Modal "Ocurrió un inconveniente" (Modal)
        [FindsBy(How = How.Id, Using = "ModalErrorTransfer")]
        private IWebElement error;

        //Modal "¡La transferencia se realizó con éxito!" 
        [FindsBy(How = How.Id, Using = "ModalSuccessTransfer")]
        private IWebElement mjeTransferenciaExitosa;

        [FindsBy(How = How.XPath, Using = "/html//p[3]")]
        private IWebElement lblIdTransaccion;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"ModalSuccessTransfer\"]//button")]
        private IWebElement btnFinalizar;
        #endregion 

        public  Boolean ExisteTituloTransferirDinero()
        {
            utiles.WaitForVisible(By.Id("transferMainTitle"));
            return lblTitulo.Displayed;
	    }
        public String ObtenercuentaOrigen()
        {
            utiles.WaitForVisible(By.XPath("//*[@id='transferMainTitle']"));
            Thread.Sleep(1000);
            return lstCuentaOrigen[0].Text;
        }
        public double ObtenerSaldoFinal(String monto)
        {
            double saldoFinal = 0;
            double saldoActual = ObtenerSaldoActual();
      //      monto = monto.Replace(",", ".");
            saldoFinal = saldoActual - Convert.ToDouble(monto);
            return saldoFinal;
        }
        public double ObtenerSaldoActual()
        {
            double saldoAct = 0;
            String saldoActual = lblSaldoActual.Text;
            Char separador = '$';
            String[] parts = saldoActual.Split(separador);
            saldoActual = parts[1].Replace(".", "");
            saldoAct = Convert.ToDouble(saldoActual);
            return saldoAct;
        }
        public Boolean ExisteMontoATransferir()
        {
            return txtMonto.Displayed;
        }
        public void IngresarDestino(String ctaDestino) 
        {		
		    //Ingreso de caracter uno por uno
		    for (int i = 0; i<ctaDestino.Length; i++) {
		    lstCuentaDestino.ElementAt(0).SendKeys(ctaDestino[i].ToString());
            Thread.Sleep(1);
		    }
            Thread.Sleep(2000);
		    lstCuentaDestino.ElementAt(0).SendKeys(Keys.Enter);
            lstCuentaDestino.ElementAt(0).SendKeys(Keys.Enter);

            //Obtiene los datos de la cuenta ingresada
            //String texto = Driver.getInstance().getDriver().findElement(By.xpath("//nz-auto-option[@id='destinationOption-0']/span")).getText();

        }

        public void SeleccionarConcepto(String concepto)
        {
            lstConceptoTransferencia.ElementAt(0).Click();
            IList<IWebElement> lista = driver.FindElements(By.XPath("//*[@id='cdk-overlay-8']/div/div/ul"));
            utiles.SeleccionarOpcionDeLista(lista, concepto);
        }

        public void IngresarMonto(String monto)
        {
            txtMonto.SendKeys(monto);
        }
        public Boolean ExisteCorreoElectronico()
        {
            return txtEmailComprobante.Displayed;
        }
        //Campo opcional
        public void IngresarEmail(String email)
        {
            _logger.Info("Ingresar email al que le llegará el comprobante");
            txtEmailComprobante.Clear();
            txtEmailComprobante.SendKeys(email);
        }
        //opcional
        public Boolean ExisteMensaje()
        {
            return txtMjeDestinatario.Displayed;
        }
        public String IngresarMensaje()
        {
            String mje = utiles.GenerateStringFechaHora(50);
            txtMjeDestinatario.SendKeys(mje);
            return mje;
        }
        public Boolean EstaHabilitadoBotonContinuar()
        {            
            return btnContinuar.Enabled;
        }
        public void Continuar()
        {
            btnContinuar.Click();
        }

        public void ConfirmarTransferencia()
        {
            btnConfirmar.Click();
        }

        public Boolean ExisteDetalleTransferenciaAConfirmar()
        {
            return detalleTransaccion.Displayed;
        }
        public Boolean ExisteModalConfirmacionTransferencia()
        {
            utiles.WaitForVisible(By.Id("ModalSuccessTransfer"));           
            return mjeTransferenciaExitosa.Displayed;
        }

        public void Finalizar()
        {
            utiles.WaitForClickeable(btnFinalizar);
            btnFinalizar.Click();
        }
        //Completar método
        public Boolean ValidarDatosDetalleTransferencia()
        {
            Boolean sonCorrectos = false;
                        
            if (lblCuentaDestinoNombre.Text == lstCuentaDestino.ElementAt(0).Text);
            else
            {
                return sonCorrectos = false;
            }
            return sonCorrectos;
        }

        public String ObtenerIdTransaccion()
        {
            return lblIdTransaccion.Text;
        }

        public String ObtenerCuentaDestino()
        {
            String ctaDestino = lblCuentaDestinoNombre.Text + " - " + lblCuentaDestinoCBU.Text;
            return ctaDestino;
        }
    }
}
