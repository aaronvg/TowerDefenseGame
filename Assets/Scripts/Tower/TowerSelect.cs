using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerSelect : MonoBehaviour {

	public GameObject upgrade;
	public AudioSource deleteSound;

	void Update() {
		if ( Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if (Physics.Raycast (ray, out hit, 100.0f)){
				if (hit.transform.gameObject == this.gameObject)
					Select ();
				else if (hit.transform.tag != "Tower Menu")
					Deselect ();
			}
		}
	}
	
	/* Used by world to select this tower */
	void Select() {
		Projector p = GetComponentInChildren<Projector>();
		p.enabled = true;
		
		Canvas c = GetComponentInChildren<Canvas>();
		c.enabled = true;
		
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
			if(p.orthographicSize > 8)
				p.orthographicSize = 8;
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
	
	/* Called when this tower is uninstalled */
	void Sell() {
		Deselect ();
		StartCoroutine (AnimateUninstall());
	}
	
	IEnumerator AnimateUninstall () {
		deleteSound.Play();
		while (transform.localScale.x > 0) {
			transform.localScale = new Vector3(transform.localScale.x - 2 * Time.deltaTime, 1, 1);
			yield return null;
		}
		while (transform.localScale.z > 0) {
			transform.localScale = new Vector3(0, 1, transform.localScale.z - 8 * Time.deltaTime);
			yield return null;
		}
		transform.localScale = new Vector3(0, 1, 0);
		while (deleteSound.isPlaying)
			yield return null;
		
		Destroy (gameObject);
	}
	
	/* Called when this tower is upgraded */
	void Upgrade() {
		Instantiate (upgrade, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
