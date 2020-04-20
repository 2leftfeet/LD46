using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBarCarry : MonoBehaviour
{
    public KeyCode giveGoldBarKey;
    public float giveRange = 3.0f;
    public LayerMask raycastTargetLayermask;
    public float prosperityIncrease = 10.0f;
    int goldBarCount = 0;

    void Update()
    {
        if(Input.GetKeyDown(giveGoldBarKey))
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, giveRange, raycastTargetLayermask);

            foreach(var col in colliders)
            {
                var village = col.GetComponent<Village>();
                if(village)
                {
                    village.ModifyProsperity(prosperityIncrease);
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
        }
    }
}
