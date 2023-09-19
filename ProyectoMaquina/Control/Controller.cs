using ProyectoMaquina.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMaquina.Control
{
    public sealed class Controller
    {

        private static Controller _instance;

        public List<IProduct> ListaProductos { get; set; }
        private Monedas _monedas;
        private Controller()
        {
            ListaProductos = new List<IProduct>();

            ListaProductos.Add(new Consumable("Coca Cola", 3200, 5));
            ListaProductos.Add(new Consumable("Milo", 9000, 1));
            ListaProductos.Add(new Consumable("Snacks", 2500, 10));
            ListaProductos.Add(new Consumable("Chocolatina", 2800, 8));
            ListaProductos.Add(new Consumable("Cerveza Aguila", 3000, 2));
            ListaProductos.Add(new Consumable("Fruco", 5000, 3));

            _monedas = new Monedas(10, 10, 10, 10);
        }



        public static Controller GetInstance()

        {
            if (_instance == null)
            {

                _instance = new Controller();
            }
            return _instance;
        }

        public string DisplayProductList()
        {
            string value = "";

            foreach (IProduct product in ListaProductos)
            {
                value += product.DisplayProduct() + '\n';
            }
            return value;
        }

        public bool ProductExists(string producto_name)
        {
            bool products_exists = false;

            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == producto_name)

                {
                    products_exists = true;
                }
            }
            return products_exists;
        }

        public bool ProductHasInventory(string producto_name)
        {
            bool has_inventory = false;
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == producto_name && product.Quantity > 0)
                {
                    has_inventory = true;
                }
            }

            return has_inventory;
        }


        public void AddProduct(string name, int price, int quantity)
        {
            ListaProductos.Add(new Consumable(name, price, quantity));
        }

        public void RefillInventory(string producto_name, int quantity)
        {
            foreach (IProduct ListProduct in ListaProductos)
            {
                if (ListProduct.Name == producto_name)
                {
                    ListProduct.AddInventory(quantity);
                    break;
                }
            }
        }

        public  Dictionary<int, int> CalcularCambio(int precioProducto, int sumaBilletes)
        {
            Dictionary<int, int> cambioMonedas = new Dictionary<int, int>();

            int[] monedas = new int[] { 500, 200, 100, 50 };

            int cambio = sumaBilletes - precioProducto;

            foreach (int moneda in monedas)
            {
                if (cambio >= moneda)
                {
                    int cantidadMonedas = cambio / moneda;
                    cambioMonedas.Add(moneda, cantidadMonedas);
                    cambio %= moneda; 
                }
                else
                {
                    cambioMonedas.Add(moneda, 0); 
                }
            }

            return cambioMonedas;
        }



        public int ObtenerPrecioProducto(string producto_name)

        {
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == producto_name)
                {
                    return product.Price;
                }
            }
            return 0;
        }

        public string ObtenerNombreProducto(string producto_name)
        {
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == producto_name)
                {
                    return product.Name;
                }
            }
            return null;



        }

        public void DecreaseInventory(string producto_name, int cantidadDeseada)
        {
            var ListaProduct = ListaProductos.FirstOrDefault(p => p.Name == producto_name);
            if (ListaProduct != null && ListaProduct.Quantity >= cantidadDeseada)
            {
                ListaProduct.Quantity -= cantidadDeseada;
            }
        }



        public int GetProductInventory(string producto_name)
        {
            var ListaProduct = ListaProductos.FirstOrDefault(p => p.Name == producto_name);
            if (ListaProduct != null)
            {
                return ListaProduct.Quantity;
            }
            return 0; 
        }

        public void SetProductPrice(string producto_name, int nuevoPrecio)
        {
            foreach (IProduct ListProduct in ListaProductos)
            {
                if (ListProduct.Name == producto_name)
                {
                    ListProduct.Price = nuevoPrecio;
                    break;
                }
            }
        }



    }
}