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
        List<HuffmanNodo> ModeloHuffman = new List<HuffmanNodo>();
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
    }
}
