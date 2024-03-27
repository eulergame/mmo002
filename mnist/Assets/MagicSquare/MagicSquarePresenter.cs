using System.Collections.Generic;
using UnityEngine;
namespace X.HotFix.Games.MagicSquare
{
	public class MagicSquarePresenter : MonoBehaviour
	{
		[SerializeField] int N;
		private void Start()
		{
			var xs = new List<int>();
			int s = (int)(N * (N + 1) / 2 / Mathf.Sqrt(N));
			for (int a11 = 1; a11 <= N; a11++)
			{
				xs.Add(a11);
				for (int a12 = 1; a12 <= N; a12++)
				{
					if (xs.Contains(a12) || (a11 + a12 >= s))
					{
						continue;
					}
					xs.Add(a12);
					for (int a13 = 1; a13 <= N; a13++)
					{
						if (xs.Contains(a13) || (a11 + a12 + a13) != s)
						{
							continue;
						}
						xs.Add(a13);
						for (int a21 = 1; a21 <= N; a21++)
						{
							if (xs.Contains(a21) || (a11 + a21 >= s))
							{
								continue;
							}
							xs.Add(a21);
							for (int a22 = 1; a22 <= N; a22++)
							{
								if (xs.Contains(a22) || (a11 + a22 >= s) || (a21 + a22 >= s))
								{
									continue;
								}
								xs.Add(a22);
								for (int a23 = 1; a23 <= N; a23++)
								{
									if (xs.Contains(a23) || (a21 + a22 + a23) != s)
									{
										continue;
									}
									xs.Add(a23);
									for (int a31 = 1; a31 <= N; a31++)
									{
										if (xs.Contains(a31) || (a11 + a21 + a31) != s || (a13 + a22 + a31) != s)
										{
											continue;
										}
										xs.Add(a31);
										for (int a32 = 1; a32 <= N; a32++)
										{
											if (xs.Contains(a32) || (a12 + a22 + a32) != s)
											{
												continue;
											}
											xs.Add(a32);
											for (int a33 = 1; a33 <= N; a33++)
											{
												if (xs.Contains(a33) || (a13 + a23 + a33) != s
													|| (a31 + a32 + a33) != s
													|| (a11 + a22 + a33) != s)
												{
													continue;
												}
												{
													Debug.Log($"bingo {s}");
													Debug.Log($"{a11} {a12} {a13}");
													Debug.Log($"{a21} {a22} {a23}");
													Debug.Log($"{a31} {a32} {a33}");
												}
											}
											xs.Remove(a32);
										}
										xs.Remove(a31);
									}
									xs.Remove(a23);
								}
								xs.Remove(a22);
							}
							xs.Remove(a21);
						}
						xs.Remove(a13);
					}
					xs.Remove(a12);
				}
				xs.Remove(a11);
			}
			Debug.Log("done");
		}
	}
}

