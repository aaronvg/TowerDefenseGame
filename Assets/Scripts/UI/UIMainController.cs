using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainController : MonoBehaviour
{
    private GameObject _gameManager;

    public GameObject TowerBuildingPrefab;

    public AudioSource ConfirmSound;
    public AudioSource CancelSound;
    public AudioSource PickupSound;

    public AudioSource WaitingMusic;
    public AudioSource EngagedMusic;

    public Text CurrencyValueText;
    public float CurrencyValueUpdateSpeed = 10f;

    private float _lerpCurrency;


    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (_gameManager == null)
        {
            Debug.LogError(
                "Game manager could not be set; is there a game object with the tag GameController in the scene?", this);
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
            CurrencyValueText.text = Mathf.RoundToInt(_lerpCurrency) + "";
        }
    }

    void StartConstruction()
    {
        // forward to construction manager
        _gameManager.SendMessage("StartConstruction");
        _gameManager.GetComponent<ConstructionState>().ConstructingWhat = TowerBuildingPrefab.transform;
        Sound_PlayConfirm();
    }

    void StopConstruction()
    {
        // forward
        _gameManager.SendMessage("StopConstruction");
        Sound_PlayCancel();
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
}
