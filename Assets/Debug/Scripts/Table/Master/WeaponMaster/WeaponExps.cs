using System;
using System.Collections.Generic;

[Serializable]
public class WeaponExpModel
{
    public int rarity_id;       // 武器のレアリティID
    public int level;           // レベル
    public int reinforce_point; // 必要経験値
}

public class WeaponExps : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapon_exps(user_id varchar, rarity_id bigint,level tinyint,reinforce_point smallint,primary key(user_id,rarity_id,level))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(WeaponExpModel[] weapon_exps_list, string user_id)
    {
        foreach (WeaponExpModel weapon_exp in weapon_exps_list)
        {
            setQuery = "insert or replace into weapon_exps(user_id,rarity_id,level,reinforce_point) values(\"" + user_id + "\"," + weapon_exp.rarity_id + "," + weapon_exp.level + "," + weapon_exp.reinforce_point + ")";
            RunQuery(setQuery);
        }
    }

    // 全ての武器経験値データを取得
    public static WeaponExpModel[] GetWeaponExpDataAll()
    {
        List<WeaponExpModel> weaponExpList = new();
        getQuery = "select * from weapon_exps";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            WeaponExpModel weaponExpModel = new();
            weaponExpModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponExpModel.level = int.Parse(dr["level"].ToString());
            weaponExpModel.reinforce_point = int.Parse(dr["reinforce_point"].ToString());
            weaponExpList.Add(weaponExpModel);
        }
        return weaponExpList.ToArray();
    }

    // 指定された武器経験値データを取得
    public static WeaponExpModel[] GetWeaponExpData(int rarity_id, int level)
    {
        List<WeaponExpModel> weaponExpList = new();
        getQuery = string.Format("select * from weapon_exps where rarity_id={0} and level={1}", rarity_id, level);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            WeaponExpModel weaponExpModel = new();
            weaponExpModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponExpModel.level = int.Parse(dr["level"].ToString());
            weaponExpModel.reinforce_point = int.Parse(dr["reinforce_point"].ToString());
            weaponExpList.Add(weaponExpModel);
        }
        return weaponExpList.ToArray();
    }
}
