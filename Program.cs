using Cassandra;
using ecommerce.Cart.Core.Controllers;
using ecommerce.Cart.Core.Dtos;
using ecommerce.Cart.Core.Repositories;
using ecommerce.Cart.Core.Repositories.Interfaces;
using ecommerce.Cart.Core.Services;
using ecommerce.Cart.Core.Services.Interfaces;
using ecommerce.Commerce.Core.Controllers;
using ecommerce.Commerce.Core.DTOs;
using ecommerce.Commerce.Core.Models;
using ecommerce.Commerce.Core.Repositories;
using ecommerce.Commerce.Core.Repositories.Interfaces;
using ecommerce.Commerce.Core.Services;
using ecommerce.Commerce.Core.Services.Interfaces;
using ecommerce.DatabaseContext.Context;
using ecommerce.DatabaseContext.Context.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;
using ProductCartDTO = ecommerce.Cart.Core.Dtos.ProductCartDTO;
using UserDTO = ecommerce.Commerce.Core.DTOs.UserDTO;
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace ecommerce;

public class Program
{
    public static void Main(string[] args)
    {
        const string connectionString = "server=localhost;port=3306;database=Ecommerce;userid=appuser;pwd=uade1234;";
        var serviceProvider = new ServiceCollection()
            .AddDbContext<MySqlContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0,33)))) //Setup databases
            .AddTransient<IConnection<ISession>, CassandraDataContext>()
            .AddTransient<IConnection<IMongoDatabase>,MongoDataContext>()
            .AddTransient<IConnection<IDatabase>, RedisDataContext>()
            .AddTransient<ICatalogRepository, CatalogRepository>() //setup repositories
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddTransient<IPaymentRepository, PaymentRepository>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IUserCartRepository, UserCartRepository>()
            .AddTransient<IInvoiceRepository, InvoiceRepository>()
            .AddSingleton<ICatalogService, CatalogService>() //setup services
            .AddSingleton<IOrderService, OrderService>()
            .AddSingleton<IPaymentService, PaymentService>()
            .AddSingleton<IProductService, ProductService>()
            .AddSingleton<IUserService, UserService>()
            .AddSingleton<IUserCartService, UserCartService>()
            .AddSingleton<IInvoiceService, InvoiceService>()
            .AddTransient<UserController>() // setup controllers
            .AddTransient<OrderController>()
            .AddTransient<PaymentController>()
            .AddTransient<ProductController>()
            .AddTransient<CatalogController>()
            .AddTransient<UserCartController>()
            .AddTransient<InvoiceController>()
            .BuildServiceProvider();

        //Initialize controllers/services/repositories
        var userController = serviceProvider.GetService<UserController>();
        var orderController = serviceProvider.GetService<OrderController>();
        var paymentController = serviceProvider.GetService<PaymentController>();
        var productController = serviceProvider.GetService<ProductController>();
        var catalogController = serviceProvider.GetService<CatalogController>();
        var cartController = serviceProvider.GetService<UserCartController>();
        var invoiceController = serviceProvider.GetService<InvoiceController>();
        
        //Initialize data
        //payment methods
        const string transferencia = "Transferencia";
        const string tarjetaCredito = "Tarjeta de Credito";
        const string tarjetaDebito = "Tarjeta de Debito";
        
        static Dictionary<int,string> GetPaymentMethods()
        {
            var paymentMethods = new Dictionary<int, string>
            {
                { 1, tarjetaCredito },
                { 2, tarjetaDebito },
                { 3, transferencia }
            };
            return paymentMethods;
        }
        var productOneDto = new ProductDTO
        {
            ProductName = "Apple iPhone 13",
            ImagesURL = new List<string>() 
                {"https://ejemplo.com/iphone13_1.png","https://ejemplo.com/iphone13_2.png",},
            MainImage = "https://ejemplo.com/iphone13_1.png",
            Description = "El último smartphone de Apple con A15 Bionic, capacidad 5G y sistema de cámara mejorado.",
            Comments = new List<string>()
            {
                "¡Gran producto!", "Me encanta la nueva cámara.","Suave y rápido."
            },
            Stock = 50
        };

        var productTwoDto = new ProductDTO()
        {
            ProductName = "Samsung Galaxy S24",
            ImagesURL = new List<string>()
                { "https://ejemplo.com/galaxyS24_1.png", "https://ejemplo.com/galaxyS24_2.png" },
            MainImage = "https://ejemplo.com/galaxyS24_1.png",
            Description = "El smartphone insignia de Samsung con Snapdragon 898, 5G y cámara de 108 MP.",
            Comments = new List<string>()
            {
                "¡Pantalla asombrosa!", "La vida de la batería podría ser mejor.", "La cámara es de primera calidad."
            },
            Stock = 20
        };
        
        var productThreeDto = new ProductDTO()
        {
            ProductName = "Cuchillo Chef",
            ImagesURL = new List<string>()
                { "https://ejemplo.com/cuchilloChef_1.png", "https://ejemplo.com/cuchilloChef2.png" },
            MainImage = "https://ejemplo.com/cuchilloChef_1.png",
            Description = "Cuchillo afilado de 20cm ideal para todo tipo de alimentos",
            Comments = new List<string>()
            {
                "Increiblemente filoso", "Se siente un poco pesado pero esta bien.", "El material es de primera calidad."
            },
            Stock = 10
        };

        var userOneDto = new UserDTO()
        {
            Name = "Juan",
            LastName = "Perez",
            Address = "Av. Corrientes 123, CABA"
        };

        var userTwoDto = new UserDTO()
        {
            Name = "Ana",
            LastName = "Gomez",
            Address = "Talcahuano 234"
        };
        
        //Initialize GUID stack to keep track of cart changes
        var logCart = new Stack<Guid>();
        
        //Logica del programa

        Console.WriteLine("Bienvenido al ecommerce Grupo 3");
        
        // 1. Crear los usuarios (Correr una sola vez y luego comentar)
        
        // userController.Create(userOneDto);
        // userController.Create(userTwoDto);
        
        //Referencia para tener los usuarios registrados
        var usersRegistered = userController.GetAll();
        
        //Creo los carritos de compra de cada uno (Correr una sola vez y luego comentar)
        
        /* foreach (var userCartDto in usersRegistered.Select(user => new UserCartDTO()
                 {
                     Products = new List<ProductCartDTO>(),
                     User = new Cart.Core.Dtos.UserDTO()
                     {
                         Name = user.Name,
                         LastName = user.LastName,
                         Adress = user.Address,
                         UserId = user.Id
                     }
                 }))
        {
            cartController.Create(userCartDto);
        } */
        
        Console.WriteLine();
        // 2. Logging

        #region Logging
        // Reading username
        var username = "";
        do
        {
            Console.WriteLine("Ingrese su usuario: ");
            username = Console.ReadLine();

            if (usersRegistered.All(x => x.Name != username))
            {
                Console.WriteLine("El usuario no existe, por favor intente nuevamente");
            }
            
        } while (usersRegistered.All(x => x.Name != username));
        
        // Reading password
        var password = "";
        do
        {
            Console.Write("Ingrese su clave de acceso: ");
            password = Console.ReadLine();

            if (password != username)
            {
                Console.WriteLine("clave incorrecta, por favor intente nuevamente.");
            }

        } while (password != username);

        Console.WriteLine("Usuario logueado correctamente!");
        

        #endregion
        
        // 3. Mostrar usuario loggeado

        #region Mostrar usuario

        var loggedUser = usersRegistered.First(x => x.Name == username);
        Console.WriteLine("Acaba de ingresar el usuario:");
        Console.WriteLine($"Id: {loggedUser.Id}");
        Console.WriteLine($"Nombre: {loggedUser.Name}");
        Console.WriteLine($"Apellido: {loggedUser.LastName}");
        Console.WriteLine($"Direccion: {loggedUser.Address}");
        Console.WriteLine();

        #endregion
        
        // PAUSA
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // Se crean productos (Correr una vez y comentar):
        
        // productController.Create(productOneDto);
        // productController.Create(productTwoDto);
        // productController.Create(productThreeDto);
        var productsAdded = productController.GetAll();
        
        

        #region Agrega productos

        // 4. Agrega Producto 1
        Console.WriteLine("Se agrega producto 1: Apple Iphone 13");

        var iphoneProduct = productsAdded.First(x => x.ProductName == productOneDto.ProductName);
        catalogController.InsertProductCatalog(new ProductCatalogDTO()
        {
            AuthorId = loggedUser.Id,
            ProductId = iphoneProduct.ProductId,
            Moment = DateTime.Now,
            Price = 999
        });
        
        Console.WriteLine();
        
        // 5. Agrega Producto 2
        Console.WriteLine("Se agrega producto 2: Samsung Galaxy S24");
        var samsungProduct= productsAdded.First(x => x.ProductName == productTwoDto.ProductName);
        catalogController.InsertProductCatalog(new ProductCatalogDTO()
        {
            AuthorId = loggedUser.Id,
            ProductId = samsungProduct.ProductId,
            Moment = DateTime.Now,
            Price = 1250
        });
        
        Console.WriteLine();
        
        // 6. Agrega Producto 3
        Console.WriteLine("Se agrega producto 3: Cuchillo de Chef");
        var cuchilloProduct= productsAdded.First(x => x.ProductName == productThreeDto.ProductName);
        catalogController.InsertProductCatalog(new ProductCatalogDTO()
        {
            AuthorId = loggedUser.Id,
            ProductId = cuchilloProduct.ProductId,
            Moment = DateTime.Now,
            Price = 150
        });
        
        Console.WriteLine();
        
        //Referencio a los productos del catalogo para despues
        var catalog = catalogController.GetCatalog();
        var iphoneCatalog = catalog.First(x => x.ProductId == iphoneProduct.ProductId);
        var samsungCatalog = catalog.First(x => x.ProductId == samsungProduct.ProductId);
        var cuchilloCatalog = catalog.First(x => x.ProductId == cuchilloProduct.ProductId);
        #endregion
        
        // PAUSA
        Console.WriteLine("Presione enter para continuar");
        Console.ReadLine();
        
        // 7. Mostrar Catalogo
        Console.WriteLine("Se muestra el catalogo");
        Print(catalogController.GetCatalog());
        Console.WriteLine();

        // "Se modifica el precio del producto: "
        // 8. cambiarPrecioProducto(1)
        Console.WriteLine("Se modifica el precio del Apple Iphone 13...");
        Console.WriteLine($"Nuevo precio: $899");
        catalogController.UpdateProductCatalog(new ProductCatalog()
        {
            Id = iphoneCatalog.Id,
            AuthorId = loggedUser.Id,
            ProductId = iphoneProduct.ProductId,
            Moment = DateTime.Now,
            Price = 899
        });
        
        //Actualizo referencia del catalogo e iphone
        catalog = catalogController.GetCatalog();
        iphoneCatalog = catalog.First(x => x.ProductId == iphoneProduct.ProductId);
        
        Console.WriteLine();
        
        // 9.mostrarCatalogo(1,2,3);
        Console.WriteLine("Se muestra el catalogo");
        Print(catalogController.GetCatalog());
        Console.WriteLine();
        
        // 10.mostrarLog(valorAnterior, valorActualizado, operador);
        Console.WriteLine("Se muestra el Log del cambio realizado");
        Print(catalogController.GetCatalogLogById(iphoneProduct.ProductId));
        Console.WriteLine();
        
        // 11. "Ingreso de nuevo usuario;
        Console.WriteLine($"Se cierra la sesion de {loggedUser.Name} ...");
        loggedUser = new User();

        #region Nuevo logging usuario

        username = "";
        // Reading username
        do
        {
            Console.WriteLine("Ingrese su usuario: ");
            username = Console.ReadLine();

            if (usersRegistered.All(x => x.Name != username))
            {
                Console.WriteLine("El usuario no existe, por favor intente nuevamente");
            }
            
        } while (usersRegistered.All(x => x.Name != username));
        
        // Reading password
        password = "";
        do
        {
            Console.Write("Ingrese su clave de acceso: ");
            password = Console.ReadLine();

            if (password != username)
            {
                Console.WriteLine("clave incorrecta, por favor intente nuevamente.");
            }

        } while (password != "password123");
        
        // 12."Usuario loggeado correctamente"
        Console.WriteLine("Usuario logueado correctamente!");
        Console.WriteLine();

            #endregion
            
        // PAUSA 
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 13.mostrarUsuario(user);

        #region mostrar Usuario
        
        loggedUser = usersRegistered.First(x => x.Name == username);
        Console.WriteLine("Acaba de ingresar el usuario:");
        Console.WriteLine($"Id: {loggedUser.Id}");
        Console.WriteLine($"Nombre: {loggedUser.Name}");
        Console.WriteLine($"Apellido: {loggedUser.LastName}");
        Console.WriteLine($"Direccion: {loggedUser.Address}");
        Console.WriteLine();
        
        #endregion
        
        #region Uso del carrito

        var cart = cartController.GetUserCart(loggedUser.Id);

        var iphoneCart = new ProductCartDTO()
        {
            ProductCatalogId = iphoneCatalog.Id,
            ImageURL = iphoneProduct.MainImage,
            Price = iphoneCatalog.Price,
            ProductName = iphoneProduct.ProductName
        };
        var cuchilloCart = new ProductCartDTO()
        {
            ProductName = cuchilloProduct.ProductName,
            ImageURL = cuchilloProduct.MainImage,
            Price = cuchilloCatalog.Price,
            ProductCatalogId = cuchilloCatalog.Id
        };
        var samsungCart = new ProductCartDTO()
        {
            ProductName = samsungProduct.ProductName,
            ImageURL = samsungProduct.MainImage,
            Price = samsungCatalog.Price,
            ProductCatalogId = samsungCatalog.Id
        };
        
        // 14.Agregar 1(UN) producto 1
        Console.WriteLine($"Se agrega UN {iphoneProduct.ProductName} al carrito");
        iphoneCart.Quantity = 1;
        cart.Products.Add(iphoneCart);
        
        UpdateCart(cart, cartController, logCart);
        
        //actualizo referencia de cart
        cart = cartController.GetUserCart(loggedUser.Id);
        
        //PAUSA
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 15.Agregar 2(DOS) productos 3
        Console.WriteLine($"Se agrega DOS {cuchilloProduct.ProductName} al carrito");
        cuchilloCart.Quantity = 2;
        cart.Products.Add(cuchilloCart);
        UpdateCart(cart, cartController, logCart);
        
        //actualizo referencia de cart
        cart = cartController.GetUserCart(loggedUser.Id);

        // PAUSA 
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();

        // 16. Mostrar Carrito
        Print(cart);
        
        // 17. Eliminar UN producto 3 del carrito
        Console.WriteLine($"Se elimina UN {cuchilloProduct.ProductName} del carrito");
        cart.Products.First(x => x.ProductName == cuchilloProduct.ProductName).Quantity--;
        UpdateCart(cart, cartController, logCart);
        
        //actualizo referencia de cart
        
        cart = cartController.GetUserCart(loggedUser.Id);
        
        // PAUSA
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 18. Mostrar Carrito
        Print(cart);
        
        // 19. Agregar UN producto 2
        Console.WriteLine($"Se agrega UN {samsungProduct.ProductName} al carrito");
        samsungCart.Quantity = 1;
        cart.Products.Add(samsungCart);
        UpdateCart(cart, cartController, logCart);
        
        //actualizo referencia de cart
        cart = cartController.GetUserCart(loggedUser.Id);
        
        // PAUSA 
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 20. Mostrar Carrito;
        Print(cart);
        
        // 21. Deshacer ultimo paso
        cartController.RestoreCart(loggedUser.Id, logCart.Pop());
        
        //actualizo referencia de cart
        cart = cartController.GetUserCart(loggedUser.Id);
        
        // PAUSA --
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 22. Mostrar Carrito:
        Print(cart);

        #endregion

        // PAUSA --
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        #region Creacion del Pedido

        // 23. Checkout del carrito
        Console.WriteLine("Se confirma el carrito y se crea un pedido");
        cartController.Checkout(loggedUser.Id);

        // PAUSA
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 24. mostrarPedido();
        Console.WriteLine("Se imprime el pedido:");
        var order = orderController.GetAllOrders().First(x => x.User.Id == loggedUser.Id);
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Pedido de {order.User.Name} :");
        
        foreach (var product in order.Products)
        {
            Console.WriteLine($"Producto: {productController.GetById(product.ProductCatalog.ProductId).ProductName}");
            Console.WriteLine($"Precio: {product.ProductCatalog.Price}");
            Console.WriteLine($"Cantidad: {product.Quantity}");
        }
        Console.WriteLine($"Total: ${order.FinalPrice}");
        
        #endregion

        #region Factura y pago

        // "Se genera la factura"
        // 25. generarFactura();
        Console.WriteLine("Generando factura...");
        invoiceController.CreateInvoice(loggedUser.Id, order.OrderId);
        var factura = invoiceController.GetAll().First(x => x.OrderId == order.OrderId);
        
        // PAUSA
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 26. mostrarFactura();
        Print(factura, loggedUser);
        Console.WriteLine();
        
        // "Se realiza el pago"
        // 27. realizarPago();
        Console.WriteLine("Metodos de pago disponibles:");
        var input = 0;
        var paymentMethods = GetPaymentMethods();
        Console.WriteLine($"1. {tarjetaCredito}");
        Console.WriteLine($"2. {tarjetaDebito}");
        Console.WriteLine($"3. {transferencia}");
        do
        {
            Console.WriteLine("Elija su metodo de pago ingresando el numero correspondiente:");
            username = Console.ReadLine();

            if (usersRegistered.All(x => x.Name != username))
            {
                Console.WriteLine("El usuario no existe, por favor intente nuevamente");
            }
        } while (!paymentMethods.ContainsKey(input));
        
        paymentController.CreatePayment(order.OrderId, loggedUser.Id, paymentMethods[input]);
        
        // PAUSA
        Console.WriteLine("\nPresione enter para continuar..");
        Console.ReadLine();
        Console.WriteLine("Continua la ejecucion del programa...");
        Console.WriteLine();
        
        // 28. mostrarPagoRealizado();
        var payment = paymentController.GetAllPayments().FirstOrDefault(x => x.OrderId == order.OrderId);
        Print(payment);
        
        #endregion
    }

    private static void Print(Invoice factura, User cliente)
    {
        Console.WriteLine($"Cliente: {cliente.Name} {cliente.LastName}");
        Console.WriteLine($"Nro de Orden: {factura.OrderId}");
        Console.WriteLine($"Monto: {factura.Price}");
        Console.WriteLine($"Condicion de Iva: {factura.Iva}");
        Console.WriteLine($"Fecha de emision: {factura.Date}");
        Console.WriteLine($"Pagada: {factura.Payed}");
    }

    private static void Print(Payment payment)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Pago realizado por {payment.User.Name} {payment.User.LastName} el dia {payment.PaymentDate}.");
        Console.WriteLine($"Metodo de pago: {payment.PaymentMethod}");
        Console.WriteLine($"Monto final: {payment.FinalPrice}");
        Console.WriteLine($"Direccion de envio: {payment.User.Address}");
        Console.WriteLine("---------------------------------");
    }

    private static async void RestoreCart(UserCartController cartController, Stack<Guid> logCart, Guid loggedUserId)
    {
        
    }

    private static void Print(UserCartDTO cart)
    {   
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Carro de {cart.User.Name} :");
        foreach (var product in cart.Products)
        {
            Console.WriteLine($"Producto: {product.ProductName}");
            Console.WriteLine($"Imagen: {product.ImageURL}");
            Console.WriteLine($"Precio: {product.Price}");
            Console.WriteLine($"Cantidad: {product.Quantity}");
            Console.WriteLine($"-");
        }
    }

    private static async void UpdateCart(UserCartDTO cart, UserCartController cartController, Stack<Guid> cartLog)
    {
        var lastLogId = cartController.ChangeUserCart(cart);
        cartLog.Push(lastLogId);
    }

    private static void Print(List<ProductCatalog> catalog)
    {
        var counter = 1;
        foreach (var product in catalog)
        {
            Console.WriteLine($"Producto {counter}:");
            Print(product);
            counter++;
        }
    }
    private static void Print(ProductCatalog productCatalog)
    {
        Console.WriteLine($"Id del producto: {productCatalog.ProductId}");
        Console.WriteLine($"Id del autor: {productCatalog.AuthorId}");
        Console.WriteLine($"Fecha: {productCatalog.Moment}");
        Console.WriteLine($"Precio: {productCatalog.Price}");
        Console.WriteLine("--------------------------------");
    }
}
