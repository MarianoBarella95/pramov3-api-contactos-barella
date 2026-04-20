# API Gestión de Contactos - PRAMOV3 - Barella, Mariano

Actividad Obligatoria realizada para la materia Programación de Aplicaciones Móviles 3. La misma fue realizada con .NET 8, a continuación detallo el cumplimiento de las consignas:

## Características principales:
* Se crean los modelos **Contacto** y **Usuario**, con los atributos solicitados.
* Posee dos servicios: **ContactoService** y **AuthService**, para manejar contactos y usuarios en memoria, y servir la lógica de la aplicación.
* Minimal Endpoint que permite obtener y leer todos los contactos.
* Implementación de Controllers para realizar funciones de **CRUD**: Buscar por Id, crear, editar y eliminar contactos.
* Se utiliza un **TokenProvider** para separar la lógica de creación del token.
* Se protegen los endpoints de los Controllers con **[Authorize]** (Para ciertos controllers, además debe tener el rol de **"Admin"**).

## **IMPORTANTE** - Seguridad de la API
En busca de proteger la seguridad de la aplicación, se generó una secret-key en [JWT Secret Key Generator](https://jwtsecrets.com/) con una longitud de 256 bits. Esta secret-key implementada no está incluida en este repositorio, le será enviada al profesor en el mensaje adjunto. 
Para utilizarla, debe cambiar el nombre de appsettings.Example.json por appsettings.json, e incluir la secret-key.
