using UnityEngine;
using System.Collections;

public class EnemyNear : Enemy
{
    protected override void Attack()
    {
        base.Attack();

        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(data.attackDelay);

        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * data.attackY, transform.forward, out hit, data.attackLength))
        {
            hit.collider.GetComponent<Player>().Hit(data.attack);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * data.attackY, transform.forward * data.attackLength);
    }
}
