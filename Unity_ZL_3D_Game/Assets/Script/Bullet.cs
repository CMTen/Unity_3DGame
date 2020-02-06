using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子彈的傷害值
    /// </summary>
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "機器人")
        {
            other.GetComponent<Player>().Hit(damage);
        }      
    }
}
