# 🤖 Proceso RPA: Automatización Tienda Online
Proyecto basado en el proceso RPA (Automatización Robótica de Procesos) para la automatización de tareas cotidianas que se realizan en una tienda online.

## 🚀 Objetivos del Proyecto

1. Consumo de una API pública (Fake Store API).
2. Guardar los datos originales en un archivo `.json` y subirlo a Drive.
3. Almacenar los productos estructurados en una base de datos local.
4. Generar un reporte de Excel con estadísticas clave.
5. Subir el reporte a Drive.

## 🧰 Tecnologías y Herramientas

- **PIX Studio**
- **C# / .NET**
- **MySQl** como base de datos local
- **Google Drive**

## ⚙️ Estructura del Proyecto
- [Base de Datos](./db/ddl.sql)
- [Datos JSON](./Proyecto/Json/Productos_2025-06-19_200817.json)
- [Reporte EXCEL](./Proyecto/Reporte/Reporte_2025-06-19_200844.xlsx)
- [Desarrollo Pix Studio](./Proyecto/Framework/RequestProducts.pix)
- [Credenciales Drive]

## Pasos de Ejecución:
1. Configurar la [base de datos](./db/ddl.sql)
2. Configure los orígenes de los datos [ODBC](https://youtu.be/qltVns9_3BM)
3. Configure sus credenciales de google drive y agregué el archivo en la carpeta Data.
4. Ingrese a Pix Studio:
- Configure el parámetro universal para la conexión a la base de datos:
```
@"Driver={MySQL ODBC 9.3 Unicode Driver};Server=127.0.0.1;Database=rpapixproject;UID=root;PWD={contraseña}"
```
- Modifique el archivo con las credenciales en la actividad iniciar servicio google drive
```
@"Data\{archivo_credenciales}"
```
- Cambie la ruta de la carpeta drive según corresponda en Cargar la carpeta > Id de la carpeta
5. Ejecute el programa.
6. Acceda al [google drive](https://drive.google.com/drive/folders/1hKAi4wDOWtIMyarSNdoQBj9hZnZsn3qh?usp=drive_link) para verificar el proceso.
- En la carpeta Logs encontrará el archivo .json obtenido del consumo de API.
- En la carpeta Reporte encontrará el archivo excel con las estadísticas generadas.

## 📽️ Video Explicativo

🎥 El siguiente video muestra la ejecución completa del flujo automatizado:  
📎 *[]*

## 👩‍💻 Desarrollado por

**Laura Vargas Rojas**  
Email: [lauramarianavargasrojas22@gmail.com]  
GitHub: [https://github.com/LauraVargas22](https://github.com/LauraVargas22)