using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHMetroList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("출발 역을 입력하세요 : ");
            string startStation = Console.ReadLine();
            Console.Write("도착 역을 입력하세요 : ");
            string arriveStation = Console.ReadLine();

            Metro(startStation, arriveStation);
        }

        static void Metro(string startStation, string arriveStation)
        {
            JHDoubleLinkedList line1 = new JHDoubleLinkedList();
            JHCircularDoubleLinkedList line2 = new JHCircularDoubleLinkedList();

            StreamReader line1Sr = new StreamReader("Line_1.txt", Encoding.Default, true);
            StreamReader line2Sr = new StreamReader("Line_2.txt", Encoding.Default, true);

            int startLineNum = 0;
            int arriveLineNum = 0;

            string line;
            int count = 1;

            while ((line = line1Sr.ReadLine()) != null)
            {
                line1.Add(new NodeData { station = line, num = count++, stationLine = 1 }); ;
            }

            count = 1;

            while ((line = line2Sr.ReadLine()) != null)
            {
                line2.Add(new NodeData { station = line, num = count++, stationLine = 2});
            }

            Console.WriteLine("---------------------------------------");

            startLineNum = line1.CheckStationLine(startStation);
            arriveLineNum = line1.CheckStationLine(arriveStation);

            if (startLineNum == 1 && arriveLineNum == 1)
            {
                line1.GetRoute(startStation, arriveStation);
            }
            else if(startLineNum == 2 && arriveLineNum == 2)
            {
                line2.GetRoute(startStation, arriveStation);
            }
            else if(startLineNum == 1 && arriveLineNum == 2)
            {
                line1.GetRoute(startStation, "시청");
                Console.Write(" -- 환승 합니다 --");
                line2.GetRoute("시청", arriveStation);
            }
            else if(startLineNum == 2 && arriveLineNum == 1)
            {
                line2.GetRoute(startStation, "시청");
                Console.Write(" -- 환승 합니다 --");
                line1.GetRoute("시청", arriveStation);
            }
        }
    }
}
