using UnityEngine;
using System.Collections.Generic;
public class DeBoink : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack
    }
    private State state;

    [Header("Elements")]
    [SerializeField] private Transform Hitpoint;
    [SerializeField] private float Hit_range;
    private List<Enemy_follow> damagesEnemy = new List<Enemy_follow>();
    [SerializeField] private BoxCollider2D Hitbox;

    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float aimLerp;
    [SerializeField] private int damage;
    [SerializeField] private float attackDelay;
    private float attackTimer=0;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    //[SerializeField] private Transform Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Idle;
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Hitpoint.position, Hit_range);
    }
    private Enemy_follow GetClosest()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        //Vector2 targetUp = Vector3.up;

        if (enemies.Length <= 0)
        {
            
            //Debug.LogWarning("No cloest");
            return null;
        }
        int cloestIndex = -1;
        float minDis = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            float disToenemy = 0;
            Collider2D enemy_checking = enemies[i].GetComponent<Collider2D>();
            
            disToenemy = Vector2.Distance(transform.position, enemy_checking.transform.position);
  

            if (disToenemy < minDis)
            {
                minDis = disToenemy;
                cloestIndex = i;
            }
            else continue;
        }
        if (cloestIndex == -1)
        {
            
            return null;
        }
        return enemies[cloestIndex].GetComponent<Enemy_follow>();
    }
    private void AutoAim()
    {
        Enemy_follow closest_Enemy = GetClosest();
        Vector3 targetUp = Vector3.up;
        if(closest_Enemy!= null)
        {   
            targetUp = (closest_Enemy.transform.position - transform.position).normalized;
            transform.up = targetUp;
        }
        else
        {

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
            Enemy_follow enemy = enemies[i].GetComponent<Enemy_follow>();
            if (!damagesEnemy.Contains(enemy) )
            {
                enemies[i].GetComponent<Enemy_Health>().TakeDamage(damage);
                damagesEnemy.Add(enemies[i].GetComponent<Enemy_follow>());
                Debug.Log("Attack" + i);
            }
            
            
        }
    }
    [NaughtyAttributes.Button]
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
