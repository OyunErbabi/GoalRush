using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool GameOver = false;
    public bool isGameStarted = false;

    public GameObject Area;

    int GameTimer = 0;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI LeftScoreText;
    public TextMeshProUGUI RightScoreText;
    public TextMeshProUGUI LeftTeamNameText;
    public TextMeshProUGUI RightTeamNameText;

    public int ScoreLeft = 0;
    public int ScoreRight = 0;

    public GameObject LeftBall;
    public GameObject RightBall;
    public GameObject BallPrefab;

    public GameObject LeftTeamLogo;
    public GameObject RightTeamLogo;

    public GameObject LeftTeamUpperIcon;
    public GameObject RightTeamUpperIcon;

    public GameObject LeftTeamUpperMainColor;
    public GameObject LeftTeamUpperSecondaryColor;
    public GameObject RightTeamUpperMainColor;
    public GameObject RightTeamUpperSecondaryColor;

    bool forceStop = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<GameObject> GoalPoints;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        var refreshRate = Screen.currentResolution.refreshRateRatio.value;
        int refreshRateInt = (int)refreshRate;        
        Application.targetFrameRate = Mathf.RoundToInt(refreshRateInt);

        GoalPoints = new List<GameObject>();

        ChangeBallSimulation(false);

        //StartGame();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    StartGame();
        //    //StartCoroutine(StartTextAnimator.Instance.StartTextAnimation());
        //}
    }

    public void AddGoalPoint(GameObject goalPoint)
    {
        GoalPoints.Add(goalPoint);
    }

    public void RemoveGoalPoints(GameObject GoalPoint)
    {
        GoalPoints.Remove(GoalPoint);
        GoalPoints.Clear();
    }

    public void Goal(GameObject go, BallSide side) 
    {
        switch (side)
        {
            case BallSide.Left:
                ScoreLeft++;
                LeftScoreText.text = ScoreLeft.ToString();
                break;
            case BallSide.Right:
                ScoreRight++;
                RightScoreText.text = ScoreRight.ToString();
                break;
        }
        
        StartCoroutine(GoalCoroutine(go));
    }

    IEnumerator GoalCoroutine(GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
        go.GetComponent<BallController>().ResetBall();
    }
    
    public void StartGame()
    {
        isGameStarted = true;
        GameOver = false;
        MatchData matchData = MatchCreator.Instance.MatchData;

        DestroyBalls();
        CreateBalls();        

        Area.transform.rotation = Quaternion.Euler(0, 0, 90);
        GameTimer = 90;
        
        ScoreLeft = 0;
        ScoreRight = 0;
        SetTeamNamesOnMainScreen();
        GetTeamIcons();
        forceStop = false;
        StartCoroutine(GameTime(matchData.speed));
    }   
    
    IEnumerator GameTime(int speed)
    {   
        OptionsButtonController.Instance.HideOptionsButton();
        yield return StartCoroutine(StartTextAnimator.Instance.StartTextAnimation());

        float _speed = 1f / speed;
        //Debug.Log("Speed : " + _speed);

        ChangeBallSimulation(true);
        AreaController.Instance.rotate = true;

        yield return null;
        while (GameTimer > 0)
        {
            if (forceStop)
            {
                break;
            }

            yield return new WaitForSeconds(_speed);
            if (GameTimer > 0)
            {
                GameTimer--;
            }
            TimerText.text = GameTimer.ToString();
            //if (GameTimer <= 45)
            //{
            //    AreaController.Instance.reverse = true;
            //}

            if (GameTimer==3)
            {
                SoundManager.Instance.PlayStopSound();
            }

        }

        

        if (!forceStop)
        {
            StopGame();
            GameOverController.Instance.GameOver();
        }

        forceStop = false;
    }

    public void StopGame()
    {
        GameOver = true;

        if(LeftBall != null)
        {
            LeftBall.GetComponent<Rigidbody2D>().simulated = false;
        }

        if(RightBall != null)
        {
            RightBall.GetComponent<Rigidbody2D>().simulated = false;
        }

        AreaController.Instance.rotate = false;
        isGameStarted = false;
        OptionsButtonController.Instance.ShowOptionsButton();
    }

    public void ChangeBallSimulation(bool state)
    {
        if(LeftBall == null || RightBall == null)
        {
            return;
        }

        LeftBall.GetComponent<Rigidbody2D>().simulated = state;
        RightBall.GetComponent<Rigidbody2D>().simulated = state;
    }


    public void CreateBalls()
    {
        LeftBall = Instantiate(BallPrefab,Vector3.left,Quaternion.identity);
        LeftBall.GetComponent<BallController>().ballSide = BallSide.Left;
        LeftBall.name = "LeftBall";
        LeftBall.GetComponent<Rigidbody2D>().simulated = false;
        LeftBall.GetComponent<BallController>().MainColor = MatchCreator.Instance.MatchData.Team1.MainColor;
        LeftBall.GetComponent<BallController>().SecondaryColor = MatchCreator.Instance.MatchData.Team1.SecondaryColor;
        LeftBall.GetComponent<BallController>().ApplyColors();

        RightBall = Instantiate(BallPrefab, Vector3.right, Quaternion.identity);
        RightBall.GetComponent<BallController>().ballSide = BallSide.Right;
        RightBall.name = "RightBall";
        RightBall.GetComponent<Rigidbody2D>().simulated = false;
        RightBall.GetComponent<BallController>().MainColor = MatchCreator.Instance.MatchData.Team2.MainColor;
        RightBall.GetComponent<BallController>().SecondaryColor = MatchCreator.Instance.MatchData.Team2.SecondaryColor;
        RightBall.GetComponent<BallController>().ApplyColors();
    }

    public void GetTeamIcons()
    {
        if(OptionsController.Instance.LeftTeamIconAdded)
        {
            LeftTeamLogo.SetActive(true);
            LeftTeamUpperIcon.SetActive(false);
            LeftTeamLogo.GetComponent<SpriteRenderer>().sprite = OptionsController.Instance.LeftTeamIcon;        
        }
        else
        {
            LeftTeamLogo.SetActive(false);
            LeftTeamUpperIcon.SetActive(true);
            LeftTeamUpperMainColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team1.MainColor;
            LeftTeamUpperSecondaryColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team1.SecondaryColor;
        }

        if (OptionsController.Instance.RightTeamIconAdded)
        {
            RightTeamLogo.SetActive(true);
            RightTeamUpperIcon.SetActive(false);
            RightTeamLogo.GetComponent<SpriteRenderer>().sprite = OptionsController.Instance.RightTeamIcon;
        }
        else
        {
            RightTeamLogo.SetActive(false);
            RightTeamUpperIcon.SetActive(true);
            RightTeamUpperMainColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team2.MainColor;
            RightTeamUpperSecondaryColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team2.SecondaryColor;

        }
    }

    public void DestroyBalls()
    {
        Destroy(LeftBall);
        Destroy(RightBall);
    }

    public void PlayButton()
    {
        StartCoroutine(PlayCor());
    }

    IEnumerator PlayCor()
    {
        yield return StartCoroutine(WindowController.Instance.HidePlayButtonCor());
        StartGame();
    }

    public void ResetMatchScreen()
    {
        forceStop = true;
        StopGame();
        GameTimer = 90;
        WindowController.Instance.ShowPlayButton();
        DestroyBalls();

        isGameStarted = true;
        GameOver = false;
        MatchData matchData = MatchCreator.Instance.MatchData;
        AreaController.Instance.rotate = false;
        Area.transform.rotation = Quaternion.Euler(0, 0, 90);
        LeftScoreText.text = "0";
        RightScoreText.text = "0";
        TimerText.text = "90";

        WindowController.Instance.ShowPlayButton();

    }

    public void SetTeamNamesOnMainScreen()
    {
        MatchData matchData = MatchCreator.Instance.MatchData;

        TimerText.text = GameTimer.ToString();
        LeftScoreText.text = "0";
        RightScoreText.text = "0";
        LeftTeamNameText.text = matchData.Team1.Name;
        RightTeamNameText.text = matchData.Team2.Name;

        GetTeamIcons();
    }

}
