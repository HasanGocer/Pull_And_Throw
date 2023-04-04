using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpperManager : MonoSingleton<UpperManager>
{
    [SerializeField] GameObject _upgradePanel;
    [SerializeField] Button _stickmanUpperButton, _addedMultiplierButton, _shotTimeButton;
    [SerializeField] TMP_Text _stickmanUpperLevel, _addedMultiplierLevel, _shotTimeLevel;
    [SerializeField] TMP_Text _StickmanUpperMoney, _addedMultiplierMoney, _shotTimeMoney;

    public void UpperManagerStart()
    {
        ButtonPlacement();
        TextPlacement();
        PanelOff();
    }
    public void PanelOn()
    {
        _upgradePanel.SetActive(true);
    }

    public void PanelOff()
    {
        _upgradePanel.SetActive(false);
    }
    private void ButtonPlacement()
    {
        _stickmanUpperButton.onClick.AddListener(StickmanUpperButton);
        _addedMultiplierButton.onClick.AddListener(AddedMultiplierButton);
        _shotTimeButton.onClick.AddListener(ShotTimeButton);
    }
    private void TextPlacement()
    {
        ItemData itemData = ItemData.Instance;
        if (itemData.factor.stickmanConstant > itemData.maxFactor.stickmanConstant)
        {
            _stickmanUpperLevel.text = "Max  ";
            _StickmanUpperMoney.text = "Max  ";
        }
        else
        {
            _stickmanUpperLevel.text = "level " + itemData.factor.stickmanConstant;
            _StickmanUpperMoney.text = itemData.fieldPrice.stickmanConstant.ToString();
        }

        if (itemData.factor.addedMultiplier > itemData.maxFactor.addedMultiplier)
        {
            _addedMultiplierLevel.text = "Max  ";
            _addedMultiplierMoney.text = "Max  ";
        }
        else
        {
            _addedMultiplierLevel.text = "level " + itemData.factor.addedMultiplier;
            _addedMultiplierMoney.text = itemData.fieldPrice.addedMultiplier.ToString();
        }

        if (itemData.factor.addedMultiplier > itemData.maxFactor.addedMultiplier)
        {
            _shotTimeLevel.text = "Max  ";
            _shotTimeMoney.text = "Max  ";
        }
        else
        {
            _shotTimeLevel.text = "level " + itemData.factor.standartMoney;
            _shotTimeMoney.text = itemData.fieldPrice.standartMoney.ToString();
        }

    }

    private void StickmanUpperButton()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

        if (itemData.fieldPrice.stickmanConstant <= gameManager.money && itemData.factor.stickmanConstant < itemData.maxFactor.stickmanConstant)
        {
            MoneySystem.Instance.MoneyTextRevork(-itemData.fieldPrice.stickmanConstant);
            itemData.SetStickmanConstant();
            TapSystem.Instance.SetNewObjectCount();
            SoundSystem.Instance.CallUpgradeSound();

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
            MoneySystem.Instance.MoneyTextRevork(-itemData.fieldPrice.addedMultiplier);
            itemData.SetAddedMultiplier();
            MultiplierSystem.Instance.NewObject();
            SoundSystem.Instance.CallUpgradeSound();

            _addedMultiplierLevel.text = "level " + itemData.factor.addedMultiplier;
            _addedMultiplierMoney.text = itemData.fieldPrice.addedMultiplier.ToString();
        }
    }
    private void ShotTimeButton()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

        if (itemData.fieldPrice.shotCountdown <= gameManager.money && itemData.factor.shotCountdown < itemData.maxFactor.shotCountdown)
        {
            MoneySystem.Instance.MoneyTextRevork((int)-itemData.fieldPrice.shotCountdown);
            itemData.SetShotCountdown();
            SoundSystem.Instance.CallUpgradeSound();

            _shotTimeLevel.text = "level " + itemData.factor.shotCountdown;
            _shotTimeMoney.text = itemData.fieldPrice.shotCountdown.ToString();
        }
    }
}
