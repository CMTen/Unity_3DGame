using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;

    private Rigidbody rig;
    private Joystick js;
    private Animator ani;
    private Transform tra;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        tra = GetComponent<Transform>();
        js = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        float v = js.Vertical;
        float h = js.Horizontal;

        rig.AddForce(-h * speed, 0, -v * speed);
        ani.SetBool("跑步開關", h != 0 || v != 0);
   
    }
}
