using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ()]
public class TalentTree : ScriptableObject {
    // Start is called before the first frame update

    public string name;
    public int lvl = 0;
    public int firstPrice = 1;
    int currentPrice;
    public int priceScale;
    public float priceScalePercent = 1;

    [Header ("Graph")]
    public Sprite talentTreeIcon;

    [Header ("Buff")]
    public BasicAttribute attribute;

    [Header ("IsScalable")]
    public bool isScalable;
    public float valueScale = 1;
    public float valueMultiplierScale = 1;

    void Start () {
        if (currentPrice <= 0) { currentPrice = firstPrice; }
        if(!isScalable){
            priceScale = 0;
            priceScalePercent = 1;
            valueScale = 1;
            valueMultiplierScale = 1;
        }
    }

    // Update is called once per frame
    void Update () {

    }

    public void Up () {
        lvl++;
        UpPrice ();
        if (isScalable) {
            attribute.Value *= valueScale;
            attribute.ValueMultiplier *= valueMultiplierScale;
        }
    }

    public void UpPrice () {
        float newPrice = currentPrice * priceScalePercent;
        int newPrice2 = Mathf.FloorToInt (newPrice);
        if (newPrice2 < currentPrice) { newPrice2 = currentPrice; }

        currentPrice = newPrice2 + priceScale;
    }

    public int GetLvl () {
        return lvl;
    }
    public int GetPrice () {
        return currentPrice;
    }

    public int GetLastPrice () {
        float last = currentPrice;

        last -= priceScale;
        last *= priceScalePercent;
        int lastInt = Mathf.FloorToInt (last);
        if (lastInt <= 0 || lvl == 1) { lastInt = firstPrice; }
        return lastInt;
    }

    public BasicAttribute GetLastAttribute () {
        BasicAttribute lastAttribute = new BasicAttribute (attribute);
        if (isScalable) {
            lastAttribute.Value /= valueScale;
            lastAttribute.ValueMultiplier /= valueMultiplierScale;
        }
        //Debug.Log ("CurrentValue: " + attribute.Value + "\nLastValue: " + lastAttribute.Value);
        return lastAttribute;
    }

    public void RemoveUpgrade () {
        lvl--;
        currentPrice = GetLastPrice ();

        if (isScalable) {
            attribute = GetLastAttribute ();
        }
    }
}