using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalManager : MonoSingleton<ParticalManager>
{
    [SerializeField] int _OPCoinParticalCount;

    public void CallCoinPartical()
    {
        ObjectPool.Instance.GetPooledObjectAdd(_OPCoinParticalCount);
    }
}
