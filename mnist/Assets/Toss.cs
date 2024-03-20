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
			p.x = i;
			o.transform.position = p;
			dots.Add(o);
			sample.Add(0);
		}
		TestAsync(r, dots, sample).Forget();
	}

	private async UniTask TestAsync(System.Random r, List<GameObject> dots, List<int> sample)
	{
		for (int i = 1; i <= N; i++)
		{
			await UniTask.Delay(TimeSpan.FromMilliseconds(30));
			Once(r, dots, sample, i);
		}
	}

	private static void Once(System.Random r, List<GameObject> dots, List<int> sample, int i)
	{
		//[0, 1)
		var e = r.NextDouble();
		var x = e < 0.5f ? 0 : 1;
		var t = dots[x].transform;
		var p = t.position;
		sample[x]++;
		p.y = 1.0f * sample[x] / i -5f;
		t.position = p;
	}
}
