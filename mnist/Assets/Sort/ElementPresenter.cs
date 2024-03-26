using System;
using UnityEngine;

namespace X.HotFix.Games.Sort
{
	internal class ElementPresenter : MonoBehaviour
	{
		[SerializeField] private float Interval;

		internal void Init(int i, int v)
		{
			var p = transform.position;
			p.y = i * Interval;
			transform.position = p;

			var s = transform.localScale;
			s.x *= v;
			transform.localScale = s;
		}
	}
}