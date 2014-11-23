using UnityEngine;
using System.Collections;

public class CreateTiles : MonoBehaviour {
	public Vector3 dim;
	int[][] layout;

	// Use this for initialization
	void Start () {
		layout = new int[(int)dim.x][];
		for (int i = 0; i < layout.Length; i++) {
			layout[i] = new int[(int)dim.z];
		}

		for (int i = 0; i < dim.x; i++) {
			for (int j = 0; j < dim.z; j++){
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				Transform cubeT = cube.transform;

				cubeT.parent = this.transform;
				cube.name = cubeT.parent.name + "Cube" + i + "" + j;

				cubeT.localPosition = new Vector3(i, 0, j);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetMouseButton (0)) {
			Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.transform.forward);
			//Debug.Log("hello");
		}*/


		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				Debug.Log(hit.transform.name);
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		for (float i = 0; i < dim.x; i++) {

			for (float j = 0; j < dim.z; j++) {
				Gizmos.DrawLine (transform.position + new Vector3(i-.5f, .5f, j-.5f),
				                 transform.position + new Vector3(i-.5f, .5f, j+.5f));
				Gizmos.DrawLine (transform.position + new Vector3(i-.5f, .5f, j-.5f),
				                 transform.position + new Vector3(i+.5f, .5f, j-.5f));
			}
		}
		Gizmos.DrawLine (transform.position + new Vector3(dim.x-.5f, .5f, -.5f),
		                 transform.position + new Vector3(dim.x-.5f, .5f, dim.z-.5f));

		Gizmos.DrawLine (transform.position + new Vector3(dim.x-.5f, .5f, dim.z-.5f),
		                 transform.position + new Vector3(-.5f, .5f, dim.z-.5f));
	}
}
