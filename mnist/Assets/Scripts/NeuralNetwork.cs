using System.Collections;
using System.Collections.Generic;
using System;

public partial class NeuralNetwork {

	int[] layer; // is the number of layers and their neurons
	Layer[] layers; // Layer Objects

	Layer[] layers_Start;
	Layer[] layers_Delta;

	public NeuralNetwork (int[] layer) 
	{
		this.layer = layer;

		layers = new Layer[layer.Length-1]; // It initializes the neurnas by passing on the number of nueronas and connections it must have

		layers_Start = new Layer[layer.Length-1];
		layers_Delta = new Layer[layer.Length-1];

		for(int i = 0; i < layers.Length; i++)
		{
			layers[i] = new Layer(layer[i], layer[i+1]);

			layers_Start[i] = new Layer(layer[i], layer[i+1]);
			layers_Delta[i] = new Layer(layer[i], layer[i+1]);
		}
	}

	public float[] FeedForward(float[] inputs)
	{
		layers[0].FeedForward(inputs);
		for(int i = 1; i < layers.Length; i ++)
		{
			layers[i].FeedForward(layers[i-1].outputs);
		}
		return layers[layers.Length-1].outputs;
	}

	public void BackProp(float[] expected)
	{
		for(int i = layers.Length-1; i >= 0; i--)
		{
			if(i == layers.Length-1)
			{
				layers[i].BackPropOutput(expected);
			}
			else
			{
				layers[i].BackPropHidden(layers[i+1].errorS, layers[i+1].weights);
			}
		}
	}

	public void UpdateNetwork()
	{
		for(int i = 0; i < layers.Length; i++)
		{
			layers[i].UpdateWeightsBias();
		}
	}
} 
