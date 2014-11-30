using UnityEngine;
using System.Collections;

public class TowerSelect : MonoBehaviour {

	void Update() {
		if ( Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit, 100.0f)){
				if (hit.transform.gameObject == this.gameObject)
					Select ();
				else {
					Deselect ();
				}
			}
		}
	}
	
	/* Used by world to select this tower */
	void Select() {
		GetComponentInChildren<Projector>().enabled = true;
		GetComponentInChildren<Canvas>().enabled = true;
	}
	
	void Deselect() {
		GetComponentInChildren<Projector>().enabled = false;
		GetComponentInChildren<Canvas>().enabled = false;
	}
}
