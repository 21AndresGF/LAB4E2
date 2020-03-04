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
        public string BinaryValue { get; set; }
        public int frecuencia { get; set; }
        //valor bandera si es parte de diccionario
        public bool Nulldata { get; set; }
        public HuffmanNodo(char id,HuffmanNodo HDerecho, HuffmanNodo HIzquierdo, int f, bool data) {
            value = id;
            NodoDerecho = HDerecho;
            NodoIzquierdo = HIzquierdo;
            frecuencia = f;
            Nulldata = data;

        }
    }
}
