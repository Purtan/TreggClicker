using System;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public string name;
    public string amount = "0";
    public string price = "0";
    public Sprite icon;

    public override string ToString()
    {
        return $"Name: {name} Amount: {amount} Price: {price}";
    }
}