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
using System.Threading;

namespace Selenium_2
{
    class GmailPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public GmailPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }

        #region Componentes
        [FindsBy(How = How.Id, Using = "identifierId")]
        private IWebElement txtCorreoElectronico;

        [FindsBy(How = How.XPath, Using = "/html/body//form/table[2]/tbody")]
        private IWebElement tblCorreos;

        [FindsBy(How = How.Id, Using = "identifierNext")]
        private IWebElement btnSiguiente;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement txtPassword;

        [FindsBy(How = How.Id, Using = "passwordNext")]
        private IWebElement btnSiguientePassword;

        [FindsBy(How = How.XPath, Using = "/html//input[6]")]
        private IWebElement btnEliminarMail;

        [FindsBy(How = How.Id, Using = "gb_71")]
        private IWebElement opcSalir;

        [FindsBy(How = How.XPath, Using = "/html//form/table[1]//a")]
        private IWebElement btnActualizar;

        [FindsBy(How = How.XPath, Using = "//div[2]//li[1]/div/div[1]/div")]
        private IWebElement opcElegirCta;

        #endregion

        public void Go()
        {
            driver.Navigate().GoToUrl("https://accounts.google.com/signin");
            driver.Manage().Window.Maximize();
        }

        // Version de gmail sin javascript
        public void GoOld()
        {
           driver.Navigate().GoToUrl("https://mail.google.com/mail/?ui=html");
        }
        public void IngresarCorreoElectronico(String VarUsuario)
        {
            txtCorreoElectronico.SendKeys(VarUsuario);
        }
        public void IngresarContrasena(String VarContrasena)
        {
            utiles.WaitForVisible(By.Name("password"));
            txtPassword.SendKeys(VarContrasena);
        }
        public void PresionarSiguiente()
        {
            btnSiguiente.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        public void PresionarSiguientePassword()
        {
            btnSiguientePassword.Click();
        }
        public void ElegirCuentaDeEmail()
        {
            opcElegirCta.Click();
        }
        public void ActualizarBandeja()
        {
            btnActualizar.Click();
        }

        //Login a gmail ingresando email y password o seleccionando cuenta, según corresponda
        public void LoginGmail(String usu, String con)
        {
            utiles.NewTab("https://mail.google.com/");
            if (driver.FindElements(By.Id("identifierId")).Count() > 0)
            {
                IngresarCorreoElectronico(usu);
                PresionarSiguiente();
            }
            else
            {
                ElegirCuentaDeEmail();
            }
            IngresarContrasena(con);
            PresionarSiguientePassword();
        }

        //Login a gmail seleccionando cuenta
        public void LoginGmailElegirCuenta(String usu, String con)
        {
            utiles.NewTab("https://mail.google.com/");
            ElegirCuentaDeEmail();
            IngresarContrasena(con);
            PresionarSiguientePassword();
        }

        //Obtener código de validación del mail
        public String ObtenerOTPMail() 
        {
            Thread.Sleep(3000);		
		    GoOld();
            ActualizarBandeja();
		    // Guardamos los mails no leídos en una lista		
		    IList<IWebElement> unreademail = driver.FindElements(By.XPath("/html/body//form/table[2]/tbody/tr"));

            String otp = "";
            String remitente = "FarmaCloud";

            // Recorre el tamaño de la casilla de correo 

            foreach (IWebElement mail in unreademail)
            {
                if (mail.Text.Contains(remitente))
                {
                    mail.Click();
                    otp = driver.FindElement(By.XPath("/html/body//tr[10]/td[3]")).Text;
                    EliminarMail();
                    CerrarSesion();
                    driver.Close();
                    utiles.OldTab();
                    driver.SwitchTo().Frame("formResponse");
                    break;
                }
            }
            return otp;
	    }

        public void CerrarSesion()
        {
            opcSalir.Click();
        }
        public void EliminarMail()
        {
            btnEliminarMail.Click();
        }
        public Boolean VerificarComprobanteTransferencia(String idTransaccion, String ctaOrigen, String ctaDestino, String concepto, String refe, String monto) 
        {
            Thread.Sleep(3000);
		    Boolean bandera = false;
            GoOld();
            ActualizarBandeja();
            // Guardamos los mails no leídos en una lista		
            IList<IWebElement> unreademail = driver.FindElements(By.XPath("/html/body//form/table[2]/tbody/tr"));

            String remitente = "FarmaCloud";
		    // Recorre el tamaño de la casilla de correo 
            foreach (IWebElement mail in unreademail)
            {
                if (mail.Text.Contains(remitente))
                {
                    mail.Click();
                    String tituloMail = driver.FindElement(By.XPath("//table[1]//table//tr[5]")).Text;
                    if (tituloMail.Contains("Comprobante de transferencia"))
                        bandera = true;
                    else
                    {
                        bandera = false;
                        break;
                    }
                    String idTransMail = driver.FindElement(By.XPath("//table[3]//tr[7]/td[3]/p")).Text;
                    if (idTransMail.Contains(idTransaccion))
                        bandera = true;
                    else
                    {
                        bandera = false;
                        break;
                    }
                    String origen = driver.FindElement(By.XPath("//table[3]//tr[9]/td[3]/p")).Text;
                    if (origen.Contains(ctaOrigen))
                        bandera = true;
                    else
                    {
                        bandera = false;
                        break;
                    }

                    String destinatario = driver.FindElement(By.XPath("//table[3]//tr[11]/td[3]/p")).Text;
                    if (destinatario.Contains(ctaDestino))
                        bandera = true;
                    else
                    {
                        bandera = false;
                        break;
                    }

                    String con = driver.FindElement(By.XPath("//table[3]//tr[13]/td[3]/p")).Text;
                    if (con.Contains(concepto))
                        bandera = true;
                    else
                    {
                        bandera = false;
                        break;
                    }

                    String referencia = driver.FindElement(By.XPath("//table[3]//tr[15]/td[3]/p")).Text;
                    if (referencia.Contains(refe))
                        bandera = true;
                    else
                    {
                        bandera = false;
                        break;
                    }
                    String mon = driver.FindElement(By.XPath("//table[3]//tr[17]/td[3]/p")).Text;
                    if (mon.Contains(monto))
                        bandera = true;
                    else
                    {
                        bandera = false;
                        break;
                    }
                    EliminarMail();
                    CerrarSesion();
                    driver.Close();
                    utiles.OldTab();
                    driver.SwitchTo().Frame("formResponse");
                    break;
                }
            else
            {
                Console.WriteLine("No llego mail");
            }	
		    }
		    return bandera;
	    }

    }
}
