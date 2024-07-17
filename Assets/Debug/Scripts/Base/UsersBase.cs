using UnityEngine;

public class UsersBase : MonoBehaviour
{
    [SerializeField] protected UsersModel usersModel;      // ユーザーテーブル
    [SerializeField] protected WalletsModel walletsModel;  // ウォレットテーブル

    protected virtual void Awake()
    {
        if (Users.Get().user_id != null)
        {
            usersModel = Users.Get();
        }
        if (Wallets.Get().free_amount != null)
        {
            walletsModel = Wallets.Get();
        }
    }

    protected virtual void Update()
    {
        if (Users.Get().user_id != null)
        {
            usersModel = Users.Get();
        }
        if (Wallets.Get().free_amount != null)
        {
            walletsModel = Wallets.Get();
        }
    }
}
