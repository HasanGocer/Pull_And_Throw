using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierTouch : MonoBehaviour
{
    [SerializeField] MultiplierObject multiplierObject;
    bool isFree = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Multiplier"))
        {
            MultiplierObject tempmultiplierObject = other.gameObject.GetComponent<MultiplierObject>();

            if (tempmultiplierObject.multiplierType == multiplierObject.multiplierType && isFree)
            {
                isFree = true;
                multiplierObject.multiplierCount += tempmultiplierObject.multiplierCount;

                if (tempmultiplierObject.multiplierPosCount != -1)
                    MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierBool[tempmultiplierObject.multiplierPosCount] = false;
                else
                    MultiplierSystem.Instance.multiplierStat.multiplierMarketClass.multiplierBool[tempmultiplierObject.multiplierMarketCount] = false;

                MultiplierSystem.Instance.ObjectBackAdded(other.gameObject);
                multiplierObject.CountTextReWrite();
                GameManager.Instance.MultiplierPlacementWrite(MultiplierSystem.Instance.multiplierStat);
            }

        }
    }

}
