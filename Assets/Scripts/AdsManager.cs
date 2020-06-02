using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId;
   
    private string placementId;
    

    private bool adsAreReady;

    private void Start()
    {
        gameId = "3619442";
      
        placementId = "rewardedVideo";

        // WAIT FOR ADS
        Advertisement.AddListener(this);

        Advertisement.Initialize(gameId, true);
    }

    public void ShowAd()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Give internet connectivity feedback here

            return;
        }

        if (!adsAreReady)
        {
            // Give not ready yet feedback

            return;
        }

        ShowOptions options = new ShowOptions();
        Advertisement.Show(placementId, options);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        switch (showResult)
        {
            case ShowResult.Finished:
               
                Debug.Log("Ok");
                GameManager.Instance.player.AddGems(100);
                UIManager.Instance.OpenShop(GameManager.Instance.player.diamonds);
                break;

            case ShowResult.Skipped:
                // Do not reward the user for skipping the ad.
                break;

            case ShowResult.Failed:
                Debug.LogWarning("The ad did not finish due to an error.");
                break;
        }
    }

    public void OnUnityAdsReady(string myPlacementId)
    {
        if (placementId != myPlacementId) return;

        adsAreReady = true;
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}