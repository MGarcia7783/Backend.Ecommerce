# 🛒 Backend.Ecommerce

**Backend.Ecommerce** es una **API RESTful desarrollada en C# con .NET 10**, orientada a un sistema de **comercio electrónico**, diseñada bajo principios de **Clean Architecture** y buenas prácticas de desarrollo backend.

El proyecto expone un backend moderno, estructurado y escalable, cubriendo desde el **modelado del dominio** hasta la **persistencia de datos** y la **exposición de endpoints REST**, permitiendo su integración con aplicaciones frontend o herramientas de consumo de API.


## 📄 Descripción Detallada

Este backend actúa como la **capa central de un sistema de e-commerce**, encargándose de la gestión de:

- Categorías  
- Productos  
- Pedidos  
- Detalles de pedido  
- Pagos  
- Usuarios  

La aplicación sigue los principios de **Clean Architecture**, promoviendo una clara **separación de responsabilidades**, el uso de **POCOs** para representar el dominio del negocio y el desacoplamiento de la lógica de aplicación respecto a los detalles de infraestructura.

La persistencia de datos se gestiona mediante **Entity Framework Core**, utilizando **migraciones** para el control del esquema en **SQL Server**. La configuración sensible, como la cadena de conexión, se obtiene desde **variables de entorno (.env)**, favoreciendo la seguridad y la portabilidad entre entornos.


## ⭐ Características Principales

- API RESTful para sistema de comercio electrónico  
- Gestión de categorías y productos  
- Gestión de pedidos y detalles de pedido  
- Gestión de pagos asociados a pedidos  
- Modelado del dominio mediante **POCOs**  
- Separación de responsabilidades siguiendo **Clean Architecture**  
- Persistencia de datos con **Entity Framework Core y migraciones**  
- Configuración mediante **variables de entorno (.env)**  
- Inyección de dependencias  
- Consumo y pruebas de la API con **Postman**


## 🛠️ Tecnologías Utilizadas

- C#  
- .NET 10  
- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- AutoMapper  
- Identity + JWT  
- Postman  


## 🚀 Empezando

Sigue estas instrucciones para obtener una copia del proyecto en tu máquina local para desarrollo y pruebas.


## 📋 Pre-requisitos

Asegúrate de tener instalado:

- .NET SDK 10  
- SQL Server  
- SQL Server Management Studio (opcional)  
- Postman  


## 🛠️ Instalación

📥 Clona el repositorio:

```bash
git clone https://github.com/MGarcia7783/Backend.Ecommerce.git
```

⚙ Configura las variables de entorno en el archivo .env

```env
DB_SERVER=localhost
DB_DATABASE=EcommerceDb
DB_USER=TuUsuario
DB_PASSWORD=TuPassword
```

🗄️ Crear la base de datos y aplicar migraciones
```bash
Add-Migration "Título para tu migración"
Update-Database
```
