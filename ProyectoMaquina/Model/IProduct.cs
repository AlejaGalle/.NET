using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMaquina.Model
{
   public interface IProduct
    {
        String Name { get; set; }
        int Quantity {  get; set; }
        int Price { get; set; }

        string DisplayProduct();

        void AddInventory(int quantity);



    }
}
