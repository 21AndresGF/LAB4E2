using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4E2_COMPRESION.Models
{
    public class HuffmanTree
    {

        Dictionary<char, int> HuffmanDictionary = new Dictionary<char, int>();
        Dictionary<char, string> valoresBinarios = new Dictionary<char, string>();
        List<HuffmanNodo> ModeloHuffman;
        List<HuffmanNodo> Registro = new List<HuffmanNodo>();
        List<HuffmanNodo> Conversiones = new List<HuffmanNodo>();
        List<ASCIIEncoding> subStrings = new List<ASCIIEncoding>();
        List<string> subs = new List<string>();
        List<int> binarios = new List<int>();
        string conversion = null;
        string fichero = null;

        /* metodo de lectura de archivo
         * entrada = archivo txt
         * salida = char list
         */
        public void Lectura(string archivo)
        {
            fichero = archivo;
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

        //metodo de insercion en el diccionario del archivo original
        public void InsertarDiccionario(string archivo)
        {
            foreach (KeyValuePair<char, int> result in HuffmanDictionary)
            {
                Registro.Add(new HuffmanNodo(result.Key, null, null, result.Value, true));
                HuffmanNodo aux = Registro[Registro.Count - 1];
                for (int i = Registro.Count - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        if (Registro[i - 1].frecuencia > Registro[i].frecuencia)
                        {
                            aux = Registro[i - 1];
                            Registro[i - 1] = Registro[i];
                            Registro[i] = aux;

                        }
                    }
                }
            }
        }

        //metodo de ordenamiento en el modelo huffman luego de la creacion de nodos vacios
        public void InsertarRegistro() {
            HuffmanNodo aux;
            for (int i = ModeloHuffman.Count - 1; i >= 0; i--)
            {
                if (i > 0)
                {
                    if (ModeloHuffman[i - 1].frecuencia > ModeloHuffman[i].frecuencia)
                    {
                        aux = ModeloHuffman[i - 1];
                        ModeloHuffman[i - 1] = ModeloHuffman[i];
                        ModeloHuffman[i] = aux;

                    }
                }
            }
        }

        //metodo que crea los nodos vacios del modelo de compresion de huffman
        public void pseudoarbol()
        {
            ModeloHuffman = new List<HuffmanNodo>(Registro);
            int i = 0;
            while (ModeloHuffman.Count != 1)
            {
                ModeloHuffman.Add(new HuffmanNodo(Convert.ToChar(i),
                    ModeloHuffman[1], ModeloHuffman[0], ModeloHuffman[0].frecuencia + ModeloHuffman[1].frecuencia, false));
                Registro.Add(ModeloHuffman[ModeloHuffman.Count - 1]);
                ModeloHuffman.RemoveAt(0);
                ModeloHuffman.RemoveAt(0);
                InsertarRegistro();
                i++;
            }
        }
        // metodo el cual asigna un valor al hijo derecho e izquierdo de los nodos en el modelo huffman
        public void MetodoRegistros() 
        {
            for (int i = Registro.Count - 1; i >= 0; i--)
            {
                if (!Registro[i].Nulldata)
                {
                    if (Registro[i].BinaryValue == null)
                    {
                        Registro[i].NodoDerecho.BinaryValue = "1";
                        Registro[i].NodoIzquierdo.BinaryValue = "0";
                    }
                    else
                    {
                        Registro[i].NodoDerecho.BinaryValue = Registro[i].BinaryValue + "1";
                        Registro[i].NodoIzquierdo.BinaryValue = Registro[i].BinaryValue + "0";
                    }
                }
            }
                int j = 0;
                while (Registro[j].Nulldata)
                {
                    valoresBinarios.Add(Registro[j].value, Registro[j].BinaryValue);
                    j++;
                }
        }
        //metodo el cual hace lectura de archivo para cambiarlo con su valor binario ascii
        public void ConversionLectura() 
        {
            using (StreamReader lector2 = new StreamReader(fichero))
            {
                while (lector2.Peek() > -1)
                {
                    string linea = lector2.ReadLine();
                    if (!String.IsNullOrEmpty(linea))
                    {
                        for (int e = 0; e < linea.Length; e++)
                        {
                            foreach (HuffmanNodo item in Conversiones)
                            {
                                if (linea[e] == item.value)
                                {
                                    conversion += item.BinaryValue;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        // metodo el cual separa los valoros de 8 bits
        public void separador() 
        {
            bool bandera2 = true;
            while (conversion.Length != 0)
            {
                subs.Add(conversion.Substring(0, 8));
                string aux = conversion.Substring(8, conversion.Length - 8);

                if (aux.Length >= 8)
                {
                    conversion = aux;
                }
                else
                {
                    while (aux.Length < 8 && bandera2 == true)
                    {
                        aux += "0";

                    }
                    conversion = aux;
                    bandera2 = false;

                }
            }
            CBinarioDecimal();
        }
        public void CBinarioDecimal() 
        {
            for (int i = 0; i < subs.Count; i++)
            {
                double calculo = 0;
                for (int j = 0; j < subs[i].Length; j++)
                {
                    string str = subs[i];
                    if (str[j].Equals('1'))
                    {
                        double calculo2 = Math.Pow(2, (str.Length - 1) - j);
                        calculo += calculo2;
                    }
                }
                binarios.Add(Convert.ToInt32(calculo));
            }
        }
        public string escribir() 
        {
            string datos = null; ;
            for (int i = 0; i < binarios.Count; i++)
            {
                char c = Convert.ToChar(binarios[i]);
                datos += c;
            }
            return datos;
        }

    }
}
