using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using System.Threading;
using Selenium_2.Pages.Claro;

namespace Selenium_2
{
    [TestClass]
    [TestCategory("Nuevo Usuario Claro PSR Firefox")]
    public class TestClaroEnrolamientoFirefox : BaseTestFirefox
    {
        //Para leer los dataprovider modo excel
        ExcelRead findExcel = new ExcelRead();

        [TestMethod]
        [Description("Enrola nuevo usuario en argentina")]
        public void TestNuevoUsuarioAr()
        {
            //Leeo el excel
            findExcel.PopulateInCollection(@"./DataProviders/Claro/Claro.xlsx");

            LoginPage login = new LoginPage(driverFirefox);
            login.Goto(1);
            login.LoginNuevoUsuarioArUy("35", findExcel.ReadData(1, "NumPos"), findExcel.ReadData(1, "NumPin"));

            TramitesPage tramites = new TramitesPage(driverFirefox);
            tramites.ValidateLogInTramites();

            EnrolamientoPage nuevoKiosko = new EnrolamientoPage(driverFirefox);
            nuevoKiosko.Enrolamiento();
            nuevoKiosko.FormularioIngreso();

            Thread.Sleep(3000);
            MiCuentaPage miCuenta = new MiCuentaPage(driverFirefox);
            tramites.ControlTramites();
            miCuenta.ValidarUsrEnrolado();
            miCuenta.LogOut();
        }

        [TestMethod]
        [Description("Enrola nuevo usuario en Uruguay")]
        public void TestNuevoUsuarioUy()
        {
            findExcel.PopulateInCollection(@"./DataProviders/Claro/Claro.xlsx");

            LoginPage login = new LoginPage(driverFirefox);
            login.Goto(3);
            login.LoginNuevoUsuarioArUy("35", findExcel.ReadData(2, "NumPos"), findExcel.ReadData(2, "NumPin"));

            TramitesPage tramites = new TramitesPage(driverFirefox);
            tramites.ValidateLogInTramites();

            EnrolamientoPage nuevoKiosko = new EnrolamientoPage(driverFirefox);
            nuevoKiosko.Enrolamiento();
            nuevoKiosko.FormularioIngreso();

            Thread.Sleep(3000);
            MiCuentaPage miCuenta = new MiCuentaPage(driverFirefox);
            tramites.ControlTramites();
            miCuenta.ValidarUsrEnrolado();
            miCuenta.LogOut();
        }

        [TestMethod]
        [Description("Enrola nuevo usuario en Paraguay-Punto de Activacion")]
        public void TestNuevoUsuarioPyActivacion()
        {
            findExcel.PopulateInCollection(@"./DataProviders/Claro/Claro.xlsx");

            LoginPage login = new LoginPage(driverFirefox);
            login.Goto(2);
            login.LoginNuevoUsuarioPy("0", findExcel.ReadData(3, "NumPos"), findExcel.ReadData(3, "NumPin"), 1);

            TramitesPage tramites = new TramitesPage(driverFirefox);
            tramites.ValidateLogInTramites();

            EnrolamientoPage nuevoKiosko = new EnrolamientoPage(driverFirefox);
            nuevoKiosko.Enrolamiento();
            nuevoKiosko.FormularioIngreso();

            Thread.Sleep(3000);
            MiCuentaPage miCuenta = new MiCuentaPage(driverFirefox);
            tramites.ControlTramites();
            miCuenta.ValidarUsrEnrolado();
            miCuenta.LogOut();
        }

        [TestMethod]
        [Description("Enrola nuevo usuario en Paraguay-Punto de Venta")]
        public void TestNuevoUsuarioPyVenta()
        {
            findExcel.PopulateInCollection(@"./DataProviders/Claro/Claro.xlsx");

            LoginPage login = new LoginPage(driverFirefox);
            login.Goto(2);
            login.LoginNuevoUsuarioPy("0", findExcel.ReadData(4, "NumPos"), findExcel.ReadData(4, "NumPin"), 2);

            TramitesPage tramites = new TramitesPage(driverFirefox);
            tramites.ValidateLogInTramites();

            EnrolamientoPage nuevoKiosko = new EnrolamientoPage(driverFirefox);
            nuevoKiosko.Enrolamiento();
            nuevoKiosko.FormularioIngreso();

            Thread.Sleep(3000);
            MiCuentaPage miCuenta = new MiCuentaPage(driverFirefox);
            tramites.ControlTramites();
            miCuenta.ValidarUsrEnrolado();
            miCuenta.LogOut();
        }

        [TestMethod]
        [Description("Enrola nuevo usuario en Paraguay-Punto de Giro")]
        public void TestNuevoUsuarioPyGiro()
        {
            findExcel.PopulateInCollection(@"./DataProviders/Claro/Claro.xlsx");

            LoginPage login = new LoginPage(driverFirefox);
            login.Goto(2);
            login.LoginNuevoUsuarioPy("0", findExcel.ReadData(5, "NumPos"), findExcel.ReadData(5, "NumPin"), 2);


            TramitesPage tramites = new TramitesPage(driverFirefox);
            tramites.ValidateLogInTramites();

            EnrolamientoPage nuevoKiosko = new EnrolamientoPage(driverFirefox);
            nuevoKiosko.Enrolamiento();
            nuevoKiosko.FormularioIngreso();

            Thread.Sleep(3000);
            MiCuentaPage miCuenta = new MiCuentaPage(driverFirefox);
            tramites.ControlTramites();
            miCuenta.ValidarUsrEnrolado();
            miCuenta.LogOut();
        }

        [TestMethod]
        [Description("Enrola nuevo usuario en Paraguay-Vendedor Directa")]
        public void TestNuevoUsuarioPyDirecta()
        {
            findExcel.PopulateInCollection(@"./DataProviders/Claro/Claro.xlsx");

            LoginPage login = new LoginPage(driverFirefox);
            login.Goto(2);
            login.LoginNuevoUsuarioPy("0", findExcel.ReadData(6, "NumPos"), findExcel.ReadData(5, "NumPin"), 4);

            TramitesPage tramites = new TramitesPage(driverFirefox);
            tramites.ValidateLogInTramites();

            EnrolamientoPage nuevoKiosko = new EnrolamientoPage(driverFirefox);
            nuevoKiosko.Enrolamiento();
            nuevoKiosko.FormularioIngreso();

            Thread.Sleep(3000);
            MiCuentaPage miCuenta = new MiCuentaPage(driverFirefox);
            tramites.ControlTramites();
            miCuenta.ValidarUsrEnrolado();
            miCuenta.LogOut();
        }
    }
}
