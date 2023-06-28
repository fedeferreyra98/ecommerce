### ecommerce

### Tabla de Contenidos 
- [Introduccion](#introduccion)
- [Objetivo](#objetivo)
- [Requerimientos](#requerimientos)
- [Estructura](#estructura)
- [Utilidades](#utilidades)


### Introduccion <a name="introduccion"></a>
App de e-commerce realizado como trabajo practico para la materia Ingenieria de Datos II de la facultad de Ingenieria en UADE

Esta aplicacion busca demostrar los conocimientos obtenidos durante la cursada, los cuales consisten en las diferentes implementaciones posibles de base de datos SQL y NoSQL

Las utilizadas en este caso son Redis, MongoDb y Microsoft SQL Server

### Objetivo <a name="objetivo"></a>
#Construir una aplicación para gestionar la creación de pedidos registrando:
  - Los datos de los usuarios que realizan pedidos.
  - Los carritos de compras.
  - La conversión de esos carritos de compras en pedidos.
  - La finalización de esos pedidos y su conversión a facturas.
  - La registración de los pagos de las facturas.
  - La imputación de los pagos y las facturas a los usuarios con cuenta corriente.

### Requerimientos <a name="requerimientos"></a>
- Definir los modelos de BD más adecuados para el desarrollo de la aplicación, justificando sus elecciones.
- Modelar y construir las BD necesarios para el desarrollo de la solución (debe entregar de cada BD utilizada el modelo físico de la estructura).
- Crear una aplicación que permita realizar las siguientes acciones:
    1.	Guardar y recuperar la sesión del usuario conectado junto con su actividad. Nos interesa saber el nombre, la dirección y el documento de identidad.
    2.	Registrar la actividad de los usuarios para determinar su categorización. Los usuarios que se conectan más de 240 minutos al día son usuarios TOP, los que se conectan entre 240 y 120 minutos usuarios MEDIUM y los de menos de 120 minutos usuarios LOW.
    3.	Gestionar los productos seleccionados junto con la cantidad de este en un carrito de compras (agregar, eliminar o cambiar productos y/o cantidades).
    4.	Guardar y recuperar o volver a estados anteriores en las acciones realizadas sobre un carrito de compras activo.
    5.	Convertir el contenido del carrito de compras en un pedido (indicando el contenido de este, los datos del cliente (nombre, apellido, dirección, condición ante el IVA, etc.) el importe de los artículos, sus descuentos y los impuestos según su condición.
    6.	Facturar el pedido y registrar el pago indicando la forma del pago (efectivo, tarjeta, cta. cte., etc.).
    7.	Llevar el control de todas las operaciones de facturación y de pagos realizadas por los usuarios, indicando el medio, operador interviniente (si lo hubiera), fecha, hora y monto. 
    8.	Una operación de pago cubre facturas completas, aunque no necesariamente consecutivas.
    9.	Llevar un catálogo de los productos con su descripción, fotos, comentarios, videos explicativos o publicitarios y toda información que se considere de interés a fin de hacer más atractiva la experiencia de compra.
    10.	Se debe mantener una lista de precios actualizada para las ventas. 
    11.	Llevar un registro de todas las actividades realizadas sobre el contenido del catálogo de los productos, indicando el valor anterior, el nuevo valor y el operador de los cambios realizados (se deben registrar cualquier cambio realizado, desde un precio hasta una imagen o video). 

### Estructura <a name="estructura"></a>

Vamos a repasar (_de la mano de chatgpt_) las estructuras y los patrones de diseño que utilizados en el proyecto:

1. **Patrón MVC (Modelo-Vista-Controlador)**: Este patrón es comúnmente usado en el desarrollo de aplicaciones. Separa la lógica de la aplicación en tres componentes interconectados:

   - **Modelo (Model)**: Representa los datos y la lógica de negocio de la aplicación. En nuestro caso, las clases en el directorio `Model`.

   - **Vista (View)**: Presenta los datos al usuario. En nuestro caso es una representación en consola.

   - **Controlador (Controller)**: Maneja la interacción del usuario, trabajando con el modelo y la vista. En nuestro caso, `Controller` es el directorio donde estan las clases encargadas de esto.

2. **Patrón Repositorio (Repository)**: Este patrón se usa para abstraer el acceso a los datos, de tal manera que se pueda cambiar la fuente de datos sin modificar la lógica de negocio. En nuestro caso, las clases `Repository` serían nuestro repositorio.

3. **Inyección de Dependencias (Dependency Injection)**: Este patrón se usa para reducir el acoplamiento entre clases y mover la responsabilidad de manejar las dependencias hacia el contenedor. En nuestro caso, `Service` depende de `Repository`. En un escenario real, podrías usar un contenedor de inyección de dependencias para inyectar esta dependencia en tiempo de ejecución.

4. **Patrón Singleton**: Este patrón restringe la instanciación de una clase a un único objeto. En nuestro caso, se podría utilizar para la conexión a Redis/MongoDb/Sql en `EntidadCualquieraRepository`, garantizando que solo existe una única conexión a Redis en toda la aplicación.

Cada uno de estos patrones se aplica para resolver problemas específicos de diseño y estructura en el desarrollo de software, permitiendo que el código sea más reutilizable, mantenible y fácil de entender.

### Utilidades <a name="utilidades"></a>

- [Curso Git en 15 minutos](https://www.youtube.com/watch?v=vlCXdvcgiE0) (para usar via consola)
- [Curso Github Desktop ](https://www.youtube.com/watch?v=UISDyE9KMlI)
- [Curso Completo de Git](https://www.youtube.com/watch?v=HiXLkL42tMU)
- [Que es MVC?](https://www.youtube.com/watch?v=m1shPjV-98U)
- [Código más Limpio con el Patrón MVC y Service Layer](https://www.youtube.com/watch?v=9-TvHe-hHeY)
- [Curso .Net Core](https://www.youtube.com/watch?v=ss61x5HLBYo&list=PLLJJqiFt6VPrSzPakVEy1_WpwqcWD1vAc)
