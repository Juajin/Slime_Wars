using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    public JewelObj JewelObj;
    public TextMeshProUGUI text;
    private void Awake()
    {
        text.text = ""+JewelObj.amount;
    }
    public void Increase(int amount)
    {
        JewelObj.amount += amount;
        text.text = ""+JewelObj.amount;
    }
    public bool Spend(int amount)
    {
        bool enoughJewel = false;
        enoughJewel = JewelObj.amount - amount < 0 ? false : true;
        JewelObj.amount = enoughJewel ? JewelObj.amount -= amount:JewelObj.amount ;
        text.text = "" + JewelObj.amount;
        return enoughJewel;
    }
}

