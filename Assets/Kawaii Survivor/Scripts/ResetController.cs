using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetController : MonoBehaviour
{
    public void OnResetClicked()
    {
        // ��ʽ1�����ص�ǰ��������ȫ���ã�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // ��ʽ2�������ֻ������ĳЩ���ݣ�Ҳ����������д�Զ����߼�
        // e.g., ����λ�á�Ѫ����������
    }
}