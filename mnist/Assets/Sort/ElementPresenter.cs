using Cysharp.Threading.Tasks;
using DG.Tweening;
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
			MoveVertical(i, 0f).Forget();
			var s = transform.localScale;
			s.x *= v;
			transform.localScale = s;

			Value = v;
		}
		public async UniTask MoveVertical(int i, float swapInSeconds)
		{
			Index = i;
			await transform.DOMoveY(i * Interval, swapInSeconds);
		}
	}
}