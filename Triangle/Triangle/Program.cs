using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle triangle = new Triangle();
            List<int[]> inputTriangle = triangle.CreateTriangle_FromText();
            int sum = triangle.CalculateMaxSum(inputTriangle);
            Console.WriteLine(sum);
            Console.Read();
        }
    }

    class Triangle
    {
        public List<int[]> CreateTriangle_FromText()
        {
            List<int[]> result=new List<int[]>();
            string path= Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + @"\InputTriangleFile.txt";
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                string line;
                while ((line=reader.ReadLine())!=null)
                {
                    result.Add(line.Split().Select(x=>int.Parse(x)).ToArray());
                }
            }
                return result;
        }
        public int CalculateMaxSum(List<int[]> tr)
        {
            for (int i= tr.Count-1; i>=1;i--)
            {
                for (int j = 0; j < tr[i].Length-1; j++)
                {
                    tr[i - 1][j] = Compare(tr[i].ElementAtOrDefault(j-1)
                        + tr[i - 1][j]
                        , tr[i].ElementAtOrDefault(j)+ tr[i - 1][j]
                        , tr[i].ElementAtOrDefault(j+1)+ tr[i - 1][j]);
                }
            }
            return tr[0][0];
        }

        public static int Compare(int a,int b,int c)
        {
            return a >= b && a >= c ?a: b >= c ? b : c;
        }
    }
}
