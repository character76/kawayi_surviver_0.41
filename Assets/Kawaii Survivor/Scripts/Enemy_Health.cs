using UnityEngine;
using UnityEngine.UI;
public class Enemy_Health : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField] private GameObject particleeffect;

    [Header("Setting")]
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Elements")]
    [SerializeField] private Slider healthSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthSlider.value = (float)health / (float)maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 health_pos = transform.position;
        health_pos.y += 1.5f;
        healthSlider.transform.position = health_pos;
        
    }
    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        healthSlider.value = (float)health / (float)maxHealth;
        //healthText.text = health + "/" + maxHealth;
        Debug.Log("healthslider" + healthSlider.value + "remaining health" + health);
        if (health <= 0)
        {
            Diemethod();
        }
    }
    private void Diemethod()
    {
        if (particleeffect != null)
        {
            particleeffect.transform.SetParent(null);
            GameObject effect = Instantiate(particleeffect, transform.position, Quaternion.identity);

            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ps.Play();
                Destroy(effect, ps.main.duration); // �����Ӳ�����������
            }
        }
        Destroy(gameObject);
        Debug.Log("Dead!");
    }
}
