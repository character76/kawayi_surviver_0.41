using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class Enemy_Bullet : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Collider2D cli;
    private RangeEnemyAttack rangeEnemy;
    
    [Header("Settings")]
    private int damage;
    [SerializeField] private float movespeed;
    private Vector3 shootpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //rig = GetComponent<Rigidbody2D>();
        //cli = GetComponent<Collider2D>();
        LeanTween.delayedCall(gameObject, 5, () => rangeEnemy.ReleaseBullet(this));
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot(int damage, Vector2 dir, Vector3 pos)
    {
        this.damage = damage;
        transform.right = dir;
        rig.linearVelocity = dir * movespeed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Takedamge");
        if (collider.TryGetComponent(out Player player))
        {
            LeanTween.cancel(gameObject);
            player.TakeDamage(1);
            this.cli.enabled = false;
            rangeEnemy.ReleaseBullet(this);
        }
    }
    public void Configure(RangeEnemyAttack rangeEnemy)
    {
        this.rangeEnemy = rangeEnemy;

    }
    public void Reload()
    {
        rig.linearVelocity = Vector2.zero;
        cli.enabled = true;
    }

}

