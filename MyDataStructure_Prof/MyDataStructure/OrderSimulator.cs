using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	internal class OrderSimulator
	{
		LinkedList jackFoodList = new LinkedList();
		LinkedList bobFoodList = new LinkedList();
		LinkedList johnFoodList = new LinkedList();

		LinkedList jackOrderList = new LinkedList();
		LinkedList bobOrderList = new LinkedList();
		LinkedList johnOrderList = new LinkedList();

		int curTotalCookingTime_jack = 0;   // 현재까지 jack 이 요리하게될 총 시간
		int curTotalCookingTime_bob = 0;   // 현재까지 bob 이 요리하게될 총 시간
		int curTotalCookingTime_john = 0;   // 현재까지 john 이 요리하게될 총 시간

		LNode jack_curFood = null;	// Jack이 현재 요리하고 있는 주문
		int jack_cookingTime = 0;
		LNode bob_curFood = null;	// bob이 현재 요리하고 있는 주문
		int bob_cookingTime = 0;
		LNode john_curFood = null;	// John 이 현재 요리하고 있는 주문
		int john_cookingTime = 0;

		public void Run()
		{
			loadCookInfo();
			loadOrderInfo();

			simulationCooking();


		}

		// 각 요리사별 요리 정보를 읽어서 출력
		void loadCookInfo()
		{
			string line;
			string[] tokens;
			using (StreamReader sr = new StreamReader("CookInfoList.txt"))
			{
				while ((line = sr.ReadLine()) != null)
				{
					tokens = line.Split(' ');
					if (tokens[0].CompareTo("Jack") == 0)
						jackFoodList.InsertTail(new CookInfoData(tokens[0], tokens[1], int.Parse(tokens[2])));
					else if (tokens[0].CompareTo("Bob") == 0)
						bobFoodList.InsertTail(new CookInfoData(tokens[0], tokens[1], int.Parse(tokens[2])));
					else if (tokens[0].CompareTo("John") == 0)
						johnFoodList.InsertTail(new CookInfoData(tokens[0], tokens[1], int.Parse(tokens[2])));
				}
			}


			Console.WriteLine("==========================================");
			Console.WriteLine("Jack Food List ====\n");
			jackFoodList.PrintForwardAll();
			Console.WriteLine("==========================================");
			Console.WriteLine("Bob Food List ====\n");
			bobFoodList.PrintForwardAll();
			Console.WriteLine("==========================================");
			Console.WriteLine("John Food List ====\n");
			johnFoodList.PrintForwardAll();
		}


		// 주문 정보를 읽어 보자
		void loadOrderInfo()
		{
			string[] orderList;
			using (StreamReader sr = new StreamReader("OrderList.txt"))
			{
				orderList = sr.ReadToEnd().Split(' ');

				for (int i = 0; i < orderList.Length; i++)
				{
					pickCooker(orderList[i]);
				}

			}
		}

		// 요리를 담당할 요리사를 찾아보자.
		void pickCooker(string foodName)
		{
			// 요리가능한 요리사들을 먼저 찾아 보고
			CookInfoData cookInfo = new CookInfoData("", foodName, 0);
			LNode? node_jack = jackFoodList.Search(cookInfo);
			LNode? node_bob = bobFoodList.Search(cookInfo);
			LNode? node_john = johnFoodList.Search(cookInfo);

			CookInfoData? jackCookInfo = (CookInfoData?)(node_jack?.data);
			CookInfoData? bobCookInfo = (CookInfoData?)node_bob?.data;
			CookInfoData? johnCookInfo = (CookInfoData?)node_john?.data;


			// 현재 총 요리시간이 가장작은 요리사를 찾는다.
			if (jackCookInfo != null &&
				((bobCookInfo != null && curTotalCookingTime_jack > curTotalCookingTime_bob) ||
				(johnCookInfo != null && curTotalCookingTime_jack > curTotalCookingTime_john)))
				jackCookInfo = null;
			if (bobCookInfo != null &&
				((jackCookInfo != null && curTotalCookingTime_bob > curTotalCookingTime_jack) ||
				(johnCookInfo != null && curTotalCookingTime_bob > curTotalCookingTime_john)))
				bobCookInfo = null;
			if (johnCookInfo != null &&
				((jackCookInfo != null && curTotalCookingTime_john > curTotalCookingTime_jack) ||
				(bobCookInfo != null && curTotalCookingTime_john > curTotalCookingTime_bob)))
				johnCookInfo = null;



			// 현재 총 요리시간이 같으면 해당 요리를 빨리 끝낼 수 있는 요리사를 찾는다.
			if (jackCookInfo != null &&
				((bobCookInfo != null && jackCookInfo.Time > bobCookInfo.Time) ||
				(johnCookInfo != null && jackCookInfo.Time > johnCookInfo.Time)))
				jackCookInfo = null;
			if (bobCookInfo != null &&
				((jackCookInfo != null && bobCookInfo.Time > jackCookInfo.Time) ||
				(johnCookInfo != null && bobCookInfo.Time > johnCookInfo.Time)))
				bobCookInfo = null;
			if (johnCookInfo != null &&
				((jackCookInfo != null && johnCookInfo.Time > jackCookInfo.Time) ||
				(bobCookInfo != null && johnCookInfo.Time > bobCookInfo.Time)))
				johnCookInfo = null;


			// jack 만 이면
			if (jackCookInfo != null && bobCookInfo == null && johnCookInfo == null)
			{
				curTotalCookingTime_jack += jackCookInfo.Time;
				jackOrderList.InsertTail(jackCookInfo);
			}
			// bob 만 이면
			else if (jackCookInfo == null && bobCookInfo != null && johnCookInfo == null)
			{
				curTotalCookingTime_bob += bobCookInfo.Time;
				bobOrderList.InsertTail(bobCookInfo);
			}
			// john 만 이면
			else if (jackCookInfo == null && bobCookInfo == null && johnCookInfo != null)
			{
				curTotalCookingTime_john += johnCookInfo.Time;
				johnOrderList.InsertTail(johnCookInfo);
			}
			else
			{
				Console.WriteLine("## 이건 뭐지??? 이런 뭣...");
			}
		}

		//
		//
		// 요리 시큘레이션
		#region 요리 시뮬레이션
		void simulationCooking()
		{
			Console.WriteLine("\n\n================================================================");
			Console.WriteLine("Jack\t\t\t\t\t\tBob\t\t\t\t\t\tJohn");

			bool isCooking = false;
			int totalCookingTime = 0;
			do
			{
				++totalCookingTime;

				isCooking = false;
				isCooking |= cooking_chef(ref jack_cookingTime, ref jack_curFood, jackFoodList);
				Console.Write("\t\t\t");
				isCooking |= cooking_chef(ref bob_cookingTime, ref bob_curFood, bobFoodList);
				Console.Write("\t\t\t");
				isCooking |= cooking_chef(ref john_cookingTime, ref john_curFood, johnFoodList);
				Console.WriteLine();
			} while (isCooking);

			Console.WriteLine("Total time is : " + (totalCookingTime - 1));
		}

		// Jack 요리 중
		bool cooking_chef(ref int cookingTime, ref LNode chefCurFood, LinkedList foodList)
		{
			if (chefCurFood == null)
			{
				cookingTime = 0;
				chefCurFood = foodList.DeleteHead();
				if (chefCurFood != null)
					cookingTime = ((CookInfoData)chefCurFood.data).Time;
			}

			// 요리 할 것이 없다면
			if (chefCurFood == null)
				return false;

			CookInfoData curFood = ((CookInfoData)chefCurFood.data);
			// 요리 중인지 완료했는지?
			if (cookingTime - 1 == 0)
			{
				Console.Write($"Done!({curFood.FoodName})      ");
				chefCurFood = null;
				return true;
			}
			else
			{
				--cookingTime;
				Console.Write($"Cooking({curFood.FoodName} {cookingTime}/{curFood.Time})");
				return true;
			}

			return true;
		}
		#endregion // 요리 시뮬레이션
	}




	//============================================================
	class CookInfoData : INodeData
	{
		public string Chef { get; set; }
		public string FoodName { get; set; }
		public int Time { get; set; }

		public CookInfoData(string chef, string foodName, int time)
		{
			Chef = chef;
			FoodName = foodName;
			Time = time;
		}

		public int CompareTo(INodeData inData)
		{
			CookInfoData otherData = (CookInfoData)inData;

			int result = 0;

			// 요리사 이름이 지정되어 있지 않으면 스킵
			if(string.IsNullOrEmpty(otherData.Chef) == false)
				result = Chef.CompareTo(otherData.Chef);
			if (result != 0) return result;

			// 요리 이름은 반드시
			result = FoodName.CompareTo(otherData.FoodName);
			if (result != 0) return result;


			// 시간이 지정되어 있지 않으면 스킵
			if(otherData.Time != 0)
				return Time.CompareTo(otherData.Time);

			return result;
		}

		public void Print()
		{
			Console.WriteLine($"{Chef} {FoodName} {Time}");
		}

		public string OutputString()
		{
			return $"{Chef} {FoodName} {Time}";
		}
	}
}
