using System;

[Serializable]
public class UsersModel
{
    public string user_id;           // ���[�U�[ID
    public string user_name;         // �\����
    public string handover_passhash; // �����p���p�X���[�h�n�b�V��
    public int has_reinforce_point;  // ���������|�C���g
    public int user_rank;            // ���[�U�[�����N
    public int user_rank_exp;        // ���[�U�[�����N�p�̌o���l
    public int login_days;           // �݌v���O�C������
    public int max_stamina;          // �ő�X�^�~�i
    public int last_stamina;         // �ŏI�X�V���X�^�~�i
    public string last_login;        // �ŏI���O�C������
}

public class Users : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists users(user_id varchar,user_name varchar,handover_passhash varchar,has_reinforce_point mediumint, user_rank smallint,user_rank_exp mediumint,login_days int,max_stamina tinyint,last_stamina tinyint,last_login varchar,primary key (user_id))";
        RunQuery(createQuery);
    }

    // ��U�m�F�p�ɍ쐬�A�ォ�����
    public static void DropTable()
    {
        string query = "drop table users";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        sqlDB.ExecuteQuery(query);
    }

    // ���R�[�h�o�^����
    public static void Set(UsersModel users)
    {
        setQuery = "insert or replace into users(user_id ,user_name ,handover_passhash ,has_reinforce_point , user_rank ,user_rank_exp ,login_days ,max_stamina ,last_stamina,last_login) values(\"" + users.user_id + "\",\"" + users.user_name + "\",\"" + users.handover_passhash + "\"," + users.has_reinforce_point + "," + users.user_rank + "," + users.user_rank_exp + ", " + users.login_days + "," + users.max_stamina + ", " + users.last_stamina + ",\"" + users.last_login + "\")";
        RunQuery(setQuery);
    }

    // ���R�[�h�擾����
    public static UsersModel Get()
    {
        getQuery = "select * from users";
        DataTable dataTable = RunQuery(getQuery);
        UsersModel usersModel = new();
        foreach (DataRow dr in dataTable.Rows)
        {
            usersModel.user_id = dr["user_id"].ToString();
            usersModel.user_name = dr["user_name"].ToString();
            usersModel.handover_passhash = dr["handover_passhash"].ToString();
            usersModel.has_reinforce_point = int.Parse(dr["has_reinforce_point"].ToString());
            usersModel.user_rank = int.Parse(dr["user_rank"].ToString());
            usersModel.user_rank_exp = int.Parse(dr["user_rank_exp"].ToString());
            usersModel.login_days = int.Parse(dr["login_days"].ToString());
            usersModel.max_stamina = int.Parse(dr["max_stamina"].ToString());
            usersModel.last_stamina = int.Parse(dr["last_stamina"].ToString());
           // usersModel.last_login = dr["last_login"].ToString();
        }
        return usersModel;
    }

    // �ŏI���O�C���X�V�@���X�g���O�C���̃J�������������ɂ��ǉ�������g��
    public static void SetLastLogin(string userId)
    {
        DateTime dt = DateTime.Now;
        string nowTimeStamp = dt.ToString("yyyy-MM-dd HH:mm:ss");
        string query = "update users set last_login = '" + nowTimeStamp + "' where user_id = '" + userId + "'";
        SqliteDatabase sqlDB = new(GameUtil.Const.SQLITE_FILE_NAME);
        sqlDB.ExecuteNonQuery(query);
    }

    // ���R�[�h�X�V
    //public static void UpdateRecord(UsersModel users)
    //{
    //    string query = "update users set user_name = \"" users.user_name "\",handover_passhash = \"" users.handover_passhash "\",has_weapon_exp_point = " users.has_weapon_exp_point ",user_rank = " users.user_rank ",user_rank_exp = " users.has_weapon_exp_point ",login_days = " users.login_days ",max_stamina = " users.max_stamina ",last_stamina values =" users.last_stamina "where user_id = \"" users.user_id "\")";
    //    SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
    //    sqlDB.ExecuteQuery(query);
    //}
}
