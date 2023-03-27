using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierPosID : MonoBehaviour
{
    public int posCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Multiplier"))
            other.GetComponent<MultiplierObject>().tempMultiplierPosCount = posCount;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Multiplier"))
            other.GetComponent<MultiplierObject>().tempMultiplierPosCount = -1;
    }
}
