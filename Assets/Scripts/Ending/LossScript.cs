using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScript : MonoBehaviour
{
    public GameObject showDeathText;
    public FadeOutIn fade;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(LoadScene());
    }

    private void OnDisable()
    {
    }


    IEnumerator LoadScene()
    {
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(1f);
        fade.StartMove();
        yield return new WaitForSecondsRealtime(5f);
        // TODO EVAPORATE HERE
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("DARK AGAIN");
        fade.tc = new Color(0, 0, 0, 255);
        yield return new WaitForSecondsRealtime(6f);
        showDeathText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
