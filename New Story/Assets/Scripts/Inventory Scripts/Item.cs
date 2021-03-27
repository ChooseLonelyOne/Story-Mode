using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public enum ItemType
    {
        Sword,
        HealtPotion,
        ManaPotion,
        Coin,
        Medkit,
        Key,
        Nothing
    }

    public ItemType itemType;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Sword:            return ItemAssets.Instance.swordSprite;
            case ItemType.HealtPotion:      return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:       return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Coin:             return ItemAssets.Instance.coinSprite;
            case ItemType.Medkit:           return ItemAssets.Instance.medkitSprite;
            case ItemType.Key:              return ItemAssets.Instance.keySprite;
        }
    }
}
