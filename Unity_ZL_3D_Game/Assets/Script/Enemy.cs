using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("怪物資料")]
    public EnemyData data;

    private Animator ani;
    private NavMeshAgent nav;
    private Transform target;
    private float timer;
    private HpValueManager hpvaluemanager;

    private void Start()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        nav.speed = data.speed;
        nav.stoppingDistance = data.stopDistance;

        target = GameObject.Find("機器人").transform;
        hpvaluemanager = GetComponentInChildren<HpValueManager>();
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// 等待
    /// </summary>
    private void Wait()
    {
        ani.SetBool("跑步開關", false);
        timer += Time.deltaTime;

        if (timer > data.cd)
        {
            Attack();
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        Vector3 posTarget = target.position;
        posTarget.y = transform.position.y;
        transform.LookAt(posTarget);
        
        nav.SetDestination(target.position);

        // print("剩餘距離：" + nav.remainingDistance);

        if (nav.remainingDistance < data.stopDistance)
        {
            Wait();
        }
        else
        {
            ani.SetBool("跑步開關", true);
        }
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected virtual void Attack()
    {
        ani.SetTrigger("攻擊觸發");
        timer = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(float damage)
    {
        if (ani.GetBool("死亡開關")) return;
        data.hp -= damage;
        hpvaluemanager.SetHp(data.hp, data.maxHp);
        StartCoroutine(hpvaluemanager.ShowValue(damage, "-", Color.white));
        if (data.hp <= 0) Dead();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {

    }
}
