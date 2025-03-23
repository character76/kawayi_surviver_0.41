using UnityEngine;

public class DeBoink : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float aimLerp;
    //[SerializeField] private Transform Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position,range,enemyMask);
        Vector2 targetUp = Vector3.up;

        if (enemies.Length<=0)
        {
            transform.up = Vector3.up;
            Debug.LogWarning("No cloest");
            return;
        }
        int cloestIndex = -1;
        float minDis = range;

        for(int i=0;i<enemies.Length;i++)
        {
            Enemy_follow enemy_checking = enemies[i].GetComponent<Enemy_follow>();
            float disToenemy = Vector2.Distance(transform.position, enemy_checking.transform.position);

            if (disToenemy < minDis)
            {
                minDis = disToenemy;
                cloestIndex = i;
            }
            else continue;
        }
        if(cloestIndex==-1)
        {
            transform.up = Vector3.Lerp(transform.up, targetUp,Time.deltaTime*aimLerp);
            return;
        }
        //Debug.Log("Closest Enemy" + cloestIndex + " dis " + minDis);
       
        targetUp= (enemies[cloestIndex].transform.position - transform.position).normalized;
        transform.up = Vector3.Lerp(transform.up, targetUp, Time.deltaTime * aimLerp); 
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
