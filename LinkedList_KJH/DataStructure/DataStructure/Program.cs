using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ChefArrayList();
		}

		// Array List 테스트
		static void testArrayList()
		{ 
			MyArrayList myArrayList = new MyArrayList();

			myArrayList.LInit();


			myArrayList.LInsert(new LData(2, 1));
			myArrayList.LInsert(new LData(2, 2));
			myArrayList.LInsert(new LData(3, 1));
			myArrayList.LInsert(new LData(3, 2));


			// 저장된 데이터 출력
			Console.WriteLine("현재 데이터 수 : {0}", myArrayList.LCount());

			LData data = new LData();
			if(myArrayList.LFirst(ref data))
			{
				data.ShowData();

				while(myArrayList.LNext(ref data))
				{
					data.ShowData();
				}
			}

			// X 가 2인 모든 데이터 삭제
			LData compData = new LData();
			compData.X = 2;
			compData.Y = 0;

			if(myArrayList.LFirst(ref data))
			{
				if(data.X == compData.X)
				{
					myArrayList.LRemove(ref data);
				}

				while(myArrayList.LNext(ref data))
				{
					if (data.X == compData.X)
					{
						myArrayList.LRemove(ref data);
					}
				}
			}


			// 삭제 후 남은 데이터 전체 출력
			Console.WriteLine("현재 데이터의 수 : {0}", myArrayList.LCount());

			if(myArrayList.LFirst(ref data))
			{
				data.ShowData();
				while(myArrayList.LNext(ref data))
				{
					data.ShowData();
				}
			}

		}

		// List Practice
		static void ChefArrayList()
		{
			MyLinkedList JackList = new MyLinkedList();
			MyLinkedList BobList = new MyLinkedList();
			MyLinkedList JohnList = new MyLinkedList();

			StreamReader sr = new StreamReader("CookInfoList.txt");

			string line;

			while ((line = sr.ReadLine()) != null)
			{
				if (line.Contains("Jack"))
					JackList.Add(new NodeData() { Info = line });
				else if(line.Contains("Bob"))
					BobList.Add(new NodeData() { Info = line });
				else if(line.Contains("John"))
					JohnList.Add(new NodeData() { Info = line });
			}

			// 저장된 데이터 출력
			JackList.Print();
			Console.WriteLine();
			Console.WriteLine();

            BobList.Print();
            Console.WriteLine();
            Console.WriteLine();

            JohnList.Print();
            Console.WriteLine();
            Console.WriteLine();

            //JackList.Sort();
            //BobList.Sort();
            //JohnList.Sort();


            //// 저장된 데이터 출력
            //JackList.Print();
            //Console.WriteLine();
            //Console.WriteLine();

            //BobList.Print();
            //Console.WriteLine();
            //Console.WriteLine();

            //JohnList.Print();
            //Console.WriteLine();
            //Console.WriteLine();
        }
	}
}
