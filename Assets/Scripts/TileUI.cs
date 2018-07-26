using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour {

	public RectTransform tileScrollViewContent;
	public Vector3 adjustingSize;
	public TilePrefab tile;

	private TilePrefab[][] spawnedTiles;

	public void ClearDisposedImages(int typeIndex)
	{
		if(spawnedTiles != null)
		{
			for(int i = 0; i < spawnedTiles.Length; i++)
			{
				foreach(TilePrefab tile in spawnedTiles[i])
				{
					tile.ClearImage(typeIndex);
				}
			}
		}
	}
	public void ClearAllDisposedImages()
	{
		if(spawnedTiles != null)
		{
			for(int i = 0; i < spawnedTiles.Length; i++)
			{
				foreach(TilePrefab tile in spawnedTiles[i])
				{
					tile.ClearAllImages();
				}
			}
		}
	}

	public void DestroyTiles()
	{
		if(spawnedTiles != null)
		{
			for(int i = 0; i < spawnedTiles.Length; i++)
			{
				foreach(TilePrefab tile in spawnedTiles[i])
				{
					Destroy(tile.gameObject);
				}
			}
		}
	}

	public void InitializeTiles()
	{
		ClearAllDisposedImages();
		DestroyTiles();
	}

	public void CreateTiles(int width, int height)
	{
		InitializeTiles();

		// Resize the ScrollView
		tileScrollViewContent.localScale = new Vector3(1f, 1f, 1f);
		tileScrollViewContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width*100f);
		tileScrollViewContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height*100f);
		tileScrollViewContent.SetPositionAndRotation(new Vector3(0f, height*100f), Quaternion.identity);

		spawnedTiles = new TilePrefab[height][];

		// Dispose tiles
		for(int heightIndex = 0; heightIndex < height; heightIndex++)
		{
			spawnedTiles[heightIndex] = new TilePrefab[width];
			for(int widthIndex = 0; widthIndex < width; widthIndex++)
			{
				spawnedTiles[heightIndex][widthIndex] = Instantiate(tile, tileScrollViewContent);
				spawnedTiles[heightIndex][widthIndex].transform.SetPositionAndRotation(new Vector3(widthIndex*100f, heightIndex*100f), Quaternion.identity);
			}
		}
	}

	// Get spawnedTiles' Data for Save
	public string[][] GetSpawnedTileData()
	{
		string[][] tileData = new string[spawnedTiles.Length][];	// 가로 * 세로 크기 할당

		for(int i = 0; i < tileData.Length; i++)
		{
			tileData[i] = new string[spawnedTiles[i].Length];
			for(int j = 0; j < tileData[i].Length; j++)
			{
				for(int k = 0; k < spawnedTiles[0][0].images.Length; k++)
				{
					string temp = spawnedTiles[i][j].disposedItems[k].ToString();
					tileData[i][j] += temp + "/";
				}
				tileData[i][j] = tileData[i][j].Remove(tileData[i][j].Length-1);	// 마지막 / 문자 제거
			}
		}
		
		return tileData;
	}

	// Set spawnedTiles' Data when loaded
	public void SetSpawnedTileData(int widthIndex, int heightIndex, string tileData, Sprite[][] spriteImageData)
	{
		string[] data = tileData.Split('/');
		for(int i = 0; i < data.Length; i++)
		{
			int tempItemIndex = data[i].ToCharArray()[0] - 48;
			spawnedTiles[heightIndex][widthIndex].disposedItems[i] = tempItemIndex;
			if(tempItemIndex > 0)	// item이 null이 아닌 경우 이미지 적용.
			{
				spawnedTiles[heightIndex][widthIndex].images[i].sprite = spriteImageData[i][tempItemIndex - 1];
				spawnedTiles[heightIndex][widthIndex].images[i].gameObject.SetActive(true);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		// Zoom In
		if(Input.mouseScrollDelta.y > 0f)
		{
			tileScrollViewContent.localScale = tileScrollViewContent.localScale + adjustingSize;
		}
		// Zoom Out
		if(Input.mouseScrollDelta.y < 0f)
		{
			tileScrollViewContent.localScale = tileScrollViewContent.localScale - adjustingSize;
		}
	}
}
