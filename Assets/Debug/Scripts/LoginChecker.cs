using UnityEngine;

public class LoginChecker : MonoBehaviour
{
    public static LoginChecker Instance { get; private set; }

    [SerializeField] bool isLogin; // ログインしているかどうかのフラグ
    public bool IsLogin { get { return isLogin; } }

    // TODO: ここに現在のログインしているユーザーの情報を入れる

    void Awake()
    {
        // シングルトンにする
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
