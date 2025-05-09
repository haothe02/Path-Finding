using System.Collections.Generic;
using UnityEngine;

public class Pathfiding
{
    public static List<Vector2Int> FindPath(MapController mapController)
    {
        Vector2Int start = mapController.GetVector2IntStart();
        Vector2Int goal = mapController.GetVector2IntGoal();

        Element[,] elements = new Element[mapController.GetWidth(), mapController.GetHeight()];
        for (int x = 0; x < mapController.GetWidth(); x++)
        {
            for (int y = 0; y < mapController.GetHeight(); y++)
            {
                elements[x, y] = new Element(new Vector2Int(x, y), mapController.IsWalkable(new Vector2Int(x, y)));
            }
        }

        List<Element> openList = new List<Element>();
        HashSet<Element> closedList = new HashSet<Element>();
        Element startElement = elements[start.x, start.y];
        Element goalElement = elements[goal.x, goal.y];
        openList.Add(startElement);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => a.fCost.CompareTo(b.fCost));
            Element current = openList[0];

            if (current == goalElement)
            {
                return RetracePath(startElement, goalElement);
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (var neighbor in GetNeighbors(current, elements, mapController))
            {
                if (!neighbor.walkable || closedList.Contains(neighbor))
                    continue;

                int newCost = current.gCost + 1;
                if (newCost < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newCost;
                    neighbor.hCost = Heuristic(neighbor.position, goal);
                    neighbor.parent = current;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }
        return null;
    }

    static List<Vector2Int> RetracePath(Element startElement, Element endElement)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Element current = endElement;
        while (current != startElement)
        {
            path.Add(current.position);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    static int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    static List<Element> GetNeighbors(Element Element, Element[,] grid, MapController mc)
    {
        List<Element> neighbors = new List<Element>();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (var dir in directions)
        {
            Vector2Int newPos = Element.position + dir;
            if (mc.IsWalkable(newPos))
            {
                neighbors.Add(grid[newPos.x, newPos.y]);
            }
        }

        return neighbors;
    }
}
