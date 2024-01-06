using System;
using UnityEngine;

[Serializable]
public class UsersModel
{
    public string manage_id;         // ���[�U�[�Ǘ�ID
    public string user_id;           // ���[�U�[ID
    public string user_name;         // �\����
    public string handover_passhash; // �����p���p�X���[�h�n�b�V��
    public int has_weapon_exp_point; // ��������o���l
    public int user_rank;            // ���[�U�[�����N
    public int user_rank_exp;        // ���[�U�[�����N�p�̌o���l
    public int login_days;           // �݌v���O�C������
    public int max_stamina;          // �ő�X�^�~�i
    public int last_stamina;         // �ŏI�X�V���X�^�~�i
}

public class Users : MonoBehaviour
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        string query = "create table if not exists users(manage_id bigint,user_id varchar,user_name varchar,handover_passhash varchar,has_weapon_exp_point mediumint, user_rank smallint,user_rank_exp mediumint,login_days int,max_stamina tinyint,last_stamina tinyint,primary key(manage_id),unique_key(user_id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        sqlDB.ExecuteQuery(query);
    }

    // ���R�[�h�o�^����
    public static void Set(UsersModel users)
    {
        string query = "insert or replace into users(manage_id ,user_id ,user_name ,handover_passhash ,has_weapon_exp_point , user_rank ,user_rank_exp ,login_days ,max_stamina ,last_stamina) values(\"" + users.manage_id + "\",\"" + users.user_id + "\",\"" + users.user_name + "\",\"" + users.handover_passhash + "\",\"" + users.has_weapon_exp_point + "\",\"" + users.user_rank + "\",\"" + users.handover_passhash + "\",\"" + users.has_weapon_exp_point + "\",\"" + users.user_rank + "\",\"" + users.user_rank_exp + "\",\"" + users.login_days + "\",\"" + users.max_stamina + "\",\"" + users.last_stamina + ")";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        sqlDB.ExecuteQuery(query);
    }

    // ���R�[�h�擾����
    public static UsersModel Get()
    {
        string query = "select * from users";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        UsersModel usersModel = new UsersModel();
        foreach (DataRow dr in dataTable.Rows)
        {
            usersModel.manage_id = dr["manage_id"].ToString();
            usersModel.user_id = dr["user_id"].ToString();
            usersModel.user_name = dr["user_name"].ToString();
            usersModel.handover_passhash = dr["handover_passhash"].ToString();
            usersModel.has_weapon_exp_point = int.Parse(dr["has_weapon_exp_point"].ToString());
            usersModel.user_rank = int.Parse(dr["user_rank"].ToString());
            usersModel.user_rank_exp = int.Parse(dr["user_rank_exp"].ToString());
            usersModel.login_days = int.Parse(dr["login_days"].ToString());
            usersModel.max_stamina = int.Parse(dr["max_stamina"].ToString());
            usersModel.last_stamina = int.Parse(dr["last_stamina"].ToString());
        }
        return usersModel;
    }
}
