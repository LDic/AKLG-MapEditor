/**
 *@ 만약 총 유형 개수(현재 : Tile, Off Tile, Entity)를 수정하고 싶으면 Unity 에디터에서 다음을 수정해주면 됨.
 *@ 1. DisposingPanel에 있는 Type panels 의 Size 및 할당 패널 수정. (추가의 경우 DisposingPanel에 Panel 추가해야 함. 삭제의 경우는 해당 패널 삭제.)
 *@ 2. ListPanel에 있는 각 버튼의 On Click()에 할당 돼있는 함수, 즉 OnTypeButtonClicked 함수의 인덱스 수정
 *@ 3. Tile Prefab에 Images의 Size 및 할당 이미지 수정. (추가의 경우 프리팹 내 이미지 추가해야 함. 삭제의 경우 프리팹 내 해당 이미지 삭제해야 함.)
 */

using UnityEngine;
using UnityEngine.UI;

public class DisposingUI : MonoBehaviour {

	public ItemPrefab disposingItem;			// 팔레트에 배치할 item prefab
	public float disposingItemYOffset;						// 팔레트에 배치될 item prefab의 y 값 조정.

	private int[] itemNumber;					// 각 type별 배치할 item prefab 수
	private ItemPrefab[][] disposingItemList;		// 팔레트에 배치되는 item prefab 목록 (0 : tile, 1 : off tile 등)
	private MapEditorManager editorInstance;

	[Header("Type Button")]
	public GameObject[] typePanels;	// 0 : tile, 1 : offTile, 2 : entity

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
				disposingItemList[i][j].itemIndex = j - 1;	// 빈 칸인 2 번째 버튼 부터 인덱스 증가 시작
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

}

