using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarGridModule : MonoBehaviour
{

    private int Heuristic(SingleGridObject a, SingleGridObject b)
    {
        return
            Mathf.Abs(a.GridPos.x - b.GridPos.x) +
            Mathf.Abs(a.GridPos.y - b.GridPos.y) +
            Mathf.Abs(a.HeightLevel - b.HeightLevel) * 2; // height penalty
    }

    internal List<SingleGridObject> FindPath(
     SingleGridObject start,
     SingleGridObject goal,
     TileAccess agentPlayer=TileAccess.All
 )
    {
        var open = new List<SingleGridObject>();
        var closed = new HashSet<SingleGridObject>();

        foreach (var ti in GridManager.Instance.AllTiles)
        {
            ti.ResetPathData();
        }

        open.Add(start);
        start.H = Heuristic(start, goal);

        while (open.Count > 0)
        {
            var current = open.OrderBy(t => t.F).First();

            if (current == goal)
                return ReconstructPath(current);

            open.Remove(current);
            closed.Add(current);

            foreach (var neighbor in GridManager.Instance.GetNeighbors(current))
            {
                if (closed.Contains(neighbor))
                    continue;

                if (!CanMove(current, neighbor, agentPlayer))
                    continue;

                int tentativeG = current.G + 1;

                if (!open.Contains(neighbor) || tentativeG < neighbor.G)
                {
                    neighbor.Parent = current;
                    neighbor.G = tentativeG;
                    neighbor.H = Heuristic(neighbor, goal);

                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }

        return null;
    }


    private bool CanMove(SingleGridObject from, SingleGridObject to, TileAccess agentPlayer)
    {
        int dh = to.HeightLevel - from.HeightLevel;



        if (!to.IsWalkable)
        {
            return false;
        }

        if (!to.IsAccessibleBy(agentPlayer))
            return false;

        // Same height → always ok
        if (dh == 0)
            return true;

        // Going down: must be standing on a stair-down
        if (dh == -1 && from.Type == TileType.StairDown)
            return true;

        // Going up: must step onto a stair-up
        if (dh == 1 && to.Type == TileType.StairUp)
            return true;

        return false;
    }

   private List<SingleGridObject> ReconstructPath(SingleGridObject end)
    {
        var path = new List<SingleGridObject>();
        SingleGridObject current = end;

        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }

}
