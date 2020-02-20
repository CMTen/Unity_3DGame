using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string googleID = "3459582";
    private string placementRevivsl = "revival";
    private Player player;

    private void Start()
    {
        Advertisement.Initialize(googleID, false);
        Advertisement.AddListener(this);
        player = FindObjectOfType<Player>();
    }

    public void ShowADRevival()
    {
        if (Advertisement.IsReady(placementRevivsl))
        {
            Advertisement.Show(placementRevivsl);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == placementRevivsl)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    print("失敗");
                    break;
                case ShowResult.Skipped:
                    print("略過");
                    player.Revival();
                    break;
                case ShowResult.Finished:
                    print("完成");
                    GameObject.Find("機器人").GetComponent<Player>().Revival();
                    break;
            }
        }
    }
}
