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
        int n = 0;
        public void Lectura()
        {
            string fichero = "";
            string contenido = string.Empty;
            try
            {
                using (StreamReader lector = new StreamReader(fichero))
                {
                    while (lector.Peek()>-1)
                    {
                        string linea = lector.ReadLine();
                        if (!String.IsNullOrEmpty(linea))
                        {
                            
                            for (int i = 0; i < linea.Length; i++)
                                
                                for (int j = 0; j < HuffmanDictionary.Count; j++)
                                {
                                    if (HuffmanDictionary.TryGetValue(linea[i], out int contador))
                                    {
                                        contador++;
                                        n++;
                                    }
                                    else
                                    {
                                        HuffmanDictionary.Add(linea[i], 1);
                                        n++;
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
            foreach (KeyValuePair<char,int> result in HuffmanDictionary)
            {
                HuffmanNodo aux;
                HuffmanNodo NuevoNodo = new HuffmanNodo(result.Key, null, null, result.Value, true);
                Registro.Add(NuevoNodo);
                for (int i = Registro.Count; i >0 ; i--)
                {
                    if (comparador(Registro[i], NuevoNodo))
                    {
                        aux = Registro[i-1];
                        Registro[i-1] = NuevoNodo;
                        Registro[i] = aux;

                    }
                    
                }
            }
        }
        public void InsertarRegistro(HuffmanNodo data) {
            HuffmanNodo aux;
            Registro.Add(data);
            for (int i = Registro.Count; i > 0; i--)
            {
                if (comparador(Registro[i], data))
                {
                    aux = Registro[i - 1];
                    Registro[i - 1] = data;
                    Registro[i] = aux;
                }

            }
        }
        public void pseudoarbol() 
        {
            InsertarDiccionario();
            int i = 0;
            ModeloHuffman = new List<HuffmanNodo>(Registro);
            while (ModeloHuffman.Count !=1)
            {
                HuffmanNodo temp = new HuffmanNodo(Convert.ToChar(i),
                    ModeloHuffman[0], ModeloHuffman[0], ModeloHuffman[1].frecuencia+ModeloHuffman[1].frecuencia, false );
                InsertarRegistro(temp);
                ModeloHuffman.RemoveAt(0);
                ModeloHuffman.RemoveAt(1);
                i++;
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
