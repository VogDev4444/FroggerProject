using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dodgeCooldown : MonoBehaviour
{
    public Slider dodgeCD;
    public GameObject player;
    private float cdLength;
    private float cdCalc = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cdLength = player.GetComponent<Movement>().getCooldownLength();
    }

    void FixedUpdate()
    {
        //check to see if the player CANNOT dodge, if the player is staggered it will still charge
        if(!player.GetComponent<Movement>().canPlayerDodge())
        {
            cdCalc++;
            dodgeCD.value = (cdCalc / 50) / cdLength;
        }
        //check to see if the player CAN dodge and if the cooldown is up
        else if(cdCalc > 250f && player.GetComponent<Movement>().canPlayerDodge())
        {
            dodgeCD.value = 1f;
            cdCalc = 0f;
        }
    }
}
