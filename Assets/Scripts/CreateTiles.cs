using UnityEngine;
using System.Collections;

public class CreateTiles : MonoBehaviour {
    public int GridWidth = 2;
    public int GridHeight = 2;

    public GameObject TilePlacerPrefab;
    public AudioSource SelectPlacerUISound;

    private Transform _highlightedTile;

	private Transform[][] layout;
    private bool _placersPlaced;

    private Transform[][] buildingsOnGrid;

	// Use this for initialization
	void Start () {
		layout = new Transform[GridWidth][];
		for (int i = 0; i < layout.Length; i++) {
			layout[i] = new Transform[GridHeight];
		}

        buildingsOnGrid = new Transform[GridWidth][];
	    for (int i = 0; i < GridWidth; i++)
	    {
	        buildingsOnGrid[i] = new Transform[GridHeight];
	    }
	}

    void SpawnPlacers()
    {
        if (_placersPlaced)
        {
            Debug.LogWarning("Attempted to create placers when they're already made", this);
            return;
        }
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                if (buildingsOnGrid[i][j] != null)
                {
                    continue;
                }
                GameObject placer = (GameObject) Instantiate(TilePlacerPrefab);
                Transform trn = placer.transform;

                trn.parent = transform;
                placer.name = trn.parent.name + " Placer " + i + "," + j;

                trn.localPosition = new Vector3(i, 0, j);

                layout[i][j] = trn;
            }
        }

        _placersPlaced = true;
    }

    void DespawnPlacers()
    {
        if (!_placersPlaced)
        {
            Debug.LogWarning("Attempted to despawn placers when they haven't been made", this);
            return;
        }
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                if (layout[i][j] != null)
                {
                    Destroy(layout[i][j].gameObject);
                }
                layout[i][j] = null;
            }
        }

        _placersPlaced = false;
    }
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            foreach (Collider child in transform.GetComponentsInChildren<Collider>())
		    {
		        if (child.Raycast(ray, out hit, 100f))
		        {
                    // Get construction manager
		            var manager = GameObject.FindWithTag("GameController");
		            var constructState = manager.GetComponent<ConstructionState>();
		            if (constructState != null && constructState.IsConstructing)
		            {
                        constructState.SendMessage("StopConstruction");
		                if (constructState.ConstructingWhat != null)
		                {
		                    var inst = (Transform) Instantiate(constructState.ConstructingWhat);
		                    inst.gameObject.name = inst.gameObject.name + " (at " + gameObject.name + " " +
		                                           child.transform.localPosition.x + "x" + child.transform.localPosition.z +
		                                           ")";
		                    inst.position = child.transform.position;

		                    buildingsOnGrid[Mathf.RoundToInt(child.transform.localPosition.x)][
                                Mathf.RoundToInt(child.transform.localPosition.y)] = inst;
		                }

		                if (SelectPlacerUISound != null)
		                {
		                    SelectPlacerUISound.Play();
		                }
		            }
		            break;
		        }
		    }
		}

	    if (_highlightedTile != null)
	    {
	        var anim = _highlightedTile.GetComponentInChildren<Animator>();
	        if (anim)
	        {
	            anim.SetBool("Highlighted", false);
            }
            _highlightedTile = null;
	    }

        foreach (Collider child in transform.GetComponentsInChildren<Collider>())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (child.Raycast(ray, out hit, 100f))
            {
                var anim = child.GetComponentInChildren<Animator>();
                if (anim)
                {
                    anim.SetBool("Highlighted", true);
                }
                _highlightedTile = child.transform;
                break;
            }
        }
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		for (float i = 0; i < GridWidth; i++) {

			for (float j = 0; j < GridHeight; j++) {
				Gizmos.DrawLine (transform.position + new Vector3(i-.5f, .5f, j-.5f),
				                 transform.position + new Vector3(i-.5f, .5f, j+.5f));
				Gizmos.DrawLine (transform.position + new Vector3(i-.5f, .5f, j-.5f),
				                 transform.position + new Vector3(i+.5f, .5f, j-.5f));
			}
		}
		Gizmos.DrawLine (transform.position + new Vector3(GridWidth-.5f, .5f, -.5f),
		                 transform.position + new Vector3(GridWidth-.5f, .5f, GridHeight-.5f));

		Gizmos.DrawLine (transform.position + new Vector3(GridWidth-.5f, .5f, GridHeight-.5f),
		                 transform.position + new Vector3(-.5f, .5f, GridHeight-.5f));
	}
}
