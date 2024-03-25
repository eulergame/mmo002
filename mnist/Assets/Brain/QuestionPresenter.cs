using System.Collections.Generic;
using UnityEngine;
namespace X.HotFix.Games.Brain
{
	public class QuestionPresenter : MonoBehaviour
	{
		[SerializeField] private int Answer;
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
	}
}

