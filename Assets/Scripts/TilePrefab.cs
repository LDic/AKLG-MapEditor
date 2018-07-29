using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TilePrefab : MonoBehaviour {

	public Image[] images;	// 0 : tile, 1 : offTile, 2 : entity
	public int[] disposedItems;
	private MapEditorManager editorInstance;

	// Use this for initialization
	void Awake () {
		editorInstance = MapEditorManager.Instance;
		disposedItems = new int[images.Length];
		ClearAllImages();
	}

	public void ClearImage(int typeIndex)
	{
		images[typeIndex].sprite = null;
		images[typeIndex].gameObject.SetActive(false);
	}

	public void ClearAllImages()
	{
		for(int i = 0; i < images.Length; i++)
		{
			ClearImage(i);
		}
	}

	public void DisposeImage()
	{
		int typeIndex = editorInstance.selectedTypeIndex;
		disposedItems[typeIndex] = editorInstance.selectedItemIndex;
		images[typeIndex].sprite = editorInstance.selectedSpriteImage;

		images[typeIndex].gameObject.SetActive(true);
		// 현재 선택된 image가 empty일 시 비활성화
		if(editorInstance.selectedSpriteImage == null) {images[typeIndex].gameObject.SetActive(false);}
		else
		{
			// 크기 조절
			images[typeIndex].gameObject.transform.localScale = new Vector3(editorInstance.selectedSpriteImage.texture.width/256f, editorInstance.selectedSpriteImage.texture.height/256f);
		}
	}

	// 드래그 중에도 타일 배치
	public void IsDragging()
	{
		if(Input.GetMouseButton(0))
			DisposeImage();
	}

}
