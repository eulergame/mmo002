// See https://aka.ms/new-console-template for more information
Game.Start();
public static class Game
{
	const int N = 4;
	const int N2 = N * N;
	const int MagicSum = (int)(N * (N2 + 1) / 2);
	static int[,] xs = new int[N + 1, N + 1];

	public static void Start()
	{
		var time = DateTime.Now;
		M5();
		Console.WriteLine($"done {(DateTime.Now - time).TotalSeconds}");
	}

	static bool Contains(this int[,] xs, int value)
	{
		for (int i = 1; i <= N; i++)
			for (int j = 1; j <= N; j++)
			{
				if (xs[i, j] == value)
				{
					return true;
				}
			}
		return false;
	}
	static bool Invalid(int row, int col, int value)
	{
		if (col == N)
		{
			{
				var total = value;
				for (int i = 1; i <= N; i++)
				{
					total += xs[row, i];
				}
				if (total != MagicSum)
				{
					return true;
				}
			}

			if (row == N && false)
			{
				{
					var total = value;
					for (int i = 1; i <= N; i++)
					{
						total += xs[i, i];
					}
					if (total != MagicSum)
					{
						return true;
					}
				}
				{
					var total = value;
					for (int i = 1; i <= N; i++)
					{
						total += xs[i, N - i + 1];
					}
					if (total != MagicSum)
					{
						return true;
					}
				}
			}
		}
		else
		{
			var total = value;
			for (int i = 1; i <= col; i++)
			{
				total += xs[row, i];
			}
			if (total >= MagicSum)
			{
				return true;
			}
		}
		if (row == N)
		{
			var total = value;
			for (int i = 1; i <= N; i++)
			{
				total += xs[i, col];
			}
			if (total != MagicSum)
			{
				return true;
			}
		}
		else
		{
			var total = value;
			for (int i = 1; i <= row; i++)
			{
				total += xs[i, col];
			}
			if (total >= MagicSum)
			{
				return true;
			}
		}
		return false;
	}
	static void M5()
	{
		var time = 0;
		time = A11(time);
	}

	private static int A11(int time)
	{
		for (int a11 = 1; a11 <= N2; a11++)
		{
			xs[1, 1] = a11;
			time = A12(time, a11);
			xs[1, 1] = 0;
		}

		return time;
	}

	private static int A12(int time, int a11)
	{
		for (int a12 = 1; a12 <= N2; a12++)
		{
			if (xs.Contains(a12) || Invalid(1, 2, a12))
			{
				continue;
			}
			xs[1, 2] = a12;
			time = A13(time, a11, a12);
			xs[1, 2] = 0;
		}

		return time;
	}

	private static int A13(int time, int a11, int a12)
	{
		for (int a13 = 1; a13 <= N2; a13++)
		{
			if (xs.Contains(a13) || Invalid(1, 3, a13))
			{
				continue;
			}
			xs[1, 3] = a13;
			time = A14(time, a11, a12, a13);
			xs[1, 3] = 0;
		}

		return time;
	}

	private static int A14(int time, int a11, int a12, int a13)
	{
		for (int a14 = 1; a14 <= N2; a14++)
		{
			if (xs.Contains(a14) || Invalid(1, 4, a14))
			{
				continue;
			}
			xs[1, 4] = a14;
			time = A21(time, a11, a12, a13, a14);
			xs[1, 4] = 0;
		}

		return time;
	}

	private static int A21(int time, int a11, int a12, int a13, int a14)
	{
		for (int a21 = 1; a21 <= N2; a21++)
		{
			if (xs.Contains(a21) || Invalid(2, 1, a21))
			{
				continue;
			}
			xs[2, 1] = a21;
			time = A22(time, a11, a12, a13, a14, a21);
			xs[2, 1] = 0;
		}

		return time;
	}

	private static int A22(int time, int a11, int a12, int a13, int a14, int a21)
	{
		for (int a22 = 1; a22 <= N2; a22++)
		{
			if (xs.Contains(a22) || Invalid(2, 2, a22))
			{
				continue;
			}
			xs[2, 2] = a22;
			time = A23(time, a11, a12, a13, a14, a21, a22);
			xs[2, 2] = 0;
		}

		return time;
	}

	private static int A23(int time, int a11, int a12, int a13, int a14, int a21, int a22)
	{
		for (int a23 = 1; a23 <= N2; a23++)
		{
			if (xs.Contains(a23) || Invalid(2, 3, a23))
			{
				continue;
			}
			xs[2, 3] = a23;
			time = A24(time, a11, a12, a13, a14, a21, a22, a23);
			xs[2, 3] = 0;
		}

		return time;
	}

	private static int A24(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23)
	{
		for (int a24 = 1; a24 <= N2; a24++)
		{
			if (xs.Contains(a24) || Invalid(2, 4, a24))
			{
				continue;
			}
			xs[2, 4] = a24;
			time = A31(time, a11, a12, a13, a14, a21, a22, a23, a24);
			xs[2, 4] = 0;
		}

		return time;
	}

	private static int A31(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24)
	{
		for (int a31 = 1; a31 <= N2; a31++)
		{
			if (xs.Contains(a31) || Invalid(3, 1, a31))
			{
				continue;
			}
			xs[3, 1] = a31;
			time = A32(time, a11, a12, a13, a14, a21, a22, a23, a24, a31);
			xs[3, 1] = 0;
		}

		return time;
	}

	private static int A32(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31)
	{
		for (int a32 = 1; a32 <= N2; a32++)
		{
			if (xs.Contains(a32) || Invalid(3, 2, a32))
			{
				continue;
			}
			xs[3, 2] = a32;
			time = A33(time, a11, a12, a13, a14, a21, a22, a23, a24, a31, a32);
			xs[3, 2] = 0;
		}

		return time;
	}

	private static int A33(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31, int a32)
	{
		for (int a33 = 1; a33 <= N2; a33++)
		{
			if (xs.Contains(a33) || Invalid(3, 3, a33))
			{
				continue;
			}
			xs[3, 3] = a33;
			time = A34(time, a11, a12, a13, a14, a21, a22, a23, a24, a31, a32, a33);
			xs[3, 3] = 0;
		}

		return time;
	}

	private static int A34(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31, int a32, int a33)
	{
		for (int a34 = 1; a34 <= N2; a34++)
		{
			if (xs.Contains(a34) || Invalid(3, 4, a34))
			{
				continue;
			}
			xs[3, 4] = a34;
			time = A41(time, a11, a12, a13, a14, a21, a22, a23, a24, a31, a32, a33, a34);
			xs[3, 4] = 0;
		}

		return time;
	}

	private static int A41(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31, int a32, int a33, int a34)
	{
		for (int a41 = 1; a41 <= N2; a41++)
		{
			if (xs.Contains(a41) || Invalid(4, 1, a41))
			{
				continue;
			}
			xs[4, 1] = a41;
			time = A42(time, a11, a12, a13, a14, a21, a22, a23, a24, a31, a32, a33, a34, a41);
			xs[4, 1] = 0;
		}

		return time;
	}

	private static int A42(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31, int a32, int a33, int a34, int a41)
	{
		for (int a42 = 1; a42 <= N2; a42++)
		{
			if (xs.Contains(a42) || Invalid(4, 2, a42))
			{
				continue;
			}
			xs[4, 2] = a42;
			time = A43(time, a11, a12, a13, a14, a21, a22, a23, a24, a31, a32, a33, a34, a41, a42);
			xs[4, 2] = 0;
		}

		return time;
	}

	private static int A43(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31, int a32, int a33, int a34, int a41, int a42)
	{
		for (int a43 = 1; a43 <= N2; a43++)
		{
			if (xs.Contains(a43) || Invalid(4, 3, a43))
			{
				continue;
			}
			xs[4, 3] = a43;
			time = A44(time, a11, a12, a13, a14, a21, a22, a23, a24, a31, a32, a33, a34, a41, a42, a43);
			xs[4, 3] = 0;
		}

		return time;
	}

	private static int A44(int time, int a11, int a12, int a13, int a14, int a21, int a22, int a23, int a24, int a31, int a32, int a33, int a34, int a41, int a42, int a43)
	{
		for (int a44 = 1; a44 <= N2; a44++)
		{
			if (xs.Contains(a44)
				|| (a14 + a23 + a32 + a41) != MagicSum
				|| Invalid(4, 4, a44)
				|| (a11 + a22 + a33 + a44) != MagicSum
				)
			{
				continue;
			}
			{
				Console.WriteLine($"bingo {MagicSum} times={++time}");
				Console.WriteLine($"{a11} {a12} {a13} {a14}");
				Console.WriteLine($"{a21} {a22} {a23} {a24}");
				Console.WriteLine($"{a31} {a32} {a33} {a34}");
				Console.WriteLine($"{a41} {a42} {a43} {a44}");
			}
		}

		return time;
	}
}

