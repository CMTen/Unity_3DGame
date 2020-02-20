using UnityEngine;

public class Item : MonoBehaviour
{
    /// <summary>
    /// 是否過關，過關前往玩家位置
    /// </summary>
    [HideInInspector]
    public bool pass;

    [Header("道具音效")]
    public AudioClip sound;

    private Transform player;
    private AudioSource aud;

    private void Start()
    {
        Physics.IgnoreLayerCollision(10, 10, false);

        aud = GetComponent<AudioSource>();
        player = GameObject.Find("機器人").transform;

        HandleCollision();
    }

    private void Update()
    {
        GoToPlayer();
    }

    /// <summary>
    /// 控制忽略碰撞
    /// </summary>
    private void HandleCollision()
    {
        Physics.IgnoreLayerCollision(10, 8);
        Physics.IgnoreLayerCollision(10, 9);
    }

    /// <summary>
    /// 前往玩家位置
    /// </summary>
    private void GoToPlayer()
    {
        if (pass)
        {
            Physics.IgnoreLayerCollision(10, 10);
            transform.position = Vector3.Lerp(transform.position, player.position, 0.5f * Time.deltaTime * 30);

            if (Vector3.Distance(transform.position, player.position) < 1.5f && !aud.isPlaying);
            {
                aud.PlayOneShot(sound, 0.3f);
                Destroy(gameObject, 0.3f);
            }
        }
    }
}
