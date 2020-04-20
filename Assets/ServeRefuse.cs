using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
 
 public class ServeRefuse: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
    [SerializeField] TextMeshProUGUI textmeshPro;
    [SerializeField] GameObject demonGoodText;
    [SerializeField] GameObject demonBadText;

    [SerializeField] GameObject demonBad;
    public void OnPointerEnter(PointerEventData eventData)
    {
        textmeshPro.text = "SERVE";
        demonBadText.SetActive(true);
        demonGoodText.SetActive(false);
        demonBad.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textmeshPro.text = "REFUSE";
        demonGoodText.SetActive(true);
        demonBadText.SetActive(false);
        demonBad.SetActive(false);
    }
}

