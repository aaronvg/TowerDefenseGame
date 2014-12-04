using UnityEngine;
using System.Collections;

public class GimmickManager : MonoBehaviour {

	Queue GimmickQueue = new Queue();
	private GameObject currentShown;

	struct Pair {
		public GameObject gim;
		public GameObject attachedObj;
		public Pair(GameObject a, GameObject b) {
			gim = a;
			attachedObj = b;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GimmickQueue.Count > 0 && currentShown == null) {
			Pair temp = (Pair)GimmickQueue.Dequeue();
			currentShown = (GameObject)Instantiate (temp.gim, transform.position, Quaternion.identity);
			currentShown.GetComponent<GimmickResponseHandler>().SetEnemy(temp.attachedObj);
		}
	}


	public void AddGimmick(GameObject gimmick, GameObject enemy) {
		GimmickQueue.Enqueue (new Pair(gimmick, enemy));

	}
}
