using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MidniteOilSoftware;

public class NPCController : MonoBehaviour
{
    [SerializeField] private MapController mapController;
    [SerializeField] private float timeNPCMove = 0.2f;
    [SerializeField] private GameObject pathGO;
    public MapController GetMapController
    {
        get => mapController;
        set => mapController = value;
    }
    private void Start()
    {
        pathElement = ObjectPoolManager.SpawnGameObject(pathGO, transform.position, Quaternion.identity);
        mapController.ChangLstObjectSpawned(true, pathElement);
        List<Vector2Int> path = Pathfiding.FindPath(mapController);
        if (path != null)
        {
            StartCoroutine(FollowPath(path));
        }
    }
    GameObject pathElement;
    IEnumerator FollowPath(List<Vector2Int> path)
    {
        foreach (var pos in path)
        {
            Vector3 worldPos = new Vector3(pos.x, pos.y, 0);
            Tween moveTween = transform.DOMove(worldPos, timeNPCMove).SetEase(Ease.Linear).OnComplete(() =>
            {
                pathElement = ObjectPoolManager.SpawnGameObject(pathGO, worldPos, Quaternion.identity);
                mapController.ChangLstObjectSpawned(true, pathElement);
                mapController.CheckDoneNpcTravel(gameObject);
            });
            yield return moveTween.WaitForCompletion();
        }
    }
}