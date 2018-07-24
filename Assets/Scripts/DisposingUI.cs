using UnityEngine;
using UnityEngine.UI;

public class DisposingUI : MonoBehaviour {

	public ItemPrefab disposingItem;			// 팔레트에 배치할 item prefab
	public float disposingItemYOffset;						// 팔레트에 배치될 item prefab의 y 값 조정.

	private int[] itemNumber;					// 각 type별 배치할 item prefab 수
	private ItemPrefab[][] disposingItemList;		// 팔레트에 배치되는 item prefab 목록 (0 : tile, 1 : off tile 등)
	private MapEditorManager editorInstance;

	[Header("Type Button")]
	public GameObject[] typePanels;	// 0 : tile, 1 : offTile, 2 : entity, 3 : event

	[Header("Button")]
	public Button[] buttonList;	// 삭제 대기

	private GameObject selectedObject;

	void Start()
	{
		editorInstance = MapEditorManager.Instance;

		// 임시 초기화
		itemNumber = new int[typePanels.Length];
		for(int i = 0; i < itemNumber.Length; i++)
		{
			itemNumber[i] = i+5;
		}

		CreateDisposingUI();

		//disposingItemList[0][2].SetSpriteImage(test);
	}

	public void CreateDisposingUI()
	{
		disposingItemList = new ItemPrefab[itemNumber.Length][];
		
		for(int i = 0; i < disposingItemList.Length; i++)
		{
			disposingItemList[i] = new ItemPrefab[itemNumber[i]];
			for(int j = 0; j < disposingItemList[i].Length; j++)
			{
				disposingItemList[i][j] = Instantiate(disposingItem, typePanels[i].transform);
				disposingItemList[i][j].typeIndex = i;
				disposingItemList[i][j].itemIndex = j;
				disposingItemList[i][j].transform.Translate(new Vector3( (j%5)*80, disposingItemYOffset - (j/5)*80) );
			}
		}
	}

	public void OnTypeButtonClicked(int index)
	{
		for(int i = 0; i < typePanels.Length; i++)
		{
			typePanels[i].SetActive(false);
		}
		typePanels[index].SetActive(true);
	}

	public void OnClearClicked()	// 삭제 대기
	{
		//InitializeButtonColor();
		NoticeUI.AddToViewport(2);
	}

	/*
	public void OnEmptyClicked()
	{
		InitializeButtonColor();
		buttonList[1].image.color = Color.yellow;
		editorInstance.SelectedObject = null;
		selectedObject = null;
	}

	public void OnWallClicked()
	{
		InitializeButtonColor();
		buttonList[2].image.color = Color.yellow;
		editorInstance.SelectedObject = editorInstance.wallPrefab;
		DisplaySelectedObject();
	}

	public void OnCharacterClicked()
	{
		InitializeButtonColor();
		buttonList[3].image.color = Color.yellow;
		editorInstance.SelectedObject = editorInstance.characterPrefab;
		DisplaySelectedObject();

	}

	public void OnEnemy1Clicked()
	{
		InitializeButtonColor();
		buttonList[4].image.color = Color.yellow;
		editorInstance.SelectedObject = editorInstance.enemy1Prefab;
		DisplaySelectedObject();
	}
	*/
}

