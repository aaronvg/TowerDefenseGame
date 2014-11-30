using UnityEngine;
using System.Collections;

public class ConstructionState : MonoBehaviour
{
    public bool IsConstructing = false;
    public Transform ConstructingWhat;

    private float _oldPitch;

    void StartConstruction()
    {
        if (IsConstructing)
        {
            Debug.LogWarning("Starting construction when construction already started, ignoring", this);
            return;
        }

        CreateTiles[] placerGrids = FindObjectsOfType<CreateTiles>();
        foreach (CreateTiles grid in placerGrids)
        {
            grid.SendMessage("SpawnPlacers");
        }

        IsConstructing = true;

        // Set the pivot camera pitch angle
        var pivot = Camera.main.GetComponent<PivotAroundPoint>();
        if (pivot != null)
        {
            _oldPitch = pivot.TargetPitch;
            pivot.TargetPitch = 80f;
        }
    }

    void StopConstruction()
    {
        if (!IsConstructing)
        {
            Debug.LogWarning("Stopping construction when construction already stopped, ignoring", this);
            return;
        }

        CreateTiles[] placerGrids = FindObjectsOfType<CreateTiles>();
        foreach (CreateTiles grid in placerGrids)
        {
            grid.SendMessage("DespawnPlacers");
        }

        IsConstructing = false;

        // Set the pivot camera pitch angle
        var pivot = Camera.main.GetComponent<PivotAroundPoint>();
        if (pivot != null)
        {
            pivot.TargetPitch = _oldPitch;
        }
    }
}
