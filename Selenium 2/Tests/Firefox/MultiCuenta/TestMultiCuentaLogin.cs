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
    public class TestMultiCuentaLoginFirefox : BaseTestFirefox
    {

        [TestMethod]
        [Description("Validar logueo con un usuario y contrasena valida")]
        public void VerificarLoginExitoso()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("tauregular", "!QAZxsw2");

            MenuPage menu = new MenuPage(driverFirefox);
            Assert.IsTrue(menu.ValidarUsuario("Testing Automatizado Usuario Regular (tauregular)").Contains("Testing Automatizado Usuario Regular (tauregular)"), "No es el usuario tauregular");
            menu.CerrarSesion();
            Assert.IsTrue(login.ExisteUsuario(), "No existe el campo usuario");
        }

        [TestMethod]
        [Description("Validar logueo con un usuario inexistente y contrasena valida")]
        public void VerificarLogginFallido()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("cualquiera", "!QAZxsw2");

            Assert.IsTrue(login.MensajeLoginFallido().Contains("Usuario o Contraseña inválidos."), "No contiene mensaje correspondiente: Usuario o contraseña invalidos");
        }

        [TestMethod]
        [Description("Validar cerrar sesion exitoso")]
        public void VerificarCerrarSesion()
        {
            LoginPage login = new LoginPage(driverFirefox);
            login.IniciarSesion("tauregular", "!QAZxsw2");

            MenuPage menu = new MenuPage(driverFirefox);
            menu.CerrarSesion();
            Assert.IsTrue(login.ExisteUsuario(), "No existe el campo usuario");
        }
    }
}
