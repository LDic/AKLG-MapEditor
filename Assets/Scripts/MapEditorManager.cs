using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class MapEditorManager : MonoBehaviour {

	public static MapEditorManager instance;

	[Header("Spawn Object")]
	public Tile tilePrefab;
	public GameObject wallPrefab;
	public GameObject characterPrefab;
	public GameObject enemy1Prefab;

	[Header("Text")]
	public Text widthText;
	public Text heightText;
	public int Width {get; set;}
	public int Height {get; set;}

	[Header("Color")]
	public Color normalColor;
	public Color hoverColor;

	// private Variables
	private Tile[,] spawnedTiles;
	private GameObject selectedObject;
	public GameObject SelectedObject
	{
		get{return selectedObject;}
		set{selectedObject = value;}
	}

	void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("There's another MapEditorManager!");
		}
		instance = this;
	}

	void Start()
	{
	}

	public void SetSize()
	{
		int temp;
		if(!int.TryParse(widthText.text, out temp)) {Width = 1;}
		else {Width = int.Parse(widthText.text);}
		if(!int.TryParse(heightText.text, out temp)) {Height = 1;}
		else {Height = int.Parse(heightText.text);}
	}

	public void ClearDisposedObject()
	{
		if(spawnedTiles != null)
		{
			foreach(Tile tile in spawnedTiles)
			{
				Destroy(tile.disposedObject);
			}
		}
	}

	public void DestroyTiles()
	{
		if(spawnedTiles != null)
		{
			foreach(Tile tile in spawnedTiles)
			{
				Destroy(tile.gameObject);
			}
		}
	}

	public void CreateTiles(int width, int height)
	{
		DestroyTiles();
		spawnedTiles = new Tile[height, width];

		for(int heightIndex = 0; heightIndex < height; heightIndex++)
		{
			for(int widthIndex = 0; widthIndex < width; widthIndex++)
			{
				spawnedTiles[heightIndex, widthIndex] = Instantiate(tilePrefab, new Vector3(0f, 0f, (float)heightIndex) + new Vector3((float)widthIndex, 0f), Quaternion.identity);
			}
		}
	}

	// Exporting to CSV file.
	private List<string[]> rowData = new List<string[]>();
	public void Save()
	{
		string[] rowDataTemp = new string[3];
		rowDataTemp[0] = "Tile";
		rowDataTemp[1] = "Object";
		rowDataTemp[2] = "Number";
		rowData.Add(rowDataTemp);
		
		string[][] output = new string[rowData.Count][];
		for(int i = 0; i < output.Length; i++)
		{
			output[i] = rowData[i];
		}

		int length = output.GetLength(0);
		string delimiter = ",";
		StringBuilder sb = new StringBuilder();
		for(int index = 0; index < length; index++)
		{
			sb.AppendLine(string.Join(delimiter, output[index]));
		}
		
		string filePath = Application.dataPath+"/"+"Saved_data.csv";
		StreamWriter outStream = File.CreateText(filePath);
		outStream.WriteLine(sb);
		outStream.Close();
	}
}
