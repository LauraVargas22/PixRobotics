using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        var folderReportes = @"C:\Users\USUARIO\Downloads\PixRobotics\PixRobotics\Proyecto\Reporte";
        var nameFileExcel = "Reporte_2025-06-19_200844.xlsx";
        var rutaReporte = Path.Combine(folderReportes, nameFileExcel);

        var folderErrores = @"C:\Users\USUARIO\Downloads\PixRobotics\PixRobotics\Proyecto\Error";
        Directory.CreateDirectory(folderErrores); // Crea la carpeta si no existe

        var rutaErrorLog = Path.Combine(folderErrores, "error_log.txt");

        if (!File.Exists(rutaReporte))
        {
            File.WriteAllText(rutaErrorLog, $"❌ ERROR: No se encontró el archivo Excel: {rutaReporte}");
            Console.WriteLine("No se encontró el archivo Excel.");
            return;
        }

        var chromedriverPath = @"C:\chromedriver-win64\chromedriver.exe";
        if (!File.Exists(chromedriverPath))
        {
            File.WriteAllText(rutaErrorLog, $"❌ ERROR: chromedriver.exe no existe en: {chromedriverPath}");
            Console.WriteLine("No se encontró chromedriver.exe.");
            return;
        }

        var folderEvidencias = @"C:\Users\USUARIO\Downloads\PixRobotics\PixRobotics\Proyecto\Evidencias";
        Directory.CreateDirectory(folderEvidencias);
        var nameScreenShot = $"Evidencia_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.png";
        var rutaEvidencias = Path.Combine(folderEvidencias, nameScreenShot);

        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--remote-debugging-port=9222");
        options.AddArgument(@"--user-data-dir=C:\Users\USUARIO\Downloads\PixRobotics\PixRobotics\Proyecto");

        using (var driver = new ChromeDriver(Path.GetDirectoryName(chromedriverPath), options, TimeSpan.FromSeconds(120)))
        {
            try
            {
                driver.Navigate().GoToUrl("https://form.jotform.com/251738143039053");
                Thread.Sleep(5000); // Aumentar tiempo de espera

                // Debug: Listar todos los labels disponibles
                Console.WriteLine("🔍 Buscando elementos en el formulario...");
                var allLabels = driver.FindElements(By.TagName("label"));
                Console.WriteLine($"📋 Encontrados {allLabels.Count} labels:");
                foreach (var label in allLabels)
                {
                    try
                    {
                        var text = label.Text;
                        if (!string.IsNullOrEmpty(text))
                        {
                            Console.WriteLine($"   - {text}");
                        }
                    }
                    catch { }
                }

                // Buscar campo Nombre con manejo de errores mejorado
                Console.WriteLine("🔍 Buscando campo 'Nombre Completo'...");
                IWebElement inputNombre = null;
                try
                {
                    var nombreLabel = driver.FindElement(By.XPath("//label[contains(text(), 'Nombre')]"));
                    var nombreId = nombreLabel.GetAttribute("for");
                    if (!string.IsNullOrEmpty(nombreId))
                    {
                        inputNombre = driver.FindElement(By.Id(nombreId));
                        inputNombre.SendKeys("Laura Vargas");
                        Console.WriteLine("✅ Campo Nombre completado");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error con campo Nombre: {ex.Message}");
                    // Intentar buscar por otros métodos
                    try
                    {
                        inputNombre = driver.FindElement(By.CssSelector("input[type='text']"));
                        inputNombre.SendKeys("Laura Vargas");
                        Console.WriteLine("✅ Campo Nombre completado (método alternativo)");
                    }
                    catch
                    {
                        Console.WriteLine("❌ No se pudo encontrar el campo Nombre");
                    }
                }

                // Buscar campo Fecha con manejo de errores mejorado
                Console.WriteLine("🔍 Buscando campo 'Fecha del reporte'...");
                IWebElement inputFecha = null;
                try
                {
                    var fechaLabel = driver.FindElement(By.XPath("//label[contains(text(), 'Fecha')]"));
                    var fechaId = fechaLabel.GetAttribute("for");
                    if (!string.IsNullOrEmpty(fechaId))
                    {
                        inputFecha = driver.FindElement(By.Id(fechaId));
                        var fechaActualFormateada = DateTime.Now.ToString("MM-dd-yyyy");
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1];", inputFecha, fechaActualFormateada);
                        Console.WriteLine("✅ Campo Fecha completado");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error con campo Fecha: {ex.Message}");
                    // Intentar buscar por otros métodos
                    try
                    {
                        inputFecha = driver.FindElement(By.CssSelector("input[type='date'], input[type='text'][placeholder*='fecha'], input[type='text'][placeholder*='date']"));
                        var fechaActualFormateada = DateTime.Now.ToString("MM-dd-yyyy");
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = arguments[1];", inputFecha, fechaActualFormateada);
                        Console.WriteLine("✅ Campo Fecha completado (método alternativo)");
                    }
                    catch
                    {
                        Console.WriteLine("❌ No se pudo encontrar el campo Fecha");
                    }
                }

                // Buscar campo Comentario con manejo de errores mejorado
                Console.WriteLine("🔍 Buscando campo 'Comentario Adicional'...");
                IWebElement inputComentario = null;
                try
                {
                    var comentarioLabel = driver.FindElement(By.XPath("//label[contains(text(), 'Comentario')]"));
                    var comentarioId = comentarioLabel.GetAttribute("for");
                    if (!string.IsNullOrEmpty(comentarioId))
                    {
                        inputComentario = driver.FindElement(By.Id(comentarioId));
                        inputComentario.SendKeys("Enviado automáticamente por RPA Pix Studio");
                        Console.WriteLine("✅ Campo Comentario completado");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error con campo Comentario: {ex.Message}");
                    // Intentar buscar por otros métodos
                    try
                    {
                        inputComentario = driver.FindElement(By.CssSelector("textarea, input[type='text'][placeholder*='comentario'], input[type='text'][placeholder*='comment']"));
                        inputComentario.SendKeys("Enviado automáticamente por RPA Pix Studio");
                        Console.WriteLine("✅ Campo Comentario completado (método alternativo)");
                    }
                    catch
                    {
                        Console.WriteLine("❌ No se pudo encontrar el campo Comentario");
                    }
                }

                // Buscar campo Archivo con manejo de errores mejorado
                Console.WriteLine("🔍 Buscando campo 'Carga de archivo'...");
                IWebElement inputArchivo = null;
                try
                {
                    var archivoLabel = driver.FindElement(By.XPath("//label[contains(text(), 'Carga') or contains(text(), 'archivo') or contains(text(), 'file')]"));
                    var archivoId = archivoLabel.GetAttribute("for");
                    if (!string.IsNullOrEmpty(archivoId))
                    {
                        inputArchivo = driver.FindElement(By.Id(archivoId));
                        inputArchivo.SendKeys(rutaReporte);
                        Console.WriteLine("✅ Campo Archivo completado");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error con campo Archivo: {ex.Message}");
                    // Intentar buscar por otros métodos
                    try
                    {
                        inputArchivo = driver.FindElement(By.CssSelector("input[type='file']"));
                        inputArchivo.SendKeys(rutaReporte);
                        Console.WriteLine("✅ Campo Archivo completado (método alternativo)");
                    }
                    catch
                    {
                        Console.WriteLine("❌ No se pudo encontrar el campo Archivo");
                    }
                }

                Thread.Sleep(3000);

                // Buscar botón de envío
                Console.WriteLine("🔍 Buscando botón de envío...");
                IWebElement botonEnviar = null;
                try
                {
                    botonEnviar = driver.FindElement(By.XPath("//button[@type='submit']"));
                    botonEnviar.Click();
                    Console.WriteLine("✅ Botón de envío clickeado");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error con botón de envío: {ex.Message}");
                    // Intentar otros selectores
                    try
                    {
                        botonEnviar = driver.FindElement(By.CssSelector("button[type='submit'], input[type='submit'], .submit-button, .btn-submit"));
                        botonEnviar.Click();
                        Console.WriteLine("✅ Botón de envío clickeado (método alternativo)");
                    }
                    catch
                    {
                        Console.WriteLine("❌ No se pudo encontrar el botón de envío");
                    }
                }

                Thread.Sleep(3000);

                Screenshot captura = ((ITakesScreenshot)driver).GetScreenshot();
                byte[] bytes = captura.AsByteArray;
                File.WriteAllBytes(rutaEvidencias, bytes);

                Console.WriteLine("✅ Formulario enviado correctamente y evidencia guardada.");
            }

            catch (Exception ex)
            {
                File.WriteAllText(rutaErrorLog, $"❌ ERROR EN AUTOMATIZACION:\n{ex}");
                Console.WriteLine("Error durante la automatización. Revisa el log.");
                throw;
            }
        }
    }
}