using System;
using UnityEngine;

[Serializable]
public class UsersModel
{
    public string manage_id;         // ユーザー管理ID
    public string user_id;           // ユーザーID
    public string user_name;         // 表示名
    public string handover_passhash; // 引き継ぎパスワードハッシュ
    public int has_weapon_exp_point; // 所持武器経験値
    public int user_rank;            // ユーザーランク
    public int user_rank_exp;        // ユーザーランク用の経験値
    public int login_days;           // 累計ログイン日数
    public int max_stamina;          // 最大スタミナ
    public int last_stamina;         // 最終更新時スタミナ
}

public class Users : MonoBehaviour
{
    // テーブル作成
    public static void CreateTable()
    {
        string query = "create table if not exists users(manage_id bigint,user_id varchar,user_name varchar,handover_passhash varchar,has_weapon_exp_point mediumint, user_rank smallint,user_rank_exp mediumint,login_days int,max_stamina tinyint,last_stamina tinyint,primary key(manage_id),unique_key(user_id))";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        sqlDB.ExecuteQuery(query);
    }

    // レコード登録処理
    public static void Set(UsersModel users)
    {
        string query = "insert or replace into users(manage_id ,user_id ,user_name ,handover_passhash ,has_weapon_exp_point , user_rank ,user_rank_exp ,login_days ,max_stamina ,last_stamina) values(\"" + users.manage_id + "\",\"" + users.user_id + "\",\"" + users.user_name + "\",\"" + users.handover_passhash + "\",\"" + users.has_weapon_exp_point + "\",\"" + users.user_rank + "\",\"" + users.handover_passhash + "\",\"" + users.has_weapon_exp_point + "\",\"" + users.user_rank + "\",\"" + users.user_rank_exp + "\",\"" + users.login_days + "\",\"" + users.max_stamina + "\",\"" + users.last_stamina + ")";
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        sqlDB.ExecuteQuery(query);
    }

    // レコード取得処理
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
