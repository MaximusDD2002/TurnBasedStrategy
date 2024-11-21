using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackAction : BaseAction
{

    public EventHandler OnSlash;
    private enum State
    {
        Aiming,
        Attacking,
        Resting
    }

    [SerializeField] private LayerMask obstaclesLayerMask;
    
    private State state;
    private int maxAttackDistance = 1;
    private float stateTimer;
    private Unit targetUnit;
    private bool canSlash;

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.Aiming:
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;

                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);
                break;
            case State.Attacking:
                if (canSlash)
                {
                    Slash();
                    canSlash = false;
                }
                break;
            case State.Resting:
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }

    }

    private void NextState()
    {

        switch (state)
        {
            case State.Aiming:
                state = State.Attacking;
                float AimingStateTime = 0.1f;
                stateTimer = AimingStateTime;
                break;
            case State.Attacking:
                state = State.Resting;
                float attackingStateTime = 0.5f;
                stateTimer = attackingStateTime;
                break;
            case State.Resting:
                ActionComplete();
                break;
        }
    }

    private void Slash()
    {
        OnSlash?.Invoke(this, EventArgs.Empty);
        targetUnit.Damage(35);
    }

    public override string GetActionName()
    {
        return "Attack";
    }
    
    public int GetMaxAttackDistance()
    {
        return maxAttackDistance;
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return GetValidActionGridPositionList(unitGridPosition);
    }
    public List<GridPosition> GetValidActionGridPositionList(GridPosition unitGridPosition)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        for (int x = -maxAttackDistance; x <= maxAttackDistance; x++)
        {
            for (int z = -maxAttackDistance; z <= maxAttackDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition (x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Made it so it won't show the position on which there no unit on
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance. GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }

                Vector3 unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitGridPosition);
                Vector3 shootDir = (targetUnit.GetWorldPosition() - unitWorldPosition).normalized;

                float unitShoulderHeight = 1.7f;
                if (Physics.Raycast(
                        unitWorldPosition + Vector3.up * unitShoulderHeight,
                        shootDir,
                        Vector3.Distance(unitWorldPosition,
                        targetUnit.GetWorldPosition()),
                        obstaclesLayerMask))
                {
                    //Obstacle in-between
                    continue;
                }
                


                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        Debug.Log(onActionComplete);
        ActionStart(onActionComplete);

        Debug.Log(onActionComplete);
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canSlash = true;

        
        Debug.Log(onActionComplete);
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        Unit thisUnit = this.unit;
        Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        int actionPoints = thisUnit.GetActionPoints();
        
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = Mathf.RoundToInt(Mathf.RoundToInt(thisUnit.GetHealthNormalized() * 20f) +
                            Mathf.RoundToInt((1 - targetUnit.GetHealthNormalized()) * 10)) * actionPoints
        };
    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPositionList(gridPosition).Count;
    }
}
