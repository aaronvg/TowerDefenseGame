using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainController : MonoBehaviour
{
    private GameObject _gameManager;

    public GameObject TowerBuildingPrefab;
	public GameObject[] TowerBuildingPrefabs;

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

	public Button ConstructTowerFilterButton;
	private int FilterTowerValue = 50;

	public Button ConstructTowerBlockButton;
	private int BlockTowerValue = 100;

	public Button ConstructTowerScannerButton;
	private int ScannerTowerValue = 200;

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

		ConstructTowerFilterButton.onClick.AddListener(() => { StartConstruction(TowerBuildingPrefabs[0].transform); 
			_spentCurrency = FilterTowerValue; });
		ConstructTowerBlockButton.onClick.AddListener(() => { StartConstruction(TowerBuildingPrefabs[1].transform);
			_spentCurrency = BlockTowerValue; });
		ConstructTowerScannerButton.onClick.AddListener(() => { StartConstruction(TowerBuildingPrefabs[2].transform);
			_spentCurrency = ScannerTowerValue; });


    }

    void Update()
    {
        var curmanager = _gameManager.GetComponent<ScoreAndCurrencyManager>();

        _lerpCurrency = Mathf.Lerp(_lerpCurrency, curmanager.CurrentCurrency, CurrencyValueUpdateSpeed * Time.deltaTime);
        if (curmanager.CurrentCurrency - _lerpCurrency < .01) _lerpCurrency = curmanager.CurrentCurrency;

        // update label
        if (CurrencyValueText != null)
        {
            CurrencyValueText.text = Mathf.RoundToInt(_lerpCurrency) + "";
        }

        // Build mode cancel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelButton_OnPressed();
        }


		// Update tower buttons based on currency
		if (curmanager.CurrentCurrency < FilterTowerValue) {
			ConstructTowerFilterButton.interactable = false;
		} else {
			ConstructTowerFilterButton.interactable = true;
		}

		if (curmanager.CurrentCurrency < BlockTowerValue) {
			ConstructTowerBlockButton.interactable = false;
		} else {
			ConstructTowerFilterButton.interactable = true;
		}

		if (curmanager.CurrentCurrency < ScannerTowerValue) {
			ConstructTowerScannerButton.interactable = false;
		} else {
			ConstructTowerScannerButton.interactable = true;
		}



    }

	void StartConstruction(Transform towerToConstruct)
	{
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
			// give back any money they were spending
			_gameManager.SendMessage ("UpdateCurrency", _spentCurrency);
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
        // Build mode cancel
        var cstate = _gameManager.GetComponent<ConstructionState>();
        _wasCanceled = true;
        if (cstate != null && cstate.IsConstructing)
        {
            StopConstruction();
        }
    }
}
