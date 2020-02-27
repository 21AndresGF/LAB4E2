using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB4E2_COMPRESION.Models
{
    public class HuffmanNodo
    {
        public HuffmanNodo NodoDerecho { get; set; }
        public HuffmanNodo NodoIzquierdo { get; set; }
        public char value { get; set; }
        public float frecuencia { get; set; }
    }
}
