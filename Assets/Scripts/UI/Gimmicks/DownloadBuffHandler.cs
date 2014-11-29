using UnityEngine;
using System.Collections;

public class DownloadBuffHandler : MonoBehaviour {

	void Run() {
		if (GetComponent<GimmickResponseHandler>().isGood) {
			foreach(GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {
				tower.BroadcastMessage("Buff");
			}
		}
		else {
			foreach(GameObject tower in GameObject.FindGameObjectsWithTag("Tower")) {
				tower.BroadcastMessage("Debuff");
			}
		}
	}
	
}
