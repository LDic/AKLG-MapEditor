using UnityEngine;
using UnityEngine.UI;

public class ItemPrefab : MonoBehaviour {

	public int itemIndex;			// index : 같은 분야에서의 인덱스. (0 : 초기화, 1 : 빈 칸, 2~ : 해당 이미지)
	public int typeIndex;			// typeIdex : 0 - tile, 1 - off tile, etc.
	public Sprite itemSpriteImage;	// Sprite Image of this Item.

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
