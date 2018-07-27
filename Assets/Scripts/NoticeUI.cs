using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour {

	private static MapEditorManager editorInstance;
	private static GameObject createPanel;
	private static GameObject clearPanel;

	void Start()
	{
		editorInstance = MapEditorManager.Instance;
		createPanel = transform.Find("CreatePanel").gameObject;
		clearPanel = transform.Find("ClearPanel").gameObject;
	}

	public static void AddToViewport(int index)
	{
		if(index == 0)
		{
			clearPanel.SetActive(false);
			createPanel.SetActive(true);
		}
		if(index == 1)
		{
			editorInstance.SaveMap();
		}
		if(index == 2)
		{
			createPanel.SetActive(false);
			clearPanel.SetActive(true);
		}
		if(index == 3)
		{
			editorInstance.LoadMap();
		}
	}

	public void ClearUI()
	{
		createPanel.SetActive(false);
		clearPanel.SetActive(false);
	}

	public void OnCreateClicked()
	{
		editorInstance.SetSize();
		editorInstance.CreateTiles(editorInstance.Width, editorInstance.Height);
		ClearUI();
	}

	public void OnClearClicked()
	{
		editorInstance.tileUI.ClearDisposedImages(editorInstance.selectedTypeIndex);
		ClearUI();
	}

	public void OnCancelClicked()
	{
		ClearUI();
	}
}
