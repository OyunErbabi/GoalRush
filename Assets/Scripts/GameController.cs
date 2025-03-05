using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public bool GameOver = false;

    public GameObject Area;

    int GameTimer = 0;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI LeftScoreText;
    public TextMeshProUGUI RightScoreText;
    public TextMeshProUGUI LeftTeamNameText;
    public TextMeshProUGUI RightTeamNameText;

    int ScoreLeft = 0;
    int ScoreRight = 0;

    public GameObject LeftBall;
    public GameObject RightBall;

    public GameObject BallPrefab;


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
        GoalPoints = new List<GameObject>();

        ChangeBallSimulation(false);

        //StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartGame();
            //StartCoroutine(StartTextAnimator.Instance.StartTextAnimation());
        }
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
        GameOver = false;
        MatchData matchData = MatchCreator.Instance.MatchData;

        DestroyBalls();
        CreateBalls();

        Area.transform.rotation = Quaternion.Euler(0, 0, 90);
        GameTimer = 90;
        TimerText.text = GameTimer.ToString();
        ScoreLeft = 0;
        ScoreRight = 0;
        LeftScoreText.text = ScoreLeft.ToString();
        RightScoreText.text = ScoreRight.ToString();
        LeftTeamNameText.text = matchData.Team1.Name;
        RightTeamNameText.text = matchData.Team2.Name;
        StartCoroutine(GameTime(matchData.speed));
    }   
    
    IEnumerator GameTime(int speed)
    {   
        yield return StartCoroutine(StartTextAnimator.Instance.StartTextAnimation());

        float _speed = 1f / speed;
        Debug.Log("Speed : " + _speed);

        ChangeBallSimulation(true);
        AreaController.Instance.rotate = true;

        yield return null;
        while (GameTimer > 0)
        {
            yield return new WaitForSeconds(_speed);
            GameTimer--;
            TimerText.text = GameTimer.ToString();
            //if (GameTimer <= 45)
            //{
            //    AreaController.Instance.reverse = true;
            //}
        }        
     
        StopGame();
    }

    public void StopGame()
    {
        GameOver = true;
        LeftBall.GetComponent<Rigidbody2D>().simulated = false;
        RightBall.GetComponent<Rigidbody2D>().simulated = false;

        AreaController.Instance.rotate = false;
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

}
