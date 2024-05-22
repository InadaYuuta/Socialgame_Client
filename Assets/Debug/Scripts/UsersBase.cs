using UnityEngine;

public class UsersBase : MonoBehaviour
{
    [SerializeField] protected UsersModel usersModel;      // ���[�U�[�e�[�u��
    [SerializeField] protected WalletsModel walletsModel;  // �E�H���b�g�e�[�u��

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
