﻿using UnityEngine;
using System.Collections;

public class EnemySpawnPoint : MonoBehaviour
{

    [Range(1,20)] public int SpawnCount;
    [Range(0,2)] public float SpawnRate;
    public bool Spawning;

    public Transform InitialDestination;

	// All the possible enemy prefabs we can spawn. indices should match the EnemyTypes enumerator
	public GameObject[] EnemyPrefabs;


	// Current enemy wave data
	public int currentWave;
	private ArrayList Enemies;
	// Points for each type of enemy, could probably move enemytype + points to a dictionary
	private int[] EnemyPoints = {10, 50, 100};

	public GameObject[] WaveMessages;
	public GameObject WinMessage;
	public GameObject LoseMessage;
	private bool Lost = false;

	public enum EnemyTypes 
	{
		spam = 0,
		bot = 1,
		virus = 2,
	}

	public enum GameState
	{
		waveInProgress,
		intermission
	}

	public GameState state = GameState.intermission;

	// TODO possibly create a GameState enum so we other components can see if we are in waveActive or intermission...


	// Use this for initialization
	void Start () {
		Enemies = new ArrayList();
		Instantiate(WaveMessages[currentWave], transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Spawning && !IsInvoking("SpawnEnemy") && SpawnCount > 0)
	    {
	        Invoke("SpawnEnemy", SpawnRate);
	    }
		
		if (Enemies.Count == 0 && state == GameState.waveInProgress) {
			EndWave ();
		}


		if (GetComponent<ScoreAndCurrencyManager> ().currentInternetPresence <= 0 && Lost == false) 
		{
			// End game with lose message
			Instantiate(LoseMessage, transform.position, Quaternion.identity);
			Lost = true;
		}

	}

    void SpawnEnemy()
    {
        SpawnCount--;
        if (SpawnCount <= 0)
        {
            Spawning = false;
        }



		// Enemies array holds the type of enemy for the next enemy to spawn.
		print ("spawning enemytype " + Enemies [SpawnCount] + " curr number " + SpawnCount);
		var go = (GameObject)Instantiate (EnemyPrefabs [(int)Enemies [SpawnCount]], transform.position, Quaternion.identity);
		//(EnemyPrefabs [(int)Enemies [SpawnCount]], InitialSpawnPoint.position, Quaternion.identity);
		EnemyBehavior behavior = go.GetComponent<EnemyBehavior> ();
		//behavior.NumPoints = EnemyPoints[(int)Enemies[SpawnCount]];    using enemy behavior settings
		behavior.SetDestination(InitialDestination);
		// go.SendMessage("SetDestination", InitialDestination);
    }

    void StartWave()
    {
		if (state == GameState.intermission) {
			switch (currentWave) 
			{
				// Phase 0
			case 0:
				AddEnemy (EnemyTypes.spam, 1);
				break;

			case 1:
				AddEnemy (EnemyTypes.spam, 3);

				break;
				
			case 2:
				AddEnemy (EnemyTypes.spam, 4);
				break;

			// Phase 1
			case 3:
				AddEnemy (EnemyTypes.spam, 2);
				AddEnemy (EnemyTypes.bot, 1);
				break;

			case 4:
				AddEnemy (EnemyTypes.spam, 4);
				AddEnemy (EnemyTypes.bot, 2);
				break;
			
			case 5:
				AddEnemy (EnemyTypes.spam, 6);
				AddEnemy (EnemyTypes.bot, 4);
				break;

		    // Phase 3
			case 6:
				AddEnemy(EnemyTypes.virus, 1);
				break;

			case 7:
				AddEnemy(EnemyTypes.virus, 2);
				break;

			case 8:
				AddEnemy(EnemyTypes.spam, 4);
				AddEnemy(EnemyTypes.bot, 4);
				AddEnemy(EnemyTypes.virus, 2);
				break;


			}

			print ("Started Wave " + currentWave);
			Spawning = true;
			SpawnCount = Enemies.Count;
			state = GameState.waveInProgress;
		}
    }

	void EndWave() 
	{
		print ("Ended Wave");
		currentWave++;
		state = GameState.intermission;
		Enemies.Clear ();


		if (currentWave == 9) {
			// end the game
			Instantiate(WinMessage, transform.position, Quaternion.identity);
			return;
		}


        // forward to UI (???)
        GameObject.FindGameObjectWithTag("UIRoot").SendMessage("EndWave");

		if(currentWave == 3) {
			Invoke("LevelTransition1", 2);
		}
		if(currentWave == 6) {
			Invoke("LevelTransition2", 2);
		}

	}

	// 5am hackyness
	public void LevelTransition1() {
		Instantiate(WaveMessages[1], transform.position, Quaternion.identity);
	}
	public void LevelTransition2() {
		Instantiate(WaveMessages[2], transform.position, Quaternion.identity);
	}

	void AddEnemy(EnemyTypes type, int amount) 
	{
		for (int i = 0; i < amount; i++) 
		{
			Enemies.Add (type);
		}
	}
	
	public void KillEnemy() {
		Enemies.RemoveAt (0);
	}



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .5f);

        if (InitialDestination)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, InitialDestination.position);
        }
    }
}
