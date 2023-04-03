using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundManager : MonoSingleton<AroundManager>
{
    [SerializeField] GameObject _around;

    public void AroundOff()
    {
        _around.SetActive(false);
    }
    public void AroundOn()
    {
        _around.SetActive(true);
    }

}
