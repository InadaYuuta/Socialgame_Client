using UnityEngine;

public class LoginChecker : MonoBehaviour
{
    public static LoginChecker Instance { get; private set; }

    [SerializeField] bool isLogin; // ���O�C�����Ă��邩�ǂ����̃t���O
    public bool IsLogin { get { return isLogin; } }

    // TODO: �����Ɍ��݂̃��O�C�����Ă��郆�[�U�[�̏�������

    void Awake()
    {
        // �V���O���g���ɂ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public void OnLoginFlag()
    {
        isLogin = true;
    }
}
