using System;
using UnityEngine;
using UniRx;
using UnityEngine.Experimental.Rendering;
namespace X.HotFix.Games.XTable
{
	public class GameEntry : MonoBehaviour
	{
		[SerializeField] GameObject cellPrefab;
		[SerializeField] float offsetX;
		[SerializeField] float offsetY;
		private void Start()
		{
			Construct(10);
			Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(x =>
			{
				ScreenCapture.CaptureScreenshot("9+.png");
				Debug.Log("saved");
			}).AddTo(this);
		}
		private void Construct(int n)
		{
			for (int row = 0; row <= n; ++row)
			{
				for (int col = 0; col <= row; ++col)
				{
					var o = Instantiate(cellPrefab, transform);
					var p = o.transform.localPosition;
					p.x = (col - (n + 1.0f) / 2f) * offsetX;
					p.y = (n / 2f - row) * offsetY;
					o.transform.localPosition = p;
					o.GetComponent<Cell>().SetNumber($"{col}+{row}={row+col}");
				}
			}
		}

	}
}
