using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPCController : MonoBehaviour
{
    [SerializeField] private MapController mapController;
    [SerializeField] private float timeNPCMove = 0.2f;

    public MapController GetMapController
    {
        get => mapController;
        set => mapController = value;
    }
    private void Start()
    {
        // mapController.GenerateRandomGrid();
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
            Tween moveTween = transform.DOMove(worldPos, timeNPCMove).SetEase(Ease.Linear);
            yield return moveTween.WaitForCompletion();
        }
    }
}