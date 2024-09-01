using System;
using System.Collections.Generic;

[Serializable]
public class GachaWeaponModel
{
    public int gacha_id;  // ガチャID
    public int weapon_id; // 武器ID
    public int weight;    // 重み
}

public class GachaWeapons : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists gacha_weapons(gacha_id bigint,weapon_id bigint,weight int,primary key(weapon_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(GachaWeaponModel[] gacha_weapon_list)
    {
        foreach (GachaWeaponModel gacha_weapons in gacha_weapon_list)
        {
            setQuery = "insert or replace into gacha_weapons(gacha_id,weapon_id,weight) values(" + gacha_weapons.gacha_id + "," + gacha_weapons.weapon_id + "," + gacha_weapons.weight + ")";
            RunQuery(setQuery);
        }
    }

    // 全てのガチャ武器データを取得
    public static GachaWeaponModel[] GetGachaWeaponDataAll()
    {
        List<GachaWeaponModel> gachaWeaponList = new();
        getQuery = "select * from gacha_weapons";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            GachaWeaponModel gachaWeaponModel = new();
            gachaWeaponModel.gacha_id = int.Parse(dr["gacha_id"].ToString());
            gachaWeaponModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            gachaWeaponModel.weight = int.Parse(dr["weight"].ToString());
            gachaWeaponList.Add(gachaWeaponModel);
        }
        return gachaWeaponList.ToArray();
    }

    // 確率表記用に並び替えてから全てのデータを取得
    public static GachaWeaponModel[] GetSortDataAll()
    {
        List<GachaWeaponModel> gachaWeaponList = new();
        getQuery = "select * from gacha_weapons order by weapon_id desc";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            GachaWeaponModel gachaWeaponModel = new();
            gachaWeaponModel.gacha_id = int.Parse(dr["gacha_id"].ToString());
            gachaWeaponModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            gachaWeaponModel.weight = int.Parse(dr["weight"].ToString());
            gachaWeaponList.Add(gachaWeaponModel);
        }
        return gachaWeaponList.ToArray();
    }

    // 指定されたガチャの武器データのみ取得
    public static GachaWeaponModel GetGachaWeaponData(int gacha_id)
    {
        GachaWeaponModel gachaWeaponModel = new();
        getQuery = "select * from gacha_weapons where gacha_id =" + gacha_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            gachaWeaponModel.gacha_id = int.Parse(dr["gacha_id"].ToString());
            gachaWeaponModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            gachaWeaponModel.weight = int.Parse(dr["weight"].ToString());
        }
        return gachaWeaponModel;
    }
}
