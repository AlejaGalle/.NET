
using ProyectoMaquina.Control;
using ProyectoMaquina.Model;
using System;


namespace ProyectoMaquina.View // Note: actual namespace depends on the project name.
{
    internal class View
    {
        static void Main(string[] args)


        {

            //aqui empieza nuestrpo programa

            Controller controller = Controller.GetInstance();



                
            string texto_bienvenida = "Bienvenido a la maquina expendedora";
            Console.WriteLine(texto_bienvenida);
            string input_cliente = "";
            string input_producto = "";
         

            while (true)
            {

                do
                {
                   Console.WriteLine("Escoja tipo de cliente: [c] o [p] (o [q] para salir )");
                   input_cliente = Console.ReadLine();

                } while (input_cliente != "C" && input_cliente != "P" && input_cliente != "Q");


                if (input_cliente == "Q")
                {
                    Console.WriteLine("Saliendo del programa...");
                    break;
                }



                Console.WriteLine("La lista de precios es : "); //to-do lista de productos

                 Console.WriteLine(controller.DisplayProductList());
               

                if (input_cliente == "C")

                {

                   Console.WriteLine("Escoja un producto de la lista ....");
                    bool valid_product = false;

                    do
                    {
                       input_producto = Console.ReadLine();

                       valid_product = controller.ProductExists(input_producto) && controller.ProductHasInventory(input_producto);
                       
                        if(valid_product ==false)
                        {
                            Console.WriteLine("Escoja un producto valido");
                        }
                    } while (!valid_product);


                    Console.WriteLine($"Ingrese la cantidad de productos que desea comprar (disponibles: {controller.GetProductInventory(input_producto)}) :");
                    int cantidadDeseada = 0;

                    while (cantidadDeseada <= 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out cantidadDeseada) && cantidadDeseada > 0)
                        {
                            if (cantidadDeseada > controller.GetProductInventory(input_producto))
                            {
                                Console.WriteLine("La cantidad deseada es mayor que el inventario disponible. Ingrese una cantidad válida.");
                                cantidadDeseada = 0;
                            }
                            else
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Ingrese una cantidad válida (mayor que cero).");

                    }

                    int precioProducto = controller.ObtenerPrecioProducto(input_producto);
                    

                    Console.WriteLine($"Ingrese {precioProducto * cantidadDeseada} en billetes para el pago de {cantidadDeseada} producto(s)");
                    int suma_billetes = 0;

                   while (true)

                   {
                      Console.WriteLine("Ingrese el billete ....");

                        try
                        {
                            suma_billetes += Convert.ToInt32(Console.ReadLine());
                        }

                        catch (FormatException e)
                        {
                            Console.WriteLine($"Porfavor ingrese un dato con un valor numerico; {e.Message }");
                        }
                     

                      Console.WriteLine("Para dejar de ingresar billetes, escriba [STOP] de lo contrario presione ENTER");

                      string input_cash = Console.ReadLine();

                      if (input_cash == "STOP")
                      {
                         break;
                      }

                   }


                    if (suma_billetes >= precioProducto * cantidadDeseada)
                    {
                        string productoComprado = controller.ObtenerNombreProducto(input_producto);


                        Console.WriteLine($"Compra exitosa. Producto(s) comprado(s): {productoComprado} (x{cantidadDeseada})");


                        int precioTotal = precioProducto * cantidadDeseada;
                        Dictionary<int, int> cambioMonedas = controller.CalcularCambio(precioTotal, suma_billetes);

                        Console.WriteLine("Cambio a entregar:");
                        foreach (var kvp in cambioMonedas)
                        {
                            Console.WriteLine($"Monedas de {kvp.Key}: {kvp.Value}");
                        }

                        controller.DecreaseInventory(input_producto, cantidadDeseada);
                    }
                    else
                    {
                       
                        Console.WriteLine("El cliente no ha ingresado suficiente dinero para realizar la compra.");
                    }

                }
                else if (input_cliente == "P")
                {
                    Console.WriteLine("¿Desea ingresar un nuevo producto (N), rellenar inventario de un producto existente (R) o buscar un producto por nombre () ?");
                    string proveedorInput = Console.ReadLine().ToUpper();

                    if (proveedorInput == "N")
                    {
                        Console.WriteLine("Ingrese el nombre del nuevo producto:");
                        string producto_name = Console.ReadLine();

                        Console.WriteLine("Ingrese el valor del nuevo producto:");
                        int valorProducto = Convert.ToInt32(Console.ReadLine()); 

                        Console.WriteLine("Ingrese la cantidad inicial en inventario:");
                        int cantidadInventario = Convert.ToInt32(Console.ReadLine());

                        if (controller.ProductExists(producto_name))
                        {
                            Console.WriteLine($"El producto '{producto_name}' ya existe en la lista. ¿Desea rellenar el inventario de este producto (R)? (S/N)");
                            string respuesta = Console.ReadLine().ToUpper();

                            if (respuesta == "S")
                            {
                                Console.WriteLine($"Ingrese la cantidad para rellenar el inventario del producto '{producto_name}':");
                                int cantidadRellenar = Convert.ToInt32(Console.ReadLine());

                                controller.RefillInventory(producto_name, cantidadRellenar);

                                Console.WriteLine($"Inventario del producto '{producto_name}' rellenado con {cantidadRellenar} unidades.");
                            }
                            else if (respuesta == "N")
                            {
                                Console.WriteLine($"Producto '{producto_name}' no agregado, ya existe en la lista.");
                            }
                            else
                            {
                                Console.WriteLine("Respuesta no válida. Volviendo al menú principal.");
                            }
                        }
                        else
                        {
                            controller.AddProduct(producto_name, valorProducto, cantidadInventario);
                            Console.WriteLine($"Producto '{producto_name}' agregado exitosamente con {cantidadInventario} en inventario.");
                        }
                    }
                    else if (proveedorInput == "R")
                    {
                        Console.WriteLine("Ingrese el nombre del producto para rellenar el inventario:");
                        string producto_name = Console.ReadLine();

                        if (controller.ProductExists(producto_name: producto_name))
                        {
                            Console.WriteLine($"Ingrese la cantidad para rellenar el inventario del producto '{producto_name}':");
                            int cantidadRellenar = Convert.ToInt32(Console.ReadLine());


                            Console.WriteLine($"Ingrese el nuevo precio del producto '{producto_name}':");
                            int nuevoPrecio = Convert.ToInt32(Console.ReadLine());

                            controller.RefillInventory(producto_name, cantidadRellenar);
                            controller.SetProductPrice(producto_name, nuevoPrecio);

                            Console.WriteLine($"Inventario del producto '{producto_name}' rellenado con {cantidadRellenar} unidades.");
                        }

                        else
                        {
                            Console.WriteLine($"El producto '{producto_name}' no existe en la lista.");
                        }

                    }

                    else if (proveedorInput == "B")
                    {
                        Console.WriteLine("Ingrese el nombre del producto que desea buscar:");
                        string nombreBuscado = Console.ReadLine();

                        List<IProduct> productosEncontrados = controller.ListaProductos.Where(producto => producto.Name.Contains(nombreBuscado)).ToList();

                        if (productosEncontrados.Count > 0)
                        {
                            Console.WriteLine("Productos encontrados:");
                            foreach (var ListProduct in productosEncontrados)
                            {
                                Console.WriteLine(ListProduct.DisplayProduct());
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron productos con ese nombre.");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Opción no válida para proveedores.");
                    }
                }

            }

            Console.WriteLine("Presione Enter para salir del programa");
            Console.ReadLine();

        }


    }
}
