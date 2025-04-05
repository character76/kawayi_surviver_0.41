using UnityEngine;

public class Bottom_Attack : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private GameObject weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Attack()
    {
        weapon.GetComponent<DeBoink>().StartAttack();
        Debug.Log("Attack！");
        // TODO: 在这里添加你的攻击逻辑，比如播放动画、检测敌人等
    }
}
