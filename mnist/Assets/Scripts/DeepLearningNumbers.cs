using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class DeepLearningNumbers : MonoBehaviour
{

	public int batch = 100;
	public float[] values;
	public int n = 10000;

	private MNISTread mnist;
	NeuralNetwork net;

	void Start()
	{
		mnist = this.GetComponent<MNISTread>();
		net = new NeuralNetwork(new int[] { 784, 30, 30, 10 });
	}
	public void Calcular()
	{

		float[][] entrance;
		entrance = new float[batch][];

		float[][] expected;
		expected = new float[batch][];

		for (int i = 0; i < entrance.Length; i++)
		{
			entrance[i] = new float[28 * 28];
			expected[i] = new float[10];
		}

		for (int i = 0; i < 60000 / batch; i++)// 60 000 -> 59 900
		{
			for (int j = 0; j < batch; j++) // Pass the input information
			{
				entrance[j] = mnist.valuesofTraining[(i * batch) + j];
				float[] lab = new float[10];
				lab[(int)mnist.lablesTraining[(i * batch) + j]] = 1;
				expected[j] = lab;
				net.FeedForward(entrance[j]);
				net.BackProp(expected[j]);
				net.UpdateNetwork();
			}
		}
		Try();
	}
	public void TestAll()
	{
		int right = 0;
		for (int i = 0; i < n; i++)
		{
			var target = mnist.valuesTesting[i];
			values = net.FeedForward(target);
			var guess = values.Select((x, i) => new { x, i }).OrderByDescending(e => e.x).First().i;
			if (guess == (int)(mnist.lablesTesting[i]))
			{
				++right;
			}
		}
		Debug.Log($"{right} of {n} rate={1.0f*right/n}");
	}
	public void Try()
	{
		int random = UnityEngine.Random.Range(1, 10000);
		var target = mnist.valuesTesting[random];
		values = net.FeedForward(target);
		Debug.Log("Label: " + mnist.lablesTesting[random]);
		mnist.DisplayNumber(mnist.valuesTesting[random], random);
		UnityEngine.Debug.Log("Probability that it is a 0: " + values[0]);
		UnityEngine.Debug.Log("Probability that it is a 1: " + values[1]);
		UnityEngine.Debug.Log("Probability that it is a 2: " + values[2]);
		UnityEngine.Debug.Log("Probability that it is a 3: " + values[3]);
		UnityEngine.Debug.Log("Probability that it is a 4: " + values[4]);
		UnityEngine.Debug.Log("Probability that it is a 5: " + values[5]);
		UnityEngine.Debug.Log("Probability that it is a 6: " + values[6]);
		UnityEngine.Debug.Log("Probability that it is a 7: " + values[7]);
		UnityEngine.Debug.Log("Probability that it is a 8: " + values[8]);
		UnityEngine.Debug.Log("Probability that it is a 9: " + values[9]);
		Debug.Log($"guess {values.Select((x, i) => new { x, i }).OrderByDescending(e => e.x).First().i}");
	}

	public void read()
	{
		values = net.FeedForward(mnist.readInfo());
	}
}
