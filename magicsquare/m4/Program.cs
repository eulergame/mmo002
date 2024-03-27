// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

const int N = 16;
static void M4(List<int> xs, int s)
{
	var time = 0;
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
				if (xs.Contains(a13) || (a11 + a12 + a13) >= s)
				{
					continue;
				}
				xs.Add(a13);
				for (int a14 = 1; a14 <= N; a14++)
				{
					if (xs.Contains(a14) || (a11 + a12 + a13 + a14 != s))
					{
						continue;
					}
					xs.Add(a14);
					for (int a21 = 1; a21 <= N; a21++)
					{
						if (xs.Contains(a21) || (a11 + a21 >= s))
						{
							continue;
						}
						xs.Add(a21);
						for (int a22 = 1; a22 <= N; a22++)
						{
							if (xs.Contains(a22) || (a21 + a22) >= s || (a12 + a22) >= s)
							{
								continue;
							}
							xs.Add(a22);
							for (int a23 = 1; a23 <= N; a23++)
							{
								if (xs.Contains(a23) || (a21 + a22 + a23) >= s || (a13 + a23) >= s)
								{
									continue;
								}
								xs.Add(a23);
								for (int a24 = 1; a24 <= N; a24++)
								{
									if (xs.Contains(a24) || (a21 + a22 + a23 + a24) != s)
									{
										continue;
									}
									xs.Add(a24);
									for (int a31 = 1; a31 <= N; a31++)
									{
										if (xs.Contains(a31) || (a11 + a21 + a31) >= s)
										{
											continue;
										}
										xs.Add(a31);
										for (int a32 = 1; a32 <= N; a32++)
										{
											if (xs.Contains(a32) || (a12 + a22 + a32) >= s || (a31 + a32) >= s)
											{
												continue;
											}
											xs.Add(a32);
											for (int a33 = 1; a33 <= N; a33++)
											{
												if (xs.Contains(a33) || (a31 + a32 + a33) >= s || (a13 + a23 + a33) >= s)
												{
													continue;
												}
												xs.Add(a33);
												for (int a34 = 1; a34 <= N; a34++)
												{
													if (xs.Contains(a34) || (a31 + a32 + a33 + a34) != s)
													{
														continue;
													}
													xs.Add(a34);
													for (int a41 = 1; a41 <= N; a41++)
													{
														if (xs.Contains(a41)
															|| (a11 + a21 + a31 + a41) != s
															|| (a14 + a23 + a32 + a41) != s
															)
														{
															continue;
														}
														xs.Add(a41);
														for (int a42 = 1; a42 <= N; a42++)
														{
															if (xs.Contains(a42) || (a12 + a22 + a32 + a42) != s)
															{
																continue;
															}
															xs.Add(a42);
															for (int a43 = 1; a43 <= N; a43++)
															{
																if (xs.Contains(a43) || (a13 + a23 + a33 + a43) != s)
																{
																	continue;
																}
																xs.Add(a43);
																for (int a44 = 1; a44 <= N; a44++)
																{
																	if (xs.Contains(a44)
																		|| (a14 + a24 + a34 + a44) != s
																		|| (a41 + a42 + a43 + a44) != s
																		|| (a11 + a22 + a33 + a44) != s
																		)
																	{
																		continue;
																	}
																	{
																		Console.WriteLine($"bingo {s} times={++time}");
																		Console.WriteLine($"{a11} {a12} {a13} {a14}");
																		Console.WriteLine($"{a21} {a22} {a23} {a24}");
																		Console.WriteLine($"{a31} {a32} {a33} {a34}");
																		Console.WriteLine($"{a41} {a42} {a43} {a44}");
																	}
																}
																xs.Remove(a43);
															}
															xs.Remove(a42);
														}
														xs.Remove(a41);
													}
													xs.Remove(a34);
												}
												xs.Remove(a33);
											}
											xs.Remove(a32);
										}
										xs.Remove(a31);
									}
									xs.Remove(a24);
								}
								xs.Remove(a23);
							}
							xs.Remove(a22);
						}
						xs.Remove(a21);
					}
					xs.Remove(a14);
				}
				xs.Remove(a13);
			}
			xs.Remove(a12);
		}
		xs.Remove(a11);
	}
}

static void Start()
{
	var xs = new List<int>();
	int s = (int)(N * (N + 1) / 2 / Math.Sqrt(N));
	M4(xs, s);
	Console.WriteLine("done");
}
Start();