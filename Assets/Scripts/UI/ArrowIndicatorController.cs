using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicatorController : MonoBehaviour
{
    public LeanTweenType tweenType;
    public float distanceFromBorder = 200;
    public float time = 2f;
    public AnimationCurve curve;

    public RectTransform leftArrow;
    public RectTransform topArrow;
    public RectTransform rightArrow;
    public RectTransform bottomArrow;

    // Start is called before the first frame update
    void Start()
    {
       // PlayLeft();
        //PlayTop();
       // PlayRight();
       // PlayBottom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLeft()
    {
        PlayArrow(leftArrow, new Vector2(distanceFromBorder, leftArrow.anchoredPosition.y));
    }

    public void PlayTop()
    {
        PlayArrow(topArrow, new Vector2(bottomArrow.anchoredPosition.x, -distanceFromBorder));
    }

    public void PlayRight()
    {
        PlayArrow(rightArrow, new Vector2(-distanceFromBorder, leftArrow.anchoredPosition.y));
    }

    public void PlayBottom()
    {
        PlayArrow(bottomArrow, new Vector2(bottomArrow.anchoredPosition.x, distanceFromBorder));
    }

    void PlayArrow(RectTransform target, Vector2 pointGo)
    {
        Vector2 goBackPoint = target.anchoredPosition;

        LeanTween.move(target, pointGo, time).setEase(curve).setOnComplete(() => {
            LeanTween.move(target, goBackPoint, 0.5f).setEase(LeanTweenType.easeInOutSine).setDelay(1.15f);
        });
        LeanTween.scale(target, Vector3.one * 1.5f, time + 1.35f).setEase(LeanTweenType.easeInOutSine);
    }
}
