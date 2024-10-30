using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

[CreateAssetMenu(fileName ="Resources",menuName ="ScriptableObject/Resources",order = 1)]
public class ResourceData : ScriptableObject
{
    public uint money;
    public uint upgradeMoney = 10;
    public uint gainMoney = 1;
    public uint factorGainMoneyBalance = 1;
    public float factorGainMoney = 1.1f;
    public float factorGainMoneyPrice = 1.5f;
}
