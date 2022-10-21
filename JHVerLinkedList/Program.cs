using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JHList();
        }

        static void JHList()
        {
            JHLinkedList JackList = new JHLinkedList();
            JHLinkedList BobList = new JHLinkedList();
            JHLinkedList JohnList = new JHLinkedList();

            StreamReader sr = new StreamReader("CookInfoList.txt");

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("Jack"))
                    JackList.Add(new NodeData() { lineInfo = line });
                else if (line.Contains("Bob"))
                    BobList.Add(new NodeData() { lineInfo = line });
                else if (line.Contains("John"))
                    JohnList.Add(new NodeData() { lineInfo = line });
            }

            // 저장된 데이터 출력
            Console.WriteLine("-------------- 정렬 전 ------------------");
            JackList.Show();
            Console.WriteLine();
            Console.WriteLine();

            BobList.Show();
            Console.WriteLine();
            Console.WriteLine();

            JohnList.Show();
            Console.WriteLine();
            Console.WriteLine();

            // 세 리스트 정렬
            JackList.Sort();
            BobList.Sort();
            JohnList.Sort();

            // 정렬후 출력
            Console.WriteLine("-------------- 정렬 후 ------------------");
            JackList.Show();
            Console.WriteLine();
            Console.WriteLine();

            BobList.Show();
            Console.WriteLine();
            Console.WriteLine();

            JohnList.Show();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
