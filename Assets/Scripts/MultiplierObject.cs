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

    [SerializeField] private MultiplierType _multiplierType;
    [SerializeField] private int _multiplierCount;

    public void StartMultiplierPlacement()
    {
        if (Random.Range(0, 2) == 0)
        {
            _multiplierType = MultiplierType.multiply;
            _multiplierCount = Random.Range(2, MultiplierSystem.Instance.multiplierMaxCount);
        }
        else
        {
            _multiplierType = MultiplierType.plus;
            _multiplierCount = Random.Range(2, MultiplierSystem.Instance.plusMaxCount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectID objectID = other.GetComponent<ObjectID>();

        if (_multiplierType == MultiplierType.multiply)
            objectID.objectCount *= _multiplierCount;
        else
            objectID.objectCount += _multiplierCount;
    }
}
