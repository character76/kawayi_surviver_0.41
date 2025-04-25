using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class DropsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Enemy_Health.onDieDrop += EnemyDieDropCallback;
    }
    private void OnDestroy()
    {
        Enemy_Health.onDieDrop -= EnemyDieDropCallback;
    }

    private void EnemyDieDropCallback(Vector2 Enemy_pos)
    {
        bool CashorCandy = Random.Range(0, 101) <= 20;
        GameObject drop = CashorCandy ? cashPrefab.gameObject : candyPrefab.gameObject;
        Instantiate(drop, Enemy_pos, Quaternion.identity,transform);
        
    }

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
