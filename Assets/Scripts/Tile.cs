using UnityEngine;

public class Tile : MonoBehaviour {

	public GameObject disposedObject;
	private Renderer renderer;
	private MapEditorManager editorInstance;

	private Quaternion spawnQuaternion = new Quaternion(0f, 90f, 90f, 1f);

	void Start()
	{
		renderer = GetComponent<Renderer>();
		editorInstance = MapEditorManager.instance;
	}

	private void DisposeObject()
	{
		if(disposedObject != null) {Destroy(disposedObject);}
		if(editorInstance.SelectedObject != null)
		{
			disposedObject = (GameObject)Instantiate(editorInstance.SelectedObject, transform.position, spawnQuaternion);
		}
	}

	void OnMouseDown()
	{
		DisposeObject();
	}

	void OnMouseEnter()
	{
		renderer.material.color = editorInstance.hoverColor;
		if(Input.GetMouseButton(0))
		{
			DisposeObject();
		}
	}
	void OnMouseExit()
	{
		renderer.material.color = editorInstance.normalColor;
	}

}
