using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackAction : BaseAction
{
    private enum State
    {
        Aiming,
        Attacking,
        Resting
    }

    private State state;
    private int MaxAttackDistance = 1;
    private float stateTimer;

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
                break;
            case State.Attacking:
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
                isActive = false;
                onActionComplete();
                break;
        }

        Debug.Log(state);
    }

    public override string GetActionName()
    {
        return "Attack";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -MaxAttackDistance; x <= MaxAttackDistance; x++)
        {
            for (int z = -MaxAttackDistance; z <= MaxAttackDistance; z++)
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
        this.onActionComplete = onActionComplete;
        isActive = true;

        Debug.Log("Aiming");

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;
    }
}
