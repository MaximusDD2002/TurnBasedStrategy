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
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

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



                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canSlash = true;
    }
}
