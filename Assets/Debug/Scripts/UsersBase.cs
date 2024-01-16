using UnityEngine;

public class UsersBase : MonoBehaviour
{
    protected UsersModel usersModel;

    protected virtual void Awake()
    {
        usersModel = Users.Get();
    }
}
