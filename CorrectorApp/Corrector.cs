using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CorrectorApp
{
    public class Corrector
    {
        private static List<String> diccionario = System.IO.File.ReadAllLines(@"diccionario.txt").ToList();

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Corrector()
        {

        }

        public List<String> LimpiarTexto(List<String> corpus)
        {
            //Poner todo el minusculas
            for (int i = 0; i < corpus.Count; ++i)
            {
                //remover los url
                corpus[i] = Regex.Replace(corpus[i], @"(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$", "");

                corpus[i] = corpus[i]
                    .ToLower()
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace("-", "")
                    .Replace("_", "")
                    .Replace("@", "")
                    .Replace("¡", "")
                    .Replace("!", "")
                    .Replace("¿", "")
                    .Replace("?", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace("=", "")
                    .Replace("+", "")
                    .Replace("%", "")
                    .Replace("$", "")
                    .Replace("~", "")
                    .Replace("'", "")
                    .Replace("*", "")
                    .Replace(@"\", "")
                    .Replace("/", "")
                    .Replace("& quot;", "")
                    .Replace("&quot;", "")
                    .Replace("&quot", "")
                    ;
                //Console.WriteLine(corpus[i]);
            }

            return corpus;
        }

        /// <summary>
        /// Code taken from https://gist.github.com/wickedshimmy/449595/ec5535d8d967741e64af5a1a5c843f34eed49381
        /// Under MIT License
        /// </summary>
        /// <param name="original"></param>
        /// <param name="modified"></param>
        /// <returns>The edit distance between the two words provided</returns>
        public int EditDistance(String original, String modified)
        {
            int len_orig = original.Length;
            int len_diff = modified.Length;

            var matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    var vals = new int[] {
                        matrix[i - 1, j] + 1,
                        matrix[i, j - 1] + 1,
                        matrix[i - 1, j - 1] + cost
                    };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];
        }

        public Boolean PalabraExiste(String palabra)
        {
            return diccionario.Contains(palabra);
        }

        /// <summary>
        /// buscar las palabras que esten en el diccionario y que esten a disctancia de edicion 1
        /// </summary>
        /// <param name="palabra"></param>
        public List<String> PalabrasDistancia1(String palabra)
        {
            List<String> palabras = new List<String>();
            int middle = diccionario.Count / 2;//mitad del archivo
            List<String> dist1_1 = new List<String>();
            List<String> dist1_2 = new List<String>();

            BackgroundWorker bw1 = new BackgroundWorker();
            bw1.DoWork += (senderbw1, arg) =>
            {
                //busca palabras distancia 1 en la primera mitad
                for (int i = 0; i < middle; ++i)
                {
                    if (EditDistance(palabra, diccionario[i]) == 1)
                    {
                        //palabra a distancia 1
                        dist1_1.Add(diccionario[i]);
                    }
                }
            };
            bw1.RunWorkerAsync();

            BackgroundWorker bw2 = new BackgroundWorker();
            bw2.DoWork += (senderbw2, arg) =>
            {
                //busca palabras distancia 1 en la segunda mitad
                for (int i = middle; i < diccionario.Count(); ++i)
                {
                    if (EditDistance(palabra, diccionario[i]) == 1)
                    {
                        //palabra a distancia 1
                        dist1_2.Add(diccionario[i]);
                    }
                }
            };
            bw2.RunWorkerAsync();

            while (bw2.IsBusy || bw1.IsBusy)
            { }
            palabras.Concat(dist1_1);
            palabras.Concat(dist1_2);

            return palabras;
        }

        public Boolean BuscarEnDiccionario(String palabra, int inicio, int fin)
        {
            return false;
        }
    }
}
