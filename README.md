# LocalCommerce

En la carpeta tools/local-development se encuentran los comando para configurar las secretos (usuarios y contraseñas) en kubernetes 
para configurar los deployments en la carpeta K8S

tambien se encuentran los comandos para agregar los secretos a vault.

y el ejemplo de los elementos que se deben crear en rabbitMQ



siguientes pasos de estudio:
- Contenerizar las WEBAPIS para ser montanadas en kubernetes (actualmente se ejecutan en local)
- Crear el microservicio de autenticación y autorización
- Crear Api Gateway (puede ser con contenedor o aplición .NET) y servicio ingress-nginx para acceder de forma externa
- Crear aplicación frontend en angular