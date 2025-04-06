using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Toggle autoAttackToggle;

    private void Start()
    {
        // ��ʼ������״̬
        autoAttackToggle.isOn = GlobalSettings.isAutoAttackOn;

        // ��Ӽ�����
        autoAttackToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        GlobalSettings.isAutoAttackOn = isOn;
        Debug.Log("�Զ�����״̬: " + isOn);
    }
}
