using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpValueManager : MonoBehaviour
{
    private Image hpBar;
    private Text hpText;
    private RectTransform hpRect;

    private void Start()
    {
        hpBar = transform.GetChild(2).GetComponent<Image>();
        hpText = transform.GetChild(3).GetComponent<Text>();
        hpRect = transform.GetChild(3).GetComponent<RectTransform>();
    }

    private void Update()
    {
        FixedAngle();
    }

    /// <summary>
    /// 固定角度
    /// </summary>
    private void FixedAngle()
    {
        transform.eulerAngles = new Vector3(-45, 0, 0);
    }

    /// <summary>
    /// 設定血量
    /// </summary>
    /// <param name="hpCurrent">目前血量</param>
    /// <param name="hpMax">最大血量</param>
    public void SetHp(float hpCurrent, float hpMax)
    {
        hpBar.fillAmount = hpCurrent / hpMax;
    }

    public IEnumerator ShowValue(float value, string mark, Color color)
    {
        hpText.text = mark + value;
        color.a = 0;
        hpText.color = color;
        hpRect.anchoredPosition = Vector2.up * 50;

        for(int i = 0; i < 20; i++)
        {
            hpText.color += new Color(0, 0, 0, 0.1f);  // 遞增透明度
            hpRect.anchoredPosition += Vector2.up * 1; 
            yield return new WaitForSeconds(0.01f);
        }

        hpText.color = new Color(0, 0, 0, 0);
    }
}
