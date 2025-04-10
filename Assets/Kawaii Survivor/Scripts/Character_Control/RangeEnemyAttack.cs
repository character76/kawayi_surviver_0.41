using UnityEngine;
using UnityEngine.Pool;
public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    private Player players;
    [SerializeField] private Enemy_Bullet enemyBulletPre;
    private ObjectPool<Enemy_Bullet> bulletsPool;
    [Header("Settings")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Enemy_Bullet bulletPrefab;
    [SerializeField] private int damage;
    [SerializeField] private float attackFre;
    private float attdelay;
    private float attTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attdelay = 1f / attackFre;
        attTimer = attdelay;
        bulletsPool = new ObjectPool<Enemy_Bullet>(CreatFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Configure(Player players)
    {
        this.players = players;
    }

    public void AutoAim()
    {
        
        ManageShooting();
    }
    private void ManageShooting()
    {
        attTimer += Time.deltaTime;
        if(attTimer>=attdelay)
        {
            attTimer = 0;
            Shoot();
        }
    }
    
    private void Shoot()
    {
        Vector2 dir = (players.GetCenter() - (Vector2)shootingPoint.position).normalized;

        Enemy_Bullet bullet = bulletsPool.Get();
        bullet.Shoot(damage, dir, shootingPoint.position);
    }
    private Enemy_Bullet CreatFunction()
    {
        Enemy_Bullet bullets= Instantiate(enemyBulletPre, shootingPoint.position, Quaternion.identity);
        bullets.Configure(this);
        return bullets;
        //return Instantiate(enemyBulletPre, shootingPoint.position, Quaternion.identity);
    }
    private void ActionOnGet(Enemy_Bullet bullets)
    {
        bullets.Reload();
        bullets.transform.position = shootingPoint.position;
        enemyBulletPre.gameObject.SetActive(true);
    }
    private void ActionOnRelease(Enemy_Bullet bullets)
    {
        enemyBulletPre.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(Enemy_Bullet bullets)
    {
        Destroy(bullets.gameObject);
    }
    public void ReleaseBullet(Enemy_Bullet bullet)
    {
        bulletsPool.Release(bullet);
    }
}
