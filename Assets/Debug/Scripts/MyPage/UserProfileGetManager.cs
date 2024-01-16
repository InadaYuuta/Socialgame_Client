using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class UserProfileGetManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userNameText;
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI userRankText;
    [SerializeField] TextMeshProUGUI userRankExpText;

    string userName;

    int lastStamina;
    int maxStamina;
    int userRank;
    int userRankExp;

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
        if (usersModel.user_rank != null)
        {
            userRank = usersModel.user_rank;
            userRankText.text = string.Format("UserRank:{0}", userRank.ToString());
        }
        if(usersModel.user_rank_exp != null)
        {
            userRankExp = usersModel.user_rank_exp;
            userRankExpText.text = string.Format("{0}Exp", userRankExp.ToString());
        }
    }

    void Update()
    {

    }
}
