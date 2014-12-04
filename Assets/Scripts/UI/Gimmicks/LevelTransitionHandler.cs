using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTransitionHandler : MonoBehaviour {

	public Button buttonToActivate;

	void Ready() {
		//buttonToActivate.interactable = true;
		Destroy(gameObject);
	}

	void ResetGame() {
		Application.LoadLevel ("DebugLevel");
	}
}
