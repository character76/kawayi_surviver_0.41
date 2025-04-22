using UnityEngine;
using UnityEngine.Pool;
public class DamageEffectsMana : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Damage_text damageTextPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Pooling")]
    private ObjectPool<Damage_text> damagePool;
    private void Awake()
    {
        Enemy_Health.onDamageTaken += EnemyHitCallBack;
    }
    private void OnDestroy()
    {
        Enemy_Health.onDamageTaken -= EnemyHitCallBack;
    }
    void Start()
    {
        damagePool = new ObjectPool<Damage_text>(CreatFunction,ActionOnGet,ActionOnRelease,ActionOnDestroy);
    }

    private Damage_text CreatFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(Damage_text damage_Text)
    {
        damage_Text.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Damage_text damage_Text)
    {
        damage_Text.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(Damage_text damage_Text)
    {
        Destroy(damage_Text.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //[NaughtyAttributes.Button]
    private void EnemyHitCallBack(int damage,Vector2 enemyPos,bool isCritical)
    {
        Vector3 spawnPos = enemyPos +Vector2.up*1;
        Damage_text DamagetextInstant = damagePool.Get();
        DamagetextInstant.transform.position = spawnPos;
        DamagetextInstant.Animator_Play(damage,isCritical);
        LeanTween.delayedCall(1, () => damagePool.Release(DamagetextInstant));
    }
}
