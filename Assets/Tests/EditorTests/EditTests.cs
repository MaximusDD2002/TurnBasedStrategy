using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditTests
{
    private GameObject obj;

    [SetUp]
    public void SetUp()
    {
        obj = new GameObject("Object");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(obj);
    }

    [Test]
    public void ActionPointsTest()
    {
        Unit unitDefender = obj.AddComponent<Unit>();
        AttackAction attackAction = obj.AddComponent<AttackAction>();
        MoveAction moveAction = obj.AddComponent<MoveAction>();

        bool canSpend = unitDefender.CanSpendActionPointsToTakeAction(attackAction);

        Assert.IsTrue(unitDefender.GetActionPoints() == 2);
        Assert.IsTrue(attackAction.GetActionPointsCost() == 1);
        Assert.IsTrue(moveAction.GetActionPointsCost() == 1);
        Assert.IsTrue(canSpend == true);
    }

    [Test]
    public void ActionNamesTest()
    {
        AttackAction attackAction = obj.AddComponent<AttackAction>();
        MoveAction moveAction = obj.AddComponent<MoveAction>();

        string attackActionName = attackAction.GetActionName();
        string MoveActionName = moveAction.GetActionName();
        int maxAttackDistance = attackAction.GetMaxAttackDistance();

        Assert.That(attackActionName, Is.Not.Null);
        Assert.That(MoveActionName, Is.Not.Null);
        Assert.That(maxAttackDistance, Is.Not.Null);
        Assert.IsTrue(attackActionName == "Attack");
        Assert.IsTrue(MoveActionName == "Move");
        Assert.IsTrue(maxAttackDistance == 1);
    }

    [Test]
    public void TurnSystemTest()
    {
        TurnSystem turnSystem = obj.AddComponent<TurnSystem>();
        int turnNumber = turnSystem.GetTurnNumber();

        Assert.That(turnNumber, Is.Not.Null);
        Assert.IsTrue(turnNumber == 1);
        Assert.IsTrue(turnSystem.IsPlayerTurn() == true);

        turnSystem.NextTurn();

        Assert.IsTrue(turnSystem.IsPlayerTurn() == false);
    }

    [Test]
    public void HealthSystemTest()
    {
        HealthSystem healthSystem = obj.AddComponent<HealthSystem>();

        float healthAmmount = healthSystem.GetHealthNormalized();
        Assert.That(healthAmmount, Is.Not.Null);

        healthSystem.Damage(50);
        Assert.IsTrue(healthAmmount >= 0);

        healthSystem.Damage(50);
        Assert.IsTrue(healthAmmount >= 0);

        healthSystem.Damage(50);
        Assert.IsTrue(healthAmmount >= 0);
    }

    [Test]
    public void UnitManagerSystemTest()
    {
        UnitManager unitManager = obj.AddComponent<UnitManager>();

        Assert.That(unitManager.GetUnitist, Is.Null);
    }
}
