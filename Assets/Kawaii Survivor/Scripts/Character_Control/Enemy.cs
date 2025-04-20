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
            enemy.SetActive(false); // ��ʼ���ؽ�ɫ
            entranceeffect.SetActive(true); // ��ʾ������ʶ
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
        enemy.SetActive(true); // ��ʼ���ؽ�ɫ
        entranceeffect.SetActive(false); // ��ʾ������ʶ
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
                Destroy(effect, ps.main.duration); // �����Ӳ�����������
            }
        }

    }

}
