using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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
    public class TestMulticuentaLoginChrome : BaseTestChrome
    {
        //Para leer los dataprovider
        ExcelRead originEntity = new ExcelRead();

        [TestMethod]
        [Description("Validar logueo con un usuario y contrasena valida")]
        public void VerificarLoginExitoso()
        {
            LoginPage login = new LoginPage(driverChrome);
            login.IniciarSesion("tauregular", "!QAZxsw2");
            MenuPage menu = new MenuPage(driverChrome);
            Assert.IsTrue(menu.ValidarUsuario("Testing Automatizado Usuario Regular (tauregular)").Contains("Testing Automatizado Usuario Regular (tauregular)"), "No es el usuario tauregular");
            menu.CerrarSesion();
            Assert.IsTrue(login.ExisteUsuario(), "No existe el campo usuario");
        }

        [TestMethod]
        [Description("Validar logueo con un usuario inexistente y contrasena invalida")]
        public void VerificarLogginFallido()
        {
            LoginPage login = new LoginPage(driverChrome);
            login.Go();
            Assert.IsTrue(login.ExisteUsuario(), "No existe el campo usuario");
            login.EnviarUsuario("cualquiera");
            Assert.IsTrue(login.ExisteContrasena(), "No existe el campo contraseña");
            login.enviarContrasena("!QAZxsw2");
            Assert.IsTrue(login.ExisteIniciarSesion(), "No existe el botón iniciar sesión");
            login.clickIniciarSesión();
            Assert.IsTrue(login.MensajeLoginFallido().Contains("Usuario o Contraseña inválidos."), "No contiene mensaje correspondiente: Usuario o contraseña invalidos");
        }

        [TestMethod]
        [Description("Validar cerrar sesion exitoso")]
        public void VerificarCerrarSesion()
        {
            //Se indica de que excel va a leer los datos de prueba
            originEntity.PopulateInCollection(@"./DataProviders/Multicuenta/Multicuenta.xlsx", "EntidadSinAprobacion");
            LoginPage login = new LoginPage(driverChrome);
            login.IniciarSesion(originEntity.ReadData(1, "Usuario"), originEntity.ReadData(1, "Password"));
            MenuPage menu = new MenuPage(driverChrome);
            menu.CerrarSesion();
            Assert.IsTrue(login.ExisteUsuario(), "No existe el campo usuario");
        }
    }
}
