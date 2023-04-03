using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] TMP_Text countText;

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

        multiplierSystem.multiplierStat.multiplierMarketClass.multiplierGO[multiplierMarketCount] = gameObject;
        multiplierSystem.multiplierStat.multiplierMarketClass.multiplierBool[multiplierMarketCount] = true;
        multiplierSystem.multiplierStat.multiplierMarketClass.multiplierCount[multiplierMarketCount] = multiplierCount;
        multiplierSystem.multiplierStat.multiplierMarketClass.multiplierTypes[multiplierMarketCount] = multiplierType;
        transform.position = multiplierSystem.multiplierMarketPos[multiplierMarketCount].transform.position;

        CountTextReWrite();
        GameManager.Instance.MultiplierPlacementWrite(MultiplierSystem.Instance.multiplierStat);
    }
    public void CountTextReWrite()
    {
        if (multiplierType == MultiplierType.plus)
            countText.text = "+ " + multiplierCount;
        else
            countText.text = "x " + multiplierCount;
    }
}
