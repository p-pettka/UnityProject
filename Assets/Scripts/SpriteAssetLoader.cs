using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAssetLoader : Singleton<SpriteAssetLoader>
{
    public string spriteName = "coinsprite.png";

    private SpriteRenderer spriteRender;

    public void SetSprites()
    {
        spriteRender.sprite = AssetBundlesManager.Instance.GetSprite(spriteName);
    }

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }
}
