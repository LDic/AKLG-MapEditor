using UnityEngine;

public class Tile : MonoBehaviour {

	public GameObject disposedObject;

	private Renderer renderer;
	private MapEditorManager editorInstance;
	private Quaternion spawnQuaternion = new Quaternion(0f, 90f, 90f, 1f);

	public int disposedObjectIndex;     // 0 : null. 1부터 시작.
	private string[] objectNames = {"", "Wall(Clone)", "Character(Clone)", "Enemy1(Clone)"};

	void Start()
	{
		renderer = GetComponent<Renderer>();
		editorInstance = MapEditorManager.instance;
	}

	private void DisposeObject()
	{
		if(disposedObject != null)
		{
			Destroy(disposedObject);
			disposedObjectIndex = 0;
		}
		if(editorInstance.SelectedObject != null)
		{
			disposedObject = (GameObject)Instantiate(editorInstance.SelectedObject, transform.position, spawnQuaternion);
			for(int i = 0; i < objectNames.Length; i++)
			{
				if(disposedObject.name == objectNames[i]) {disposedObjectIndex = i;}
			}
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
