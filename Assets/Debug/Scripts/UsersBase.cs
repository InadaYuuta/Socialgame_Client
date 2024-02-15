using UnityEngine;

public class UsersBase : MonoBehaviour
{
    [SerializeField] protected UsersModel usersModel;      // ユーザーテーブル
    [SerializeField] protected WalletsModel walletsModel;  // ウォレットテーブル

    protected virtual void Awake()
    {
        if (Users.Get() != null)
        {
            usersModel = Users.Get();
        }
        if (Wallets.Get() != null)
        {
            walletsModel = Wallets.Get();
        }
    }

    protected virtual void Update()
    {
        if (Users.Get() != null)
        {
            usersModel = Users.Get();
        }
        if (Wallets.Get() != null)
        {
            walletsModel = Wallets.Get();
        }
    }
}
