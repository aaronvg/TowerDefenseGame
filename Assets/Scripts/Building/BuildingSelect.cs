using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingSelect : MonoBehaviour {
	
	public GameObject upgrade;
	public AudioSource deleteSound;
	public GameObject particles;
	
	/* Check if this building is being clicked on */
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
	
	/* Used by world to select this building */
	void Select() {
		Canvas c = GetComponentInChildren<Canvas>();
		c.enabled = true;
		c.gameObject.GetComponent<BoxCollider>().enabled = true;
	}
	
	/* Used by world to deselect this tower */
	void Deselect() {
		Canvas c = GetComponentInChildren<Canvas>();
		c.enabled = false;
		c.gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	/* Called when this tower is uninstalled */
	void Sell() {
		Deselect ();
		StartCoroutine (AnimateUninstall());
	}
	
	/* Animates the uninstall process */
	IEnumerator AnimateUninstall () {
		deleteSound.Play();
		
		// detach the particles so they fade out naturally
		particles.particleSystem.emissionRate = 0;
		particles.transform.parent = null;
		particles.SendMessage("Kill");
		
		while (transform.localScale.x > 0) {
			transform.localScale = new Vector3(transform.localScale.x - 2 * Time.deltaTime, 0.8f, 0.8f);
			yield return null;
		}
		while (transform.localScale.z > 0) {
			transform.localScale = new Vector3(0, 0.8f, transform.localScale.z - 8 * Time.deltaTime);
			yield return null;
		}
		transform.localScale = new Vector3(0, 0.8f, 0);
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
