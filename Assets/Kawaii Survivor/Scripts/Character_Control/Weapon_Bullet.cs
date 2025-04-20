using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Weapon_Bullet : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Collider2D cli;
    private RangeWeapon rangeweapon;
    [Header("Settings")]
    private int damage;
    [SerializeField] private float movespeed;
    [SerializeField] private LayerMask enemyMask;
    private Vector3 shootpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //rig = GetComponent<Rigidbody2D>();
        //cli = GetComponent<Collider2D>();
        //LeanTween.delayedCall(gameObject, 5, () => rangeEnemy.ReleaseBullet(this));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Configure(RangeWeapon Rweapon)
    {
        this.rangeweapon = Rweapon;
    }
    public void Shoot(Vector2 direction,int damage)
    {
        Invoke("Release", 1);
        this.damage = damage;
        transform.right = direction;
        rig.linearVelocity = direction * movespeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("碰撞检测发生在：" + collision.gameObject.name + "，Layer: " + collision.gameObject.layer+ IsInLayerMask(enemyMask, collision.gameObject.layer));

        if (IsInLayerMask( collision.gameObject.layer,enemyMask))
        {
            //Debug.Log("Collide with: " + collision.gameObject.name);

            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Attack(enemy);
            }
            else
            {
                //Debug.LogWarning("碰撞对象没有 Enemy 组件：" + collision.gameObject.name);
            }
        }
    }
    private bool IsInLayerMask(int layer,LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
    private void Attack(Enemy enemy)
    {
        //Debug.Log("Try to take damage");

        Enemy_Health health = enemy.GetComponent<Enemy_Health>();
        if (health != null)
        {
            CancelInvoke();
            //Debug.Log("Take damage");
            health.TakeDamage(damage);
            Release();
        }
        else
        {
            //Debug.LogError("Enemy 对象上没有 Enemy_Health 组件：" + enemy.gameObject.name);
        }
    }
    public void Reload()
    {
        rig.linearVelocity = Vector2.zero;
        cli.enabled = true;
    }
    private void Release()
    {
        if (!gameObject.activeSelf)
            return;
        rangeweapon.ReleaseBullet(this);
    }
}
