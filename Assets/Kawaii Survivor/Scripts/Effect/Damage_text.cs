using UnityEngine;
using TMPro;
public class Damage_text : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damage_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [NaughtyAttributes.Button]
    public void Animator_Play(int damage,bool isCritical)
    {

        damage_text.text = damage.ToString();
        //if (!gameObject.activeInHierarchy)
        //{
        //gameObject.SetActive(true);
        //}
        damage_text.color = isCritical ? Color.yellow : Color.white;
        animator.Play("Damage_text");
    }
}
