using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    GameObject touchCenter;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    [SerializeField]
    private GameObject gameoverUI;
    [SerializeField]
    private GameObject pSystem;
    public JewelObj jewelOBJ;
    private ObjectPooler objectPooler;
    private int gameSpeed = 2;
    private float score = 1;
    private GameObject currentGameArea, nextGameArea;
    public static GameManagement Instance;
    public bool startFlag = false;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Instance = this;
    }
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        touchCenter = GameObject.FindGameObjectWithTag("TouchCenter");
        if (pSystem == null)
        {
            pSystem = new GameObject();
        }
        currentGameArea = objectPooler.SpawnFromPool("Level", new Vector3(0, 0, 0));
        nextGameArea = objectPooler.SpawnFromPool("Level", new Vector3(0, 20, 0));
        SetBestScore();
    }
    private void FixedUpdate()
    {
        if ((int)score % 100 == 0)
        {
            score++;
            gameSpeed += 1;
        }
        if (startFlag)
        {
            score += (float)gameSpeed / 20;
            if (currentGameArea.transform.localPosition.y < -20)
            {
                currentGameArea.SetActive(false);
                currentGameArea = nextGameArea;
                nextGameArea = objectPooler.SpawnFromPool("Level", new Vector3(0, 20, 0));
            }
            currentGameArea.transform.Translate(Vector3.down * gameSpeed / 100);
            nextGameArea.transform.Translate(Vector3.down * gameSpeed / 100);
            scoreText.text = "" + (int)score;
        }

    }
    private void SetBestScore()
    {
        bestScoreText.text = "Best:" + jewelOBJ.bestScore;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        foreach (var item in GameObject.FindGameObjectsWithTag("Obstackle"))
        {
            foreach (var obs in item.transform.parent.GetComponentsInChildren<EdgeCollider2D>())
            {
                obs.enabled = false;
            }
        }
        gameoverUI.SetActive(false);
        pSystem.SetActive(true);
        SetPlayerTransparant();
        StartCoroutine(EnableThornCollidersBack());
    }
    private void SetPlayerTransparant()
    {
        var color = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color;
        color.a = 0.5f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = color;
    }
    private void SetPlayerOpaque()
    {
        var color = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().color = color;
    }
    public void GameOver()
    {
        jewelOBJ.bestScore = jewelOBJ.bestScore < score ? (int)score : jewelOBJ.bestScore;
        SetBestScore();
        PauseGame();
        pSystem.SetActive(false);
        gameoverUI.SetActive(true);
    }
    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void TouchtoTouchPad()
    {

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempPosition = mousePosition;
        tempPosition.z = -1f;
        pSystem.transform.position = tempPosition;
        pSystem.GetComponent<ParticleSystem>().Play();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rb2d.gravityScale = 2;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SlimeMovement(mousePosition, touchCenter.transform.localPosition);
        startFlag = true;
    }
    IEnumerator EnableThornCollidersBack()
    {
        yield return new WaitForSeconds(3);
        foreach (var item in GameObject.FindGameObjectsWithTag("Obstackle"))
        {
            foreach (var obs in item.transform.parent.GetComponentsInChildren<EdgeCollider2D>())
            {
                obs.enabled = true;
            }
        }
        SetPlayerOpaque();
    }
}
