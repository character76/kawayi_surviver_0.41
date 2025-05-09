using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Elements")]
    [SerializeField] private Slider healthSlider;
    
    [SerializeField] private TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthSlider.value = 1;
        healthText.text = health + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamge(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;
        healthSlider.value = (float)health / (float)maxHealth;
        healthText.text = health + "/" + maxHealth;
        //Debug.Log("healthslider"+ healthSlider.value + "remaining health"+health);
        if (health<=0)
        {
            Diemethod();
        }
    }
    private void Diemethod()
    {
        Debug.Log("DaveDead!");
        SceneManager.LoadScene(0);

    }
}
