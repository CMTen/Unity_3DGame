﻿using UnityEngine;

public class LevelMananger : MonoBehaviour
{
    public GameObject skill;           // 隨機技能 (遊戲物件)
    public GameObject objLight;        // 光照 (遊戲物件)
    public GameObject shin;

    public Animator aniDoor;          // 門 (動畫)

    [Header("是否自動顯示技能")]
    public bool autoShowSkill;         // 是否顯示技能
    [Header("是否自動開門")]
    public bool autoOpenDoor;          // 是否自動開門

    private void Start()
    {
        // GameObject.Find("") 無法找到隱藏物件
        aniDoor = GameObject.Find("Door").GetComponent<Animator>();

        // 如果 是 顯示技能 呼叫 顯示技能方法
        if (autoShowSkill) ShowSkill();

        // 如果 是 自動開門 延遲呼叫 開門方法
        if (autoOpenDoor) Invoke("OpenDoor", 5);

        // 延遲調用("方法名稱", 延遲時間)
        // Invoke("OpenDoor", 5);

        // 重複調用("方法名稱", 延遲時間, 重複頻率)
        // InvokeRepeating("OpenDoor", 0, 1.5f);
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
}
