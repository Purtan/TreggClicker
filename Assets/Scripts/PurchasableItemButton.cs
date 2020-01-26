using System;
using Assets.Scripts.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchasableItemButton : MonoBehaviour {
    public TMP_Text labelText;
    public TMP_Text amountText;
    public TMP_Text pricetext;
    public Image icon;
    private Item item;

    public bool dialogueDiscovered; //if the user has seen this dialogue before

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(() => Game.OnItemClicked(item));
    }

    public void SetItem(Item item) {
        //        Debug.Log($"Created an item {item}");
        this.item = item;
        labelText.text = item.name;
        icon.sprite = item.icon;
        amountText.text = item.amount.ToBigInteger().ToString("N0");
        pricetext.text = $"{item.price} TGs";
    }
}
