using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerData dataPlayer;

    public void BuyHp_500()
    {
        dataPlayer.maxHp += 500;
        dataPlayer.hp = dataPlayer.maxHp;
    }

    public void BuyAtk_50()
    {
        dataPlayer.attack += 50;
    }

    public void LoadLevel()
    {
        dataPlayer.hp = dataPlayer.maxHp;
        SceneManager.LoadScene("關卡1");
    }
}
