using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
namespace X.HotFix.Games.Sort
{
	public class SortPresenter : MonoBehaviour
	{
		[SerializeField] private int N;
		[SerializeField] private int Minimum;
		[SerializeField] private int Maximum;
		[SerializeField] private GameObject ElementPrefab;

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
		private async UniTask Sort(List<ElementPresenter> elements)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				var e = Smallest(elements, i, elements.Count - 1);
				Swap(elements, elements[i], e);
				await UniTask.Delay(TimeSpan.FromMilliseconds(1600));
			}
		}

		private void Swap(List<ElementPresenter> elements, ElementPresenter elementPresenter, ElementPresenter e)
		{
			if (elementPresenter.Index == e.Index)
			{
				return;
			}
			var i = elementPresenter.Index;
			elementPresenter.MoveVertical(e.Index);
			elements[e.Index] = elementPresenter;
			e.MoveVertical(i);
			elements[i] = e;
		}

		private ElementPresenter Smallest(List<ElementPresenter> elements, int from, int to)
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

