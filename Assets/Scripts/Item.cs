using System;
using System.Linq;
using System.Numerics;
using Assets.Scripts.Util;
using UnityEngine;

[Serializable]
public class Item {
    public string name;
    public string amount = "0";
    public string price = "0";
    public Sprite icon;
    public ItemProperty[] properties;
    [HideInInspector] public bool visible;
    public bool Revealed => amount.ToBigInteger() > 0 || visible;

    public Sprite dialogueIcon; //dialogue popup on first purchase
    public string dialogueText;
    public bool dialogueDiscovered; //if the user has seen this item's dialogue before



    public override string ToString() {
        return $"Name: {name} Amount: {amount} Price: {price}";
    }

    public BigInteger this[string key] {
        get {
            var result = properties.FirstOrDefault(p => p.name == key);
            if(result != null) {
                return result.value.ToBigInteger();
            }

            Debug.LogError($"Couldn't find property {key} on item {name}");
            return default;
        }
    }

    public bool HasProperty(string key) {
        return properties.Any(p => p.name == key);
    }
}

[Serializable]
public class ItemProperty {
    public string name;
    public string value;
}