## Apis de prueba (C# - .NET core 6.0) 

Esta proyecto tiene como finalidad la implementacion de una API que sera utilizada en el desarrollo de un crud de tareas 

## Instalación

Debes tener instalado en tu ordenador visual studio 2022 y la version de Sql server 2022 (Microsoft SQL Server Management Studio)

- - - - - - - - - - - - - - - -
## Dependencias - Paquetes NuGet
- - - - - - - - - - - - - - - -
Microsoft.EntityFrameworkCore 7.0.11
Microsoft.EntityFrameworkCore.SqlServer 7.0.11
Microsoft.EntityFrameworkCore.Tools 7.0.11

- - - - - - - - -
## Base de datos
- - - - - - - - -
Cadena para nueva conexion consola de administracion de paquetes(recuerda cambiar el Server con el nombre de tu servidor local):

Scaffold-DbContext "Server=DESKTOP-02388JP;Database=Crud_prueba;Trusted_Connection=True; 
Encrypt=false;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models  

Puedes crear la base de datos de esta manera, este paso es fundamental para el buen funcionamiento de la API. Debes tener en cuenta que Dias_activos es un campo calculado 
por lo cual no es necesario ingresarlo directamente.

Create database Crud_prueba 

use Crud_prueba

create table Tareas(
Codigo_tarea int not null primary key,
Mi_tarea varchar(30) not null,
Fecha_inicio date not null,
Dias_activos as datediff(day, Fecha_inicio, getdate()),
Estado varchar(20) not null
)

insert into Tareas values('1001', 'Tarea de prueba', 15/01/2024, 'Pendiente')

- Utiliza Postman para probar la Api y verificar que la conexion don la DB esta funcionando 

## Indicaciones finales

En caso de tener inconvenientes para ejecutar este proyecto, puedes configurarlo desde cero en tu entorno de desarrollo local, a continuacion sigue estos pasos:

1. Crea la base de datos con los pasos que aqui se describen
2. Crea un nuevo proyecto en visual studio 2022 ASP.NET Core Web Api
3. Escoje la compatibilidad .NET 6.0 (la demas configuracion queda igual)
4. Una vez creado el proyecto elimina el archivo WeatherForecast.cs y el controlador WeatherForecastController.cs
5. Instala los paquetes NuGet que estan descritos en este archivo en la seccion de dependencias
6. Crea una carpeta llamada Models (Importante para guardar la configuracion de la DataBase) 
7. configura la conexion a la base de datos con la cadena de conexion que esta en este archivo (escribe en el Server el nombre de tu servidor local)
8. Una vez establecida la conexion debes crear los archivos de la misma manera que estan el proyecto (un nuevo controlador y modelos) -
9. una vez creados el controlador y los modelos copia el codigo correspondiente a cada uno de ellos
10. Configura los archivos program.cs y appsettings.json para el buen funcionamiento de la Api (ten en cuenta el nombre de tu Server y el Localhost que usaras del lado del Front)


