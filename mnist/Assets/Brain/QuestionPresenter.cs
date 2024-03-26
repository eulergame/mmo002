﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AngouriMath;
using System;
namespace X.HotFix.Games.Brain
{
	public class QuestionPresenter : MonoBehaviour
	{
		[SerializeField] private int Answer;
		private void Start()
		{
			foreach (var x in Plus2One(15, 3))
			{
				Debug.Log(x);
			}
			{
				Entity expr = "x + sin(y x)";
				Debug.Log(expr);
				Debug.Log(expr.Differentiate("x"));
			}
			{
				Entity expr = "2 + 3";
				Debug.Log(expr.EvalNumerical()); // 5
				Entity expr2 = "true implies false";
				Debug.Log(expr2.EvalBoolean()); // false
			}
			{
				Debug.Log(Envaluate("3 + 3 x 3".Replace('x','*'))); 
				//Debug.Log(Envaluate("3+3/3")); 
			}
			foreach (var x in RandomNumbersNoRepeated(1,10,4))
			{
				Debug.Log(x);
			}
		}
		//Geometric Sequence
		public static List<int> GeometricSequence(int initValue, int commonRatio, int n)
		{
			var result = new List<int>();
			result.Add(initValue);
			for (int i = 1; i <= n - 1; ++i)
			{
				result.Add(result[i - 1] * commonRatio);
			}
			return result;
		}
		//Arithmetic Sequence
		public static List<int> ArithmeticSequence(int initValue, int commonDifference, int n)
		{
			var result = new List<int>();
			result.Add(initValue);
			for (int i = 1; i <= n - 1; ++i)
			{
				result.Add(result[i - 1] + commonDifference);
			}
			return result;
		}
		//n >= 2
		public static List<int> Plus2One(int total, int n)
		{
			var result = new List<int>();
			result.Add(total);
			for (int i = 1; i <= n - 1; ++i)
			{
				var maximum = result.Select((value,index) => (value,index)).Max();
				var x = UnityEngine.Random.Range(0, maximum.value + 1);
				x = Mathf.Min(x, maximum.value - x);
				result[maximum.index] = x;
				result.Add(maximum.value - x);
			}
			return result;
		}
		public static int Envaluate(string expression)
		{
			Entity e = expression;
			return (int)e.EvalNumerical().RealPart;
		}
		
		public static IEnumerable<int> RandomNumbersNoRepeated(int minimux, int maxmum, int n)
		{
			var r = new System.Random();
			return Enumerable.Range(minimux, maxmum-minimux+1).OrderBy(x=>r.NextDouble()).Take(n);
		}

	}
}
