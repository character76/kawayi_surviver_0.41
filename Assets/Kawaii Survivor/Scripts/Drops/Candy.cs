using UnityEngine;

using System.Collections;

public class Candy : MonoBehaviour,ICollectable
{
    private bool collected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Collect(Player player)
    {
        if (collected == true)
            return;
        else
            collected = true;
        StartCoroutine(MoveTowardsPlayer(player));

    }
    IEnumerator MoveTowardsPlayer(Player player)
    {
        float timer = 0;
        Vector2 initialpos = transform.position;
        while(timer<1)
        {
            Vector2 targetPosition = player.GetCenter();
            transform.position = Vector2.Lerp(initialpos, targetPosition, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        Collected();
    }
    private void Collected()
    {
        gameObject.SetActive(false);
    }
}
