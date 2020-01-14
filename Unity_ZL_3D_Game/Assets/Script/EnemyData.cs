﻿using UnityEngine;

[CreateAssetMenu(fileName = "怪物資料", menuName = "Jimmy/怪物資料")]
public class EnemyData : ScriptableObject
{
    [Header("移動速度"), Range(0, 10)]
    public float speed;
    [Header("血量"), Range(10, 5000)]
    public float hp;
    [Header("攻擊力"), Range(10, 1000)]
    public float attack;
    [Header("冷卻時間"), Range(1, 10)]
    public float cd;
    [Header("停止距離"), Range(0.5f, 100)]
    public float stopDistance;
}
