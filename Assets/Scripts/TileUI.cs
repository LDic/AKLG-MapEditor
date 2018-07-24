using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour {

	public RectTransform tileScrollViewContent;
	public Vector3 adjustingSize;
	public TilePrefab tile;

	private TilePrefab[,] spawnedTiles;

	public void ClearAllDisposedImages()
	{
		if(spawnedTiles != null)
		{
			foreach(TilePrefab tile in spawnedTiles)
			{
				tile.ClearAllImages();
			}
		}
	}

	public void DestroyTiles()
	{
		if(spawnedTiles != null)
		{
			foreach(TilePrefab tile in spawnedTiles)
			{
				Destroy(tile.gameObject);
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
		tileScrollViewContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width*100f);
		tileScrollViewContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height*100f);
		tileScrollViewContent.SetPositionAndRotation(new Vector3(0f, height*100f), Quaternion.identity);

		spawnedTiles = new TilePrefab[height, width];

		// Dispose tiles
		for(int heightIndex = 0; heightIndex < height; heightIndex++)
		{
			for(int widthIndex = 0; widthIndex < width; widthIndex++)
			{
				spawnedTiles[heightIndex, widthIndex] = Instantiate(tile, tileScrollViewContent);
				spawnedTiles[heightIndex, widthIndex].transform.SetPositionAndRotation(new Vector3(widthIndex*100f, heightIndex*100f), Quaternion.identity);
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
