using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldBarCarry : MonoBehaviour
{
    public KeyCode giveGoldBarKey;
    public float giveRange = 3.0f;
    public LayerMask raycastTargetLayermask;
    public float prosperityIncrease = 10.0f;
    int goldBarCount = 0;

    public GameObject textObj;
    void Update()
    {
        if(goldBarCount > 0)
        {
            if(Input.GetKeyDown(giveGoldBarKey))
            {
                var col = Physics2D.OverlapCircle(transform.position, giveRange, raycastTargetLayermask);

                if(col)
                {
                    var village = col.GetComponent<Village>();
                    if(village)
                    {
                        village.ModifyProsperity(prosperityIncrease);
                        goldBarCount--;
                        textObj.GetComponent<Text>().text = goldBarCount.ToString();
                    }
                    Debug.Log("yeee");
                    var church = col.GetComponent<Church>();
                    if(church)
                    {
                        Debug.Log("yeete");
                        church.SpawnInquisitor(this);
                        goldBarCount--;
                        textObj.GetComponent<Text>().text = goldBarCount.ToString();
                    }
                    
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<GoldBar>())
        {
            Destroy(other.gameObject);
            goldBarCount++;
            textObj.GetComponent<Text>().text = goldBarCount.ToString();
        }
    }
}
