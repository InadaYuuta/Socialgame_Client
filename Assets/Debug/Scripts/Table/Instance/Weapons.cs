using System;
using System.Collections.Generic;

[Serializable]
public class WeaponModel
{
    public int weapon_id;
    public int rarity_id;
    public int level;
    public int level_max;
    public int current_exp;
    public int limit_break;
    public int limit_break_max;
    public int evolution;
}

public class Weapons : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapons(user_id varchar,weapon_id bigint,rarity_id bigint,level tinyint,level_max tinyint,current_exp int,limit_break tinyint,limit_break_max tinyint,evolution tinyint,primary key(user_id,weapon_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(WeaponModel[] weapons_model_list, string user_id)
    {
        if (weapons_model_list == null || user_id == null) { return; }
        foreach (WeaponModel weapons in weapons_model_list)
        {
            setQuery = "insert or replace into weapons(user_id,weapon_id,rarity_id,level,level_max,current_exp,limit_break,limit_break_max,evolution) values(\"" + user_id + "\"," + weapons.weapon_id + "," + weapons.rarity_id + "," + weapons.level + "," + weapons.level_max + "," + weapons.current_exp + "," + weapons.limit_break + "," + weapons.limit_break_max + "," + weapons.evolution + ")";
            RunQuery(setQuery);
        }
        UpdateDate(weapons_model_list, user_id);
    }

    // 更新
    public static void UpdateDate(WeaponModel[] weapons, string user_id)
    {
        foreach (WeaponModel weapon in weapons)
        {
            setQuery = string.Format("UPDATE weapons SET " +
                "user_id = \"{0}\"," +
                "weapon_id = {1}," +
                "rarity_id = {2}," +
                "level = {3}," +
                "level_max = {4}," +
                "current_exp = {5}," +
                "limit_break = {6}," +
                "limit_break_max = {7}," +
                "evolution = {8}" +
                " WHERE user_id = \"{0}\" AND weapon_id = {1}",
                user_id,
                weapon.weapon_id,
                weapon.rarity_id,
                weapon.level,
                weapon.level_max,
                weapon.current_exp,
                weapon.limit_break,
                weapon.limit_break_max,
                weapon.evolution);
            RunQuery(setQuery);
        }
    }

    // 全ての武器データの取得
    public static WeaponModel[] GetWeaponDataDefault(string selectQuery)
    {
        List<WeaponModel> weaponsList = new();
        getQuery = selectQuery;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            WeaponModel weaponModel = new();
            weaponModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            weaponModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponModel.level = int.Parse(dr["level"].ToString());
            weaponModel.level_max = int.Parse(dr["level_max"].ToString());
            weaponModel.current_exp = int.Parse(dr["current_exp"].ToString()); ;
            weaponModel.limit_break = int.Parse(dr["limit_break"].ToString()); ;
            weaponModel.limit_break_max = int.Parse(dr["limit_break_max"].ToString()); ;
            weaponModel.evolution = int.Parse(dr["evolution"].ToString());
            weaponsList.Add(weaponModel);
        }
        return weaponsList.ToArray();
    }

    // 全ての武器データの取得
    public static WeaponModel[] GetWeaponDataAll()
    {
        WeaponModel[] weaponsList;
        getQuery = "select * from weapons";
        weaponsList = GetWeaponDataDefault(getQuery);
        return weaponsList;
    }

    /// <summary>
    /// レアリティ順に並び替えてデータを取得
    /// isDescがtrueなら昇順、falseなら降順
    /// </summary>
    /// <param name="isDesc"></param>
    /// <returns></returns>
    public static WeaponModel[] GetRaritySortDesc(bool isDesc)
    {
        WeaponModel[] weaponsList;
        getQuery = "select * from weapons";
        if (isDesc)
        {
            weaponsList = GetWeaponDataDefault(string.Format("{0}{1}", getQuery, " order by weapon_id asc"));
        }
        else
        {
            weaponsList = GetWeaponDataDefault(string.Format("{0}{1}", getQuery, " order by weapon_id desc"));
        }
        return weaponsList;
    }

    // 指定された武器IDの武器だけを取得
    public static WeaponModel GetWeaponData(int weapon_id)
    {
        WeaponModel weaponModel = new();
        getQuery = string.Format("select * from weapons where weapon_id = {0}", weapon_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            weaponModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            weaponModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponModel.level = int.Parse(dr["level"].ToString());
            weaponModel.level_max = int.Parse(dr["level_max"].ToString());
            weaponModel.current_exp = int.Parse(dr["current_exp"].ToString()); ;
            weaponModel.limit_break = int.Parse(dr["limit_break"].ToString()); ;
            weaponModel.limit_break_max = int.Parse(dr["limit_break_max"].ToString()); ;
            weaponModel.evolution = int.Parse(dr["evolution"].ToString());
        }
        return weaponModel;
    }


    // 指定された武器の削除(進化時にクライアント側だけ進化前の武器が残ってしまうため)
    // 間違っても進化以外の場所で呼ばないように注意、論理削除ではなく物理削除のため
    public static void DeleteWeapon(int weapon_id)
    {
        getQuery = string.Format("delete from weapons where weapon_id = {0}", weapon_id);
        RunQuery(getQuery);
    }
}
