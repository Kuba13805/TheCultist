using System;
using UnityEngine;
using Random = System.Random;

public class TestPlayer
{
   public static event Action<int> OnDiceRoll;
   public static bool TestAbility(int basePlayerNumber, int baseTestDifficulty)
   {
      var random = new Random();

      var testResult = random.Next(1, 20);

      testResult += basePlayerNumber;
      Debug.Log("Test result: " + testResult + " => " + "dice roll result: " 
                + (testResult - basePlayerNumber) + ", player value of ability added: " + basePlayerNumber + ". Test difficulty: " + baseTestDifficulty);
      
      OnDiceRoll?.Invoke(testResult);
      
      return testResult >= baseTestDifficulty;
   }
}
