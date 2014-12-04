using UnityEngine;
using System.Collections;

public class BuildPanelSlide : MonoBehaviour {

	public float width;
	public float speed;
	
	public GameObject emailPanel;
	public GameObject socialPanel;
	public GameObject downloadPanel;
	
	private bool emailVisible;
	private bool socialVisible;
	private bool downloadVisible;

	
	void Start() {
		emailVisible = false;
		socialVisible = false;
		downloadVisible = false;
	}

	/* Toggles the email panel */
	public void ToggleEmail() { 
		if(socialVisible) {
			StartCoroutine(Swap(socialPanel, emailPanel));
			emailVisible = true;
			socialVisible = false;	
		}
		else if(downloadVisible) {
			StartCoroutine(Swap(downloadPanel, emailPanel));
			emailVisible = true;
			downloadVisible = false;	
		}
		else if(emailVisible) {
			StartCoroutine(SlideOut(emailPanel));
			emailVisible = false;
		}
		else {
			StartCoroutine(SlideIn(emailPanel));
			emailVisible = true;
		}
	}
	
	/* Toggles the social panel */
	public void ToggleSocial() { 
		if(emailVisible) {
			StartCoroutine(Swap(emailPanel, socialPanel));
			emailVisible = false;
			socialVisible = true;	
		}
		else if(downloadVisible) {
			StartCoroutine(Swap(downloadPanel, socialPanel));
			socialVisible = true;
			downloadVisible = false;	
		}
		else if(socialVisible) {
			StartCoroutine(SlideOut(socialPanel));
			socialVisible = false;
		}
		else {
			StartCoroutine(SlideIn(socialPanel));
			socialVisible = true;
		}
	}
	
	/* Toggles the download panel */
	public void ToggleDownload() { 
		if(socialVisible) {
			StartCoroutine(Swap(socialPanel, downloadPanel));
			downloadVisible = true;
			socialVisible = false;	
		}
		else if(emailVisible) {
			StartCoroutine(Swap(emailPanel, downloadPanel));
			emailVisible = false;
			downloadVisible = true;	
		}
		else if(downloadVisible) {
			StartCoroutine(SlideOut(downloadPanel));
			downloadVisible = false;
		}
		else {
			StartCoroutine(SlideIn(downloadPanel));
			downloadVisible = true;
		}
	}

	/* Slides a panel in from offscreen */
	IEnumerator SlideIn(GameObject panel) {
		RectTransform rt = panel.GetComponent<RectTransform>();
		
		while(rt.position.x <= width / 2) {
			rt.position += new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		rt.position = new Vector3(width / 2, rt.position.y, rt.position.z);
	}
	
	/* Slides a panel out from onscreen */
	IEnumerator SlideOut(GameObject panel) {
		RectTransform rt = panel.GetComponent<RectTransform>();
		
		while(rt.position.x >= -width) {
			rt.position -= new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		rt.position = new Vector3(-width, rt.position.y, rt.position.z);
	}
	
	/* Slides the from panel out and slides the to panel in */
	IEnumerator Swap(GameObject from, GameObject to) {
		// slide old panel out
		RectTransform fromRT = from.GetComponent<RectTransform>();
		
		while(fromRT.position.x >= -width) {
			fromRT.position -= new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		fromRT.position = new Vector3(-width, fromRT.position.y, fromRT.position.z);
		
		// Slide new panel in
		RectTransform toRT = to.GetComponent<RectTransform>();
		
		while(toRT.position.x <= width / 2) {
			toRT.position += new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		toRT.position = new Vector3(width / 2, toRT.position.y, toRT.position.z);
	}
}
