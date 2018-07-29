using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class MapEditorManager : MonoBehaviour {

	public static MapEditorManager Instance;

	[Header("Spawn")]
	public TileUI tileUI;

	[Header("Text")]
	public Text widthText;
	public Text heightText;
	public int Width {get; set;}
	public int Height {get; set;}

	// private Variables
	private Sprite[][] spriteImageData;				// 폴더로부터 가져온 이미지파일. 0 : Tile, 1 : Off Tile, etc.

	[Header("Selected Information")]
	public Sprite selectedSpriteImage;					// 선택된 이미지
	public int selectedTypeIndex, selectedItemIndex;    // 선택된 이미지의 유형, 해당 유형에서의 인덱스


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
		string[] filePathList = Directory.GetDirectories(UnityEngine.Application.dataPath + "/Textures/");
		spriteImageData = new Sprite[Directory.GetDirectories(UnityEngine.Application.dataPath+"/Textures/").Length][];

		for(int i = 0; i < filePathList.Length; i++)
		{
			string[] temp = Directory.GetFiles(filePathList[i], "*.png");
			spriteImageData[i] = new Sprite[temp.Length];

			for(int j = 0; j < temp.Length; j++)
			{
				if(!temp[j].EndsWith(".meta"))
				{
					Texture2D tempTexture = GetTextureFromLocal(temp[j]);
					spriteImageData[i][j] = Sprite.Create(tempTexture, new Rect(0f, 0f, (float)tempTexture.width, (float)tempTexture.height), Vector2.zero);
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
	public void SaveMap()
	{
		SaveFileDialog saveFile = new SaveFileDialog();
		saveFile.Title = "저장 경로를 선택하세요.";
		saveFile.Filter = "CSV File(*.csv)|*.csv";

		if(saveFile.ShowDialog() == DialogResult.OK)
		{
			string saveFilePath = saveFile.FileName;
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
		OpenFileDialog openFile = new OpenFileDialog();
		openFile.Title = "불러올 파일을 선택하세요.";
		openFile.Filter = "CSV File(*.csv)|*.csv";

		if(openFile.ShowDialog() == DialogResult.OK)
		{
			string loadFilePath = openFile.FileName;

			string[] readData = File.ReadAllLines(loadFilePath);    // 0 : 맵 크기. 1~ : tile의 데이터.
			string[] sizeData = readData[0].Split(',');
			int widthSize = int.Parse(sizeData[0]);
			int heightSize = int.Parse(sizeData[1]);

			Width = widthSize; Height = heightSize;

			// Create Tiles
			tileUI.CreateTiles(widthSize, heightSize);

			for(int i = 1; i < readData.Length; i++)    // readData의 인덱스 1 부터 tile 데이터가 시작되므로 0은 제외.
			{
				string[] temp = readData[i].Split(',');
				for(int j = 0; j < temp.Length; j++)
				{
					tileUI.SetSpawnedTileData(j, i-1, temp[j], spriteImageData);
				}
			}
		}
	}

}
