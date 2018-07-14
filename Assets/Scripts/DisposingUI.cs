using UnityEngine;
using UnityEngine.UI;

public class DisposingUI : MonoBehaviour {

	private MapEditorManager editorInstance;

	[Header("Button")]
	public Button[] buttonList;
	
	void Start()
	{
		editorInstance = MapEditorManager.instance;
	}

	private void InitializeButtonColor()
	{
		foreach(Button button in buttonList)
		{
			button.image.color = Color.white;
		}
	}

	public void OnClearClicked()
	{
		InitializeButtonColor();
		editorInstance.ClearDisposedObject();
	}

	public void OnEmptyClicked()
	{
		InitializeButtonColor();
		buttonList[1].image.color = Color.yellow;
		editorInstance.SelectedObject = null;
	}

	public void OnWallClicked()
	{
		InitializeButtonColor();
		buttonList[2].image.color = Color.yellow;
		editorInstance.SelectedObject = editorInstance.wallPrefab;
	}

	public void OnCharacterClicked()
	{
		InitializeButtonColor();
		buttonList[3].image.color = Color.yellow;
		editorInstance.SelectedObject = editorInstance.characterPrefab;
	}

	public void OnEnemy1Clicked()
	{
		InitializeButtonColor();
		buttonList[4].image.color = Color.yellow;
		editorInstance.SelectedObject = editorInstance.enemy1Prefab;
	}
}

