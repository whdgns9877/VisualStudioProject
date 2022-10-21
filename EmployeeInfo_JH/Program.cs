using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("직원의 이름과 숫자를 입력하세요 : ");
            string[] input = Console.ReadLine().Split(',',' ');
            string name = input[0];
            int num = Convert.ToInt32(input[1]);
            EmployeeList(name, num);
        }

        static void EmployeeList(string name, int num)
        {
            JHCircularLinkedList employeeList = new JHCircularLinkedList();

            StreamReader sr = new StreamReader("workerinfo.txt");
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                employeeList.Add(new NodeData { Lineinfo = line });
            }
            employeeList.SetInfo();
            employeeList.Show(name, num);
        }
    }
}
