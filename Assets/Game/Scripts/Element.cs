using UnityEngine;

public class Element
{
    public Vector2Int position;
    public bool walkable;
    public int gCost, hCost;
    public Element parent;

    public int fCost => gCost + hCost;

    public Element(Vector2Int pos, bool walkable)
    {
        this.position = pos;
        this.walkable = walkable;
    }
}

