using UnityEngine;

public class DeBoink : Weapon
{

    //[SerializeField] private Transform Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;

        }
        //Attack();
        //Debug.Log("Closest Enemy" + cloestIndex + " dis " + minDis);
        
    }
    
    
    private void AutoAim()
    {
        Enemy closest_Enemy = GetClosest();
        Vector3 targetUp = Vector3.up;
        if(closest_Enemy!= null)
        {
            targetUp = (closest_Enemy.transform.position - transform.position).normalized;
            transform.up = targetUp;
        }
        else
        {
            //Debug.Log("no close");
        }
        
        transform.up = Vector3.Lerp(transform.up, targetUp, Time.deltaTime * aimLerp);
        if(targetUp!=Vector3.up)
        {
            if (GlobalSettings.isAutoAttackOn)
            {
                MangeAttackTimer();
            }
        }
        IncrementAttackTimer();
    }

    private void MangeAttackTimer()
    {
        IncrementAttackTimer();
        if (attackTimer>=attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }
    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(Hitpoint.position,Hitbox.bounds.size,Hitpoint.localEulerAngles.z,enemyMask);

        for (int i=0;i<enemies.Length;i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            if (!damagesEnemy.Contains(enemy) )
            {
                enemies[i].GetComponent<Enemy_Health>().TakeDamage(damage);
                damagesEnemy.Add(enemies[i].GetComponent<Enemy>());
                Debug.Log("Attack" + i);
            }
            
            
        }
    }
    public void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;
        damagesEnemy.Clear();
        animator.speed = 1f / attackDelay;

    }
    private void Attacking()
    {
        Attack();

    }
    private void StopAttack()
    {
        state = State.Idle;
        damagesEnemy.Clear();
    }


}
