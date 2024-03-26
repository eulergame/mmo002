using System;
using UnityEngine;

namespace X.HotFix.Games.Sort
{
	internal class ElementPresenter : MonoBehaviour
	{
		public int Value { get; set; }
		public int Index { get; set; }
		[SerializeField] private float Interval;

		internal void Init(int i, int v)
		{
			MoveVertical(i);
			var s = transform.localScale;
			s.x *= v;
			transform.localScale = s;

			Value = v;
		}
		public void MoveVertical(int i)
		{
			var p = transform.position;
			p.y = i * Interval;
			transform.position = p;

			Index = i;
		}
	}
}