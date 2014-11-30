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
		Projector p = GetComponentInChildren<Projector>();
		p.enabled = true;
		GetComponentInChildren<Canvas>().enabled = true;
		StartCoroutine(GrowRange(p));
	}
	
	void Deselect() {
		Projector p = GetComponentInChildren<Projector>();
		GetComponentInChildren<Canvas>().enabled = false;
		StartCoroutine(ShrinkRange(p));
	}
	
	IEnumerator GrowRange(Projector p) {
		while(p.orthographicSize < 8) {
			p.orthographicSize += 128 * Time.deltaTime;
			yield return null;
		}
	}	
	
	IEnumerator ShrinkRange(Projector p) {
		while(p.orthographicSize > 0) {
			p.orthographicSize -= 128 * Time.deltaTime;
			yield return null;
		}
		p.enabled = false;
	}
}
