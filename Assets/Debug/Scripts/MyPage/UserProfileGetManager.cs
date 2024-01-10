using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserProfileGetManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI staminaText;

    string userName;

    int lastStamina;

    UsersModel usersModel;

    void Start()
    {
        usersModel = Users.Get();
        if (!string.IsNullOrEmpty(usersModel.user_name))
        {
            userName = usersModel.user_name;
            userNameText.text = userName;
        }
        if (usersModel.last_stamina != null)
        {
            lastStamina = usersModel.last_stamina;
            staminaText.text = string.Format("Stamina:{0}", lastStamina.ToString());
        }
    }

    void Update()
    {

    }
}
