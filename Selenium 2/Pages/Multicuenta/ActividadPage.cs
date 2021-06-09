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
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Selenium_2.Pages.Multicuenta
{
    class ActividadPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private Actions acciones;
        private Utils utiles;

        public ActividadPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
            acciones = new Actions(driver);
            utiles = new Utils(driver);
        }
        #region Componentes
        [FindsBy(How = How.XPath, Using = "//h2[contains(text(),'Actividad en la cuenta')]")]
        private IWebElement lblTitulo;

        [FindsBy(How = How.Id, Using = "accountAvailableBalance")]
        private IWebElement lblSaldoDisponible;

        [FindsBy(How = How.Id, Using = "mainFilter")]
        private IWebElement txtFiltroODC;

        [FindsBy(How = How.XPath, Using = "//nz-input-group/span/i")]
        private IWebElement opcLimpiarFiltro;

        [FindsBy(How = How.Id, Using = "mainFilterSearchBtn")]
        private IWebElement btnBuscar;

        [FindsBy(How = How.Id, Using = "dateFilter")]
        private IWebElement pckFiltroFecha;

        [FindsBy(How = How.Id, Using = "allMovementFilter")]
        private IList<IWebElement> lstMovimientos;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"filter-end\"]//button")]
        private IWebElement lstDescargar;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"cdk-overlay-17\"]//li[2]")]
        private IWebElement opcPDF;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"cdk-overlay-17\"]//li[1]")]
        private IWebElement opcCSV;

        [FindsBy(How = How.XPath, Using = "//tr[1]/td[1]/span")]
        private IWebElement idTransaccionGrilla;

        [FindsBy(How = How.XPath, Using = "//tr[1]//h5")]
        private IWebElement montoTransaccionGrilla;

        [FindsBy(How = How.Id, Using = "activityMainTable")]
        private IWebElement tblGrilla;

        [FindsBy(How = How.XPath, Using = "//div[1]/div/inner-popup//table")]
        private IWebElement dateWidget;

        #endregion 

        public void ClickFechas()
        {
            utiles.WaitForClickeable(pckFiltroFecha);
            pckFiltroFecha.Click();
        }

        public Boolean DownloadnSeeCSV()
        {
           // string downloadFilePath = @"D:\Test\Actividad-Cuenta-30502793175-00002056.csv";
           string downloadFilePath = "C:\\Users\\Actividad-Cuenta-30502793175-00002056.csv";
           DescargarArchivo("");

           Thread.Sleep(8000);
           var exist = File.Exists(downloadFilePath);
           File.Delete(downloadFilePath);
            //var text = File.ReadAllText(downloadFilePath); Por si pinta leer, cambiar a public String 
           return exist;
        }
        public void DescargarArchivo(string archivo)
        {

            if(archivo == "PDF")
            {
                utiles.WaitForVisible(By.Id("activityMainTable"));
                utiles.WaitForClickeable(lstDescargar);
                lstDescargar.Click();
                utiles.WaitForClickeable(opcPDF);
                opcPDF.Click();
            }
            else
            {                
                utiles.WaitForVisible(By.Id("activityMainTable"));
                utiles.WaitForClickeable(lstDescargar);
                lstDescargar.Click();
                utiles.WaitForClickeable(opcCSV);
                opcCSV.Click();
            }

        }
        public Boolean ExisteSaldoDisponible()
        {
            utiles.WaitForClickeable(lblSaldoDisponible);
            return lblSaldoDisponible.Displayed;
        }

        public Boolean SeActualizoGrilla(String id, String monto)
        {
            Boolean obt = false;           
            wait.Until(ExpectedConditions.TextToBePresentInElement(idTransaccionGrilla, id));
            if (montoTransaccionGrilla.Text.Contains(monto))
                obt = true;
            else { 
            obt = false;
            }
            return obt;
        }

        public double ObtenerSaldoFinal(String monto)
        {
            double saldoFinal = 0;
            double saldoActual = GetSaldoDisponible();
            do
            {
              saldoActual = GetSaldoDisponible();
            //  monto = monto.Replace(",", ".");
              saldoFinal = saldoActual + Convert.ToDouble(monto);
            } while (saldoActual <= 0);
            return saldoFinal;
        }

        public void IngresarOrigenDestinoConvenio(String busq)
        {
            txtFiltroODC.Clear();
            txtFiltroODC.SendKeys(busq);
        }

        public void LimpiarFiltro()
        {
            opcLimpiarFiltro.Click();
        }

        public void ClickBuscar()
        {
            utiles.WaitForClickeable(btnBuscar);
            btnBuscar.Click();
            Thread.Sleep(2000);

        }

        public void FiltrarPorFecha()
        {
            String today = DateTime.Now.ToString("dd");

            if (today.Contains("0"))
            {
                today.Substring(0);
                IngresarFechaDesdeYHasta(today);
            }
            else
            {
                IngresarFechaDesdeYHasta(today);
            }
        }

        public void IngresarFechaDesdeYHasta(string fecha)
        {
            IList<IWebElement> columns = dateWidget.FindElements(By.TagName("td"));

            foreach (IWebElement cell in columns)
            {
                if (cell.Text.Contains(fecha))
                {
                    cell.Click();
                    cell.Click();
                    Thread.Sleep(3000);
                    break;
                }
            }
        }

        public void IngresoOrigenDestinoOConvenio(String busq)
        {
            utiles.WaitForClickeable(txtFiltroODC);
            txtFiltroODC.Clear();
            txtFiltroODC.SendKeys(busq);
            Thread.Sleep(2000);

        }

        public Boolean ExisteListaDescargar()
        {
            utiles.WaitForClickeable(lstDescargar);
            return lstDescargar.Displayed;
        }

        public Boolean ExisteOpcionLimpiarFiltro()
        {
            return opcLimpiarFiltro.Displayed;
        }

        public Boolean ResultadoGrilla(String destino)
        {
            Boolean rtdo = false;
            IList<IWebElement> tabla = tblGrilla.FindElements(By.TagName("tr"));
            IList<IWebElement> columnsList = null;

            foreach (IWebElement row in tabla)
            {
                columnsList = tblGrilla.FindElements(By.XPath("//table/tbody/tr/td[2]"));

                foreach (IWebElement column in columnsList)
                {
                    if (column.Text.Contains(destino))
                    {
                        rtdo = true;
                        break;
                    }
                    else
                    {
                        rtdo = false;
                    }
                }
            }
            return rtdo;
        }

        public Boolean buscarTransaccion(String destino)
        {
            Boolean rtdo = false;
            IList<IWebElement> tabla = tblGrilla.FindElements(By.TagName("tr"));
            IList<IWebElement> columnsList = null;

            foreach (IWebElement row in tabla)
            {
                columnsList = tblGrilla.FindElements(By.XPath("//table/tbody/tr/td[1]"));

                foreach (IWebElement column in columnsList)
                {
                    if (column.Text.Contains(destino))
                    {
                        rtdo = true;
                        break;
                    }
                    else
                    {
                        rtdo = false;
                    }
                }
            }
            return rtdo;
        }

        public Boolean ResultadosGrillaFiltroPorFecha()
        {
            String today = DateTime.Now.ToString("dd");

            Boolean rtdo = false;

            IList<IWebElement> rowList = tblGrilla.FindElements(By.TagName("tr"));
            IList<IWebElement> columnsList = null;

            foreach (IWebElement row in rowList)
            {
                columnsList = row.FindElements(By.XPath("//table/tbody/tr/td[6]"));

                foreach (IWebElement column in columnsList)
                {
                    String dia = column.Text.Substring(0, 2);
                    if (dia.Contains(today))
                    {
                        rtdo = true;
                    }
                    else
                    {
                        rtdo = false;
                        break;
                    }
                }
            }
            return rtdo;
        }
        public Boolean GrillaSinResultado()
        {
            Boolean rtdo = false;
            if (driver.FindElement(By.XPath("//*[@id=\"activityMainTable\"]//nz-embed-empty")).Text.Contains("No se encontraron movimientos. Intenta con otros términos de búsqueda"))
            {
                rtdo = true;
            }
            return rtdo;
        }
        public Boolean ExisteTituloACtividad()
        {
            utiles.WaitForClickeable(lblTitulo);
            return lblTitulo.Displayed;
        }
        public Boolean ExisteCampoOringenDc()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(txtFiltroODC));
            return txtFiltroODC.Displayed;
        }
        public Double GetSaldoDisponible()
        {
            double saldoAct = 0;
            String saldoActual = lblSaldoDisponible.Text;
            Char separador = '$';
            String[] parts = saldoActual.Split(separador);
            saldoActual = parts[1].Replace(".", "");
            saldoAct = Convert.ToDouble(saldoActual);
            return saldoAct;
        }
        public String DownloadnReadPdf()
        {
            string downloadFilePath = @"D:\Test\Activity30502793175-00002056.pdf";
            DescargarArchivo("PDF");
            Thread.Sleep(8000);

            if (File.Exists(downloadFilePath))
            {
                PdfReader reader = new PdfReader(downloadFilePath);
                StringBuilder text = new StringBuilder();

                for (int i = 1; i < reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                reader.Close();
                File.Delete(@"D:\Test\Activity30502793175-00002056.pdf");
                return text.ToString();
            }
            else
            {
                throw new Exception("El archivo no se descargo");
            }
        }
        public String ObtenerIdTransaccion()
        {
            return idTransaccionGrilla.Text;
        }

    }
}
