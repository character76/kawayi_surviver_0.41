using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Toggle autoAttackToggle;

    private void Start()
    {
        // 初始化开关状态
        autoAttackToggle.isOn = GlobalSettings.isAutoAttackOn;

        // 添加监听器
        autoAttackToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        GlobalSettings.isAutoAttackOn = isOn;
        Debug.Log("自动攻击状态: " + isOn);
    }
}
