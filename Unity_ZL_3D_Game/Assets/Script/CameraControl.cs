using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("速度"), Range(0, 100)]
    public float speed = 5.5f;
    [Header("鏡頭上限")]
    public float top;
    [Header("鏡頭下限")]
    public float bottom;

    private Transform player;

    private void Start()
    {
        player = GameObject.Find("機器人").transform;
    }

    private void LateUpdate()
    {
        Track();
    }

    /// <summary>
    /// 攝影機追蹤效果
    /// </summary>
    private void Track()
    {
        Vector3 posP = player.position;
        Vector3 posC = transform.position;

        posP.x = 0;
        posP.y = 20f;
        posP.z += 14.5f;

        posP.z = Mathf.Clamp(posP.z, top, bottom);

        transform.position = Vector3.Lerp(posC, posP, 0.3f * Time.deltaTime * speed);
    }
}
