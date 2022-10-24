using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDataStructure
{
	internal class SubwayPathFinder
	{
		DLinkedList listLine1 = new DLinkedList();
		CDLinkedList listLine2 = new CDLinkedList();

		const string transferStationName = "시청"; // 환승역

		public void Run()
		{
			// 지하철 데이터를 로드하자.
			loadLine_1();
			loadLine_2();


			Console.Write("출발역 : ");
			string startStation = Console.ReadLine();
			Console.Write("도착역 : ");
			string endStation = Console.ReadLine();

			// 최단 거리 찾아 보기
			findShortPath(startStation, endStation);

		}

		// 1호선 데이터
		void loadLine_1()
		{
			string line;
			using (StreamReader sr = new StreamReader("Line_1.txt"))
			{
				short stationNum = 1; // 역번호
				DNode transferStationNode = null;
				DNode stationNode = null;
				while ((line = sr.ReadLine()) != null)
				{
					// 역 추가
					stationNode = listLine1.InsertTail(new StationInfoData(line) { Num1 = stationNum++ });
					// 환승역 저장
					if (line.CompareTo(transferStationName) == 0)
						transferStationNode = stationNode;
				}

				// 환승역기준으로 얼마나 떨어져 있는지를 기록하자.	
				short idx = 0;
				listLine1.TraversalBackward(transferStationNode, delegate (DNode node)
				{
					((StationInfoData)node.data).Num2 = idx--;
				});

				idx = 0;
				listLine1.TraversalForward(transferStationNode, delegate (DNode node)
				{
					((StationInfoData)node.data).Num2 = idx++;
				});


			}

			/*Console.WriteLine("==========================================");
			Console.WriteLine("1 호선  ====\n");
			listLine1.PrintForwardAll();
			Console.WriteLine("==========================================\n");*/
		}

		// 2호선 데이터
		void loadLine_2()
		{
			string line;
			using (StreamReader sr = new StreamReader("Line_2.txt"))
			{
				DNode transferStationNode = null;
				DNode stationNode = null;
				while ((line = sr.ReadLine()) != null)
				{
					// 역 추가
					stationNode = listLine2.InsertTail(new StationInfoData(line));
					// 환승역 저장
					if (line.CompareTo(transferStationName) == 0)
						transferStationNode = stationNode;
				}

				// 환승역기준으로 얼마나 떨어져 있는지를 기록하자.	
				short idx = 0;
				listLine2.TraversalBackward(transferStationNode, delegate (DNode node)
				{
					((StationInfoData)node.data).Num1 = ++idx;// 왼쪽으로는
				});
				idx = 0;
				listLine2.TraversalForward(transferStationNode, delegate (DNode node)
				{
					((StationInfoData)node.data).Num2 = ++idx; // 오른쪽으로
				});
			}

			/*
			Console.WriteLine("==========================================");
			Console.WriteLine("2 호선  ====\n");
			listLine2.PrintForwardAll();
			Console.WriteLine("==========================================\n");
			//*/
		}

		// 최단 거리 찾아 보기 - 그리고 출력까지
		void findShortPath(string startStation, string endStation)
		{
			StationInfoData ssi = new StationInfoData(startStation);
			StationInfoData esi = new StationInfoData(endStation);


			//
			//
			// 먼저 역이 몇호선에 걸쳐 있는지를 찾는다.
			// 
			// 찾는 역 중에 '시청'역이 있다면 예외처리를 해야 한다. 1호선,2호선에 모두 걸쳐 있기 때문에
			//




			// 환승역
			DNode findedStartStation = null;
			DNode findedEndStation = null;

			int start_linenum = 0;  // 도착지가 1호선에 있으면 1, 2호선에 있으면 2, 없으면 0
			int end_linenum = 0;  // 도착지가 1호선에 있으면 1, 2호선에 있으면 2, 없으면 0

			// 찾고자 하는 역 중에 환승역(시청)이 있을 때는 예외 처리
			if (startStation.CompareTo(transferStationName) == 0)
			{
				// 도착지를 먼저
				// 도착지가 어디 있는지 찾기
				findedEndStation = whereTheStation(endStation, ref end_linenum);
				if (findedEndStation == null)
					return;
				esi = (StationInfoData)findedEndStation.data; // 찾은 데이터를 교체

				// 출발지가 어디 있는지 찾기 (라인도 지정 - 시청이 같은 라인에 존재할 것이기 때문에
				findedStartStation = whereTheStation(startStation, ref start_linenum, end_linenum);
				if (findedStartStation == null)
					return;
				ssi = (StationInfoData)findedStartStation.data; // 찾은 데이터를 교체
			}
			else if (endStation.CompareTo(transferStationName) == 0)
			{
				// 출발지가 어디 있는지 찾기
				findedStartStation = whereTheStation(startStation, ref start_linenum);
				if (findedStartStation == null)
					return;
				ssi = (StationInfoData)findedStartStation.data; // 찾은 데이터를 교체


				// 도착지가 어디 있는지 찾기 (라인도 지정 - 시청이 같은 라인에 존재할 것이기 때문에
				findedEndStation = whereTheStation(endStation, ref end_linenum, start_linenum);
				if (findedEndStation == null)
					return;
				esi = (StationInfoData)findedEndStation.data; // 찾은 데이터를 교체
			}
			else
			{
				// 출발지가 어디 있는지 찾기
				findedStartStation = whereTheStation(startStation, ref start_linenum);
				if (findedStartStation == null)
					return;
				ssi = (StationInfoData)findedStartStation.data; // 찾은 데이터를 교체


				// 도착지가 어디 있는지 찾기
				findedEndStation = whereTheStation(endStation, ref end_linenum);
				if (findedEndStation == null)
					return;
				esi = (StationInfoData)findedEndStation.data; // 찾은 데이터를 교체
			}



			//
			//
			// 찾고자 하는 역이 몇호선인지 알아냈으니 각 호선별로 최단거리를 찾아서 출력한다.
			//
			//
			//
			//
			//
			//



			// 둘다 1호선?
			if (start_linenum == 1 && end_linenum == 1)
			{
				Console.WriteLine("== 결과 ==");
				if (ssi.Num1 < esi.Num1)
				{
					listLine1.TraversalForward(findedStartStation, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				else
				{
					listLine1.TraversalBackward(findedStartStation, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				return;
			}
			// 둘다 2호선?
			else if (start_linenum == 2 && end_linenum == 2)
			{
				Console.WriteLine("== 결과 ==");
				int leftPathCount = 0;    // 왼쪽방향 거리
				int rightPathCount = 0;   // 오른쪽방향 거리

				if (ssi.Num1 < esi.Num1)
				{
					leftPathCount = esi.Num1 - ssi.Num1;
					rightPathCount = ssi.Num1 + esi.Num2;
				}
				else
				{
					leftPathCount = ssi.Num2 + esi.Num1;
					rightPathCount = esi.Num1 - ssi.Num1;
				}

				// 왼쪽 길이 더 짧은 경우
				if (leftPathCount < rightPathCount)
				{
					listLine2.TraversalBackward(findedStartStation, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				// 오른쪽 길이 더 짧은 경우
				else
				{
					listLine2.TraversalForward(findedStartStation, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				return;
			}
			// 1호선에서 2호선으로 
			else if (start_linenum == 1 && end_linenum == 2)
			{
				int linenum = 0;
				// 1호선 환승역 찾기
				DNode miStation_1 = whereTheStation(transferStationName, ref linenum, 1);
				if (miStation_1 == null) return; // 설마?
				StationInfoData msi_1 = (StationInfoData)miStation_1.data; // 찾은 데이터를 교체
				// 2호선 환승역 찾기
				DNode miStation_2 = whereTheStation(transferStationName, ref linenum, 2);
				if (miStation_2 == null) return; // 설마?
				StationInfoData msi_2 = (StationInfoData)miStation_2.data; // 찾은 데이터를 교체

				//
				// 1호선 출발역에서 환승역까지
				Console.WriteLine("== 결과 ==");
				if (ssi.Num1 < msi_1.Num1)
				{
					listLine1.TraversalForward(findedStartStation, miStation_1, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				else
				{
					listLine1.TraversalBackward(findedStartStation, miStation_1, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				// 환승역에서 2호선 도착역까지
				Console.WriteLine("======= 환승 ======");
				int leftPathCount = 0;    // 왼쪽방향 거리
				int rightPathCount = 0;   // 오른쪽방향 거리

				if (msi_2.Num1 < esi.Num1)
				{
					leftPathCount = esi.Num1 - msi_2.Num1;
					rightPathCount = msi_2.Num1 + esi.Num2;
				}
				else
				{
					leftPathCount = msi_2.Num2 + esi.Num1;
					rightPathCount = esi.Num1 - msi_2.Num1;
				}

				// 왼쪽 길이 더 짧은 경우
				if (leftPathCount < rightPathCount)
				{
					listLine2.TraversalBackward(miStation_2, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				// 오른쪽 길이 더 짧은 경우
				else
				{
					listLine2.TraversalForward(miStation_2, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
			}
			// 2호선에서 1호선으로 
			else
			{
				int linenum = 0;
				// 1호선 환승역 찾기
				DNode miStation_1 = whereTheStation(transferStationName, ref linenum, 1);
				if (miStation_1 == null) return; // 설마?
				StationInfoData msi_1 = (StationInfoData)miStation_1.data; // 찾은 데이터를 교체
				// 2호선 환승역 찾기
				DNode miStation_2 = whereTheStation(transferStationName, ref linenum, 2);
				if (miStation_2 == null) return; // 설마?
				StationInfoData msi_2 = (StationInfoData)miStation_2.data; // 찾은 데이터를 교체


				//
				//
				//
				Console.WriteLine("== 결과 ==");
				// 2호선 출발역에서 환승역까지
				int leftPathCount = 0;    // 왼쪽방향 거리
				int rightPathCount = 0;   // 오른쪽방향 거리

				if (ssi.Num1 < msi_2.Num1)
				{
					leftPathCount = Math.Abs(msi_2.Num1 - ssi.Num1);
					rightPathCount = ssi.Num1 + msi_2.Num2;
				}
				else
				{
					leftPathCount = ssi.Num2 + msi_2.Num1;
					rightPathCount = Math.Abs(msi_2.Num1 - ssi.Num1);
				}

				// 왼쪽 길이 더 짧은 경우
				if (leftPathCount < rightPathCount)
				{
					listLine2.TraversalBackward(findedStartStation, miStation_2, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				// 오른쪽 길이 더 짧은 경우
				else
				{
					listLine2.TraversalForward(findedStartStation, miStation_2, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}

				// 환승역에서 1호선 도착역까지
				Console.WriteLine("======= 환승 ======");				
				if (msi_1.Num1 < esi.Num1)
				{
					listLine1.TraversalForward(miStation_1, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				else
				{
					listLine1.TraversalBackward(miStation_1, findedEndStation, delegate (DNode node)
					{
						Console.WriteLine($"{node.data.OutputString()}");
					});
				}
				
			}
		}

		// 역이 어디에 있는지를 찾아 보자.
		// targetLineNum : 검색하는 라인을 지정하는 경우 0은 기본값
		DNode whereTheStation(string stationName, ref int lineNum, int targetLineNum = 0)
		{
			StationInfoData ssi = new StationInfoData(stationName);

			// 출발지가 어디 있는지 찾기
			DNode findedStation = null;
			lineNum = 0;  // 출발지가 1호선에 있으면 1, 2호선에 있으면 2, 없으면 0


			// 검색할 라인을 1호선으로 지정한 경우
			if(targetLineNum == 1)
			{
				findedStation = listLine1.Search(ssi);
				if (findedStation == null)
				{
					Console.WriteLine(">> 역을 찾을 수가 없습니다.");
					return null;
				}
				lineNum = 1;
				return findedStation;
			}
			// 검색할 라인을 2호선으로 지정한 경우
			else if (targetLineNum == 2)
			{
				findedStation = listLine2.Search(ssi);
				if (findedStation == null)
				{
					Console.WriteLine(">> 역을 찾을 수가 없습니다.");
					return null;
				}
				lineNum = 2;
				return findedStation;
			}
			else
			{
				// 검색할 라인을 지정하지 않은 경우
				findedStation = listLine1.Search(ssi);
				if (findedStation != null)
					lineNum = 1;
				else
				{
					findedStation = listLine2.Search(ssi);
					if (findedStation != null)
						lineNum = 2;
				}
				if (lineNum == 0)
				{
					Console.WriteLine(">> 역을 찾을 수가 없습니다.");
					return null;
				}

				return findedStation;
			}



			return null;			
		}
	}



	class StationInfoData : INodeData
	{
		public string Name { get; set; }

		public short Num1 { get; set; }		// 역 데이터 1
		public short Num2 { get; set; }		// 역 데이터 2
		
		public StationInfoData(string name)
		{
			Name = name;
		}

		public int CompareTo(INodeData inData)
		{
			StationInfoData otherData = (StationInfoData)inData;

			return Name.CompareTo(otherData.Name);
		}

		public void Print()
		{
			Console.WriteLine($"{Name} {Num1} {Num2}");
		}

		public string OutputString()
		{
			return $"{Name}";
		}
	}
}
