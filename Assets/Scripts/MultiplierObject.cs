using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierObject : MonoBehaviour
{
    public enum MultiplierType
    {
        multiply = 0,
        plus = 1
    }

    public Collider multiplierCollider;
    public MultiplierType multiplierType;
    public int multiplierCount;
    public int tempMultiplierPosCount = -1;
    public int multiplierMarketCount;
    public int multiplierPosCount = -1;

    public void StartMultiplierPlacement()
    {
        MultiplierSystem multiplierSystem = MultiplierSystem.Instance;

        if (Random.Range(0, 2) == 0)
        {
            multiplierType = MultiplierType.multiply;
            multiplierCount = Random.Range(2, multiplierSystem.multiplierMaxCount);
        }
        else
        {
            multiplierType = MultiplierType.plus;
            multiplierCount = Random.Range(2, multiplierSystem.plusMaxCount);
        }

        while (multiplierSystem.multiplierStat.multiplierMarketClass.multiplierBool[multiplierMarketCount = Random.Range(0, multiplierSystem.multiplierStat.multiplierMarketClass.multiplierBool.Count)]) ;

        multiplierSystem.multiplierStat.multiplierMarketClass.multiplierBool[multiplierMarketCount] = true;
        multiplierSystem.multiplierStat.multiplierMarketClass.multiplierCount[multiplierMarketCount] = multiplierCount;
        multiplierSystem.multiplierStat.multiplierMarketClass.multiplierTypes[multiplierMarketCount] = multiplierType;
        transform.position = multiplierSystem.multiplierStatPos[multiplierMarketCount].transform.position;
    }
}
