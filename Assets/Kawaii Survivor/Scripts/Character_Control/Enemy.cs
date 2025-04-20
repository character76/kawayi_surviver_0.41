using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected bool isentering = false;
    [Header("Elements")]
    protected Player player_dave;

    [SerializeField] protected GameObject enemy;
    [SerializeField] protected Collider2D colliders;
    [Header("Setting")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float destroyRadius;
    protected int health;
    [Header("Effect")]
    [SerializeField] protected GameObject particleeffect;
    [SerializeField] protected GameObject entranceeffect;
    [Header("DEBUG")]
    [SerializeField] protected bool showGizmos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        health = maxHealth;
        player_dave = FindFirstObjectByType<Player>();
        if (player_dave == null)
        {
            Debug.LogWarning("noplayer found destroy");
            Destroy(gameObject);
        }

        SpawnSequence();
    }
    void SpawnSequence()
    {
        if (entranceeffect != null)
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
        
    }
    private void SpawnSequenceComplete()
    {
        enemy.SetActive(true); // 初始隐藏角色
        entranceeffect.SetActive(false); // 显示出场标识
        colliders.enabled = true;
        isentering = false;
    }
    private void DestroyEnemy()
    {
        float distance = (player_dave.transform.position - transform.position).magnitude;
        //Debug.Log(player_dave.transform.position - transform.position);
        if (distance < destroyRadius)
        {
            PlayEffect();
            Destroy(gameObject);
        }
    }
    public void PlayEffect()
    {
        if (particleeffect != null)
        {
            particleeffect.transform.SetParent(null);
            GameObject effect = Instantiate(particleeffect, transform.position, Quaternion.identity);

            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ps.Play();
                Destroy(effect, ps.main.duration); // 等粒子播放完再销毁
            }
        }

    }

}
