using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab = default;
    [SerializeField] private GameObject rangePrefab = default;
    [HideInInspector] public bool isTargeting = false;

    [Header("Single influence")]
    public KeyCode singleKey;
    [Header("Cancelling cast")]
    public KeyCode cancelKey;

    private LineRenderer lineInstance;
    private GameObject rangeInstance;

    private float currentRange = 0f;

    private void Update()
    {
        UpdateLine();
    }

    public void StartCast(float range)
    {
        isTargeting = true;
        CursorManager.instance.SetCursorState("CAST");

        // Range renderer.
        rangeInstance = Instantiate(rangePrefab, Vector3.zero, Quaternion.identity, transform);
        float effectiveRange = range*2;
        rangeInstance.transform.localScale = new Vector3(effectiveRange, effectiveRange, 0);

        currentRange = range;

        // Line renderer.
        GameObject go = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineInstance = go.GetComponent<LineRenderer>();
    }

    public void EndCast()
    {
        isTargeting = false;

        Destroy(rangeInstance);
        rangeInstance = null;

        Destroy(lineInstance.gameObject);
        lineInstance = null;

        CursorManager.instance.SetCursorState("DEFAULT");
    }

    private void UpdateLine()
    {
        if(lineInstance)
        {
            Vector2 casterPos = transform.position;
            Vector2 targetPos = CursorManager.instance.GetWorldSpacePosition();

            if(IsDistanceValid(casterPos, targetPos, currentRange))
            {
                lineInstance.SetPosition(0, casterPos);
                lineInstance.SetPosition(1, targetPos);
            }
            else
            {
                lineInstance.SetPosition(0, casterPos);
                lineInstance.SetPosition(1, casterPos);
            }
        }
    }

    public bool IsDistanceValid(Vector2 casterPos, Vector2 targetPos, float range)
    {
        //range *= 2;
        return ((casterPos - targetPos).sqrMagnitude) < range*range ? true : false;
    }
    
}
