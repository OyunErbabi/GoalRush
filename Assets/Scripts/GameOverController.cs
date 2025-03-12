using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public static GameOverController Instance;
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

    public GameObject GameOverPanel;

    public TextMeshProUGUI GameEndText;
    public TextMeshProUGUI DrawScoreText;
    public TextMeshProUGUI LeftTeamScore;
    public TextMeshProUGUI RightTeamScore;
    public GameObject Ball;
    public GameObject BallMainColor;
    public GameObject BallSecondColor;

    public void GameOver()
    {   
        StartCoroutine(GameOverCor());
    }

    IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(1.5f);
        GenerateGameOverPanel();
        GameOverPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        AdManager.Instance.ShowInterstitialAd();
    }

    public void GenerateGameOverPanel()
    {
        if(GameController.Instance.ScoreLeft == GameController.Instance.ScoreRight)
        {
            GameEndText.text = "DRAW";
            LeftTeamScore.text = "";
            RightTeamScore.text = "";
            DrawScoreText.text = string.Format("{0} - {1}", GameController.Instance.ScoreLeft, GameController.Instance.ScoreRight);
            Ball.SetActive(false);
        }
        else
        {
            GameEndText.text = "WINNER";
            LeftTeamScore.text = GameController.Instance.ScoreLeft.ToString();
            RightTeamScore.text = GameController.Instance.ScoreRight.ToString();
            DrawScoreText.text = "";
            Ball.SetActive(true);

            if(GameController.Instance.ScoreLeft > GameController.Instance.ScoreRight)
            {
                BallMainColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team1.MainColor;
                BallSecondColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team1.SecondaryColor;
            }
            else
            {
                BallMainColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team2.MainColor;
                BallSecondColor.GetComponent<Image>().color = MatchCreator.Instance.MatchData.Team2.SecondaryColor;
            }

        }
    }

    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        GameOverPanel.SetActive(false);
        GameController.Instance.ResetMatchScreen();
        GameController.Instance.SetTeamNamesOnMainScreen();
    }

}
