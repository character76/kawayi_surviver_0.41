using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Weapon_Bullet : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Collider2D cli;

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
    public void Shoot(Vector2 direction,int damage)
    {
        this.damage = damage;
        transform.right = direction;
        rig.linearVelocity = direction * movespeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("��ײ��ⷢ���ڣ�" + collision.gameObject.name + "��Layer: " + collision.gameObject.layer+ IsInLayerMask(enemyMask, collision.gameObject.layer));

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
                //Debug.LogWarning("��ײ����û�� Enemy �����" + collision.gameObject.name);
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
            //Debug.Log("Take damage");
            health.TakeDamage(damage);
        }
        else
        {
            //Debug.LogError("Enemy ������û�� Enemy_Health �����" + enemy.gameObject.name);
        }
    }
}
