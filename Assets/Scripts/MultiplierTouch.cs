using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierTouch : MonoBehaviour
{
    [SerializeField] MultiplierObject multiplierObject;
    public bool isFree = false;

    private void OnCollisionEnter(Collision other)
    {
        print(1);
        if (other.gameObject.CompareTag("Multiplier"))
        {
            print(2);
            MultiplierObject tempmultiplierObject = other.gameObject.GetComponent<MultiplierObject>();
            print(3);

            if (tempmultiplierObject.multiplierType == multiplierObject.multiplierType && !isFree)
            {
                print(4);
                other.gameObject.GetComponent<MultiplierTouch>().isFree = true;
                print(5);
                isFree = true;
                print(6);
                multiplierObject.multiplierCount += tempmultiplierObject.multiplierCount;
                print(7);

                if (tempmultiplierObject.multiplierPosCount != -1)
                {
                    print(8);
                    MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierBool[tempmultiplierObject.multiplierPosCount] = false;
                    tempmultiplierObject.multiplierPosCount = -1;
                    print(9);
                }
                else
                {
                    print(10);
                    MultiplierSystem.Instance.multiplierStat.multiplierMarketClass.multiplierBool[tempmultiplierObject.multiplierMarketCount] = false;
                    tempmultiplierObject.multiplierMarketCount = -1;
                    print(11);
                }
                print(12);

                MultiplierSystem.Instance.ObjectBackAdded(other.gameObject);
                print(13);
                multiplierObject.CountTextReWrite();
                print(14);
                GameManager.Instance.MultiplierPlacementWrite(MultiplierSystem.Instance.multiplierStat);
                print(15);
                isFree = false;
                print(16);
            }

        }
    }

}
