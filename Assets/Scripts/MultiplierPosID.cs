using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierPosID : MonoBehaviour
{
    public int posCount;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MultiplierObject>().multiplierPosCount = posCount;
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<MultiplierObject>().multiplierPosCount = -1;
    }
}
