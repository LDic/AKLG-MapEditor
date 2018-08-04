using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour {

	public GameObject tileScrollViewContent;
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

		spawnedTiles = new TilePrefab[height][];

		// Resize the ScrollView
		tileScrollViewContent.transform.localScale = new Vector3(1f, 1f, 1f);
		tileScrollViewContent.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0f, width*100f);
		tileScrollViewContent.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0f, height*100f);
		//tileScrollViewContent.SetSizeWithCurrentAnchors(0, width*100f);
		//tileScrollViewContent.SetSizeWithCurrentAnchors(0, height*100f);

		// Dispose tiles
		float xOffset = 55.5f;
		float yOffset = 60f;
		for(int heightIndex = height-1; heightIndex >= 0; heightIndex--)	// 위쪽부터 생성해야 가장 아래쪽 타일이 위로 오므로 이미지가 커도 보임.
		{
			spawnedTiles[heightIndex] = new TilePrefab[width];
			for(int widthIndex = 0; widthIndex < width; widthIndex++)
			{
				spawnedTiles[heightIndex][widthIndex] = Instantiate(tile, tileScrollViewContent.transform);
				spawnedTiles[heightIndex][widthIndex].transform.SetPositionAndRotation(new Vector3(xOffset + widthIndex*100f, yOffset + heightIndex*100f), Quaternion.identity);
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
	public void SetSpawnedTileData(int widthIndex, int heightIndex, string tileData, int[][] gotImageNumber, Sprite[][] spriteImageData)
	{
		string[] data = tileData.Split('/');
		for(int i = 0; i < data.Length; i++)
		{
			int tempItemIndex = int.Parse(data[i]);
			spawnedTiles[heightIndex][widthIndex].disposedItems[i] = tempItemIndex;
			if(tempItemIndex > 0)	// item이 null이 아닌 경우 이미지 적용.
			{
				// 저장된 인덱스 값에 맞는 sprite 할당
				int foundIndex = 0;
				while(gotImageNumber[i][foundIndex] != tempItemIndex) {foundIndex++; if(foundIndex > 1000) {return;}}	// 무한 루프 방지
				
				spawnedTiles[heightIndex][widthIndex].images[i].sprite = spriteImageData[i][foundIndex];
				spawnedTiles[heightIndex][widthIndex].images[i].gameObject.SetActive(true);
				// 크기 조절
				spawnedTiles[heightIndex][widthIndex].images[i].gameObject.transform.localScale = new Vector3(spriteImageData[i][foundIndex].texture.width/256f, spriteImageData[i][foundIndex].texture.height/256f);
			}
		}
	}

	// Toggle tile visibility
	public void ActivateTileVisibility(int typeIndex)
	{
		// 타일 생성 안 됐을 때 클릭 시 이 함수 무시
		if(spawnedTiles == null)
			return;

		for(int i = 0; i < spawnedTiles.Length; i++)
		{
			foreach(TilePrefab tile in spawnedTiles[i])
			{
				tile.images[typeIndex].color = Color.white;
			}
		}
	}
	public void DeactivateTileVisibility(int typeIndex)
	{
		// 타일 생성 안 됐을 때 클릭 시 이 함수 무시
		if(spawnedTiles == null)
			return;

		for(int i = 0; i < spawnedTiles.Length; i++)
		{
			foreach(TilePrefab tile in spawnedTiles[i])
			{
				tile.images[typeIndex].color = Color.clear;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
		// Zoom In
		if(Input.mouseScrollDelta.y > 0f)
		{
			tileScrollViewContent.GetComponent<RectTransform>().localScale += adjustingSize;
			tileScrollViewContent.GetComponent<RectTransform>().position = new Vector3(50f, 70.5f);
		}
		// Zoom Out
		if(Input.mouseScrollDelta.y < 0f)
		{
			tileScrollViewContent.GetComponent<RectTransform>().localScale -= adjustingSize;
			tileScrollViewContent.GetComponent<RectTransform>().position = new Vector3(50f, 70.5f);
		}
	}

}
