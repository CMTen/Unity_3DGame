using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;
    [Header("玩家資料")]
    public PlayerData data;
    [Header("子彈")]
    public GameObject bullet;

    private Rigidbody rig;
    private Joystick js;
    private Animator ani;
    private Transform target;
    private LevelMananger levelManager;
    private HpValueManager hpvaluemanager;
    private Vector3 posBullet;
    private float timer;
    private Enemy[] enemys;
    private float[] enemysDis;

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
        if (timer < data.cd)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // 取得所有敵人
            enemys = FindObjectsOfType<Enemy>();

            // 過關
            if (enemys.Length == 0)
            {
                levelManager.Pass();
                return;
            }

            timer = 0;
            ani.SetTrigger("攻擊觸發");

            // 取得所有敵人位置
            enemysDis = new float[enemys.Length];
            for (int i = 0; i < enemys.Length; i++)
            {
                enemysDis[i] = Vector3.Distance(transform.position, enemys[i].transform.position);
            }

            float min = enemysDis.Min();
            int index = enemysDis.ToList().IndexOf(min);
            Vector3 enemyPos = enemys[index].transform.position;
            enemyPos.y = transform.position.y;
            transform.LookAt(enemyPos);

            posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
            Vector3 angle = transform.eulerAngles;
            Quaternion qua = Quaternion.Euler(angle.x + 90, angle.y, angle.z);
            GameObject temp = Instantiate(bullet, posBullet, qua);
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.BulletSpeed);
            temp.AddComponent<Bullet>();
            temp.GetComponent<Bullet>().damage = data.attack;
            temp.GetComponent<Bullet>().player = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
        Gizmos.DrawSphere(posBullet, 0.1f);
    }
}
