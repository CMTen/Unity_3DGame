using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "Jimmy/玩家資料")]
public class PlayerData : ScriptableObject
{
    [Header("血量"), Range(200, 10000)]
    public float hp;

    public float maxHp;
}
