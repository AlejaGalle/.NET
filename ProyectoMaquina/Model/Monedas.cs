using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMaquina.Model
{
    public class Monedas
    {
        public int Moneda500 { get; set; }
        public int Moneda200 { get; set; }
        public int Moneda100 { get; set; }
        public int Moneda50 { get; set; }

        public Monedas(int moneda500, int moneda200, int moneda100, int moneda50)
        {
            Moneda500 = moneda500;
            Moneda200 = moneda200;
            Moneda100 = moneda100;
            Moneda50 = moneda50;
        }
    }

}
