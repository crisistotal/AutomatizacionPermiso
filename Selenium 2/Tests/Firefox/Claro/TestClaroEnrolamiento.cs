using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using System.Threading;
using Selenium_2.Pages;

namespace Selenium_2
{
    [TestClass]
    [TestCategory("Carga de Permiso para circular ")]
    public class CargaPermiso : BaseTestFirefox
    {
        //Para leer los dataprovider modo excel
        ExcelRead readExcel = new ExcelRead();

        [TestMethod]
        [Description("CargaPermiso")]
        public void TestCargaDatosPermiso()
        {
            //Leeo el excel
            readExcel.PopulateInCollection(@"./DataProviders/Permiso/Datos.xlsx");
            PermisoPage permiso = new PermisoPage(driverFirefox);

            permiso.Goto();

            permiso.clickTramite();

            Thread.Sleep(1000);

        }
    }
}
