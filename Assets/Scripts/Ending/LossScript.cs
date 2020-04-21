using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossScript : MonoBehaviour
{
    public GameObject showDeathText;
    public FadeOutIn fade;

    public GameObject player1;
    public GameObject player2;
    public GameObject playerParticles1;
    public GameObject playerParticles2;


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
        player1.SetActive(false);
        player2.SetActive(false);
        playerParticles1.SetActive(true);
        playerParticles2.SetActive(true);
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
