using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepLearningNumeros : MonoBehaviour {

	public int batch = 100;
	public float[] values;

	private MNISTread mnist;
	NeuralNetwork net;

	void Start()
	{
		mnist = this.GetComponent<MNISTread>();
		net = new NeuralNetwork(new int[] {784, 30, 30, 10});
	}
	public void Calcular () {

		float[][] entrance;
		entrance = new float[batch][];

		float[][] expected;
		expected = new float[batch][];

		for(int i = 0; i < entrance.Length; i++)
		{
			entrance[i] = new float[28*28];
			expected[i] = new float[10];
		}

		for(int i = 0; i < 60000/batch; i++)// 60 000 -> 59 900
		{
			for(int j = 0; j < batch; j++) // Pass the input information
			{
				entrance[j] = mnist.valuesofTraining[(i*batch)+j];
				float[] lab = new float[10];
				lab[(int)mnist.lablesTraining[(i*batch)+j]] = 1;
				expected[j] = lab;
				net.FeedForward(entrance[j]);
				net.BackProp(expected[j]);
				net.UpdateNetwork();

			}
		}
		Probar();
	}

	public void Probar()
	{
		int alazar = Random.Range(1, 10000);
		values = net.FeedForward(mnist.valuesTesting[alazar]);
		//Debug.Log("Label: " + mnist.lablesEntrenamiento[alazar]);
		mnist.VisualizarNumero(mnist.valuesTesting[alazar], alazar);
		UnityEngine.Debug.Log("Probability that it is a 0: " + net.FeedForward(mnist.valuesTesting[alazar])[0]);
		UnityEngine.Debug.Log("Probability that it is a 1: " + net.FeedForward(mnist.valuesTesting[alazar])[1]);
		UnityEngine.Debug.Log("Probability that it is a 2: " + net.FeedForward(mnist.valuesTesting[alazar])[2]);
		UnityEngine.Debug.Log("Probability that it is a 3: " + net.FeedForward(mnist.valuesTesting[alazar])[3]);
		UnityEngine.Debug.Log("Probability that it is a 4: " + net.FeedForward(mnist.valuesTesting[alazar])[4]);
		UnityEngine.Debug.Log("Probability that it is a 5: " + net.FeedForward(mnist.valuesTesting[alazar])[5]);
		UnityEngine.Debug.Log("Probability that it is a 6: " + net.FeedForward(mnist.valuesTesting[alazar])[6]);
		UnityEngine.Debug.Log("Probability that it is a 7: " + net.FeedForward(mnist.valuesTesting[alazar])[7]);
		UnityEngine.Debug.Log("Probability that it is a 8: " + net.FeedForward(mnist.valuesTesting[alazar])[8]);
		UnityEngine.Debug.Log("Probability that it is a 9: " + net.FeedForward(mnist.valuesTesting[alazar])[9]);

	}

	public void leer()
	{
		values = net.FeedForward(mnist.leerInfo());
	}
}
