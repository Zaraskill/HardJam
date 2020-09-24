using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public static FinalScore instance;

    public GameObject panelScoreFinal;

    public Text player1Score;
    public Text player2Score;

    public Image player1Win;
    public Image player2Win;
    public Image egalWin;

    public PlayersVar players;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (panelScoreFinal.activeInHierarchy)
            panelScoreFinal.SetActive(false);
    }

    public void StopGameplayAndShowFinalScore()
    {
        GameManager.instance.state = EnumStateScene.FinalScore;
        panelScoreFinal.SetActive(true);
        if (players.player1.score < 10)
        {
            player1Score.text = "0" + players.player1.score;
        }
        else
        {

            player1Score.text = players.player1.score.ToString();
        }

        if (players.player2.score < 10)
        {
            player2Score.text = "0" + players.player2.score;
        }
        else
        {

            player2Score.text = players.player2.score.ToString();
        }


        if (players.player1.score > players.player2.score)
        {
            player1Win.gameObject.SetActive(true);
            player2Win.gameObject.SetActive(false);
            //egalWin.gameObject.SetActive(false);
        }
        else if (players.player1.score < players.player2.score)
        {
            player1Win.gameObject.SetActive(false);
            player2Win.gameObject.SetActive(true);
            //egalWin.gameObject.SetActive(false);
        }
        else
        {
            player1Win.gameObject.SetActive(false);
            player2Win.gameObject.SetActive(false);
            //egalWin.gameObject.SetActive(true);
        }
    }
}
