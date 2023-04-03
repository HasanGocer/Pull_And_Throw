using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierTouch : MonoBehaviour
{
    [SerializeField] MultiplierObject multiplierObject;
    public bool isFree = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Multiplier"))
        {
            MultiplierObject tempmultiplierObject = other.gameObject.GetComponent<MultiplierObject>();

            if (tempmultiplierObject.multiplierType == multiplierObject.multiplierType && !isFree)
            {
                other.gameObject.GetComponent<MultiplierTouch>().isFree = true;
                isFree = true;
                multiplierObject.multiplierCount += tempmultiplierObject.multiplierCount;

                if (tempmultiplierObject.multiplierPosCount != -1)
                {
                    MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierBool[tempmultiplierObject.multiplierPosCount] = false;
                    tempmultiplierObject.multiplierPosCount = -1;
                }
                else
                {
                    MultiplierSystem.Instance.multiplierStat.multiplierMarketClass.multiplierBool[tempmultiplierObject.multiplierMarketCount] = false;
                    tempmultiplierObject.multiplierMarketCount = -1;
                }

                MultiplierSystem.Instance.ObjectBackAdded(other.gameObject);
                multiplierObject.CountTextReWrite();
                GameManager.Instance.MultiplierPlacementWrite(MultiplierSystem.Instance.multiplierStat);
                isFree = false;
            }
        }
        else if (other.gameObject.CompareTag("Object"))
            SoundSystem.Instance.CallPipeSound();
    }

}
