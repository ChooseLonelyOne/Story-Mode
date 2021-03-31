using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite phoneSprite;
    public Sprite keySprite;

    [Space]
    public Sprite swordUISprite;
    public Sprite healthPotionUISprite;
    public Sprite manaPotionUISprite;
    public Sprite coinUISprite;
    public Sprite phoneUISprite;
    public Sprite keyUISprite;
}
