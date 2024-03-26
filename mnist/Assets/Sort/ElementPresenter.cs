using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

namespace X.HotFix.Games.Sort
{
	enum eElementState
	{
		UnSort,
		Swapping,
		Sorted,
	}
	internal class ElementPresenter : MonoBehaviour
	{
		public int Value { get; set; }
		public int Index { get; set; }
		public eElementState State { get; set; }
		[SerializeField] private float Interval;

		internal void Init(int i, int v)
		{
			MoveVertical(i, 0f).Forget();
			var s = transform.localScale;
			s.x *= v;
			transform.localScale = s;

			Value = v;
			UpdateState(eElementState.UnSort);
		}

		public void UpdateState(eElementState e)
		{
			State = e;
			GetComponent<SpriteRenderer>().color = GetColor;
		}

		public async UniTask MoveVertical(int i, float swapInSeconds)
		{
			Index = i;
			await transform.DOMoveY(i * Interval, swapInSeconds);
		}
		Color GetColor => State switch
		{
			eElementState.UnSort => Color.white,
			eElementState.Swapping => Color.red,
			eElementState.Sorted => Color.green,
			_ => throw new NotImplementedException(),
		};
	}
}