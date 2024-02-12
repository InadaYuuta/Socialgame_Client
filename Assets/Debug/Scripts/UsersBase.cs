using UnityEngine;

public class UsersBase : MonoBehaviour
{
    [SerializeField] protected UsersModel usersModel;      // ���[�U�[�e�[�u��
    [SerializeField] protected WalletsModel walletsModel; // �E�H���b�g�e�[�u��
    //protected ItemsModel itemsModel;

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
        //if (Items.Get() != null)
        //{
        //    itemsModel = Items.Get();
        //}
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
