# LocalCommerce

En la carpeta tools/local-development se encuentran los comando para configurar las secretos (usuarios y contrase�as) en kubernetes 
para configurar los deployments en la carpeta K8S

tambien se encuentran los comandos para agregar los secretos a vault.

y el ejemplo de los elementos que se deben crear en rabbitMQ



siguientes pasos de estudio:
- Contenerizar las WEBAPIS para ser montanadas en kubernetes (actualmente se ejecutan en local)
- Crear el microservicio de autenticaci�n y autorizaci�n
- Crear Api Gateway (puede ser con contenedor o aplici�n .NET) y servicio ingress-nginx para acceder de forma externa
- Crear aplicaci�n frontend en angular