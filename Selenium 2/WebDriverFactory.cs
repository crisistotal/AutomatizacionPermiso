using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Reflection;
using NUnit.Framework;
using System;

namespace Selenium_2
{
    public class WebDriverFactory
    {
        public IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return GetChromeDriver();
                case BrowserType.Firefox:
                    return GetFirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException("No existe explorador");
            }
        } 

        //IWebDrvier es una interfaz que busca en el escritorio por defecto de, las ref erencias del proyecto, el ChromeDriver.exe
        //Este metodo busca la localizaicon del Chrome driver y devuelve un objeto ChromeDriver() con parametro de localizacion
        private IWebDriver GetChromeDriver()
        {
            var directorio = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", @"D:\Test");
            return new ChromeDriver(directorio,options);
        }

        //tambien se pueden agregar configuraciones previas par el explorador agregandolas como parametro en FirefoxDriver

        //Todo: Quitar opciones que no hagan falta
        private IWebDriver GetFirefoxDriver()
        {
            var directorio = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            FirefoxOptions options = new FirefoxOptions();
            #region Opciones Utiles
            options.SetPreference("browser.download.folderList", 2);
            options.SetPreference("browser.download.dir", @"D:\Test");
            options.SetPreference("browser.download.manager.alertOnEXEOpen", false);
            options.SetPreference("browser.helperApps.neverAsk.saveToDisk",
                  "application/msword, application/csv, application/ris, text/csv, image/png, application/pdf, text/html, text/plain, application/zip, application/x-zip, application/x-zip-compressed, application/download, application/octet-stream");
            options.SetPreference("browser.download.manager.showWhenStarting", false);
            options.SetPreference("browser.download.manager.focusWhenStarting", false);
            options.SetPreference("browser.download.useDownloadDir", true);
            options.SetPreference("browser.helperApps.alwaysAsk.force", false);
            options.SetPreference("browser.download.manager.alertOnEXEOpen", false);
            options.SetPreference("browser.download.manager.closeWhenDone", true);
            options.SetPreference("browser.download.manager.showAlertOnComplete", false);
            options.SetPreference("browser.download.manager.useWindow", false);
            options.SetPreference("services.sync.prefs.sync.browser.download.manager.showWhenStarting", false);
            options.SetPreference("pdfjs.disabled", true);

            #endregion
            return new FirefoxDriver(directorio, options);
        }
    }
}
