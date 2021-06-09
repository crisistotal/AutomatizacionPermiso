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
    [TestCategory("Transferir dinero Multicuenta")]
    public class TestMultiTransferirDineroChrome : BaseTestChrome
    {
        //Para leer los dataprovider
        ExcelRead originEntity = new ExcelRead();

        [TestMethod]
        [Description("Validar transferencia individual sin aprobación a una cuenta interna")]
        public void TransferenciaExitosaACuentaInterna()
        {            
            String monto = "50,5";
            String mje = "";
            //Se indica de que excel va a leer los datos de prueba
            originEntity.PopulateInCollection(@"./DataProviders/Multicuenta/Multicuenta.xlsx", "EntidadSinAprobacion");

            LoginPage login = new LoginPage(driverChrome);
            login.IniciarSesion(originEntity.ReadData(2, "Usuario"), originEntity.ReadData(2, "Password"));
            MenuPage menu = new MenuPage(driverChrome);
            menu.CambiarAIframeFormResponse();
            ActividadPage actividad = new ActividadPage(driverChrome);
            Assert.IsTrue(actividad.ExisteSaldoDisponible(), "No se muestra el campo Dinero Disponible");
            double saldoFinalCtaDest = actividad.ObtenerSaldoFinal(monto);
          
            //Logueo a cuenta origen - Transferir dinero            
            login.IniciarSesion(originEntity.ReadData(1, "Usuario"), originEntity.ReadData(1, "Password"));
            Assert.IsTrue(menu.ExisteOpcionTransferirDinero(), "No existe la opción transferir dinero");
            Thread.Sleep(3000);
            menu.IngresarATransferirDinero();
            menu.CambiarAIframeFormResponse();
            TransferirDineroPage transferir = new TransferirDineroPage(driverChrome);
            Assert.IsTrue(transferir.ExisteTituloTransferirDinero(), "No se visualiza el título Transferir Dinero");
            Assert.IsTrue(transferir.ObtenercuentaOrigen().Contains(originEntity.ReadData(1, "DescripcionCuenta")), "No es la cuenta origen correspondiente al usuario logueado");
            transferir.IngresarDestino(originEntity.ReadData(2, "CBU"));
            Double saldoFinal = transferir.ObtenerSaldoFinal(monto);
            Assert.IsTrue(transferir.ExisteMontoATransferir(), "No se encontro el campo Monto a transferir");
            transferir.IngresarMonto(monto);
            transferir.SeleccionarConcepto("Alquiler");
            Assert.IsTrue(transferir.ExisteCorreoElectronico(), "No existe el campo correo electronico");
            transferir.IngresarEmail("testingbitsion@gmail.com");
            Assert.IsTrue(transferir.ExisteMensaje(), "No existe el campo mensaje");
            mje = transferir.IngresarMensaje();
            Assert.IsTrue(transferir.EstaHabilitadoBotonContinuar(), "El botón Continuar no está habilitado");
            transferir.Continuar();
            Assert.IsTrue(transferir.ExisteDetalleTransferenciaAConfirmar(), "No se muestra el detalle de la transferencia");
            String ctaDestino = transferir.ObtenerCuentaDestino();
            transferir.ConfirmarTransferencia();
            CodigoValidacionPage otp = new CodigoValidacionPage(driverChrome);
            Assert.IsTrue(otp.ExisteModalIngresarCodigo(), "No se muestra la pantalla Ingresar Código");
            Assert.IsTrue(otp.ExisteBtnEnviarCodigoAlEmail(), "No se muestra la opción Enviar código al email");
            otp.PresionarOpcionEnviarCodigoAlEmail();
            otp.IngresarOTP();
            otp.ConfirmarOperacion();
            Assert.IsTrue(transferir.ExisteModalConfirmacionTransferencia(), "No se confirmo la transacción");
            String id = transferir.ObtenerIdTransaccion();
            transferir.Finalizar();
            menu.CambiarAIframeMenu();
            menu.IngresarActividad();
            menu.CambiarAIframeFormResponse();
            Thread.Sleep(2000);
            Assert.IsTrue(actividad.ExisteSaldoDisponible(), "No se muestra el campo Dinero Disponible");
            Assert.AreEqual(actividad.GetSaldoDisponible(), saldoFinal, 0, "No se actualizo el Dinero Disponible de la cuenta origen correctamente");
            Assert.IsTrue(actividad.SeActualizoGrilla(id, monto), "No se actualizo la grilla con la transacción saliente");
            login.IniciarSesion(originEntity.ReadData(2, "Usuario"), originEntity.ReadData(2, "Password"));
            menu.CambiarAIframeFormResponse();
            Assert.IsTrue(actividad.ExisteSaldoDisponible(), "No se muestra el campo Dinero Disponible");
            Thread.Sleep(2000);
            Assert.AreEqual(actividad.GetSaldoDisponible(), saldoFinalCtaDest, 0, "No se actualizo el Dinero Disponible de la cuenta destino correctamente");
            Assert.IsTrue(actividad.SeActualizoGrilla(id, monto), "No se actualizo la grilla con la transacción entrante");
            GmailPage gmail = new GmailPage(driverChrome);
		    gmail.LoginGmailElegirCuenta("testingbitsion@gmail.com","!QAZxsw2");
            Assert.IsTrue(gmail.VerificarComprobanteTransferencia(id, "Farmacia Testing Automatizado - 0000001700000000045160", ctaDestino, "FAC", mje.Substring(0, 37), monto), "Los datos del comprobante no son correctos");
    
}

        [TestMethod, NUnit.Framework.Order(1)]
        [Description("Validar transferencia individual fallida  a una cuenta externa")]
        public void TransferenciaNoExitosaACuentaExterna()
        {
            String monto = "100";
            String mje = "";
            //Se indica de que excel va a leer los datos de prueba
            originEntity.PopulateInCollection(@"./DataProviders/Multicuenta/Multicuenta.xlsx", "EntidadSinAprobacion");

            LoginPage login = new LoginPage(driverChrome);
            login.IniciarSesion(originEntity.ReadData(1, "Usuario"), originEntity.ReadData(1, "Password"));
            MenuPage menu = new MenuPage(driverChrome);
            Assert.IsTrue(menu.ExisteOpcionTransferirDinero(), "No existe la opción transferir dinero");
            Thread.Sleep(3000);
            menu.IngresarATransferirDinero();
            menu.CambiarAIframeFormResponse();
            TransferirDineroPage transferir = new TransferirDineroPage(driverChrome);
            Assert.IsTrue(transferir.ExisteTituloTransferirDinero(), "No se visualiza el título Transferir Dinero");
            Assert.IsTrue(transferir.ObtenercuentaOrigen().Contains(originEntity.ReadData(1, "DescripcionCuenta")), "No es la cuenta origen correspondiente al usuario logueado");
            transferir.IngresarDestino(originEntity.ReadData(3, "ALIAS"));
            Double saldoFinal = transferir.ObtenerSaldoFinal(monto);
            Assert.IsTrue(transferir.ExisteMontoATransferir(), "No se encontro el campo Monto a transferir");
            transferir.IngresarMonto(monto);
            transferir.SeleccionarConcepto("Alquiler");
            Assert.IsTrue(transferir.ExisteCorreoElectronico(), "No existe el campo correo electronico");
            Assert.IsTrue(transferir.ExisteMensaje(), "No existe el campo mensaje");
            mje = transferir.IngresarMensaje();
            Assert.IsTrue(transferir.EstaHabilitadoBotonContinuar(), "El botón Continuar no está habilitado");
            transferir.Continuar();
            Assert.IsTrue(transferir.ExisteDetalleTransferenciaAConfirmar(), "No se muestra el detalle de la transferencia");          
            transferir.ConfirmarTransferencia();
            CodigoValidacionPage otp = new CodigoValidacionPage(driverChrome);
            Assert.IsTrue(otp.ExisteModalIngresarCodigo(), "No se muestra la pantalla Ingresar Código");
            Assert.IsTrue(otp.ExisteBtnEnviarCodigoAlEmail(), "No se muestra la opción enviar código al email");
            otp.PresionarOpcionEnviarCodigoAlEmail();
            otp.IngresarOTP();
            otp.ConfirmarOperacion();
            Assert.IsTrue(transferir.ExisteModalConfirmacionTransferencia(), "No se confirmo la transacción");
            String id = transferir.ObtenerIdTransaccion();
            transferir.Finalizar();
            menu.CambiarAIframeMenu();
            menu.IngresarActividad();
            menu.CambiarAIframeFormResponse();
            Thread.Sleep(2000);
            ActividadPage actividad = new ActividadPage(driverChrome);
            Assert.IsTrue(actividad.ExisteSaldoDisponible(), "No se muestra el campo Dinero Disponible");
            Assert.AreEqual(actividad.GetSaldoDisponible(), saldoFinal, 0, "No se actualizo el Dinero Disponible de la cuenta origen correctamente");
            Assert.IsTrue(actividad.SeActualizoGrilla(id, monto), "No se actualizo la grilla con la transacción saliente");

        }

    }
}
