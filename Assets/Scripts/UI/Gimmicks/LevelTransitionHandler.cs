using UnityEngine;
using System.Collections;

public class LevelTransitionHandler : MonoBehaviour {

	void Ready() {
		Destroy(gameObject);
	}

	void ResetGame() {
		Application.LoadLevel ("DebugLevel");
	}
}
