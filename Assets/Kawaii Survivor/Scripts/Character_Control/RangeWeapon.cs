using UnityEngine;
using UnityEngine.Pool;
public class RangeWeapon : Weapon
{
    [Header("Elements")]
    private Player players;
    [SerializeField] private Weapon_Bullet bullet;
    [Header("Pooling")]
    private ObjectPool<Weapon_Bullet> bulletPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        players = FindFirstObjectByType<Player>();
        base.Start();
        bulletPool = new ObjectPool<Weapon_Bullet>(CreatFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);

    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
    }
    private void AutoAim()
    {
        Enemy closest_Enemy = GetClosest();
        Vector3 targetUp = Vector3.up;
        if (closest_Enemy != null)
        {
            targetUp = (closest_Enemy.transform.position - transform.position).normalized;
            transform.up = targetUp;
            ManageShooting();
            return;
        }
        else
        {
            //Debug.Log("no close");
        }

        transform.up = Vector3.Lerp(transform.up, targetUp, Time.deltaTime * aimLerp);
        
        //ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            Shoot();
        }
    }
    private void Shoot()
    {
        //Vector2 dir = (players.GetCenter() - (Vector2)Hitpoint.position).normalized;
        int damage = GetDamage(out bool isCriticalhit);
        Weapon_Bullet bulletInstance = bulletPool.Get();
        //Debug.Log("Shoot" + damage);
        bulletInstance.Shoot(transform.up, damage, isCriticalhit);
    }
    private Weapon_Bullet CreatFunction()
    {
        Weapon_Bullet bullets = Instantiate(bullet, Hitpoint.position, Quaternion.identity);
        bullets.Configure(this);
        return bullets;
        //return Instantiate(enemyBulletPre, shootingPoint.position, Quaternion.identity);
    }
    private void ActionOnGet(Weapon_Bullet bullets)
    {
        bullets.Reload();
        bullets.transform.position = Hitpoint.position;
        bullet.gameObject.SetActive(true);
    }
    private void ActionOnRelease(Weapon_Bullet bullets)
    {
        bullet.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(Weapon_Bullet bullets)
    {
        Destroy(bullets.gameObject);
    }
    public void ReleaseBullet(Weapon_Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

}
