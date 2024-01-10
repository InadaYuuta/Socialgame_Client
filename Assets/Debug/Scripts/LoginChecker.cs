using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginChecker : MonoBehaviour
{
    bool isLogin = false;
    public bool IsLogin { get { return isLogin; } }

    bool isCreate = false;

    [SerializeField] int manage_id;            // ユーザー管理ID
    [SerializeField] string user_id;           // ユーザーID
    [SerializeField] string user_name;         // 表示名
    [SerializeField] string handover_passhash; // 引き継ぎパスワードハッシュ
    [SerializeField] int has_weapon_exp_point; // 所持武器経験値
    [SerializeField] int user_rank;            // ユーザーランク
    [SerializeField] int user_rank_exp;        // ユーザーランク用の経験値
    [SerializeField] int login_days;           // 累計ログイン日数
    [SerializeField] int max_stamina;          // 最大スタミナ
    [SerializeField] int last_stamina;         // 最終更新時スタミナ

    void Awake()
    {
        if (!isCreate)
        {
            DontDestroyOnLoad(gameObject);
            isCreate = true;

            // セーブデータがあるか確認
        }
    }

    void Update()
    {

    }
}
