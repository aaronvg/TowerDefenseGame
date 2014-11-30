using UnityEngine;
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
				// wave 0 is just a spam email
			case 0:
				AddEnemy (EnemyTypes.spam, 1);
				break;

			case 1:
				AddEnemy (EnemyTypes.spam, 2);
				AddEnemy (EnemyTypes.bot, 2);
				break;
				
			case 2:
				AddEnemy (EnemyTypes.bot, 2);
				break;
				
			case 3:
				AddEnemy (EnemyTypes.spam, 2);
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

        // forward to UI (???)
        foreach (var i in FindObjectsOfType<Canvas>())
	    {
	        i.SendMessage("EndWave");
	    }
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
