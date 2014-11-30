using UnityEngine;
using System.Collections;

public class ConstructionState : MonoBehaviour
{
    public bool IsConstructing = false;
    public Transform ConstructingWhat;

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
    }
}
