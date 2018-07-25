using UnityEngine;
using UnityEngine.UI;

public class DisposingUI : MonoBehaviour {

	public ItemPrefab disposingItem;			// 팔레트에 배치할 item prefab
	public float disposingItemYOffset;						// 팔레트에 배치될 item prefab의 y 값 조정.

	private int[] itemNumber;					// 각 type별 배치할 item prefab 수
	private ItemPrefab[][] disposingItemList;		// 팔레트에 배치되는 item prefab 목록 (0 : tile, 1 : off tile 등)
	private MapEditorManager editorInstance;

	[Header("Type Button")]
	public GameObject[] typePanels;	// 0 : tile, 1 : offTile, 2 : item, 3 : entity, 4 : event

	[Header("Button")]
	public Button[] buttonList;	// 삭제 대기

	void Start()
	{
		editorInstance = MapEditorManager.Instance;

		// Initialize
		itemNumber = new int[typePanels.Length];
		for(int i = 0; i < itemNumber.Length; i++)
		{
			itemNumber[i] = editorInstance.GetSpriteImageDataLength(i) + 2;	// 초기화, 빈 칸 버튼 + 실제 이미지 버튼
		}

		CreateDisposingUI();
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
				disposingItemList[i][j].transform.Translate(new Vector3((j%5)*80, disposingItemYOffset - (j/5)*80));
				// 0, 1은 초기화, 빈 칸이므로 이미지 적용에서 제외
				if(j >= 2) {disposingItemList[i][j].SetSpriteImage(editorInstance.GetSpriteImageData(i, j-2));}
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


	
}

