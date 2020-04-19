using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unclipper : MonoBehaviour
{
    Vector2 baseOffset;

    void Start()
    {
        var circleCol = GetComponent<CircleCollider2D>();
        if(circleCol)
        {
            baseOffset = circleCol.offset;
            return;
        }

        var boxCol = GetComponent<BoxCollider2D>();
        if(boxCol)
        {
            baseOffset = boxCol.offset;
            return;
        }

        var polygonCol = GetComponent<PolygonCollider2D>();
        if(polygonCol)
        {
            foreach(var point in polygonCol.points)
            {
                baseOffset += point;
            }
            baseOffset /= polygonCol.points.Length;
            return;
        }

        baseOffset = Vector2.zero;
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + baseOffset.y);
    }
}
