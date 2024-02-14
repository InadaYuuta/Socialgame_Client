using System;
using System.Collections.Generic;

[Serializable]
public class WeaponRarityModel
{
    public int rarity_id;
    public string rarity_name;
}

public class WeaponRarities : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapon_rarities(rarity_id bigint,rarity_name varchar,primary key(rarity_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(WeaponRarityModel[] weapon_rarities_list)
    {
        foreach (WeaponRarityModel weapon_rarities in weapon_rarities_list)
        {
            setQuery = "insert or replace into weapon_rarities(rarity_id,rarity_name) values(" + weapon_rarities.rarity_id + ",\"" + weapon_rarities.rarity_name + "\")";
            RunQuery(setQuery);
        }
    }

    // 全てのガチャ武器データを取得
    public static WeaponRarityModel[] GetGachaWeaponDataAll()
    {
        List<WeaponRarityModel> weaponRarityList = new();
        getQuery = "select * from weapon_rarities";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            WeaponRarityModel weaponRarityModel = new();
            weaponRarityModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponRarityModel.rarity_name = dr["rarity_name"].ToString();
            weaponRarityList.Add(weaponRarityModel);
        }
        return weaponRarityList.ToArray();
    }

    // 指定されたガチャの武器データのみ取得
    public static WeaponRarityModel GetGachaWeaponData(int rarity_id)
    {
        WeaponRarityModel weaponRarityModel = new();
        getQuery = "select * from weapon_rarities where rarity_id =" + rarity_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            weaponRarityModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponRarityModel.rarity_name = dr["rarity_name"].ToString();
        }
        return weaponRarityModel;
    }
}