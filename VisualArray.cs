using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    static class VisualArray                                    //класс для матрицы
    {
        public static DataTable ToDataTable<T>(this T[,] matr)
        {
            var res = new DataTable();
            for (int i = 0; i < matr.GetLength(1); i++)
            {

                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            for (int i = 0; i < matr.GetLength(0); i++)
            {

                var row = res.NewRow();
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    row[j] = matr[i, j];

                }
                res.Rows.Add(row);
            }
            return res;
        }

    }

}
