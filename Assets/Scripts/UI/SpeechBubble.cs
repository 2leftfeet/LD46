using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public RectTransform bubbleOne;
    public RectTransform bubbleTwo;
    public RectTransform bubbleFinal;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlayShowText(string playText)
    {
        text.text = playText;

        LeanTween.scale(bubbleOne, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBounce);
        LeanTween.scale(bubbleTwo, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBounce).setDelay(0.33f);
        LeanTween.scale(bubbleFinal, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBounce).setDelay(0.66f);

        LeanTween.scale(bubbleOne, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBounce).setDelay(3f);
        LeanTween.scale(bubbleTwo, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBounce).setDelay(3f + 0.33f);
        LeanTween.scale(bubbleFinal, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBounce).setDelay(3f + 0.66f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
