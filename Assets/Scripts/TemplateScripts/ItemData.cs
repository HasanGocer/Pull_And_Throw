using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public int stickmanConstant, addedMultiplier;
        public float standartMoney;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void AwakeID()
    {
        field.stickmanConstant = standart.stickmanConstant + (factor.stickmanConstant * constant.stickmanConstant);
        fieldPrice.stickmanConstant = fieldPrice.stickmanConstant * factor.stickmanConstant;
        field.addedMultiplier = standart.addedMultiplier + (factor.addedMultiplier * constant.addedMultiplier);
        fieldPrice.addedMultiplier = fieldPrice.addedMultiplier * factor.addedMultiplier;
        field.standartMoney = standart.standartMoney + (factor.standartMoney * constant.standartMoney);
        fieldPrice.standartMoney = fieldPrice.standartMoney * factor.standartMoney;

        /*
         field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        */
        if (factor.stickmanConstant > maxFactor.stickmanConstant)
        {
            factor.stickmanConstant = maxFactor.stickmanConstant;
            field.stickmanConstant = standart.stickmanConstant + (factor.stickmanConstant * constant.stickmanConstant);
            fieldPrice.stickmanConstant = fieldPrice.stickmanConstant / (factor.stickmanConstant - 1);
            fieldPrice.stickmanConstant = fieldPrice.stickmanConstant * factor.stickmanConstant;
        }
        if (factor.addedMultiplier > maxFactor.addedMultiplier)
        {
            factor.addedMultiplier = maxFactor.addedMultiplier;
            field.addedMultiplier = standart.addedMultiplier + (factor.addedMultiplier * constant.addedMultiplier);
            fieldPrice.addedMultiplier = fieldPrice.addedMultiplier / (factor.addedMultiplier - 1);
            fieldPrice.addedMultiplier = fieldPrice.addedMultiplier * factor.addedMultiplier;
        }
        if (factor.standartMoney > maxFactor.standartMoney)
        {
            factor.standartMoney = maxFactor.standartMoney;
            field.standartMoney = standart.standartMoney + (factor.standartMoney * constant.standartMoney);
            fieldPrice.standartMoney = fieldPrice.standartMoney / (factor.standartMoney - 1);
            fieldPrice.standartMoney = fieldPrice.standartMoney * factor.standartMoney;
        }


        /*
          if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }
        */

        StartCoroutine(Buttons.Instance.LoadingScreen());
    }
    public void SetStickmanConstant()
    {
        factor.stickmanConstant++;

        field.stickmanConstant = standart.stickmanConstant + (factor.stickmanConstant * constant.stickmanConstant);
        fieldPrice.stickmanConstant = fieldPrice.stickmanConstant / (factor.stickmanConstant - 1);
        fieldPrice.stickmanConstant = fieldPrice.stickmanConstant * factor.stickmanConstant;

        if (factor.stickmanConstant > maxFactor.stickmanConstant)
        {
            factor.stickmanConstant = maxFactor.stickmanConstant;
            field.stickmanConstant = standart.stickmanConstant + (factor.stickmanConstant * constant.stickmanConstant);
            fieldPrice.stickmanConstant = fieldPrice.stickmanConstant / (factor.stickmanConstant - 1);
            fieldPrice.stickmanConstant = fieldPrice.stickmanConstant * factor.stickmanConstant;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetAddedMultiplier()
    {
        factor.addedMultiplier++;

        field.addedMultiplier = standart.addedMultiplier + (factor.addedMultiplier * constant.addedMultiplier);
        fieldPrice.addedMultiplier = fieldPrice.addedMultiplier / (factor.addedMultiplier - 1);
        fieldPrice.addedMultiplier = fieldPrice.addedMultiplier * factor.addedMultiplier;

        if (factor.addedMultiplier > maxFactor.addedMultiplier)
        {
            factor.addedMultiplier = maxFactor.addedMultiplier;
            field.addedMultiplier = standart.addedMultiplier + (factor.addedMultiplier * constant.addedMultiplier);
            fieldPrice.addedMultiplier = fieldPrice.addedMultiplier / (factor.addedMultiplier - 1);
            fieldPrice.addedMultiplier = fieldPrice.addedMultiplier * factor.addedMultiplier;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetStandartMoney()
    {
        factor.standartMoney++;

        field.standartMoney = standart.standartMoney + (factor.standartMoney * constant.standartMoney);
        fieldPrice.standartMoney = fieldPrice.standartMoney / (factor.standartMoney - 1);
        fieldPrice.standartMoney = fieldPrice.standartMoney * factor.standartMoney;

        if (factor.standartMoney > maxFactor.standartMoney)
        {
            factor.standartMoney = maxFactor.standartMoney;
            field.standartMoney = standart.standartMoney + (factor.standartMoney * constant.standartMoney);
            fieldPrice.standartMoney = fieldPrice.standartMoney / (factor.standartMoney - 1);
            fieldPrice.standartMoney = fieldPrice.standartMoney * factor.standartMoney;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }


    /*
     public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    */
}
