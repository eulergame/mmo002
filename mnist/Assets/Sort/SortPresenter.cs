using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
namespace X.HotFix.Games.Sort
{
	public enum eSortAlgorithm
	{
		Selection,
		Bubble,
		Insertion,
	}
	public class SortPresenter : MonoBehaviour
	{
		[SerializeField] private int N;
		[SerializeField] private int Minimum;
		[SerializeField] private int Maximum;
		[SerializeField] private GameObject ElementPrefab;
		[SerializeField] private float SwapInSeconds;
		[SerializeField] private float IntervalInSeconds;
		[SerializeField] private eSortAlgorithm ESortAlgorithm;
		private void Start()
		{
			var elements = new List<ElementPresenter>();
			var xs = RandomNumbersNoRepeated(Minimum, Maximum, N).ToArray();
			for (int i = 0; i < N; i++)
			{
				var o = GameObject.Instantiate(ElementPrefab, transform);
				var c = o.GetComponent<ElementPresenter>();
				c.Init(i, xs[i]);
				elements.Add(c);
			}
			MessageBroker.Default.Receive<Event.Sort>().Subscribe(x =>
			{
				Sort(elements).Forget();
			}).AddTo(this);
		}
		//The algorithm repeatedly selects the smallest (or largest) element from the unsorted portion of the list
		//and swaps it with the first element of the unsorted part.
		//This process is repeated for the remaining unsorted portion until the entire list is sorted. 
		//Time Complexity: The time complexity of Selection Sort is O(N2)
		private async UniTask Sort(List<ElementPresenter> elements)
		{
			Func<List<ElementPresenter>, UniTask> s = ESortAlgorithm switch
			{
				eSortAlgorithm.Selection => Selection,
				eSortAlgorithm.Bubble => Bubble,
				eSortAlgorithm.Insertion => Insertion,
				_ => throw new NotImplementedException(),
			};
			await s(elements);
		}

		private async UniTask Selection(List<ElementPresenter> elements)
		{
			for (int i = 0; i <= elements.Count - 1; i++)
			{
				var e = Smallest(elements, i, elements.Count - 1);
				e.UpdateState(eElementState.Swapping);
				await Swap(elements, elements[i], e, SwapInSeconds);
				e.UpdateState(eElementState.Sorted);
				await UniTask.Delay(TimeSpan.FromSeconds(IntervalInSeconds));
			}
		}

		//In Bubble Sort algorithm, 
		//traverse from left and compare adjacent elements and the higher one is placed at right side.
		//In this way, the largest element is moved to the rightmost end at first.
		//This process is then continued to find the second largest and place it and so on until the data is sorted.
		private async UniTask Bubble(List<ElementPresenter> elements)
		{
			for (int i = 0; i <= elements.Count - 2; i++)
			{
				for (int k = 0; k <= elements.Count - 2 - i; k++)
				{
					if (elements[k].Value > elements[k + 1].Value)
					{
						elements[k].UpdateState(eElementState.Swapping);
						elements[k + 1].UpdateState(eElementState.Swapping);
						await Swap(elements, elements[k], elements[k + 1], SwapInSeconds);
						await UniTask.Delay(TimeSpan.FromSeconds(IntervalInSeconds));
						elements[k].UpdateState(eElementState.UnSort);
						elements[k + 1].UpdateState(eElementState.UnSort);
					}
				}
				elements[elements.Count - i - 1].UpdateState(eElementState.Sorted);
			}
			await UniTask.Delay(TimeSpan.FromSeconds(IntervalInSeconds));
			elements[0].UpdateState(eElementState.Sorted);
		}
		//Insertion Sort Algorithm
		//To sort an array of size N in ascending order iterate over the array and compare the current element(key) to its predecessor,
		//if the key element is smaller than its predecessor,
		//compare it to the elements before.Move the greater elements one position up to make space for the swapped element.
		private async UniTask Insertion(List<ElementPresenter> elements)
		{
			elements[0].UpdateState(eElementState.Sorted);
			await UniTask.Delay(TimeSpan.FromSeconds(IntervalInSeconds));
			for (int i = 1; i <= elements.Count - 1; i++)
			{
				for (int k = i; k > 0; k--)
				{
					if (elements[k].Value < elements[k - 1].Value)
					{
						elements[k].UpdateState(eElementState.Swapping);
						await Swap(elements, elements[k], elements[k - 1], SwapInSeconds);
						await UniTask.Delay(TimeSpan.FromSeconds(IntervalInSeconds));
						elements[k].UpdateState(eElementState.Sorted);
						elements[k-1].UpdateState(eElementState.Sorted);
					}
					else
					{
						break;
					}
				}
				elements[i].UpdateState(eElementState.Sorted);
			}
		}
		private static async UniTask Swap(List<ElementPresenter> elements, ElementPresenter elementPresenter, ElementPresenter e, float swapInSeconds)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
			var a = elementPresenter.Index;
			var b = e.Index;
			if (elementPresenter.Index != e.Index)
			{
				await UniTask.WhenAll(
					elementPresenter.MoveVertical(b, swapInSeconds),
					e.MoveVertical(a, swapInSeconds)
					);
			}
			elements[b] = elementPresenter;
			elements[a] = e;
		}

		private static ElementPresenter Smallest(List<ElementPresenter> elements, int from, int to)
		{
			ElementPresenter e = elements[from];
			for (int i = from + 1; i <= to; i++)
			{
				if (elements[i].Value < e.Value)
				{
					e = elements[i];
				}
			}
			return e;
		}
		public static IEnumerable<int> RandomNumbersNoRepeated(int minimux, int maxmum, int n)
		{
			var r = new System.Random();
			return Enumerable.Range(minimux, maxmum - minimux + 1).OrderBy(x => r.NextDouble()).Take(n);
		}
	}
}

