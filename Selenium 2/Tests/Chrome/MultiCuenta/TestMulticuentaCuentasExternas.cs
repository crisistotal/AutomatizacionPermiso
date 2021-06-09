using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Selenium_2.Pages.Multicuenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium_2
{
    [TestClass]
    [TestCategory("Logeo Multicuenta")]
    public class TestMulticuentaCuentasExternasChrome : BaseTestChrome
    {
        //Para leer los dataprovider
        ExcelRead originEntity = new ExcelRead();

        [TestMethod, NUnit.Framework.Order(1)]
        [Description("Validar agendar un CVU exitosamente")]
        public void ValidarAsociacionCVU()
        {
            originEntity.PopulateInCollection(@"./DataProviders/Multicuenta/Multicuenta.xlsx", "EntidadSinAprobacion");

            LoginPage login = new LoginPage(driverChrome);
            login.IniciarSesion(originEntity.ReadData(1, "Usuario"), originEntity.ReadData(1, "Password"));
            MenuPage menu = new MenuPage(driverChrome);
            menu.IngresarAdmCtasExternas();
            menu.CambiarAIframeFormResponse();
            AdministrarCuentasExternasPage cuentasExternas = new AdministrarCuentasExternasPage(driverChrome);
            Assert.IsTrue(cuentasExternas.ExisteTitulo(), "No se muestra el título");
            Assert.IsTrue(cuentasExternas.GetTitulo().Contains("CBU/CVU externos"), "El título no es CBU/CVU externos");
            Assert.IsTrue(cuentasExternas.ExisteOpcionAgendarCBU(), "No se muestra opción agendar un CBU/CVU");
            cuentasExternas.ClickOpcionAgendarCBUCVU();
            cuentasExternas.IngresarCuentaExternaAAsociar(originEntity.ReadData(4,"CBU"));
            cuentasExternas.ClickBuscarCuentaAAgendar();
            Assert.IsTrue(cuentasExternas.GetNombreCuentaAAgendar().Contains("Farmacia Testing"), "No es la cuenta farmacia testing");
            Assert.IsTrue(cuentasExternas.GetCBUCuentaAAgendar().Contains("0000247100000000012236"), "No es el cbu ingresado");
            Assert.IsTrue(cuentasExternas.GetTipodeCuentaAAgendar().Contains("VIRTUAL"), "No es una cuenta virtual");
            cuentasExternas.ClickContinuar();
            CodigoValidacionPage otp = new CodigoValidacionPage(driverChrome);
            Assert.IsTrue(otp.ExisteModalIngresarCodigo(), "No se muestra la pantalla Ingresar Código");
            Assert.IsTrue(otp.ExisteBtnEnviarCodigoAlEmail(), "No se muestra la opción Enviar código al email");
            otp.PresionarOpcionEnviarCodigoAlEmail();
            otp.IngresarOTP();
            otp.ConfirmarOperacion();
            Assert.IsTrue(cuentasExternas.GetTituloMjeConfirmacion().Contains("CBU/CVU agendado"), "El mensaje de confirmación no tiene el título CBU/CVU agendado");
            Assert.IsTrue(cuentasExternas.GetMjeConfirmacion().Contains("Desde ahora puedes realizar transferencias a esa cuenta"), "El mensaje de confirmación no es correcto");
            Thread.Sleep(3000);
            cuentasExternas.ClickContinuar();
            Assert.IsTrue(cuentasExternas.GetNombreCuentaExterna().Contains("Farmacia Testing"), "No se muestra la cuenta agendada");       

        }
        [TestMethod]
        [Description("Validar cancelación del eliminar asociación de una cuenta externa")]
        public void CancelarEliminarAsociacionCVU()
        {
            originEntity.PopulateInCollection(@"./DataProviders/Multicuenta/Multicuenta.xlsx", "EntidadSinAprobacion");

            LoginPage login = new LoginPage(driverChrome);
            login.IniciarSesion(originEntity.ReadData(1, "Usuario"), originEntity.ReadData(1, "Password"));
            MenuPage menu = new MenuPage(driverChrome);
            menu.IngresarAdmCtasExternas();
            menu.CambiarAIframeFormResponse();
            AdministrarCuentasExternasPage cuentasExternas = new AdministrarCuentasExternasPage(driverChrome);
            Assert.IsTrue(cuentasExternas.ExisteTitulo(), "No se muestra el título");
            Assert.IsTrue(cuentasExternas.GetTitulo().Contains("CBU/CVU externos"), "El título no es CBU/CVU externos");
            Assert.IsTrue(cuentasExternas.ExisteOpcionEliminarCuenta(), "No se muestra opción eliminar asociación");
            cuentasExternas.ClickEliminarCuenta();
            Assert.IsTrue(cuentasExternas.ExisteModalEliminar(), "No se muestra el mensaje para confirmar la eliminación");
            Assert.IsTrue(cuentasExternas.GetMensajeEliminar().Contains("Está a punto de eliminar la asociación con la cuenta"), "No se muestra el mensaje correspondiente a eliminar");
            Assert.IsTrue(cuentasExternas.GetCBUEliminacion().Contains("0000247100000000012236"), "El CBU a eliminar no es correcto");
            Assert.IsTrue(cuentasExternas.GetNombreCuentaEliminacion().Contains("Farmacia Testing"), "El nombre de la cuenta a eliminar no es correcto");
            cuentasExternas.ClickCancelarEliminar();
            Assert.IsTrue(cuentasExternas.GetNombreCuentaExterna().Contains("Farmacia Testing"), "La cuenta no fue eliminada");
        }
        [TestMethod]
        [Description("Eliminar asociación de una cuenta externa exitosamente")]
        public void EliminarAsociacionCBUCVUExitosamente()
        {
            originEntity.PopulateInCollection(@"./DataProviders/Multicuenta/Multicuenta.xlsx", "EntidadSinAprobacion");

            LoginPage login = new LoginPage(driverChrome);
            login.IniciarSesion(originEntity.ReadData(1, "Usuario"), originEntity.ReadData(1, "Password"));
            MenuPage menu = new MenuPage(driverChrome);
            menu.IngresarAdmCtasExternas();
            menu.CambiarAIframeFormResponse();
            AdministrarCuentasExternasPage cuentasExternas = new AdministrarCuentasExternasPage(driverChrome);
            Assert.IsTrue(cuentasExternas.ExisteTitulo(), "No se muestra el título");
            Assert.IsTrue(cuentasExternas.GetTitulo().Contains("CBU/CVU externos"), "El título no es CBU/CVU externos");
            Assert.IsTrue(cuentasExternas.ExisteOpcionEliminarCuenta(), "No se muestra opción eliminar asociación");
            cuentasExternas.ClickEliminarCuenta();
            Assert.IsTrue(cuentasExternas.ExisteModalEliminar(), "No se muestra el mensaje para confirmar la eliminación");
            Assert.IsTrue(cuentasExternas.GetMensajeEliminar().Contains("Está a punto de eliminar la asociación con la cuenta"), "No se muestra el mensaje correspondiente a eliminar");
            Assert.IsTrue(cuentasExternas.GetCBUEliminacion().Contains("0000247100000000012236"), "El CBU a eliminar no es correcto");
            Assert.IsTrue(cuentasExternas.GetNombreCuentaEliminacion().Contains("Farmacia Testing"), "El nombre de la cuenta a eliminar no es correcto");
            cuentasExternas.ClickConfirmarEliminar();
            Thread.Sleep(3000);
            Assert.IsFalse(cuentasExternas.GetNombreCuentaExterna().Contains("Farmacia Testing"), "La cuenta no fue eliminada");
        }
    }
}
