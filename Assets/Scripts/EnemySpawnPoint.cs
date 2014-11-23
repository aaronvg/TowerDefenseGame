using UnityEngine;
using System.Collections;

public class EnemySpawnPoint : MonoBehaviour
{

    [Range(1,20)] public int SpawnCount;
    [Range(0,2)] public float SpawnRate;
    public bool Spawning;

    public GameObject EnemyToSpawn;
    public Transform InitialDestination;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Spawning && !IsInvoking("SpawnEnemy") && SpawnCount > 0)
	    {
	        Invoke("SpawnEnemy", 2f);
	    }
	}

    void SpawnEnemy()
    {
        SpawnCount--;
        if (SpawnCount <= 0)
        {
            Spawning = false;
        }

        var go = (GameObject) Instantiate(EnemyToSpawn);
        go.SendMessage("SetDestination", InitialDestination);
    }

    void StartWave()
    {
        Spawning = true;
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
