using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toss : MonoBehaviour
{
	[SerializeField] int N = 100;
	[SerializeField] int Pairs = 1;
	[SerializeField] GameObject DotPrefab;
	// Start is called before the first frame update
	void Start()
	{
		var r = new System.Random();
		var dots = new List<GameObject>();
		var sample = new List<int>();
		for (int i = 0; i <= Pairs; i++)
		{
			var o = GameObject.Instantiate(DotPrefab);
			var p = o.transform.position;
			p.x = i - 5;
			o.transform.position = p;
			dots.Add(o);
			sample.Add(0);
		}
		TestAsync(r, dots, sample, N, Pairs).Forget();
	}

	private static async UniTask TestAsync(System.Random r, List<GameObject> dots, List<int> sample, int N, int Pairs)
	{
		for (int i = 1; i <= N; i++)
		{
			await UniTask.Delay(TimeSpan.FromMilliseconds(10));
			var x = Once(r, dots, sample, i, Pairs);
			sample[x]++;
			UpdateCurve(dots, sample, Pairs, i);
		}
	}

	private static void UpdateCurve(List<GameObject> dots, List<int> sample, int Pairs, int i)
	{
		for (int k = 0; k <= Pairs; k++)
		{
			var t = dots[k].transform;
			var p = t.position;
			p.y = (1.0f * sample[k] / i) * 10 - 5f;
			t.position = p;
		}
	}

	private static int Once(System.Random r, List<GameObject> dots, List<int> sample, int i, int Pairs)
	{
		int x = 0;
		for (int k = 0; k < Pairs; k++)
		{
			//[0, 1)
			var e = r.NextDouble();
			x += PNCheck(e);
		}
		return x;
	}

	private static int PNCheck(double e)
	{
		return e < 0.5f ? 0 : 1;
	}
}
