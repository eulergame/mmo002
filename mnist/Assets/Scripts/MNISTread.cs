using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MNISTread : MonoBehaviour {

	public GameObject cubePrefab;
	public GameObject[][] cubes;
	public float[] values;
	public float[][] valuesofTraining;
	public float[] lablesTraining;
	public float[][] valuesTesting;
	public float[] lablesTesting;
	public int currentIssue;

	void Awake(){

		lablesTraining = new float[60000]; // Save the image number
		valuesofTraining = new float[60000][]; // Save 28*28 pixels information

		lablesTesting = new float[10000];
		valuesTesting = new float[10000][];

		for(int v = 0; v < valuesofTraining.Length; v++) // initializes the values of the train to 0
			valuesofTraining[v] = new float[28*28];

		for(int v = 0; v < valuesTesting.Length; v++)
			valuesTesting[v] = new float[28*28];

		CreateCubes();
		Initialize("train-labels-idx1-ubyte","train-images-idx3-ubyte");
		InitializeTests("t10k-labels-idx1-ubyte", "t10k-images-idx3-ubyte");
	}

	void Initialize (string label_nombre, string images_nombre) {

		FileStream ifsLabels = File.Open(Application.dataPath + "/" + label_nombre, FileMode.Open);
		FileStream ifsImages = File.Open(Application.dataPath + "/" + images_nombre, FileMode.Open);

		BinaryReader brLabels = new BinaryReader(ifsLabels);
		BinaryReader brImages = new BinaryReader(ifsImages);
		
		int magic1 = brImages.ReadInt32(); // discard
        int numImages = brImages.ReadInt32(); 
        int numRows = brImages.ReadInt32(); 
        int numCols = brImages.ReadInt32(); 

        int magic2 = brLabels.ReadInt32(); 
        int numLabels = brLabels.ReadInt32(); 

		byte[][] pixels = new byte[28][];
        for (int i = 0; i < pixels.Length; ++i)
          pixels[i] = new byte[28];

		for (int di = 0; di < 60000; ++di) 
        {
        	for (int i = 0; i < 28; ++i)
        	{
            	for (int j = 0; j < 28; ++j)
            	{
            		byte b = brImages.ReadByte();
            		pixels[i][j] = b;
					valuesofTraining[di][(28*i)+j] = pixels[i][j];
            	}
        	}

        	byte lbl = brLabels.ReadByte();
			lablesTraining[di] = lbl;
    	} // each image
	}

	void InitializeTests (string label_nombre, string images_nombre) {

		FileStream ifsLabels = File.Open(Application.dataPath + "/" + label_nombre, FileMode.Open);
		FileStream ifsImages = File.Open(Application.dataPath + "/" + images_nombre, FileMode.Open);

		BinaryReader brLabels = new BinaryReader(ifsLabels);
		BinaryReader brImages = new BinaryReader(ifsImages);
		
		int magic1 = brImages.ReadInt32(); // discard
        int numImages = brImages.ReadInt32(); 
        int numRows = brImages.ReadInt32(); 
        int numCols = brImages.ReadInt32(); 

        int magic2 = brLabels.ReadInt32(); 
        int numLabels = brLabels.ReadInt32(); 

		byte[][] pixels = new byte[28][];
        for (int i = 0; i < pixels.Length; ++i)
          pixels[i] = new byte[28];

		for (int di = 0; di < 10000; ++di) 
        {
        	for (int i = 0; i < 28; ++i)
        	{
            	for (int j = 0; j < 28; ++j)
            	{
            		byte b = brImages.ReadByte();
            		pixels[i][j] = b;
					valuesTesting[di][(28*i)+j] = pixels[i][j];
            	}
        	}

        	byte lbl = brLabels.ReadByte();
			lablesTesting[di] = lbl;
			DisplayNumber(valuesTesting[10000-1], 10000-1);
    	} // each image
	}

	public void DisplayNumber(float[] info, int labelId)
	{
		for (int i = 0; i < 28; ++i)
      	{
      		for (int j = 0; j < 28; ++j)
      	 	{
				Renderer rend = cubes[i][j].GetComponent<Renderer>();
				rend.material.color = new Color32((byte)info[(28*i)+j], (byte)info[(28*i)+j], (byte)info[(28*i)+j], 1);
        	}
      	}

		currentIssue = (int) lablesTesting[labelId];
	}

	public void Borrar()
	{
		for (int i = 0; i < 28; ++i)
      	{
      		for (int j = 0; j < 28; ++j)
      	 	{
				Renderer rend = cubes[i][j].GetComponent<Renderer>();
				rend.material.color = Color.black;
        	}
      	}
	}

	public float[] readInfo()
	{
		float[] val = new float[28*28];
		for (int i = 0; i < 28; ++i)
      	{
      		for (int j = 0; j < 28; ++j)
      	 	{
				val[(28*i)+j] = cubes[i][j].GetComponent<Renderer>().material.color.r;
        	}
      	}
		return val;
	}

	void CreateCubes()
	{
		// It takes care of spawnering all the cubes for visualization
		cubes = new GameObject[28][];
		for(int c = 0; c < cubes.Length; c++)
			cubes[c] = new GameObject[28];

		GameObject parent = new GameObject("Drawing");

		for(int col = 0; col < 28; col++)
		{
			for(int row = 0; row < 28; row++)
			{
				Vector3 pos = new Vector3(-14+row, 14-col,0);
				cubes[col][row] = (GameObject) Instantiate(cubePrefab,pos, Quaternion.identity);
				cubes[col][row].transform.name = "id_" + ((col*28)+row).ToString();
				cubes[col][row].transform.parent = parent.transform;
			}
		}
	}
}
