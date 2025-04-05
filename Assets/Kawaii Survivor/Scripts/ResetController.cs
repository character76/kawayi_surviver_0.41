using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetController : MonoBehaviour
{
    public void OnResetClicked()
    {
        // 方式1：重载当前场景（完全重置）
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // 方式2：如果你只想重置某些数据，也可以在这里写自定义逻辑
        // e.g., 重置位置、血量、变量等
    }
}