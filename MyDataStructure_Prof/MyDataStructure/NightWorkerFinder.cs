using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	internal class NightWorkerFinder
	{
		CLinkedList workerList = new CLinkedList();
		public void Run()
		{
			loadWorkerInfo();

			Console.WriteLine();
			Console.Write("정보를 입력하시오 ==>  ");
			string inputString = Console.ReadLine();

			string[] tokens = inputString.Split(' ');
			string inputWorkerName = tokens[0];
			int day = int.Parse(tokens[1]);

			LNode target = workerList.Search(new WorkerInfoData(inputWorkerName, 0));
			if(target == null)
			{
				Console.WriteLine("입력한 직원은 존재하지 않습니다.");
			}
			else
			{
				while(--day >= 0)
				{
					target = target.next;
				}

				WorkerInfoData data = (WorkerInfoData)target.data;
				Console.WriteLine("검색 결과 : {0} {1}", data.Name, data.Num);
			}

		}

		// 직원정보를 로드하자.
		void loadWorkerInfo()
		{
			string line;
			string[] tokens;
			using (StreamReader sr = new StreamReader("workerinfo.txt"))
			{
				while ((line = sr.ReadLine()) != null)
				{
					tokens = line.Split(", ");
					workerList.InsertTail(new WorkerInfoData(tokens[0], int.Parse(tokens[1])));
				}
			}


			Console.WriteLine("==========================================");
			Console.WriteLine("Worker List ====\n");
			workerList.PrintForwardAll();
		}
	}



	//============================================================
	class WorkerInfoData : INodeData
	{
		public string Name { get; set; } // 직원 이름
		public int Num { get; set; }	// 사번

		public WorkerInfoData(string name, int num)
		{
			Name = name;
			Num = num;
		}

		public int CompareTo(INodeData inData)
		{
			WorkerInfoData otherData = (WorkerInfoData)inData;

			int result = 0;
			
			// 직원이름 비교
			result = Name.CompareTo(otherData.Name);
			if (result == 0) return result;

			// 사번으로 비교
			return Num.CompareTo(otherData.Num);
		}

		public void Print()
		{
			Console.WriteLine($"{Name} {Num}");
		}

		public string OutputString()
		{
			return $"{Name} {Num}";
		}
	}
}
