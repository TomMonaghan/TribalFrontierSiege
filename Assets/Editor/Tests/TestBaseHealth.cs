using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;


public class TestBaseHealth
{
    [Test]
    public void CalculateBaseHealth_Test()
    {
        var baseHealth = new BoardManager.BaseHealth();
        var baseUpgrades = 2;
        var baseHealing = 5;
        var damageTaken = 10;
        var expectedHealth = (20 + (5 * 2) + 5 - 10);

        var health = baseHealth.CalculateBaseHealth(baseUpgrades, damageTaken, baseHealing);
        
        Assert.That(health, Is.EqualTo(expectedHealth));
    }
}

