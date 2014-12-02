using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Queue, List, Dictionary


public class ScoreAndCurrencyManager : MonoBehaviour
{
	private int internetPresenceRate;
	private int currentInternetPresence;

	// The max points we can get for internet.
	private int maxInternetPresencePossible = 100;
    public int CurrencyTotal;
	public int CurrentCurrency;

	private int currentWave;

	void Start() {
		internetPresenceRate = 0;
		currentInternetPresence = 0;


		CurrencyTotal = 0;
		StartCoroutine ("UpdateInternetPresence");
	}


	void Update() {


		// Update Currency UI


	    // Update Score / Internet Presence UI

	}

	// Currently updates internet presence bar every second.
	// TODO we could do this so that it updates every frame but we spread the update rate over several frames.
	IEnumerator UpdateInternetPresence()
	{
		while(true) 
		{ 
			currentInternetPresence += internetPresenceRate;

			if(currentInternetPresence > maxInternetPresencePossible) {
				currentInternetPresence = maxInternetPresencePossible;
			}
			yield return new WaitForSeconds(1f);
		}
	}
	
	
	// Updates the RATE of increase of our internet points.
	void UpdateInternetPresenceRate(int additionalRate) {
		internetPresenceRate += additionalRate;
		Debug.Log ("Internet rate updated to " + internetPresenceRate);
	}

	// Updates the actual total we have right now for internet points.
	void UpdateInternetPresencePoints(int points) {
		currentInternetPresence += points;
		if(currentInternetPresence > maxInternetPresencePossible) {
			currentInternetPresence = maxInternetPresencePossible;
		}

		if (currentInternetPresence < 0)
			currentInternetPresence = 0;

		Debug.Log ("Current internet presence poitns updated to " + currentInternetPresence);
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


