using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
	public struct LData
	{
		public int X;
		public int Y;

		public LData(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public void ShowData()
		{
			Console.WriteLine("[{0}, {1}]", X, Y);
		}
	}

	internal class MyArrayList
	{
		const int LIST_LEN = 100;
		LData[] arr = new LData[LIST_LEN];	//리스트의 저장소인 배열
		int numOfData;		// 저장된 데이터의 수
		int curPosition;    // 데이터 참조위치를 기록(배열의 인덱스)


		// 초기화하는 함수 : 리스트 생성후 먼저 호출되어야 한다.
		public void LInit()
		{
			numOfData = 0;
			curPosition = -1; // -1은 아직 아무 위치도 가리키지 않고 있다
		}

		// 리스트에 데이터를 추가한다.
		public void LInsert(LData data)
		{
			// 더 이상 저장할 공간이 없다면
			if(numOfData >= LIST_LEN)
			{
				Console.WriteLine("저장이 불가능합니다.");
				return;
			}

			// 데이터 추가
			arr[numOfData] = data;
			++numOfData; // 저장된 데이터 수 증가
		}

		// 리스트에서 첫번째 데이터를 가져온다.
		public bool LFirst(ref LData data)
		{
			// 저장된 데이터가 하나도 없다면 false 를 전달한다.
			if (numOfData == 0)
				return false;

			// 참조 위치 초기화, 첫번째 데이터의 참조를 의미
			curPosition = 0;

			// 데이터 전달
			data = arr[0];

			return true;

		}

		// 리스트에서 참조된 데이터의 다음 데이터를 가져온다.
		public bool LNext(ref LData data)
		{
			// 더 이상 참조할 데이터가 없다.
			if (curPosition >= numOfData - 1)
				return false;

			// 현재 데이터 참조 위치를 다음으로 변경
			curPosition++;

			// 데이터 전달
			data = arr[curPosition];

			return true;
		}

		// 함수의 마지막 반환 데이터를 삭제, 데이터는 반환된다.
		// LFirst 나 LNext 에 의해서 참조된 데이터를 삭제한다는 의미
		public bool LRemove(ref LData data)
		{
			// 현재 참조하고 있는 데이터가 없다면 false를
			if (curPosition < 0 || curPosition >= numOfData - 1)
				return false;

			// 삭제할 데이터를 가져와서 호출한 쪽에 전달해 준다.
			data = arr[curPosition];

			// 삭제를 위한 데이터의 이동
			for(int i = curPosition; i < numOfData - 1; ++i)
			{
				arr[i] = arr[i + 1];
			}

			// 데이터 수 감소
			numOfData--;
			// 참조위치를 하나 되돌린다.
			curPosition--;
			return true;

		}

		// 리스트에 저장되어 있는 데이터의 수를 반환한다.
		public int LCount()
		{
			return numOfData;
		}



	}
}
