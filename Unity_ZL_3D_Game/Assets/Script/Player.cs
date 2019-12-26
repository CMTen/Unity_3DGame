using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;

    private Rigidbody rig;
    private Joystick js;
    private Animator ani;
    private Transform target;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        js = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();
        target = GameObject.Find("目標").transform;
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        float v = -js.Vertical;
        float h = -js.Horizontal;

        rig.AddForce(h * speed, 0, v * speed);
        ani.SetBool("跑步開關", h != 0 || v != 0);

        Vector3 pos = transform.position;
        target.position = new Vector3(pos.x + h, 0.01f, pos.z + v);

        //transform.LookAt(target);  // 這會吃土

        Vector3 posTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(posTarget);

    }
}
