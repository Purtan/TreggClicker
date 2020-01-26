using System;
using System.Numerics;
using Assets.Scripts.Util;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class Game : MonoBehaviour {

    private static Game instance;

    public ParticleSystem ps;
    public RectTransform t;
    public Text text;
    public string treggString;
    public Vector3 rotateTregg;
    private BigInteger treggsPerClicks = 1;
    
    private BigInteger treggs;
    private readonly Inventory inventory = new Inventory();
    private float timeSinceLastUpdate;
    private float timeBetweenUpdates = 1f;

    public static BigInteger Treggs
    {
        get => instance.treggs;
        set
        {
            instance.Emit((int)(value - Treggs));
            
            instance.treggs = value;
            ShoppingList.Update();
            instance.text.text = $"{Treggs} {instance.treggString}";
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Treggs = 0;
        Load();
    }

    private void OnApplicationPause() {
        Save();
    }
    
    private void OnApplicationQuit() {
        Save();
    }

    public void Save() {
        PlayerPrefs.SetString("Treggs", Treggs.ToString());
    }

    public void Load() {
        Treggs = BigInteger.Parse(PlayerPrefs.GetString("Treggs"));
    }

    void Update() {
        t.Rotate(rotateTregg * Time.deltaTime);
        Vector3 target = new Vector3(1, 1, 1);
        Vector3 current = t.localScale;
        float lerp = 0.2f;

        t.localScale = Vector3.Lerp(current, target, lerp);

        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= timeBetweenUpdates)
        {
            timeSinceLastUpdate = 0f;
            OnUpdate();
        }
    }

    private void OnUpdate()
    {
        Treggs += inventory.GetPropertySum("TreggsPerUpdate", 0);
    }

    public void Click()
    {
        Treggs += treggsPerClicks * inventory.GetPropertySum("ClickMultiplier", 1);
        t.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void Emit(int parts) {
        ps.Emit(parts);
    }

    public static void OnItemClicked(Item item)
    {
        if (Treggs < item.price.ToBigInteger())
            return;
            
        Debug.Log($"Bought {item.name}");
        Treggs -= item.price.ToBigInteger();
        instance.inventory.Add(item);
        
        // TODO: OH GOD PLEASE FIX ME.... OH GOD
        item.amount = (item.amount.ToBigInteger() + 1).ToString();
//        item.price = (Convert.ToDouble(item.price) * 1.1).ToString();
        item.price = (item.price.ToBigInteger() + item["PriceIncrease"]).ToString();
        Debug.Log(item.price);
        ShoppingList.Update();
    }
}