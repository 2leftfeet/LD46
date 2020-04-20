using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScript : MonoBehaviour
{
    public FadeOutIn fade;

    // Start is called before the first frame update
    void OnEnable()
    {
        InfluenceManager.InfluenceEmpty += OnInfluenceEmpty;
    }

    private void OnDisable()
    {
        InfluenceManager.InfluenceEmpty -= OnInfluenceEmpty;
    }

    private void OnInfluenceEmpty()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        fade.StartMove();
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("LossVictory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
