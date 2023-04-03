using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierSystem : MonoSingleton<MultiplierSystem>
{
    [System.Serializable]
    public class MultiplierClass
    {
        public List<bool> multiplierBool = new List<bool>();
        public List<int> multiplierCount = new List<int>();
        public List<MultiplierObject.MultiplierType> multiplierTypes = new List<MultiplierObject.MultiplierType>();
        public List<GameObject> multiplierGO = new List<GameObject>();
    }
    [System.Serializable]
    public class MultiplierStat
    {
        public MultiplierClass multiplierClass = new MultiplierClass();
        public MultiplierClass multiplierMarketClass = new MultiplierClass();
    }

    [SerializeField] int _OPMultiplierObjectCount;
    public int multiplierMaxCount, plusMaxCount;
    [HideInInspector] public bool isMove;
    public MultiplierStat multiplierStat = new MultiplierStat();
    public List<GameObject> multiplierMarketPos = new List<GameObject>();
    public List<GameObject> multiplierStatPos = new List<GameObject>();

    public void MultiplierOn()
    {

        for (int i = 0; i < multiplierMarketPos.Count; i++)
            if (multiplierStat.multiplierMarketClass.multiplierBool[i]) multiplierStat.multiplierMarketClass.multiplierGO[i].SetActive(true);

        for (int i = 0; i < multiplierStatPos.Count; i++)
            if (multiplierStat.multiplierClass.multiplierBool[i]) multiplierStat.multiplierClass.multiplierGO[i].SetActive(true);
    }

    public void ObjectPlacement()
    {
        for (int i = 0; i < multiplierMarketPos.Count; i++)
            if (multiplierStat.multiplierMarketClass.multiplierBool[i])
            {
                GameObject obj = ObjectPool.Instance.GetPooledObject(_OPMultiplierObjectCount, multiplierMarketPos[i].transform.position);
                MultiplierObject multiplierObject = obj.GetComponent<MultiplierObject>();

                multiplierStat.multiplierMarketClass.multiplierGO[i] = obj;
                multiplierObject.multiplierCount = multiplierStat.multiplierMarketClass.multiplierCount[i];
                multiplierObject.multiplierMarketCount = i;
                multiplierObject.multiplierPosCount = -1;
                multiplierObject.multiplierType = multiplierStat.multiplierMarketClass.multiplierTypes[i];

                MaterialSystem.Instance.PipeDraw(ref obj);
                multiplierObject.CountTextReWrite();
                obj.SetActive(false);
            }

        for (int i = 0; i < multiplierStatPos.Count; i++)
            if (multiplierStat.multiplierClass.multiplierBool[i])
            {
                GameObject obj = ObjectPool.Instance.GetPooledObject(_OPMultiplierObjectCount, multiplierStatPos[i].transform.position);
                MultiplierObject multiplierObject = obj.GetComponent<MultiplierObject>();

                multiplierStat.multiplierClass.multiplierGO[i] = obj;
                multiplierObject.multiplierCount = multiplierStat.multiplierClass.multiplierCount[i];
                multiplierObject.multiplierPosCount = i;
                multiplierObject.multiplierMarketCount = -1;
                multiplierObject.multiplierType = multiplierStat.multiplierClass.multiplierTypes[i];

                MaterialSystem.Instance.PipeDraw(ref obj);
                multiplierObject.CountTextReWrite();
                obj.SetActive(false);
            }
    }
    public bool MarketIsFree()
    {
        bool isFree = false;

        for (int i = 0; i < multiplierStat.multiplierMarketClass.multiplierBool.Count; i++)
            if (!multiplierStat.multiplierMarketClass.multiplierBool[i]) isFree = true;

        return isFree;
    }
    public void NewObject()
    {
        GameObject multiplier = ObjectPool.Instance.GetPooledObject(_OPMultiplierObjectCount);

        MaterialSystem.Instance.PipeDraw(ref multiplier);
        multiplier.GetComponent<MultiplierObject>().StartMultiplierPlacement();
    }
    public void ObjectBackAdded(GameObject multiplier)
    {
        ObjectPool.Instance.AddObject(_OPMultiplierObjectCount, multiplier);
    }
}
