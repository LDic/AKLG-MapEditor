﻿using UnityEngine;
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
	private Sprite[][] spriteImageData;				// 폴더로부터 가져온 이미지파일. 0 : Tile, 1 : Off Tile, etc.

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
		
		BringTextureData();
	}

	// 맵 생성
	public void SetSize()
	{
		int temp;
		if(!int.TryParse(widthText.text, out temp)) {Width = 1;}
		else {Width = int.Parse(widthText.text);}
		if(!int.TryParse(heightText.text, out temp)) {Height = 1;}
		else {Height = int.Parse(heightText.text);}
	}

	public void CreateTiles(int width, int height)
	{
		tileUI.CreateTiles(width, height);
	}

	// Sprite Image Data Get 함수
	public Sprite GetSpriteImageData(int typeIndex, int itemIndex)
	{
		return spriteImageData[typeIndex][itemIndex];
	}
	public int GetSpriteImageDataLength(int typeIndex)
	{
		return spriteImageData[typeIndex].Length;
	}

	/** Save texture data from folder */
	public void BringTextureData()
	{
		string[] filePathList = Directory.GetDirectories(Application.dataPath + "/Textures/");
		spriteImageData = new Sprite[Directory.GetDirectories(Application.dataPath+"/Textures/").Length][];

		for(int i = 0; i < filePathList.Length; i++)
		{
			string[] temp = Directory.GetFiles(filePathList[i]);
			spriteImageData[i] = new Sprite[temp.Length/2]; // GetFiles()가 .meta 파일까지 포함하므로 제외하기 위해 2로 나눠 .png 개수만큼만 할당.

			for(int j = 0; j < temp.Length; j++)
			{
				if(!temp[j].EndsWith(".meta"))
				{
					Texture2D tempTexture = GetTextureFromLocal(temp[j]);
					spriteImageData[i][j/2] = Sprite.Create(tempTexture, new Rect(0f, 0f, (float)tempTexture.width, (float)tempTexture.height), Vector2.zero);
				}
			}
		}

	}
	private Texture2D GetTextureFromLocal(string filePath)
	{
		Texture2D tex = null;
		byte[] fileData;

		if(File.Exists(filePath))
		{
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}


	// Exporting to CSV file.
	public void Save()
	{
		string saveFilePath = EditorUtility.SaveFilePanel("Save csv file", "", "Saved_data.csv", "csv");
		if(saveFilePath.Length > 0)
		{
			using(StreamWriter outStream = new StreamWriter(saveFilePath))
			{
				outStream.WriteLine(Width.ToString() + "," + Height.ToString());

				string[][] fileData = tileUI.GetSpawnedTileData();
				for(int i = 0; i < fileData.Length; i++)
				{
					for(int j = 0; j < fileData[i].Length - 1; j++)
					{
						outStream.Write(fileData[i][j] + ",");
					}
					outStream.WriteLine(fileData[i][fileData[i].Length - 1]);
				}
			}
		}
	}

	// Loading Data
	public void LoadMap()
	{
		string loadFilePath = EditorUtility.OpenFilePanel("Load csv file", "", "csv");

		string[] readData = File.ReadAllLines(loadFilePath);	// 0 : 맵 크기. 1~ : tile의 데이터.
		int widthSize = readData[0].ToCharArray()[0] - 48;
		int heightSize = readData[0].ToCharArray()[2] - 48;

		Width = widthSize; Height = heightSize;

		// Create Tiles
		tileUI.CreateTiles(widthSize, heightSize);

		for(int i = 1; i < readData.Length; i++)	// readData의 인덱스 1 부터 tile 데이터가 시작되므로 0은 제외.
		{
			string[] temp = readData[i].Split(',');
			for(int j = 0; j < temp.Length; j++)
			{
				tileUI.SetSpawnedTileData(j, i-1, temp[j], spriteImageData);
			}
		}
	}

}
