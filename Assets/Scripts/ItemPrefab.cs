using UnityEngine;
using UnityEngine.UI;

public class ItemPrefab : MonoBehaviour {

	public int itemIndex;			// index : 같은 분야에서의 인덱스. (-1 : 초기화, 0 : 빈 칸, 1~ : 해당 이미지)
	public int typeIndex;			// typeIdex : 0 - tile, 1 - off tile, etc.
	public Sprite itemSpriteImage;	// Sprite Image of this Item.
	public Text text;				// self text

	private MapEditorManager editorInstance;

	// Use this for initialization
	void Start () {
		editorInstance = MapEditorManager.Instance;
	}

	public void SelectItem()
	{
		editorInstance.selectedItemIndex = itemIndex;
		editorInstance.selectedTypeIndex = typeIndex;
		editorInstance.selectedSpriteImage = itemSpriteImage;
		MainUI.SetSelectedImage(itemSpriteImage);
		// 초기화 버튼일 시 안내 창 띄움
		if(itemIndex == -1)
		{
			NoticeUI.AddToViewport(2);
		}
	}

	public void SetSpriteImage(Sprite spriteImage)
	{
		itemSpriteImage = spriteImage;
		gameObject.GetComponent<Image>().sprite = spriteImage;
	}
	
}
