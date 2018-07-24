using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEditor;

public class MapEditorManager : MonoBehaviour {

	public static MapEditorManager Instance;

	[Header("Spawn")]
	public TileUI tileUI;
	/*public GameObject wallPrefab;
	public GameObject characterPrefab;
	public GameObject enemy1Prefab;*/

	[Header("Text")]
	public Text widthText;
	public Text heightText;
	public int Width {get; set;}
	public int Height {get; set;}

	// private Variables
	private TilePrefab spawnedTiles;
	private GameObject selectedObject;	//삭제 대기
	public GameObject SelectedObject	//삭제 대기
	{
		get{return selectedObject;}
		set{selectedObject = value;}
	}

	[Header("Selected Information")]
	public Sprite selectedSpriteImage;					// 선택된 이미지
	public int selectedTypeIndex, selectedItemIndex;	// 선택된 이미지의 유형, 해당 유형에서의 인덱스

	void Awake()
	{
		if(Instance != null)
		{
			Debug.LogError("There's another MapEditorManager!");
		}
		Instance = this;
	}

	void Start()
	{
		Width = 5; Height = 5;
		//CreateTiles(Width, Height);
	}

	public void SetSize()
	{
		int temp;
		if(!int.TryParse(widthText.text, out temp)) {Width = 1;}
		else {Width = int.Parse(widthText.text);}
		if(!int.TryParse(heightText.text, out temp)) {Height = 1;}
		else {Height = int.Parse(heightText.text);}
	}

	/*
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
	}*/

	public void CreateTiles(int width, int height)
	{
		/*ClearDisposedObject();
		DestroyTiles();
		spawnedTiles = new Tile[height, width];

		for(int heightIndex = 0; heightIndex < height; heightIndex++)
		{
			for(int widthIndex = 0; widthIndex < width; widthIndex++)
			{
				spawnedTiles[heightIndex, widthIndex] = Instantiate(tilePrefab, new Vector3(tileSpawnOffset.x, 0f, (float)heightIndex) + new Vector3((float)widthIndex, 0f, tileSpawnOffset.z), Quaternion.identity, tileSpawnScreen);
			}
		}*/
		tileUI.CreateTiles(width, height);

	}

	// Exporting to CSV file.
	//private List<string[]> rowData = new List<string[]>();
	
	public void Save()
	{
		List<string[]> rowData = new List<string[]>();
		string[] rowDataTemp = new string[Width*Height + 2];
		
		rowDataTemp[0] = Width.ToString();
		rowDataTemp[1] = Height.ToString();

		int rowDataIndex = 2;
		/*
		foreach(TilePrefab tile in spawnedTiles)
		{
			rowDataTemp[rowDataIndex] = tile.disposedObjectIndex.ToString();
			rowDataIndex++;
		}
		*/
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
		using(StreamWriter outStream = new StreamWriter(filePath, true))
		{
			outStream.Write(sb);
		}


		//string test = EditorUtility.OpenFilePanel("Saving Data", Application.dataPath, "csv");
	}
}
