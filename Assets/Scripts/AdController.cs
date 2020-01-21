using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
    private readonly string playstoreID = "3434518";
    private readonly string appstoreID = "3434519";
    private readonly string rewardedVideo_AD = "rewardedVideo";
    private readonly string video_AD = "video";
    private static int counter = 0;
    public static AdController Instance;
    //if test your ad set true otherwise set false
    public bool isTestAd;
    #region SINGLETON
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    private void Start()
    {
        InitializeMonetization();
    }
    private void InitializeMonetization()
    {
        Monetization.Initialize(playstoreID, isTestAd);
        return;
    }
    public void PlayVideoAD()
    {
        counter++;
        Debug.Log(counter);
        if (!Monetization.IsReady(video_AD)) { return; }
        ShowAdPlacementContent videoAD = Monetization.GetPlacementContent(video_AD) as ShowAdPlacementContent;
        if (videoAD == null||counter%6!=5) { return; }
        videoAD.Show();
    }
    public void PlayRewardedVideoAD()
    {
        if (!Monetization.IsReady(rewardedVideo_AD)) { return; }
        ShowAdPlacementContent rewardedVideoAD = Monetization.GetPlacementContent(rewardedVideo_AD) as ShowAdPlacementContent;
        if (rewardedVideoAD == null) { return; }
        rewardedVideoAD.Show(HandleShowResult);
    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isGetLifeBefore)
                {
                    GameManagement.Instance.ContinueGame();
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isGetLifeBefore = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 0;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Jewel").GetComponent<Jewel>().Increase(1);
                }
                break;
            case ShowResult.Skipped:

                break;
            case ShowResult.Failed:

                break;
        }

    }

}
