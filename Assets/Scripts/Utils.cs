using UnityEngine;

public class Utils
{
    public static int GenerateRandomNumberInPercentage()
    {
        int maxPercent = 100;
        int minPercent = 0;

        return Random.Range(minPercent, maxPercent + 1);
    }
}