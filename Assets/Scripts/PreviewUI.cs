using UnityEngine;

public class PreviewUI : MonoBehaviour {

	public GameObject previewContent;
	public TilePrefab tile;

	private TilePrefab[][] previewTiles;

	public void DestroyPreviewTiles()
	{
		for(int i = 0; i < previewTiles.Length; i++)
		{
			for(int j = 0; j < previewTiles[i].Length; j++)
			{
				Destroy(previewTiles[i][j]);
			}
		}
	}

	public void CreatePreviewTiles(int width, int height)
	{
		if(previewTiles != null) {DestroyPreviewTiles();}

		previewTiles = new TilePrefab[height][];

		for(int heightIndex = height-1; heightIndex >= 0; heightIndex--)    // 위쪽부터 생성해야 가장 아래쪽 타일이 위로 오므로 이미지가 커도 보임.
		{
			previewTiles[heightIndex] = new TilePrefab[width];
			for(int widthIndex = 0; widthIndex  < width; widthIndex ++)
			{
				previewTiles[heightIndex][widthIndex] = Instantiate(tile, previewContent.transform);
			}
		}
	}
}
