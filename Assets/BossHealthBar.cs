using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    float health;
    RectTransform healthBar;
    float healthBarValue;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<RectTransform>();
        healthBarValue = healthBar.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        health = GameObject.Find("Boss").GetComponent<BossMovement>().health;
        healthBar.sizeDelta = new Vector2(healthBarValue - (healthBarValue * (((10-health) * 10) / 100)), healthBar.sizeDelta.y);
        //healthBar.position = new Vector3((healthBarValue * (((10 - health) * 10) / 100)/2),0,0);
        
    }
}
