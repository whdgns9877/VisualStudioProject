using System.Xml.Linq;
using System;

namespace MyDataStructure
{
	internal class Program
	{
		//============================================================
		static void Main(string[] args)
		{

			TestClass testClass = new TestClass();
			testClass.Run();
		}		





		//============================================================
		class TestClass
		{
			public void Run()
			{
				// 퀵정렬
				//testQuickSort();

				// 요리 주문을 해보자.
				//OrderSimulator sr = new OrderSimulator();
				//sr.Run();

				// 당직자를 알아 보자.
				//NightWorkerFinder nf = new NightWorkerFinder();
				//nf.Run();


				// 지하철 최단거리 찾아 보자.
				SubwayPathFinder spf = new SubwayPathFinder();
				spf.Run();






				//testStudentInfo();

				//testBST();
			}

			void testQuickSort()
			{
				// 숫자를 지정된 숫자 만큼 랜덤하게 생성해서 배열에 할당
				Random random = new Random();
				int[] arrData = new int[10];
				for (int i = 0; i < arrData.Length; i++)
				{
					arrData[i] = random.Next(100, 1000);

					Console.Write("{0} ", arrData[i]);
				}
				Console.WriteLine();


				//
				// 퀵소트 실행
				QuickSort qs = new QuickSort();
				qs.Sort(arrData, 0, arrData.Length - 1);


				// 결과 내용 출력
				Console.WriteLine("## 결과");
				for (int i = 0; i < arrData.Length; i++)
				{
					Console.Write("{0} ", arrData[i]);
				}
				Console.WriteLine();

			}

			


			// 학생정보 테스트
			void testStudentInfo()
			{
				DLinkedList myDLinkList = new DLinkedList();

				string line;
				string[] tokens;
				using (StreamReader sr = new StreamReader("studentsinfo.txt"))
				{
					while ((line = sr.ReadLine()) != null)
					{
						tokens = line.Split(' ');
						myDLinkList.InsertTail(new StudentData(tokens[0], tokens[2], int.Parse(tokens[1])));
					}
				}


				myDLinkList.PrintBackwardAll();




			}


			// BST 테스트
			void testBST()
			{
				BST tree = new BST();

				if (!tree.Insert(new TreeData(10))) Console.WriteLine("중복 데이터!!");
				if (!tree.Insert(new TreeData(8))) Console.WriteLine("중복 데이터!!");
				if (!tree.Insert(new TreeData(12))) Console.WriteLine("중복 데이터!!");
				if (!tree.Insert(new TreeData(12))) Console.WriteLine("중복 데이터!!");

				tree.Print();
				if (!tree.Remove(new TreeData(10))) Console.WriteLine("없는 데이터!!");
				tree.Print();

				BTNode found = tree.Find(new TreeData(8));
				if (found != null) Console.WriteLine(found.data.OutputString());
			}




		}


		

		//============================================================
		class StudentData : INodeData
		{
			public string Major { get; set; }
			public string Name { get; set; }
			public int Num { get; set; }

			public StudentData(string major, string name, int num)
			{
				Major = major;
				Name = name;
				Num = num;
			}

			public int CompareTo(INodeData inData)
			{
				StudentData otherData = (StudentData)inData;

				int result = Major.CompareTo(otherData.Major);
				if (result != 0) return result;


				result = Name.CompareTo(otherData.Name);
				if (result != 0) return result;


				return Num.CompareTo(otherData.Num);
			}

			public void Print()
			{
				Console.WriteLine($"{Major} {Name} {Num}");
			}

			public string OutputString()
			{
				return $"{Major} {Name} {Num}";
			}
		}

		//============================================================
		class TreeData : INodeData
		{
			public int Value { get; set; }

			public TreeData(int value)
			{
				Value = value;
			}

			public int CompareTo(INodeData inData)
			{
				TreeData otherData = (TreeData)inData;
				return Value.CompareTo(otherData.Value);
			}

			public void Print()
			{
				Console.WriteLine($"{Value}");
			}

			public string OutputString()
			{
				return $"{Value}";
			}
		}




	}
}