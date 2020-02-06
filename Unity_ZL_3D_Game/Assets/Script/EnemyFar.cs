using UnityEngine;
using System.Collections;

public class EnemyFar : Enemy
{
    [Header("子彈")]
    public GameObject bullet;

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateBullet());
    }

    /// <summary>
    /// 產生子彈
    /// </summary>
    private IEnumerator CreateBullet()
    {
        yield return new WaitForSeconds(data.attackDelay);
        posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
        GameObject temp = Instantiate(bullet, posBullet, transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.BulletSpeed);

        temp.AddComponent<Bullet>();
        temp.GetComponent<Bullet>().damage = data.attack;
    }

    private Vector3 posBullet;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
        Gizmos.DrawSphere(posBullet, 0.1f);
    }
}
