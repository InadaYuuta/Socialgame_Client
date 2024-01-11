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
    int maxStamina;

    UsersModel usersModel;

    void Start()
    {
        usersModel = Users.Get();
        if (!string.IsNullOrEmpty(usersModel.user_name))
        {
            userName = usersModel.user_name;
            userNameText.text = string.Format("UserName:{0}", userName);
        }
        if (usersModel.last_stamina != null && usersModel.max_stamina != null)
        {
            lastStamina = usersModel.last_stamina;
            maxStamina = usersModel.max_stamina;
            staminaText.text = string.Format("Stamina:{0}/{1}", lastStamina.ToString(), maxStamina.ToString());
        }
    }

    void Update()
    {

    }
}
