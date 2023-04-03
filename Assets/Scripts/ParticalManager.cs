using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalManager : MonoSingleton<ParticalManager>
{
    [SerializeField] int _OPCoinParticalCount;

    public void CallCoinPartical(GameObject pos)
    {
        ObjectPool.Instance.GetPooledObjectAdd(_OPCoinParticalCount, pos.transform.position);
    }
}
