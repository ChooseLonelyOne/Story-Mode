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
        Phone,
        Key,
        Nothing
    }

    public ItemType itemType;

    public Sprite GetUISprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Sword:            return ItemAssets.Instance.swordUISprite;
            case ItemType.HealtPotion:      return ItemAssets.Instance.healthPotionUISprite;
            case ItemType.ManaPotion:       return ItemAssets.Instance.manaPotionUISprite;
            case ItemType.Coin:             return ItemAssets.Instance.coinUISprite;
            case ItemType.Phone:            return ItemAssets.Instance.phoneUISprite;
            case ItemType.Key:              return ItemAssets.Instance.keyUISprite;
        }
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:            return ItemAssets.Instance.swordSprite;
            case ItemType.HealtPotion:      return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion:       return ItemAssets.Instance.manaPotionSprite;
            case ItemType.Coin:             return ItemAssets.Instance.coinSprite;
            case ItemType.Phone:            return ItemAssets.Instance.phoneSprite;
            case ItemType.Key:              return ItemAssets.Instance.keySprite;
        }
    }
}
