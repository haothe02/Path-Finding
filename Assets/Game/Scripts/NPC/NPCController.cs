using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private MapController mapController;
    [SerializeField] private float moveSpeed = 2f;

    public MapController GetMapController 
    { 
        get => mapController;
        set => mapController = value;
    }
    private void Start()
    {
        mapController.GenerateRandomGrid();
        List<Vector2Int> path = Pathfiding.FindPath(mapController);
        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }
    }

    IEnumerator FollowPath(List<Vector2Int> path)
    {
        foreach (var pos in path)
        {
            Vector3 worldPos = new Vector3(pos.x, pos.y, 0);
            while (Vector3.Distance(transform.position, worldPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, worldPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}