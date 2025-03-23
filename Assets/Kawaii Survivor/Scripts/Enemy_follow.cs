using UnityEngine;

public class Enemy_follow : MonoBehaviour
{
    [Header("Elements")] 
    private player player_dave;
    private bool isentering = false;
    [SerializeField] private GameObject enemy;

    [Header("setting")]
    [SerializeField]private float speed;
    [SerializeField] private float destroyRadius;

    [Header("Effect")]
    [SerializeField] private GameObject particleeffect;
    [SerializeField] private GameObject entranceeffect;

    [Header("DEBUG")]
    [SerializeField] private bool showGizmos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_dave = FindFirstObjectByType<player>(); 

        if(player_dave == null)
        {
            Debug.LogWarning("noplayer found destroy");
            Destroy(gameObject);
        }

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
            Debug.Log("quiting");
            return;
        }
            
        
        FollowPlayer();

        TryAttack();
        
        

    }

    private void FollowPlayer()
    {
        Vector2 dir = (player_dave.transform.position - transform.position).normalized;
        
        //Debug.Log(dir);

        Vector2 targetpos = (Vector2)transform.position + speed * dir * Time.deltaTime;

        transform.position = targetpos;
    }
    private void TryAttack()
    {
        float distance = (player_dave.transform.position - transform.position).magnitude;
        //Debug.Log(player_dave.transform.position - transform.position);
        if (distance<destroyRadius)
        {
            PlayEffect();
            Destroy(gameObject);
        }
    }

    private void PlayEffect()
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, destroyRadius);
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
        isentering = false;
    }


}
