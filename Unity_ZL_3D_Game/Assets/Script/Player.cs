using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;
    [Header("玩家資料")]
    public PlayerData data;

    private Rigidbody rig;
    private Joystick js;
    private Animator ani;
    private Transform target;
    private LevelMananger levelManager;
    private HpValueManager hpvaluemanager;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        js = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();

        target = GameObject.Find("目標").transform;

        levelManager = FindObjectOfType<LevelMananger>();  // 透過類型尋找物件 ( 場景上只有一個 )
        hpvaluemanager = GetComponentInChildren<HpValueManager>();
    }

    // 固定更新：一秒執行 50 次 - 處理物理行為
    private void FixedUpdate()
    {
        move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "傳送區域")
        {
            StartCoroutine(levelManager.NextLevel());
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
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

        if (v == 0 && h == 0) Attack();
    }

    public void Hit(float damage)
    {
        if (ani.GetBool("死亡開關")) return;
        data.hp -= damage;
        hpvaluemanager.SetHp(data.hp, data.maxHp);
        StartCoroutine(hpvaluemanager.ShowValue(damage, "-", Color.white));
        if (data.hp <= 0) Dead();
    }

    private void Dead()
    {
        ani.SetBool("死亡開關", true);
        enabled = false;

        StartCoroutine(levelManager.ShowRevival());
    }

    public void Revival()
    {
        ani.SetBool("死亡開關", false);
        enabled = true;
        data.hp = data.maxHp;
        hpvaluemanager.SetHp(data.hp, data.maxHp);
        levelManager.HideRevival();
    }

    private void Attack()
    {
        ani.SetTrigger("攻擊觸發");
    }
}
