using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour {

	private MapEditorManager editorInstance;
	private static GameObject createPanel;
	private static GameObject exportPanel;
	private static GameObject clearPanel;

	void Start()
	{
		editorInstance = MapEditorManager.Instance;
		createPanel = transform.Find("CreatePanel").gameObject;
		exportPanel = transform.Find("ExportPanel").gameObject;
		clearPanel = transform.Find("ClearPanel").gameObject;
	}

	public static void AddToViewport(int index)
	{
		if(index == 0)
		{
			exportPanel.SetActive(false);
			clearPanel.SetActive(false);
			createPanel.SetActive(true);
		}
		if(index == 1)
		{
			createPanel.SetActive(false);
			clearPanel.SetActive(false);
			exportPanel.SetActive(true);
		}
		if(index == 2)
		{
			createPanel.SetActive(false);
			exportPanel.SetActive(false);
			clearPanel.SetActive(true);
		}
	}

	public void ClearUI()
	{
		createPanel.SetActive(false);
		exportPanel.SetActive(false);
		clearPanel.SetActive(false);
	}

	public void OnAdditionClicked()
	{
		editorInstance.Save();
		ClearUI();
	}

	public void OnCreateClicked()
	{
		editorInstance.SetSize();
		editorInstance.CreateTiles(editorInstance.Width, editorInstance.Height);
		ClearUI();
	}

	public void OnClearClicked()
	{
		//editorInstance.ClearDisposedObject();
		ClearUI();
	}

	public void OnCancelClicked()
	{
		ClearUI();
	}
}
