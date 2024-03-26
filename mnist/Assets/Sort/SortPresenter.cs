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
			var xs = RandomNumbersNoRepeated(Minimum, Maximum, N).ToArray();
			for (int i = 0; i < N; i++)
			{
				var o = GameObject.Instantiate(ElementPrefab, transform);
				var c = o.GetComponent<ElementPresenter>();
				c.Init(i, xs[i]);
			}
			MessageBroker.Default.Receive<Event.Sort>().Subscribe(x => {
				Sort();
			}).AddTo(this);
		}

		private void Sort()
		{
		}

		public static IEnumerable<int> RandomNumbersNoRepeated(int minimux, int maxmum, int n)
		{
			var r = new System.Random();
			return Enumerable.Range(minimux, maxmum - minimux + 1).OrderBy(x => r.NextDouble()).Take(n);
		}
	}
}

