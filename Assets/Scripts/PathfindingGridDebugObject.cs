using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingGridDebugObject : GridDebugObject
{
    private PathNode pathNode;

    public override void SetGridObject(object gridObject)
    {
        base.SetGridObject(gridObject);
        pathNode = (PathNode)gridObject;
    }

    protected override void Update()
    {
        base.Update();
    }
}
