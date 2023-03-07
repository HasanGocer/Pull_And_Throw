using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierSystem : MonoSingleton<MultiplierSystem>
{
    public class MultiplierClass
    {
        public List<bool> multiplierBool = new List<bool>();
        public List<int> multiplierCount = new List<int>();
        public List<MultiplierObject.MultiplierType> multiplierTypes = new List<MultiplierObject.MultiplierType>();
    }

    public MultiplierClass multiplierClass = new MultiplierClass();
    public int multiplierMaxCount, plusMaxCount;
    public List<GameObject> marketPos = new List<GameObject>();
    public List<GameObject> multiplierPos = new List<GameObject>();
}
