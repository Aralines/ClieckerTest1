using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textGainMoney;
    [SerializeField] private TextMeshProUGUI textUpgradeMoneyPrice;
    [SerializeField] private TextMeshProUGUI textLevelCount;
    [SerializeField] private Button upgradeButton; // Ссылка на кнопку улучшения

    [SerializeField] private uint money;
    public uint gainMoney = 1;
    [SerializeField] private uint upgradeMoneyPrice = 10;
    [SerializeField] private uint factorGainMoneyBalance = 1;

    [SerializeField] private float factorGainMoney = 1.1f;
    [SerializeField] private float factorGainMoneyPrice = 1.4f;
    [SerializeField] private uint levelCount = 1;

    private void Start()
    {
        UpdateUI();
    }

    public void GainMoney()
    {
        money += gainMoney;
        UpdateUI();
    }

    public void UpgradeMoney()
    {
        if (money >= upgradeMoneyPrice)
        {
            money -= upgradeMoneyPrice;
            upgradeMoneyPrice = (uint)(upgradeMoneyPrice * factorGainMoneyPrice + levelCount);
            gainMoney = (uint)(gainMoney * factorGainMoney + factorGainMoneyBalance);
            levelCount++;

            if (levelCount % 3 == 0)
            {
                factorGainMoneyBalance++;
                factorGainMoneyBalance = (uint)(factorGainMoneyBalance * 1.2f);
            }
            if (levelCount % 6 == 0)
            {
                factorGainMoneyBalance++;
                factorGainMoneyBalance *= 2;
            }
            if (levelCount % 23 == 0)
            {
                factorGainMoneyBalance++;
                factorGainMoneyBalance *= 3;
            }

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        UpdateMoney();
        textGainMoney.text = "+" + gainMoney.ToString();
        textUpgradeMoneyPrice.text = "Upgrade " + upgradeMoneyPrice.ToString();
        textLevelCount.text = levelCount.ToString();

        // Обновляем состояние кнопки
        upgradeButton.interactable = money >= upgradeMoneyPrice;
    }

    private void UpdateMoney()
    {
        textMoney.text = money.ToString();
    }
}
