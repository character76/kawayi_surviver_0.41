using System;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy candyPrefab;
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
        Instantiate(candyPrefab, Enemy_pos, Quaternion.identity,transform);
        
    }

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
