using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    GameObject touchCenter;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject gameoverUI;
    [SerializeField]
    private GameObject pSystem;
    private ObjectPooler objectPooler;
    private int gameSpeed = 2;
    private float score=1;
    private GameObject currentGameArea, nextGameArea;
    public static GameLoop Instance;
    public bool startFlag = false;
    public void PauseGame()
    {
        Time.timeScale = 0;

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
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

    }
    public void GameOver() {
        PauseGame();
        pSystem.SetActive(false);
        gameoverUI.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (score % 100 == 0)
        {
            gameSpeed+=2;
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
    public void TouchtoTouchPad()
    {

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tempPosition = mousePosition;
        tempPosition.z = -1f;
        Debug.Log(tempPosition-touchCenter.transform.localPosition);
        pSystem.transform.position =tempPosition;
        pSystem.GetComponent<ParticleSystem>().Play();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rb2d.gravityScale=2;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SlimeMovement(mousePosition, touchCenter.transform.localPosition);
        startFlag = true;
    }
}
