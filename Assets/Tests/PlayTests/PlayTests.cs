using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayTests
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

    [UnityTest]
    public IEnumerator UnitManagerSetupTest()
    {
        UnitManager unitManager = obj.AddComponent<UnitManager>();

        yield return null;

        Assert.That(unitManager.GetUnitist(), Is.Not.Null);
        Assert.That(unitManager.GetFriendlyUnitList(), Is.Not.Null);
        Assert.That(unitManager.GetEnemyUnitList(), Is.Not.Null);
    }

    [UnityTest]
    public IEnumerator levelGridSetupTest()
    {
        Pathfinding pathfinding = obj.AddComponent<Pathfinding>();
        LevelGrid levelGrid = obj.AddComponent<LevelGrid>();
        yield return null;

        Assert.That(levelGrid.GetHeight(), Is.Not.Null);
        Assert.That(levelGrid.GetWidth(), Is.Not.Null);

        Assert.IsTrue(levelGrid.GetHeight() == 0);
        Assert.IsTrue(levelGrid.GetWidth() == 0);
    }

    
    [UnityTest]
    public IEnumerator HealthSystemTest()
    {
        HealthSystem healthSystem = obj.AddComponent<HealthSystem>();

        float healthAmmount = healthSystem.GetHealthNormalized();
        Assert.That(healthAmmount, Is.Not.Null);
        yield return null;

        healthSystem.Damage(50);
        Assert.IsTrue(healthAmmount >= 0);

        healthSystem.Damage(50);
        Assert.IsTrue(healthAmmount >= 0);

        healthSystem.Damage(50);
        Assert.IsTrue(healthAmmount >= 0);
    }
    
    [UnityTest]
    public IEnumerator ActionTest()
    {
        AttackAction attackAction = obj.AddComponent<AttackAction>();
        MoveAction moveAction = obj.AddComponent<MoveAction>();

        yield return null;

        Assert.IsTrue(attackAction.GetActionPointsCost() == 1);
        Assert.IsTrue(moveAction.GetActionPointsCost() == 1);
    }

    [UnityTest]
    public IEnumerator ActionNameTest() 
    {
        AttackAction attackAction = obj.AddComponent<AttackAction>();
        MoveAction moveAction = obj.AddComponent<MoveAction>();

        string attackActionName = attackAction.GetActionName();
        string MoveActionName = moveAction.GetActionName();
        int maxAttackDistance = attackAction.GetMaxAttackDistance();

        yield return null;
        
        Assert.That(attackActionName, Is.Not.Null);
        Assert.That(MoveActionName, Is.Not.Null);
        Assert.That(maxAttackDistance, Is.Not.Null);
        Assert.IsTrue(attackActionName == "Attack");
        Assert.IsTrue(MoveActionName == "Move");
        Assert.IsTrue(maxAttackDistance == 1);
    }
}
