using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PI : MonoBehaviour
{
	[SerializeField] TextAsset textPI;
	[SerializeField] int n;
	[SerializeField] float radius;
	[SerializeField] float angleStep;
	[SerializeField] float radiusScale;
	[SerializeField] float sizeScale;
	[SerializeField] GameObject numberPrefab;

	// Start is called before the first frame update
	async void Start()
	{
		var angle = 0f;
		var pi = textPI.text;
		var s = 1f;
		for (int i = 0; i < n; i++)
		{
			Debug.Log(pi[i]);
			var o = GameObject.Instantiate(numberPrefab, transform);
			var t = o.GetComponent<TextMeshPro>();
			t.text = pi[i].ToString();
			var p = o.transform.position;
			angle += angleStep * Mathf.Deg2Rad;
			radius *= (1.0f - radiusScale);
			p.x = radius * Mathf.Cos(angle);
			p.y = radius * Mathf.Sin(angle);
			o.transform.position = p;
			s *= (1 - sizeScale);
			var ls = o.transform.localScale;
			ls.x = s;
			ls.y = s;
			o.transform.localScale = ls;
			{
				var r = o.transform.localRotation.eulerAngles;
				r.z = angle * Mathf.Rad2Deg - 90f;
				o.transform.eulerAngles = r;
			}
			await UniTask.Delay(TimeSpan.FromMilliseconds(16));
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
