using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab = default;
    [SerializeField] private GameObject rangePrefab = default;

    [Header("Single influence")]
    public KeyCode singleKey;
    [SerializeField] private float range = default;
    [Header("Cancelling cast")]
    public KeyCode cancelKey;

    private LineRenderer lineInstance;
    private GameObject rangeInstance;

    private void Update()
    {
        UpdateLine();
    }

    public void CreateRange()
    {
        rangeInstance = Instantiate(rangePrefab, Vector3.zero, Quaternion.identity, transform);
        float effectiveRange = range*2;
        rangeInstance.transform.localScale = new Vector3(effectiveRange, effectiveRange, 0);
    }

    public void DestroyRange()
    {
        Destroy(rangeInstance);
        rangeInstance = null;
    }

    public void CreateLine()
    {
        GameObject go = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineInstance = go.GetComponent<LineRenderer>();
    }

    public void DestroyLine()
    {
        Destroy(lineInstance.gameObject);
        lineInstance = null;
    }

    private void UpdateLine()
    {
        if(lineInstance)
        {
            Vector2 casterPos = transform.position;
            Vector2 targetPos = CursorManager.instance.GetWorldSpacePosition();

            if(IsDistanceValid(casterPos, targetPos))
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

    public bool IsDistanceValid(Vector2 casterPos, Vector2 targetPos)
    {
        return ((casterPos - targetPos).sqrMagnitude) < range*range ? true : false;
    }
    
}
