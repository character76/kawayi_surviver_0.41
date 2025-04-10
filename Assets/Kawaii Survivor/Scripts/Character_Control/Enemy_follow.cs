using UnityEngine;

public class Enemy_follow : MonoBehaviour
{
    [Header("Elements")] 
    private Player player_dave;
    private bool isentering = false;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Collider2D colliders;
   
    

    [Header("Setting")]
    [SerializeField]private float speed;
    [SerializeField] private float destroyRadius;
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Effect")]
    [SerializeField] private GameObject particleeffect;
    [SerializeField] private GameObject entranceeffect;

    [Header("DEBUG")]
    [SerializeField] private bool showGizmos;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFre;
    private float attackDelay;
    private float attackTimer;

    [Header("Range Enemy Relate")]
    [SerializeField] private bool range_enemy;
    [SerializeField] private float rangePlayerDetectionRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        player_dave = FindFirstObjectByType<Player>(); 

        if(player_dave == null)
        {
            Debug.LogWarning("noplayer found destroy");
            Destroy(gameObject);
        }

        attackDelay = 1f / attackFre;

        //HIde the renderer
        //SHow spawn indicator

        //Scale spawn indi to show
        //then show enemy and hide spawn 
        

        if(entranceeffect!=null)
        {
            isentering = true;
            enemy.SetActive(false); // 初始隐藏角色
            entranceeffect.SetActive(true); // 显示出场标识
            Vector3 targetScale = entranceeffect.transform.localScale * 1.2f; 
            LeanTween.scale(entranceeffect, targetScale, .3f)
                .setLoopPingPong(4)
                .setOnComplete(SpawnSequenceComplete);
        }

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
    private void DestroyEnemy()
    {
        float distance = (player_dave.transform.position - transform.position).magnitude;
        //Debug.Log(player_dave.transform.position - transform.position);
        if (distance<destroyRadius)
        {
            PlayEffect();
            Destroy(gameObject);
        }
    }

    public void PlayEffect()
    {
        if(particleeffect!=null)
        {
            particleeffect.transform.SetParent(null);
            GameObject effect = Instantiate(particleeffect,transform.position, Quaternion.identity);

            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ps.Play();
                Destroy(effect, ps.main.duration); // 等粒子播放完再销毁
            }
        }

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

    private void SpawnSequenceComplete()
    {
        enemy.SetActive(true); // 初始隐藏角色
        entranceeffect.SetActive(false); // 显示出场标识
        colliders.enabled=true;
        isentering = false;
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
