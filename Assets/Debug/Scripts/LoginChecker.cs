using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginChecker : MonoBehaviour
{
    bool isLogin = false;
    public bool IsLogin { get { return isLogin; } }

    bool isCreate = false;

    [SerializeField] int manage_id;            // ���[�U�[�Ǘ�ID
    [SerializeField] string user_id;           // ���[�U�[ID
    [SerializeField] string user_name;         // �\����
    [SerializeField] string handover_passhash; // �����p���p�X���[�h�n�b�V��
    [SerializeField] int has_weapon_exp_point; // ��������o���l
    [SerializeField] int user_rank;            // ���[�U�[�����N
    [SerializeField] int user_rank_exp;        // ���[�U�[�����N�p�̌o���l
    [SerializeField] int login_days;           // �݌v���O�C������
    [SerializeField] int max_stamina;          // �ő�X�^�~�i
    [SerializeField] int last_stamina;         // �ŏI�X�V���X�^�~�i

    void Awake()
    {
        if (!isCreate)
        {
            DontDestroyOnLoad(gameObject);
            isCreate = true;

            // �Z�[�u�f�[�^�����邩�m�F
        }
    }

    void Update()
    {

    }
}
