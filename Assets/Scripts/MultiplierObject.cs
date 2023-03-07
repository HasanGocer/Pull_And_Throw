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
    public int multiplierPosCount = -1;

    public void StartMultiplierPlacement()
    {
        if (Random.Range(0, 2) == 0)
        {
            multiplierType = MultiplierType.multiply;
            multiplierCount = Random.Range(2, MultiplierSystem.Instance.multiplierMaxCount);
        }
        else
        {
            multiplierType = MultiplierType.plus;
            multiplierCount = Random.Range(2, MultiplierSystem.Instance.plusMaxCount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectID objectID = other.GetComponent<ObjectID>();

        if (multiplierType == MultiplierType.multiply)
            objectID.objectCount *= multiplierCount;
        else
            objectID.objectCount += multiplierCount;
    }
}
