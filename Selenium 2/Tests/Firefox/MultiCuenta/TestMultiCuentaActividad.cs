using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium_2.Pages.Multicuenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_2
{
    [TestClass]
    [TestCategory("Logeo Multicuenta")]
    public class TestMultiCuentaActividadFirefox : BaseTestFirefox
    {
        [TestMethod]
        [Description("Validar descarga grilla Actividad en pdf")]
        public void VerificarDescargarGrillaPdfExitosamente()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("tauregular", "!QAZxsw2");

            MenuPage menu = new MenuPage(driverFirefox);
            menu.CambiarAIframeFormResponse();

            ActividadPage actividad = new ActividadPage(driverFirefox);
            Assert.IsTrue(actividad.ExisteListaDescargar(), "No se muestra el campo Descargar");
            Assert.IsTrue(actividad.DownloadnReadPdf().Contains("Número de Cuenta: 30502793175-00002056"),"El documento no es de la cuenta logueada");
        }

        [TestMethod]
        [Description("Validar descarga grilla Actividad en CSV")]
        public void VerificarDescargarGrillaCSVExitosamente()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("tauregular", "!QAZxsw2");

            MenuPage menu = new MenuPage(driverFirefox);
            menu.CambiarAIframeFormResponse();

            ActividadPage actividad = new ActividadPage(driverFirefox);
            Assert.IsTrue(actividad.ExisteListaDescargar(), "No se muestra el campo Descargar");
            Assert.IsTrue(actividad.DownloadnSeeCSV(),"El docucumento no se descargo");
        }

        [TestMethod]
        [Description("Validar que filtre correctamente por campo origen/destino y fecha desde y hasta")]
        public void VerificarBusquedaExitosamente()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("tauregular", "!QAZxsw2");

            MenuPage menu = new MenuPage(driverFirefox);
            menu.CambiarAIframeFormResponse();

            ActividadPage actividad = new ActividadPage(driverFirefox);
            actividad.ExisteCampoOringenDc();
            actividad.IngresoOrigenDestinoOConvenio("CAMARA DE FARMACIAS DE CORDOBA");
            actividad.ClickBuscar();
            Assert.IsTrue(actividad.ResultadoGrilla("Enviaste dinero a CAMARA DE FARMACIAS"), "No se encontraron resultados de búsqueda para el filtro origen/destino");
            actividad.ClickFechas();
            actividad.FiltrarPorFecha();
            Assert.IsTrue(actividad.ResultadosGrillaFiltroPorFecha(), "No se encontraron resultados de búsqueda para el filtro fecha desde fecha hasta");
        }

        [TestMethod]
        [Description("Validar búsqueda sin resultado")]
        public void VerificarBusquedaSinExito()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("tauregular", "!QAZxsw2");

            MenuPage menu = new MenuPage(driverFirefox);
            menu.CambiarAIframeFormResponse();

            ActividadPage actividad = new ActividadPage(driverFirefox);
            Assert.IsTrue(actividad.ExisteCampoOringenDc(), "No se muestra el campo Origen/Destino o Convenio");
            actividad.IngresoOrigenDestinoOConvenio("cualquiera");
            actividad.ClickBuscar();
            Assert.IsTrue(actividad.GrillaSinResultado(), "El sistema no informa que no se encontraron resultados");
        }

        [TestMethod]
        [Description("Verificar que se haya realizado la reversión de una transacción externa fallida")]
        public void VerificarReversionTransaccionExterna()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("tauregular", "!QAZxsw2");

            MenuPage menu = new MenuPage(driverFirefox);
            menu.CambiarAIframeFormResponse();

            ActividadPage actividad = new ActividadPage(driverFirefox);
            Assert.IsTrue(actividad.ExisteCampoOringenDc(), "No se muestra el campo Origen/Destino o Convenio");
            actividad.IngresoOrigenDestinoOConvenio("PABLITO.AIMAR");
            actividad.ClickBuscar();
            //actividad.ClickFechas();
            //actividad.FiltrarPorFecha();
            String id = actividad.ObtenerIdTransaccion();
            Assert.IsTrue(actividad.ExisteOpcionLimpiarFiltro(), "No se muestra la opcion x (Limpiar Filtro)");
            //actividad.LimpiarFiltro();
            Assert.IsTrue(actividad.buscarTransaccion(id), "No se encontro la reversión de la transacción " + id);
        }     
    }
}
