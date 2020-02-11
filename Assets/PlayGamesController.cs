using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGamesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        AuthenticateUser(); 
    }

    private void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {

            }
            else {
                Application.Quit();
            }



        });
    }
    public static void PostToLeaderboard(long newScore) {
        Social.ReportScore(newScore, GPGSIds.leaderboard_high_score, (bool success) =>
        {
             if (success) { 
             
             }
        });
    }
    public static void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_score);
    }


   
}
