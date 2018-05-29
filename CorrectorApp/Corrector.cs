using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CorrectorApp
{
    public class Corrector
    {
        private static List<String> diccionario = File.ReadAllLines(@"diccionario.txt").ToList();
        private static Dictionary<String, double> frec_unigramas = new Dictionary<String, double>();
        private static Dictionary<String, double> frec_bigramas = new Dictionary<String, double>();
        private static Dictionary<String, int> conteo_unigramas = new Dictionary<String, int>();
        private static Dictionary<String, int> conteo_bigramas = new Dictionary<String, int>();
        private static Dictionary<String, int> conteo_uni_qgramas = new Dictionary<String, int>();
        private static Dictionary<String, int> conteo_bi_qgramas = new Dictionary<String, int>();
        private static int[,] matriz_inserciones = new int[43, 43];
        private static int[,] matriz_eliminaciones = new int[43, 43];
        private static int[,] matriz_sustituciones = new int[43, 43];
        private static int[,] matriz_transposiciones = new int[43, 43];
        private readonly List<String> encabezado_matriz = new List<String>{ "0", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "á", "é", "í", "ó", "ú", "ñ", "ü", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Corrector()
        {
            CargarFrecuencias();
            CargarConteos();
            CargarMatrices();
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
        /// <param name="palabraError"></param>
        public List<Tuple<String, String, String, int>> PalabrasDistancia1(String palabraError)
        {
            // Tuple<Palabra con error, Palabra candidata, Operacion, Posicion>
            List<Tuple<String, String, String, int>> palabras = new List<Tuple<String, String, String, int>>();
            int middle = diccionario.Count / 2;//mitad del archivo
            List<Tuple<String, String, String, int>> dist1_1 = new List<Tuple<String, String, String, int>>();
            List<Tuple<String, String, String, int>> dist1_2 = new List<Tuple<String, String, String, int>>();

            BackgroundWorker bw1 = new BackgroundWorker();
            Boolean finishedBW1 = false;
            bw1.DoWork += (senderbw1, arg) =>
            {
                //busca palabras distancia 1 en la primera mitad
                for (int i = 0; i < middle; ++i)
                {
                    String palabraDistancia1 = diccionario[i];
                    if (EditDistance(palabraError, palabraDistancia1) == 1)
                    {
                        String operacion = "";
                        int posicionDiferencia = 0;
                        if (palabraError.Count() > palabraDistancia1.Count())
                        {
                            operacion = "Insercion";
                            posicionDiferencia = palabraError.Zip(palabraDistancia1, (c1, c2) => c1 == c2).TakeWhile(b => b).Count();
                        }
                        else if (palabraError.Count() < palabraDistancia1.Count())
                        {
                            operacion = "Eliminacion";
                            posicionDiferencia = palabraError.Zip(palabraDistancia1, (c1, c2) => c1 == c2).TakeWhile(b => b).Count();
                        }
                        else
                        {
                            posicionDiferencia = palabraError.Zip(palabraDistancia1, (c1, c2) => c1 == c2).TakeWhile(b => b).Count();

                            if (posicionDiferencia != palabraError.Count() - 1)
                            {
                                operacion = (palabraError[posicionDiferencia] == palabraDistancia1[posicionDiferencia + 1]
                                        && palabraError[posicionDiferencia + 1] == palabraDistancia1[posicionDiferencia])
                                            ? "Transposicion"
                                            : "Sustitucion";
                            }
                            else
                            {
                                operacion = "Sustitucion";
                            }
                        }

                        var tupla = new Tuple<String, String, String, int>(palabraError, palabraDistancia1, operacion, posicionDiferencia);
                        dist1_1.Add(tupla);
                    }
                }
                finishedBW1 = true;
            };
            bw1.RunWorkerAsync();

            BackgroundWorker bw2 = new BackgroundWorker();
            Boolean finishedBW2 = false;
            bw2.DoWork += (senderbw2, arg) =>
            {
                //busca palabras distancia 1 en la segunda mitad
                for (int i = middle; i < diccionario.Count(); ++i)
                {
                    String palabraDistancia1 = diccionario[i];
                    if (EditDistance(palabraError, palabraDistancia1) == 1)
                    {
                        String operacion = "";
                        
                        int posicionDiferencia = 0;
                        if (palabraError.Count() > palabraDistancia1.Count())
                        {
                            operacion = "Insercion";
                            posicionDiferencia = palabraError.Zip(palabraDistancia1, (c1, c2) => c1 == c2).TakeWhile(b => b).Count();
                        }
                        else if (palabraError.Count() < palabraDistancia1.Count())
                        {
                            operacion = "Eliminacion";
                            posicionDiferencia = palabraError.Zip(palabraDistancia1, (c1, c2) => c1 == c2).TakeWhile(b => b).Count();
                        }
                        else
                        {
                            posicionDiferencia = palabraError.Zip(palabraDistancia1, (c1, c2) => c1 == c2).TakeWhile(b => b).Count();

                            if (posicionDiferencia != palabraError.Count() - 1)
                            {
                                operacion = (palabraError[posicionDiferencia] == palabraDistancia1[posicionDiferencia + 1]
                                        && palabraError[posicionDiferencia + 1] == palabraDistancia1[posicionDiferencia])
                                            ? "Transposicion"
                                            : "Sustitucion";
                            }
                            else
                            {
                                operacion = "Sustitucion";
                            }
                        }

                        var tupla = new Tuple<String, String, String, int>(palabraError, palabraDistancia1, operacion, posicionDiferencia);
                        dist1_2.Add(tupla);
                    }
                }
                finishedBW2 = true;
            };
            bw2.RunWorkerAsync();

            while (!finishedBW1 || !finishedBW2)
            { }
            palabras.AddRange(dist1_1);
            palabras.AddRange(dist1_2);

            return palabras;
        }
        
        /// <summary>
        /// Cargar los archivos de frecuencias.
        /// </summary>
        private void CargarFrecuencias()
        {
            // Frecuencias de unigramas de palabras
            var bw1 = new BackgroundWorker();
            Boolean finishedBW1 = false;
            bw1.DoWork += (senderbw1, arg) =>
            {
                var unig = File.ReadAllLines(@"frecuencias_unigramas.csv").ToList();

                if (unig != null)
                {
                    foreach (var frecUnig in unig)
                    {
                        if (!String.IsNullOrWhiteSpace(frecUnig))
                        {
                            var frecuencia = frecUnig.Split(';');

                            if (!frec_unigramas.ContainsKey(frecuencia[0]))
                            {
                                frec_unigramas.Add(frecuencia[0], Double.Parse(frecuencia[1].Replace('.', ',')));
                            }
                        }
                    }
                }
                finishedBW1 = true;
            };
            bw1.RunWorkerAsync();

            // Frecuencias de bigramas de palabras
            var bw2 = new BackgroundWorker();
            Boolean finishedBW2 = false;
            bw2.DoWork += (senderbw2, arg) =>
            {
                var big = File.ReadAllLines(@"frecuencias_bigramas.csv").ToList();

                if (big != null)
                {
                    foreach (var frecBig in big)
                    {
                        if (!String.IsNullOrWhiteSpace(frecBig))
                        {
                            var frecuencia = frecBig.Split(';');

                            if (!frec_bigramas.ContainsKey(frecuencia[0]))
                            {
                                frec_bigramas.Add(frecuencia[0], Double.Parse(frecuencia[1].Replace('.', ',')));
                            }
                        }
                    }
                }
                finishedBW2 = true;
            };
            bw2.RunWorkerAsync();

            while (!finishedBW1 || !finishedBW2)
            { }
            Console.WriteLine("Finished with unigrams and bigrams");
        }

        /// <summary>
        /// Cargar los archivos de los conteos.
        /// </summary>
        private void CargarConteos()
        {
            // Conteo de unigramas de caracteres
            var bw1 = new BackgroundWorker();
            Boolean finishedBW1 = false;
            bw1.DoWork += (senderbw1, arg) =>
            {
                var uniqg = File.ReadAllLines(@"conteos_uni_qgramas.txt").ToList();

                if (uniqg != null)
                {
                    foreach (var conteoUniqg in uniqg)
                    {
                        if (!String.IsNullOrWhiteSpace(conteoUniqg))
                        {
                            var conteo = conteoUniqg.Split(',');

                            if (!conteo_uni_qgramas.ContainsKey(conteo[0]))
                            {
                                conteo_uni_qgramas.Add(conteo[0], Int32.Parse(conteo[1]));
                            }
                        }
                    }
                }
                finishedBW1 = true;
            };
            bw1.RunWorkerAsync();

            // Conteo de bigramas de caracteres
            var bw2 = new BackgroundWorker();
            Boolean finishedBW2 = false;
            bw2.DoWork += (senderbw2, arg) =>
            {
                var biqg = File.ReadAllLines(@"conteos_bi_qgramas.txt").ToList();

                if (biqg != null)
                {
                    foreach (var conteoBiqg in biqg)
                    {
                        if (!String.IsNullOrWhiteSpace(conteoBiqg))
                        {
                            var conteo = conteoBiqg.Split(',');

                            if (!conteo_bi_qgramas.ContainsKey(conteo[0]))
                            {
                                conteo_bi_qgramas.Add(conteo[0], Int32.Parse(conteo[1]));
                            }
                        }
                    }
                }
                finishedBW2 = true;
            };
            bw2.RunWorkerAsync();

            // Conteo de unigramas de palabras
            var bw3 = new BackgroundWorker();
            Boolean finishedBW3 = false;
            bw3.DoWork += (senderbw3, arg) =>
            {
                var unig = File.ReadAllLines(@"conteos_unigramas.csv").ToList();

                if (unig != null)
                {
                    foreach (var conteoUnig in unig)
                    {
                        if (!String.IsNullOrWhiteSpace(conteoUnig))
                        {
                            var conteo = conteoUnig.Split(';');

                            if (!conteo_unigramas.ContainsKey(conteo[0]))
                            {
                                conteo_unigramas.Add(conteo[0], Int32.Parse(conteo[1]));
                            }
                        }
                    }
                }
                finishedBW3 = true;
            };
            bw3.RunWorkerAsync();

            // Conteo de bigramas de palabras
            var bw4 = new BackgroundWorker();
            Boolean finishedBW4 = false;
            bw4.DoWork += (senderbw4, arg) =>
            {
                var big = File.ReadAllLines(@"conteos_bigramas.csv").ToList();

                if (big != null)
                {
                    foreach (var conteoBig in big)
                    {
                        if (!String.IsNullOrWhiteSpace(conteoBig))
                        {
                            var conteo = conteoBig.Split(';');

                            if (!conteo_bigramas.ContainsKey(conteo[0]))
                            {
                                conteo_bigramas.Add(conteo[0], Int32.Parse(conteo[1]));
                            }
                        }
                    }
                }
                finishedBW4 = true;
            };
            bw4.RunWorkerAsync();

            while (!finishedBW1 || !finishedBW2 || !finishedBW3 || !finishedBW4)
            { }
            Console.WriteLine("Finished conteos");
        }

        /// <summary>
        /// Cargar los archivos de las matrices.
        /// </summary>
        private void CargarMatrices()
        {
            // Matriz de inserciones
            var bw1 = new BackgroundWorker();
            Boolean finishedBW1 = false;
            bw1.DoWork += (senderbw1, arg) =>
            {
                var mIns = File.ReadAllLines(@"matrices_inserciones.csv").ToList();
                var mInsCount = mIns.Count() - 1;

                for (int i = 1; i < mInsCount; i++)
                {
                    var linea = mIns[i].Split(',');
                    for (int j = 1; j < mInsCount; j++)
                    {
                        matriz_inserciones[i - 1, j - 1] = Int32.Parse(linea[j]);
                    }
                }
                finishedBW1 = true;
            };
            bw1.RunWorkerAsync();

            // Matriz de eliminaciones
            var bw2 = new BackgroundWorker();
            Boolean finishedBW2 = false;
            bw2.DoWork += (senderbw2, arg) =>
            {
                var mElim = File.ReadAllLines(@"matrices_deleciones.csv").ToList();
                var mElimCount = mElim.Count() - 1;

                for (int i = 1; i < mElimCount; i++)
                {
                    var linea = mElim[i].Split(',');
                    for (int j = 1; j < mElimCount; j++)
                    {
                        matriz_eliminaciones[i - 1, j - 1] = Int32.Parse(linea[j]);
                    }
                }
                finishedBW2 = true;
            };
            bw2.RunWorkerAsync();

            // Matriz de sustituciones
            var bw3 = new BackgroundWorker();
            Boolean finishedBW3 = false;
            bw3.DoWork += (senderbw3, arg) =>
            {
                var mSust = File.ReadAllLines(@"matrices_sustituciones.csv").ToList();
                var mSustCount = mSust.Count() - 1;

                for (int i = 1; i < mSustCount; i++)
                {
                    var linea = mSust[i].Split(',');
                    for (int j = 1; j < mSustCount; j++)
                    {
                        matriz_sustituciones[i - 1, j - 1] = Int32.Parse(linea[j]);
                    }
                }
                finishedBW3 = true;
            };
            bw3.RunWorkerAsync();

            // Matriz de transposiciones
            var bw4 = new BackgroundWorker();
            Boolean finishedBW4 = false;
            bw4.DoWork += (senderbw4, arg) =>
            {
                var mTransp = File.ReadAllLines(@"matrices_transposiciones.csv").ToList();
                var mTranspCount = mTransp.Count() - 1;

                for (int i = 1; i < mTranspCount; i++)
                {
                    var linea = mTransp[i].Split(',');
                    for (int j = 1; j < mTranspCount; j++)
                    {
                        matriz_transposiciones[i - 1, j - 1] = Int32.Parse(linea[j]);
                    }
                }
                finishedBW4 = true;
            };
            bw4.RunWorkerAsync();

            while (!finishedBW1 || !finishedBW2 || !finishedBW3 || !finishedBW4)
            { }

            Console.WriteLine("Finished with the matrices");
        }
  
        private Double ObtenerFrecuenciaBigrama(String bigrama)
        {
            return frec_bigramas.ContainsKey(bigrama) ? frec_bigramas[bigrama] : 0.4;
        }

        private Double ObtenerFrecuenciaUnigrama(String unigrama)
        {
            return frec_unigramas.ContainsKey(unigrama) ? frec_unigramas[unigrama] : 0.4;
        }

        private int ObtenerConteoBigramaLetra(String bigramaLetra)
        {
            return conteo_bi_qgramas.ContainsKey(bigramaLetra) ? conteo_bi_qgramas[bigramaLetra] : 0;
        }

        private int ObtenerConteoUnigramaLetra(String unigramaLetra)
        {
            return conteo_uni_qgramas.ContainsKey(unigramaLetra) ? conteo_uni_qgramas[unigramaLetra] : 0;
        }

        private String ObtenerCandidatoMayorProbabilidad(Dictionary<String, Double> candidatosConProbabilidad)
        {
            return candidatosConProbabilidad.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }

        private Double ObtenerProbabilidadMatriz(Tuple<String, String, String, int> tupla)
        {
            // Tuple<Palabra con error, Palabra candidata, Operacion, Posicion>
            Double prob = 0;
            int ocurrencias = 0;
            int conteoBigramaLetra = 0;
            String caracterX = "";
            String caracterY = "";

            switch (tupla.Item3)
            {
                case "Eliminacion":
                    break;
                case "Insercion":
                    break;
                case "Sustitucion":
                    break;
                case "Transposicion":
                    caracterX = tupla.Item2[tupla.Item4].ToString();
                    caracterY = tupla.Item2[tupla.Item4 + 1].ToString();

                    ocurrencias = matriz_transposiciones[encabezado_matriz.IndexOf(caracterY), encabezado_matriz.IndexOf(caracterX)];
                    conteoBigramaLetra = ObtenerConteoBigramaLetra(caracterX + caracterY);
                    prob = ocurrencias + 1 / conteoBigramaLetra;
                    break;
            }
            
            return prob;
        }
        
        public String ObtenerOracionCorregida(String oracionConErrores)
        {
            string fraseCorregida = "";
            
            List<String> oracion = new List<String>();           
            oracion = oracionConErrores.Split(' ', '\t').ToList();
            String palabra = "";

            for (int i = 0; i < oracion.Count(); i++)
            {
                palabra = oracion[i];

                if (!PalabraExiste(palabra))
                {
                    // Buscar las palabras a distancia 1 (candidatos). Tuple<Palabra, Candidato, Operacion, Posicion>
                    List <Tuple<String, String, String, int>> candidatos = PalabrasDistancia1(palabra);
                    Dictionary<String, Double> candidatosConProbabilidad = new Dictionary<String, Double>();

                    foreach (Tuple<String, String, String, int> tuplaCandidato in candidatos)
                    {
                        String candidato = tuplaCandidato.Item2;
                        String palabraAnterior = i == 0 ? "<s>" : oracion[i - 1];
                        String palabraPosterior = i == oracion.Count() - 1 ? "</s>" : oracion[i + 1];
                        String bigramaAnterior = palabraAnterior + "," + candidato;                        
                        String bigramaPosterior = candidato + "," + palabraPosterior;

                        Double probUnigramaCandidato = ObtenerFrecuenciaUnigrama(candidato);
                        Double probBigramaAnterior = ObtenerFrecuenciaBigrama(bigramaAnterior);
                        Double probBigramaPosterior = ObtenerFrecuenciaBigrama(bigramaPosterior);
                        Double probMatriz = ObtenerProbabilidadMatriz(tuplaCandidato);

                        Double probCandidato = probUnigramaCandidato * probBigramaAnterior * probBigramaPosterior * probMatriz;

                        candidatosConProbabilidad.Add(candidato, probCandidato);
                    }

                    palabra = ObtenerCandidatoMayorProbabilidad(candidatosConProbabilidad);
                }

                fraseCorregida += palabra + " ";
            }

            return fraseCorregida;
        }
    }
}
