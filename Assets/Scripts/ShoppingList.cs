using System.Linq;
using Assets.Scripts.Util;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    private static ShoppingList instance;
    
    public Item[] items;
    public Transform shoppingListContainer;
    public GameObject shoppingItemPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Update();
    }

    public static void Update()
    {
        instance.UpdateInternal();
    }

    private void UpdateInternal()
    {
        instance.shoppingListContainer.ClearChildren();
        foreach (var item in items.Where(i => i.price.ToBigInteger() <= Game.Treggs || i.Revealed))
        {
            Instantiate(shoppingItemPrefab, shoppingListContainer).GetComponent<PurchasableItemButton>().SetItem(item);
        }
    }
}