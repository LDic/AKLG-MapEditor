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
		//itemSpriteImage = gameObject.GetComponent<Image>().sprite;
	}

	public void SelectItem()
	{
		editorInstance.selectedItemIndex = itemIndex;
		editorInstance.selectedTypeIndex = typeIndex;
		editorInstance.selectedSpriteImage = itemSpriteImage;
	}

	public void SetSpriteImage(Sprite spriteImage, Texture2D texture)
	{
		itemSpriteImage = spriteImage;
		gameObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0f, 0f, 256f, 256f), Vector2.zero);
		Debug.Log(gameObject.GetComponent<Image>().sprite);
	}

	
}
