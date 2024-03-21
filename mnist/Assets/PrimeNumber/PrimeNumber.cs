using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PrimeNumber : MonoBehaviour
{
	[SerializeField] int n;
	// Start is called before the first frame update
	void Start()
	{
		Debug.Log($"{n} is {(CheckPrimer(n, 2).prime ? "" : "not")} prime");
		Debug.Log($"{n} = {(string.Join(",", Decompose(n).Select(x => $"{x.Key} to {x.Value}")))}");
	}

	private static (bool prime, int element) CheckPrimer(int n, int from)
	{
		for (int i = from; i <= n / 2; i++)
		{
			if (n % i == 0)
			{
				return (false, i);
			}
		}
		return (true, 0);
	}
	private static Dictionary<int, int> Decompose(int n)
	{
		Dictionary<int, int> elments = new Dictionary<int, int>();
		int from = 2;
		for (; ; )
		{
			var x = CheckPrimer(n, from);
			if (!x.prime)
			{
				if (elments.ContainsKey(x.element))
				{
					elments[x.element]++;

				}
				else
				{
					elments.Add(x.element, 1);
				}
				from = x.element;
				n /= x.element;
			}
			else
			{
				if (elments.ContainsKey(n))
				{
					elments[n]++;

				}
				else
				{
					elments.Add(n, 1);
				}
				break;
			}
		}
		return elments;
	}
}
