using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField]private Unit unit;
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(unit.GetMoveAction().GetValidActionGridPositionList());
            unit.GetMoveAction().GetValidActionGridPositionList();
        }
    }
}
