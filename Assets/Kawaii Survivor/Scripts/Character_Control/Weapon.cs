using UnityEngine;
using System.Collections.Generic;
public abstract class Weapon : MonoBehaviour
{
    protected enum State
    {
        Idle,
        Attack
    }
    protected State state;
    [Header("Elements")]
    [SerializeField] protected Transform Hitpoint;
    [SerializeField] protected float Hit_range;
    protected List<Enemy> damagesEnemy = new List<Enemy>();
    [SerializeField] protected BoxCollider2D Hitbox;

    [Header("Settings")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected float aimLerp;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackDelay;
    protected float attackTimer = 0;

    [Header("Animation")]
    [SerializeField] protected Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
        if (Hitpoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Hitpoint.position, Hit_range);
    }
    protected Enemy GetClosest()
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
        return enemies[cloestIndex].GetComponent<Enemy>();
    }
    protected int GetDamage(out bool isCriticalhit)
    {
        isCriticalhit = false;
        if(Random.Range(0,101)<= 50)
        {
            isCriticalhit = true;
            return damage * 2;
        }
        return damage;
    }
    
}
