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
        if(!player.GetComponent<Movement>().hasPlayerDodged())
        {
            cdCalc++;
            dodgeCD.value = (cdCalc / 50) / cdLength;
        }
        else if(cdCalc > 250f && player.GetComponent<Movement>().hasPlayerDodged())
        {
            Debug.Log("cdCalc: " + cdCalc + "Current Slider Value: " + dodgeCD.value);
            dodgeCD.value = 1f;
            cdCalc = 0f;
        }
    }
}
