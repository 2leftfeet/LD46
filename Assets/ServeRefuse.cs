using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
 
 public class ServeRefuse: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
    [SerializeField] TextMeshProUGUI textmeshPro;
    [SerializeField] GameObject demonGood;
    [SerializeField] GameObject demonBad;
    public void OnPointerEnter(PointerEventData eventData)
    {
        textmeshPro.text = "SERVE";
        demonGood.SetActive(false);
        demonBad.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textmeshPro.text = "REFUSE";
        demonGood.SetActive(true);
        demonBad.SetActive(false);
    }
}

