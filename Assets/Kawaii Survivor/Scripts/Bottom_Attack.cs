using UnityEngine;

public class Bottom_Attack : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private GameObject weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Attack()
    {
        weapon.GetComponent<DeBoink>().StartAttack();
        Debug.Log("Attack��");
        // TODO: �����������Ĺ����߼������粥�Ŷ����������˵�
    }
}
