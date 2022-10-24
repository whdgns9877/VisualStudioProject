using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
	// 배열기반 퀵정렬
	internal class QuickSort
	{
		public void Sort(int[] arrData, int left, int right)
		{
			// 재귀함수 탈출조건
			if (left > right) return;

			int pivot = subSort(arrData, left, right);
			Sort(arrData, left, pivot - 1);
			Sort(arrData, pivot + 1, right);
		}

		int subSort(int[] arrData, int left, int right)
		{
			int pivot = left;
			int low = left + 1;
			int high = right;

			while(low <= high)
			{
				// pivot 보다 큰 값이 나올때까지
				while (low <= right && arrData[pivot] > arrData[low]) ++low; // low 오른쪽으로 이동

				// pivot 보다 작은 값이 나올때까지
				while (high >= left + 1 && arrData[pivot] < arrData[high]) --high; // high 왼쪽으로 이동

				if(low <= high)
				{
					swap(arrData, low, high);
				}
			}

			// pivot 과 high 를 교환
			swap(arrData, pivot, high);
			return high;
		}

		void swap(int[] arrData, int t1, int t2)
		{
			int temp = arrData[t1];
			arrData[t1] = arrData[t2];
			arrData[t2] = temp;
		}
	}
}
