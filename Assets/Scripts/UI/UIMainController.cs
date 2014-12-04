using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainController : MonoBehaviour
{
    private GameObject _gameManager;

    public AudioSource ConfirmSound;
    public AudioSource CancelSound;
    public AudioSource PickupSound;

    public AudioSource WaitingMusic;
    public AudioSource EngagedMusic;

    public Text CurrencyValueText;
    public float CurrencyValueUpdateSpeed = 10f;

    public Button StartWaveButton;
    public Button CancelButton;
    private bool _wasCanceled;
	private int _spentCurrency;

	public Button[] towerButtons;
	public int[] towerCosts;
	public GameObject[] TowerBuildingPrefabs;

	public Button[] buildingButtons;
	public int[] buildingCosts;
	public GameObject[] BuildingPrefabs;


    public Text WaveLabel;

    private float _lerpCurrency;


    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (_gameManager == null)
        {
            Debug.LogError(
                "Game manager could not be set; is there a game object with the tag GameController in the scene?", this);
        }

		for (int i = 0; i < towerButtons.Length; i++) {
			int j = i; // because otherwise C# always uses the last value in the loop when these get called.
			towerButtons[j].onClick.AddListener(() => { StartConstruction(TowerBuildingPrefabs[j].transform, towerCosts[j]); });
		}

		for (int i = 0; i < buildingButtons.Length; i++) {
			int j = i; // because otherwise C# always uses the last value in the loop when these get called.
			buildingButtons[j].onClick.AddListener(() => { StartConstruction(BuildingPrefabs[j].transform, buildingCosts[j]); });
		}


    }

    void Update()
    {
        var curmanager = _gameManager.GetComponent<ScoreAndCurrencyManager>();

        _lerpCurrency = Mathf.Lerp(_lerpCurrency, curmanager.CurrentCurrency, CurrencyValueUpdateSpeed * Time.deltaTime);
        if (curmanager.CurrentCurrency - _lerpCurrency < .01) _lerpCurrency = curmanager.CurrentCurrency;

        // update label
        if (CurrencyValueText != null)
        {
            CurrencyValueText.text = "Disk Space: " + Mathf.RoundToInt(_lerpCurrency) + " KB";
        }

        // Build mode cancel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelButton_OnPressed();
        }


		// Update tower buttons based on currency
		for (int i = 0; i < towerButtons.Length; i++) {
			if(curmanager.CurrentCurrency < towerCosts[i]) {
				towerButtons[i].interactable = false;
			} else {
				towerButtons[i].interactable = true;
			}
		}

		// Update building buttons.
		for (int i = 0; i < buildingButtons.Length; i++) {
			if(curmanager.CurrentCurrency < buildingCosts[i]) {
				buildingButtons[i].interactable = false;
			} else {
				buildingButtons[i].interactable = true;
			}
		}

    }

	void StartConstruction(Transform towerToConstruct, int spentCurrency)
	{
		_spentCurrency = spentCurrency;

		// forward to construction manager
		_gameManager.SendMessage("StartConstruction");
		_gameManager.GetComponent<ConstructionState>().ConstructingWhat = towerToConstruct;
		Sound_PlayConfirm();

		_gameManager.SendMessage ("UpdateCurrency", -_spentCurrency);
		
		// Make cancel button visible
		CancelButton.gameObject.SetActive(true);
	}
	
    void StopConstruction()
    {
        // forward
        _gameManager.SendMessage("StopConstruction");
        if (_wasCanceled)
        {

            Sound_PlayCancel();
        }


        // Make cancel button invisible
        CancelButton.gameObject.SetActive(false);

        _wasCanceled = false;
    }

    void StartWave()
    {
        _gameManager.SendMessage("StartWave");
        if (WaitingMusic != null)
        {
            WaitingMusic.Stop();
        }
        if (EngagedMusic != null)
        {
            EngagedMusic.Play();
        }

        Sound_PlayConfirm();

        StartWaveButton.interactable = false;
    }

    void EndWave()
    {
        if (EngagedMusic != null)
        {
            EngagedMusic.Stop();
        }
        if (WaitingMusic != null)
        {
            WaitingMusic.Play();
        }
        StartWaveButton.interactable = true;

        // Set wave label text
        WaveLabel.text = "Wave " + (_gameManager.GetComponent<EnemySpawnPoint>().currentWave + 1);
    }

    void UpdateCurrency()
    {
        Sound_PlayPickup();
    }

    void Sound_PlayConfirm()
    {
        ConfirmSound.Play();
    }

    void Sound_PlayCancel()
    {
        CancelSound.Play();
    }

    void Sound_PlayPickup()
    {
        if (PickupSound)
        {
            PickupSound.Play();
        }
    }

    void CancelButton_OnPressed()
    {
		// give back any money they were spending
		_gameManager.SendMessage ("UpdateCurrency", _spentCurrency);
        // Build mode cancel
        var cstate = _gameManager.GetComponent<ConstructionState>();
        _wasCanceled = true;
        if (cstate != null && cstate.IsConstructing)
        {
            StopConstruction();
        }
    }
}
