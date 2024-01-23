using UnityEngine;

public class UsersBase : MonoBehaviour
{
    protected UsersModel usersModel;     // ユーザーテーブル
    protected WalletsModel walletsModel; // ウォレットテーブル

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
}
