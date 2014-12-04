using UnityEngine;
using System.Collections;

public class BuildPanelSlide : MonoBehaviour {

	public float width;
	public float speed;
	
	public RectTransform emailPanel;
	public RectTransform socialPanel;
	public RectTransform downloadPanel;
	
	private bool isSwapping = false;


	/* Toggles the email panel */
	public void ToggleEmail() { 
		if(!isSwapping) {
			if(socialPanel.position.x > -width / 2) 
				StartCoroutine(Swap(socialPanel, emailPanel));

			else if(downloadPanel.position.x > -width / 2) 
				StartCoroutine(Swap(downloadPanel, emailPanel));

			else if(emailPanel.position.x > -width / 2) 
				StartCoroutine(SlideOut(emailPanel));

			else 
				StartCoroutine(SlideIn(emailPanel));
		}
	}
	
	/* Toggles the social panel */
	public void ToggleSocial() { 
		if(!isSwapping) {
			if(emailPanel.position.x > -width / 2) 
				StartCoroutine(Swap(emailPanel, socialPanel));

			else if(downloadPanel.position.x > -width / 2) 
				StartCoroutine(Swap(downloadPanel, socialPanel));

			else if(socialPanel.position.x > -width / 2) 
				StartCoroutine(SlideOut(socialPanel));

			else 
				StartCoroutine(SlideIn(socialPanel));
		}
	}
	
	/* Toggles the download panel */
	public void ToggleDownload() { 
		if(!isSwapping) {
			if(socialPanel.position.x > -width / 2)
				StartCoroutine(Swap(socialPanel, downloadPanel));

			else if(emailPanel.position.x > -width / 2)
				StartCoroutine(Swap(emailPanel, downloadPanel));

			else if(downloadPanel.position.x > -width / 2)
				StartCoroutine(SlideOut(downloadPanel));

			else
				StartCoroutine(SlideIn(downloadPanel));
		}
	}

	/* Slides a panel in from offscreen */
	IEnumerator SlideIn(RectTransform panel) {
		while(panel.position.x < width / 2 - speed * Time.deltaTime) {
			panel.position += new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		panel.position = new Vector3(width / 2, panel.position.y, panel.position.z);
	}
	
	/* Slides a panel out from onscreen */
	IEnumerator SlideOut(RectTransform panel) {
		while(panel.position.x > -width) {
			panel.position -= new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		panel.position = new Vector3(-width, panel.position.y, panel.position.z);
	}
	
	/* Slides the from panel out and slides the to panel in */
	IEnumerator Swap(RectTransform from, RectTransform to) {
		isSwapping = true;
	
		// slide old panel out
		while(from.position.x > -width) {
			from.position -= new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		from.position = new Vector3(-width, from.position.y, from.position.z);
		
		// Slide new panel in
		while(to.position.x < width / 2 - speed * Time.deltaTime) {
			to.position += new Vector3(speed * Time.deltaTime, 0, 0);
			yield return null;	
		}
		to.position = new Vector3(width / 2, to.position.y, to.position.z);
		
		isSwapping = false;
	}
}
