using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelMananger : MonoBehaviour
{
    [Header("隨機技能與光照")]
    public GameObject skill;           // 隨機技能 (遊戲物件)
    public GameObject objLight;        // 光照 (遊戲物件)
   
    public GameObject shin;

    private Animator aniDoor;          // 門 (動畫)
    private Image imgCross;
    private AdManager adManager;

    [Header("是否自動顯示技能")]
    public bool autoShowSkill;         // 是否顯示技能
    [Header("是否自動開門")]
    public bool autoOpenDoor;          // 是否自動開門
    [Header("復活畫面，看廣告復活")]
    public GameObject panelRevival;

    public Button btnRevival;


    private void Start()
    {
        // GameObject.Find("") 無法找到隱藏物件
        aniDoor = GameObject.Find("Door").GetComponent<Animator>();

        imgCross = GameObject.Find("轉場").GetComponent<Image>();

        // 如果 是 顯示技能 呼叫 顯示技能方法
        if (autoShowSkill) ShowSkill();

        // 如果 是 自動開門 延遲呼叫 開門方法
        if (autoOpenDoor) Invoke("OpenDoor", 5);

        // 延遲調用("方法名稱", 延遲時間)
        // Invoke("OpenDoor", 5);

        // 重複調用("方法名稱", 延遲時間, 重複頻率)
        // InvokeRepeating("OpenDoor", 0, 1.5f);

        adManager = FindObjectOfType<AdManager>();
        btnRevival.onClick.AddListener(adManager.ShowADRevival);
    }

    /// <summary>
    /// 顯示技能
    /// </summary>
    private void ShowSkill()
    {
        skill.SetActive(true);
    }

    /// <summary>
    /// 開門、光照
    /// </summary>
    private void OpenDoor()
    {
        aniDoor.SetTrigger("開門觸發");
        objLight.SetActive(true);
        shin.SetActive(true);
    }

    /// <summary>
    /// 載入下一關
    /// </summary>
    /// <returns></returns>
    public IEnumerator NextLevel()
    {
        print("載入下一關");

        for (int i = 0; i < 10; i++)
        {
            imgCross.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.2f);

        if (SceneManager.GetActiveScene().name.Contains("Boss"))
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++index);
        }
    }

    /// <summary>
    /// 顯示復活畫面
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowRevival()
    {
        panelRevival.SetActive(true);
        Text textSecond = panelRevival.transform.GetChild(1).GetComponent<Text>();

        for (int i = 10; i > 0; i--)
        {
            textSecond.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// 關閉復活畫面
    /// </summary>
    public void HideRevival()
    {
        StopCoroutine(ShowRevival());
        panelRevival.SetActive(false);
    }

    /// <summary>
    /// 過關：開門、金幣前往玩家
    /// </summary>
    public void Pass()
    {
        OpenDoor();

        Item[] items = FindObjectsOfType<Item>();

        for (int i = 0; i < items.Length; i++)
        {
            items[i].pass = true;
        }
    }
}
