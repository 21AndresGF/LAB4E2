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
        public void InsertarDiccionario(string archivo)
        {
            fichero = archivo;
            Lectura();
            foreach (KeyValuePair<char, int> result in HuffmanDictionary)
            {
                Registro.Add(new HuffmanNodo(result.Key, null, null, result.Value, true));
                HuffmanNodo aux = Registro[Registro.Count - 1];
                for (int x = Registro.Count - 1; x >= 0; x--)
                {
                    if (x > 0)
                    {
                        if (Registro[x - 1].frecuencia > Registro[x].frecuencia)
                        {
                            aux = Registro[x - 1];
                            Registro[x - 1] = Registro[x];
                            Registro[x] = aux;

                        }
                    }
                }
            }
        }
        public void InsertarRegistro() {
            HuffmanNodo aux;
            for (int j = ModeloHuffman.Count - 1; j >= 0; j--)
            {
                if (j > 0)
                {
                    if (ModeloHuffman[j - 1].frecuencia > ModeloHuffman[j].frecuencia)
                    {
                        aux = ModeloHuffman[j - 1];
                        ModeloHuffman[j - 1] = ModeloHuffman[j];
                        ModeloHuffman[j] = aux;

                    }
                }
            }
        }
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
        public void MetodoRegistros() 
        {
            for (int t = Registro.Count - 1; t >= 0; t--)
            {
                if (!Registro[t].Nulldata)
                {
                    if (Registro[t].BinaryValue == null)
                    {
                        Registro[t].NodoDerecho.BinaryValue = "1";
                        Registro[t].NodoIzquierdo.BinaryValue = "0";
                    }
                    else
                    {
                        Registro[t].NodoDerecho.BinaryValue = Registro[t].BinaryValue + "1";
                        Registro[t].NodoIzquierdo.BinaryValue = Registro[t].BinaryValue + "0";
                    }
                }
            }
                int i = 0;
                while (Registro[i].Nulldata)
                {
                    valoresBinarios.Add(Registro[i].value, Registro[i].BinaryValue);
                    i++;
                }
        }
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

            for (int i = 0; i < subs.Count; i++)
            {
                double calculo = 0;
                for (int x = 0; x < subs[i].Length; x++)
                {
                    string str = subs[i];
                    if (str[x].Equals('1'))
                    {
                        double calculo2 = Math.Pow(2, (str.Length - 1) - x);
                        calculo += calculo2;
                    }
                }
                binarios.Add(Convert.ToInt32(calculo));
            }

        }
        public void escribir() 
        {
            string datos = null; ;
            for (int p = 0; p < binarios.Count; p++)
            {
                char c = Convert.ToChar(binarios[p]);
                datos += c;
            }
        }

    }
}
