using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo_JH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 : insert , 2 : delete , 3 : print_all , 4 : print_major , 5 : print_year , 6 : print_name , 7 : exit");

            string[] input = null;

            do
            {
                Console.Write("=> ");
                input = Console.ReadLine().Split(' ');
                Info(input);
            }
            while (input[0] != "7");
        }

        static void Info(string[] input)
        {
            LinkedList majorList = new LinkedList();

            StreamReader sr = new StreamReader("studentsinfo.txt");

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if(majorList == null)
                {
                    majorList.Add(new Node());
                }
            }
        }
    }
}
