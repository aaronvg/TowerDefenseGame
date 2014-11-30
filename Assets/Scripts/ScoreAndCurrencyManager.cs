using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Queue, List, Dictionary


public class ScoreAndCurrencyManager : MonoBehaviour
{
	private int internetPresenceRate;
	private int currentInternetPresence;
    public int CurrencyTotal;
	public int CurrentCurrency;

	private int currentWave;

	void Start() {
		internetPresenceRate = 0;
		currentInternetPresence = 0;


		CurrencyTotal = 0;
		CurrentCurrency = 10;
		StartCoroutine ("UpdateInternetPresence");
	}


	void Update() {
		// Update Currency UI


	    // Update Score / Internet Presence UI



	    // Calculate what we can buy and is unlocked.
	    // Send message to another component, such as BuildingManager

	}

	// Currently updates internet presence bar every second.
	// TODO we could do this so that it updates every frame but we spread the update rate over several frames.
	IEnumerator UpdateInternetPresence()
	{
		while(true) 
		{ 
			currentInternetPresence += internetPresenceRate;
			yield return new WaitForSeconds(1f);
		}
	}
	
	
	// Other events in the game will update our current internet presence or currency
	void UpdateInternetPresence(int additionalRate) {
		internetPresenceRate += additionalRate;
		Debug.Log ("Score updated to " + currentInternetPresence);
	}


	void UpdateCurrency(int additionalCurrency) {
        CurrentCurrency += additionalCurrency;

        var uiRoot = GameObject.FindGameObjectWithTag("UIRoot");
        if (uiRoot)
        {
            Debug.LogWarning("Did it work?");
            uiRoot.SendMessage("UpdateCurrency");
        }
	}

}


