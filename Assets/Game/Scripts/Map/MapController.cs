using MidniteOilSoftware;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private int maxGrid;
    [SerializeField] private GameObject floorPrefab, wallPrefab, goalPrefab;
    [SerializeField] private GameObject npcPrefab;
    
    private int[,] grid;
    int width, height;
    Vector2Int start, goal;
    Camera cam;
    public int GetHeight()
    {
        return height;
    }
    public int GetWidth()
    {
        return width;
    }
    public Vector2Int GetVector2IntStart()
    { 
        return start; 
    }
    public Vector2Int GetVector2IntGoal()
    {
        return goal;
    }
    void Start()
    {
        CaculateCamPosAndOrthosize();
        GenerateRandomGrid();
        SpawnGridVisuals();
    }
    void CaculateCamPosAndOrthosize() 
    {
        cam = Camera.main;
        width = height = Random.Range(5, maxGrid);
        cam.transform.position = new Vector3Int(width / 2, height / 2, -10);
        cam.orthographicSize = width;
    }
    public void GenerateRandomGrid()
    {
        grid = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = (Random.value < 0.2f) ? 1 : 0;
            }
        }

        start = Vector2Int.zero;
        goal = new Vector2Int(width - 1, height - 1);
        grid[start.x, start.y] = 2;
        grid[goal.x, goal.y] = 3;
    }

    public void SpawnGridVisuals()
    {
        Vector3 pos = Vector3.zero;
        GameObject go = null;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pos.x = x; 
                pos.y = y;
                if (grid[x, y] == 1) go = ObjectPoolManager.SpawnGameObject(wallPrefab, pos, Quaternion.identity);
                else if (grid[x, y] == 3) go = ObjectPoolManager.SpawnGameObject(goalPrefab, pos, Quaternion.identity);
                else go = ObjectPoolManager.SpawnGameObject(floorPrefab, pos, Quaternion.identity);

                go.name = $"Tile_{x}_{y}";
            }
        }

        GameObject npc = ObjectPoolManager.SpawnGameObject(npcPrefab, new Vector3(start.x, start.y, 0), Quaternion.identity);
        npc.GetComponent<NPCController>().GetMapController = this;
    }

    public bool IsWalkable(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height && grid[pos.x, pos.y] != 1;
    }
}
