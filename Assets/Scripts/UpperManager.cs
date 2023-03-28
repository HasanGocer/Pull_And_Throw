using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpperManager : MonoSingleton<UpperManager>
{
    [SerializeField] Button _stickmanUpperButton, _addedMultiplierButton, _standartMoneyUpperButton;
    [SerializeField] TMP_Text _stickmanUpperLevel, _addedMultiplierLevel, _standartMoneyUpperLevel;
    [SerializeField] TMP_Text _StickmanUpperMoney, _addedMultiplierMoney, _StandartMoneyUpperMoney;

    public void UpperManagerStart()
    {
        ButtonPlacement();
        TextPlacement();
    }

    private void ButtonPlacement()
    {
        _stickmanUpperButton.onClick.AddListener(StickmanUpperButton);
        _addedMultiplierButton.onClick.AddListener(AddedMultiplierButton);
        _standartMoneyUpperButton.onClick.AddListener(StandartMoneyUpperButton);
    }
    private void TextPlacement()
    {
        ItemData itemData = ItemData.Instance;

        _stickmanUpperLevel.text = "level " + itemData.factor.stickmanConstant;
        _StickmanUpperMoney.text = itemData.fieldPrice.stickmanConstant.ToString();
        _addedMultiplierLevel.text = "level " + itemData.factor.addedMultiplier;
        _addedMultiplierMoney.text = itemData.fieldPrice.addedMultiplier.ToString();
        _standartMoneyUpperLevel.text = "level " + itemData.factor.standartMoney;
        _StandartMoneyUpperMoney.text = itemData.fieldPrice.standartMoney.ToString();
    }

    private void StickmanUpperButton()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

        if (itemData.fieldPrice.stickmanConstant <= gameManager.money && itemData.factor.stickmanConstant < itemData.maxFactor.stickmanConstant)
        {
            MoneySystem.Instance.MoneyTextRevork(itemData.fieldPrice.stickmanConstant);
            itemData.SetStickmanConstant();
            TapSystem.Instance.SetNewObjectCount();

            _stickmanUpperLevel.text = "level " + itemData.factor.stickmanConstant;
            _StickmanUpperMoney.text = itemData.fieldPrice.stickmanConstant.ToString();
        }
    }
    private void AddedMultiplierButton()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

        if (itemData.fieldPrice.addedMultiplier <= gameManager.money && itemData.factor.addedMultiplier < itemData.maxFactor.addedMultiplier && MultiplierSystem.Instance.MarketIsFree())
        {
            MoneySystem.Instance.MoneyTextRevork(itemData.fieldPrice.addedMultiplier);
            itemData.SetAddedMultiplier();
            MultiplierSystem.Instance.NewObject();

            _addedMultiplierLevel.text = "level " + itemData.factor.addedMultiplier;
            _addedMultiplierMoney.text = itemData.fieldPrice.addedMultiplier.ToString();
        }
    }
    private void StandartMoneyUpperButton()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

        if (itemData.fieldPrice.standartMoney <= gameManager.money && itemData.factor.standartMoney < itemData.maxFactor.standartMoney)
        {
            MoneySystem.Instance.MoneyTextRevork((int)itemData.fieldPrice.standartMoney);
            itemData.SetStandartMoney();
            TapSystem.Instance.SetNewObjectCount();

            _standartMoneyUpperLevel.text = "level " + itemData.factor.standartMoney;
            _StandartMoneyUpperMoney.text = itemData.fieldPrice.standartMoney.ToString();
        }
    }
}
