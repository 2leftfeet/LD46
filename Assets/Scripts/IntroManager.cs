using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public enum Sequence { Begin, SpawnVillager, Possessed, Sacrificed, Attacked, Done  }

    private readonly string INTRO_DONE_KEY = "INTRO_DONE_KEY";

    [Header("References")]
    [SerializeField]
    private SpeechBubble speechBubble;
    [SerializeField]
    private EnemyWaves enemyWaves;
    [SerializeField]
    private Spawner spawnerOne;
    [SerializeField]
    private Spawner spawnerTwo;

    private Sequence sequence = Sequence.Begin;
    private bool isIntro = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerPrefs.HasKey(INTRO_DONE_KEY))
        {
            StartTutorial();
        }
        else
        {
            InfluenceManager.Instance.decayInfluence = true;
        }
    }

    [ContextMenu("Reset Intro")]
    public void ResetIntro()
    {
        PlayerPrefs.DeleteKey(INTRO_DONE_KEY);
    }

    void StartTutorial()
    {
        isIntro = true;

        spawnerOne.enabled = false;
        spawnerTwo.enabled = false;

        enemyWaves.enabled = false;

        StartCoroutine(BeginSequence());
    }

    IEnumerator BeginSequence()
    {
        sequence = Sequence.Begin;

        yield return new WaitForSeconds(2.5f);
        speechBubble.PlayShowText("Welcome back to the mortal world.");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("As you may be aware, I am not at my fullest of power.");
        yield return new WaitForSeconds(6f);
        StartCoroutine(SpawnVillagerSequence());
    }

    IEnumerator SpawnVillagerSequence()
    {
        SingleInfluence.OnPossess += OnPossess;
        sequence = Sequence.SpawnVillager;
        spawnerOne.SingleSpawn();

        speechBubble.PlayShowText("It seems that one of the villages has a strolling peasant. Go bring him to me!");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("You can possess them by using my powers that I've given to you.");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("Press SPACE button and click on the peasant while its in your range.");
    }

    private void OnPossess()
    {
        StopAllCoroutines();

        SingleInfluence.OnPossess -= OnPossess;
        StartCoroutine(PossessedSequence());
    }

    IEnumerator PossessedSequence()
    {
        sequence = Sequence.Possessed;
        SacrificePoint.OnSacrifice += OnSacrifice;

        speechBubble.PlayShowText("Goood, gooood!");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("But be careful with succumbing too many villagers to my cause!");
        yield return new WaitForSeconds(6f);
        speechBubble.PlayShowText("Less prosperous village means less souls to feast on...");
        yield return new WaitForSeconds(7f);
        speechBubble.PlayShowText("Enough chit-chat, bring it to the altar on your BOTTOM RIGHT and press F");
    }

    private void OnSacrifice()
    {
        StopAllCoroutines();

        SacrificePoint.OnSacrifice -= OnSacrifice;
        StartCoroutine(SacrificedSequence());
    }

    IEnumerator SacrificedSequence()
    {
        sequence = Sequence.Sacrificed;

        speechBubble.PlayShowText("Deliciooouus mortal. I've grown in power!");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("These delicious juices will keep me alive!");
        yield return new WaitForSeconds(5f);

        StartCoroutine(AttackedSequence());
    }

    IEnumerator AttackedSequence()
    {
        sequence = Sequence.Attacked;
        EnemyWaves.OnWaveDefeat += OnWaveDefeat;


        speechBubble.PlayShowText("Hmmm.. I believe my appearence hasn't gone unnoticed.");
        yield return new WaitForSeconds(6f);
        speechBubble.PlayShowText("Soon, those non-believers will come to take me...");
        yield return new WaitForSeconds(6f);
        speechBubble.PlayShowText("Quick! Bring more minions to protect me!");
        // TODO Reenable spawning
        spawnerOne.enabled = true;
        spawnerTwo.enabled = true;
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("Click ALT and mark a position to defend for!");
        enemyWaves.SpawnSingleWave();
        yield return new WaitForSeconds(1f);
        enemyWaves.enabled = true;
    }

    private void OnWaveDefeat()
    {
        StopAllCoroutines();

        EnemyWaves.OnWaveDefeat -= OnWaveDefeat;
        StartCoroutine(FinishSequence());
    }

    IEnumerator FinishSequence()
    {
        enemyWaves.enabled = false;
        sequence = Sequence.Done;
        PlayerPrefs.SetInt(INTRO_DONE_KEY, 1);
        yield return new WaitForSeconds(2f);
        speechBubble.PlayShowText("You serve me well, there may actually be use of you.");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("Without a doubt, there will be more. And you will protect me with your life.");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("Make sure to spend their gold to increase the prosperity of a village.");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("Or buy the services of a powerful inquisitor at the church.");
        yield return new WaitForSeconds(5f);
        speechBubble.PlayShowText("You can do both by pressing F.");
        enemyWaves.enabled = true;
        InfluenceManager.Instance.decayInfluence = true;
    }
}
