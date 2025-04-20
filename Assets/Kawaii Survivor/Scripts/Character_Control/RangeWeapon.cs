using UnityEngine;

public class RangeWeapon : Weapon
{
    [Header("Elements")]
    private Player players;
    [SerializeField] private Weapon_Bullet bullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        players = FindFirstObjectByType<Player>();
        base.Start();

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

        Weapon_Bullet bulletInstance = Instantiate(bullet, Hitpoint.position, Quaternion.identity);
        bulletInstance.Shoot(transform.up, damage);
    }
}
