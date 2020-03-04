using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LAB4E2_COMPRESION.Models
{
    public class HuffmanTree
    {
        Dictionary<char, int> HuffmanDictionary = new Dictionary<char, int>();
        List<HuffmanNodo> ModeloHuffman;
        List<HuffmanNodo> Registro = new List<HuffmanNodo>();
        string fichero = null;
        int n = 0;
        /* metodo de lectura de archivo
         * entrada = archivo txt
         * salida = char list
         */
        public void Lectura()
        {
            string contenido = string.Empty;
            try
            {
                using (StreamReader lector = new StreamReader(fichero))
                {
                    while (lector.Peek() > -1)
                    {
                        string linea = lector.ReadLine();
                        if (!String.IsNullOrEmpty(linea))
                        {
                            for (int i = 0; i < linea.Length; i++) 
                            {
                                bool bandera = false;
                                if (HuffmanDictionary.ContainsKey(linea[i]))
                                {
                                    bandera = true;
                                }
                                if (bandera)
                                {
                                    int n = HuffmanDictionary[linea[i]];
                                    n++;
                                    HuffmanDictionary[linea[i]] = n;
                                }
                                else
                                {
                                    HuffmanDictionary.Add(linea[i], 1);
                                }
                            }    
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InsertarDiccionario()
        {
            Lectura();
            foreach (KeyValuePair<char, int> result in HuffmanDictionary)
            {
                HuffmanNodo aux;
                HuffmanNodo NuevoNodo = new HuffmanNodo(result.Key, null, null, result.Value, true);
                Registro.Add(NuevoNodo);
                for (int i = Registro.Count-1; i > 0; i--)
                {
                    if (comparador(Registro[i], NuevoNodo))
                    {
                        aux = Registro[i - 1];
                        Registro[i - 1] = NuevoNodo;
                        Registro[i] = aux;

                    }
                }
            }
        }
        public void InsertarRegistro(HuffmanNodo data) {
            HuffmanNodo aux;
            Registro.Add(data);
            for (int i = ModeloHuffman.Count; i > 0; i--)
            {
                if (comparador(ModeloHuffman[i], data))
                {
                    aux = ModeloHuffman[i - 1];
                    ModeloHuffman[i - 1] = data;
                    ModeloHuffman[i] = aux;
                }

            }
        }
        public void pseudoarbol(string archivo)
        {
            fichero = archivo;
            InsertarDiccionario();
            int i = 0;
            ModeloHuffman = new List<HuffmanNodo>(Registro);
            while (ModeloHuffman.Count != 1)
            {
                HuffmanNodo temp = new HuffmanNodo(Convert.ToChar(i),
                    ModeloHuffman[0], ModeloHuffman[1], ModeloHuffman[0].frecuencia + ModeloHuffman[1].frecuencia, false);
                InsertarRegistro(temp);
                ModeloHuffman.RemoveAt(0);
                ModeloHuffman.RemoveAt(1);
                i++;
                Registro.Add(temp);
            }
            MetodoRegistros();
        }
        public void MetodoRegistros() 
        {
            for(int i= Registro.Count-1 ; i>0 ; i--)  
            {
                HuffmanNodo aux = Registro[i];
                if (!aux.Nulldata)
                {
                    aux.NodoDerecho.BinaryValue = (aux.BinaryValue);
                    aux.NodoDerecho.BinaryValue.Add("1");
                    aux.NodoIzquierdo.BinaryValue = (aux.BinaryValue);
                    aux.NodoIzquierdo.BinaryValue.Add("0");
                }
            }
        }
        public bool comparador(HuffmanNodo valor1, HuffmanNodo valor2)
        {
            if (valor1.frecuencia > valor2.frecuencia)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
