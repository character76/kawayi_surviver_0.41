using UnityEngine;

public class DefEnemy : Enemy
{
    [Header("Setting")]
    [SerializeField]private float speed;
    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFre;
    private float attackDelay;
    private float attackTimer;

    [Header("Range Enemy Relate")]
    [SerializeField] private bool range_enemy;
    [SerializeField] private float rangePlayerDetectionRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        attackDelay = 1f / attackFre;

    }

    // Update is called once per frame
    void Update()
    {
        if (isentering)
        {
            //Debug.Log("quiting");
            return;
        }
        if(player_dave!=null)
        {
            FollowPlayer();
        }
            
        if (attackTimer >= attackDelay)
        {
            TryAttack();
            
        }
        else Wait();
        
        
    }
    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void FollowPlayer()
    {
        if(range_enemy)
        {
            float distance = (player_dave.transform.position - transform.position).magnitude;
            if(distance<rangePlayerDetectionRange)
            {
                return;
            }
        }
        Vector2 dir = (player_dave.transform.position - transform.position).normalized;
        
        //Debug.Log(dir);

        Vector2 targetpos = (Vector2)transform.position + speed * dir * Time.deltaTime;

        transform.position = targetpos;
    }
    

    

    private void OnDrawGizmos()
    {
        if(showGizmos)
        {
            if(!range_enemy)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, destroyRadius);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, rangePlayerDetectionRange);
            }
            
        }
       else
        {
            return;
        }
    }

    private void TryAttack()
    {
        if(!range_enemy)
        {
            float distance = (player_dave.transform.position - transform.position).magnitude;
            attackTimer = 0;
            if (distance < destroyRadius)
            {
                Attack();


            }
        }
        else
        {
            float distance = (player_dave.transform.position - transform.position).magnitude;
            if(distance<= rangePlayerDetectionRange)
            {
                Attack();
            }
        }
        
        
        
    }
    private void Attack()
    {
        //attackTimer = 0;
        if(!range_enemy)
        {
            player_dave.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Shooot");

        }
        
    }

    

}
