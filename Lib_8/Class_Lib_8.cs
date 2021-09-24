using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lib_8
{
    public class Class_Lib_8
    {/// <summary>
     /// Нахождение максимума каждого столбца
     /// </summary>
     /// <param name="dmas">in int[,] dmas - массив, элементы которого нельзя изменить из-за модификатора "in"</param>
     /// <returns>allmax - результат вычислений</returns>
        public static string FindMaxOfColumn(in int[,] dmas)
        {
            string allmax = "";
            int max;
            for (int j = 0; j < dmas.GetLength(1); j++)
            {
                max = 0;
                for (int i = 0; i < dmas.GetLength(0); i++)
                {
                    if (dmas[i, j] > max) max = dmas[i, j];
                }
                allmax += max.ToString() + " ";
            }            
            return allmax;
        }
    }
}
