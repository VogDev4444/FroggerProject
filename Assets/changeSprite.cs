using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class changeSprite : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public Sprite greyHeart; //used for when attack is on cooldown
    public Sprite activeHeart; //used when players are able to shoot

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void changeRetical(int mode)
    {
        //depending on what is called it changes the heart
        if(mode == 0)
        {
            spriteRender.sprite = greyHeart;
        }
        else if (mode == 1)
        {
            spriteRender.sprite = activeHeart;
        }
    }
}
