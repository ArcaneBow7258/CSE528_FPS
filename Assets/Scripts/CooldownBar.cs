using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CooldownBar : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerShoot cooldown;
    public int abilityNum;
    Image image;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount =  cooldown.coolCount[abilityNum] / cooldown.cooldowns[abilityNum];
    }
}
