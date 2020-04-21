using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutIn : MonoBehaviour
{
    public Color tc;
    public float fadeInSpeed = 2f;
    public bool playOnStart;

    private Image image;
    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        if (playOnStart)
            move = true;
    }

    public void StartMove()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            var c = image.color;
            image.color = new Color(
                Mathf.MoveTowards(c.r, tc.r, fadeInSpeed * Time.unscaledDeltaTime),
                Mathf.MoveTowards(c.g, tc.g, fadeInSpeed * Time.unscaledDeltaTime),
                Mathf.MoveTowards(c.b, tc.b, fadeInSpeed * Time.unscaledDeltaTime),
                Mathf.MoveTowards(c.a, tc.a, fadeInSpeed * Time.unscaledDeltaTime));
        }
    }
}
