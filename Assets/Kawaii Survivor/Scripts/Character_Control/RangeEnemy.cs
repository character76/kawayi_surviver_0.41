using UnityEngine;
[RequireComponent(typeof(RangeEnemyAttack))]
public class RangeEnemy : Enemy
{
    [Header("Elements")]
    private RangeEnemyAttack Rattack;


    [Header("Setting")]
    [SerializeField] private float speed;
    [SerializeField] private float rangePlayerDetectionRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        Rattack = GetComponent<RangeEnemyAttack>();   
        Rattack.Configure(player_dave);

    }
    void Update()
    {
        if (isentering)
        {
            //Debug.Log("quiting");
            return;
        }

        ManageAttack();
        transform.localScale = player_dave.transform.position.x > transform.position.x ? Vector3.one : Vector3.one.With(x:-1);
    }
    private void ManageAttack()
    {
        float distance = (player_dave.transform.position - transform.position).magnitude;
        if (distance > rangePlayerDetectionRange)
        {
            FollowPlayer();

        }
        else
        {
            TryAttack();
        }
    }
    
    private void FollowPlayer()
    {
        
        Vector2 dir = (player_dave.transform.position - transform.position).normalized;
        Vector2 targetpos = (Vector2)transform.position + speed * dir * Time.deltaTime;

        transform.position = targetpos;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, rangePlayerDetectionRange);
        }
        else
        {
            return;
        }
    }
    private void TryAttack()
    {
        Rattack.AutoAim();
    }
    private void Attack()
    {
        Debug.Log("Shooot");
    }
}


