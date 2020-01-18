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
        currentGameArea = objectPooler.SpawnFromPool("GameArea", new Vector3(0, 150, 0));
        nextGameArea = objectPooler.SpawnFromPool("GameArea", new Vector3(0, 1920, 0));

    }
    public void GameOver() {
        pSystem.SetActive(false);
        gameoverUI.SetActive(true);

        Time.timeScale = 0;
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
            if (currentGameArea.GetComponent<RectTransform>().localPosition.y < -1920)
            {
                currentGameArea = nextGameArea;
                nextGameArea = objectPooler.SpawnFromPool("GameArea", new Vector3(0, 1920, 0));
            }
            currentGameArea.transform.Translate(Vector3.down * gameSpeed / 100);
            nextGameArea.transform.Translate(Vector3.down * gameSpeed / 100);
            scoreText.text = "" + (int)score;
        }

    }
    private void OnMouseDown()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pSystem.GetComponent<RectTransform>().position = new Vector3(mousePosition.x, mousePosition.y, 0);
        pSystem.GetComponent<ParticleSystem>().Play();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().rb2d.gravityScale=1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SlimeMovement(mousePosition, touchCenter.transform.position);
        startFlag = true;
    }
}
