using System;
using System.Collections.Generic;

[Serializable]
public class EvolutionWeaponModel
{
    public int evolution_weapon_id; // 進化後武器ID
    public int rarity_id;           // レアリティID
    public int weapon_category;     // 武器のカテゴリー
    public string weapon_name;      // 武器の名前
}

public class EvolutionWeapons : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists evolution_weapons(evolution_weapon_id bigint, rarity_id bigint,weapon_category tinyint,weapon_name varchar,primary key(evolution_weapon_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(EvolutionWeaponModel[] evolution_weapon_model)
    {
        foreach (EvolutionWeaponModel evolution_weapon in evolution_weapon_model)
        {
            setQuery = "insert or replace into evolution_weapons(evolution_weapon_id,rarity_id,weapon_category,weapon_name) values(\"" + evolution_weapon.evolution_weapon_id + "," + evolution_weapon.rarity_id + "," + evolution_weapon.weapon_category + "\"," + evolution_weapon.weapon_name + ")";
            RunQuery(setQuery);
        }
    }

    // 全ての進化後武器データを取得
    public static EvolutionWeaponModel[] GetEvolutionWeaponDataAll()
    {
        List<EvolutionWeaponModel> EvolutionWeaponList = new();
        getQuery = "select * from evolution_weapons";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            EvolutionWeaponModel evolutionWeaponModel = new();
            evolutionWeaponModel.evolution_weapon_id = int.Parse(dr["evolution_weapon_id"].ToString());
            evolutionWeaponModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            evolutionWeaponModel.weapon_category = int.Parse(dr["weapon_category"].ToString());
            evolutionWeaponModel.weapon_name = dr["weapon_name"].ToString();
            EvolutionWeaponList.Add(evolutionWeaponModel);
        }
        return EvolutionWeaponList.ToArray();
    }

    // 指定された進化後武器データを取得
    public static EvolutionWeaponModel[] GetEvolutionWeaponData(int evolution_weapon_id)
    {
        List<EvolutionWeaponModel> EvolutionWeaponList = new();
        getQuery = string.Format("select * from evolution_weapons where evolution_weapon_id={0}", evolution_weapon_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            EvolutionWeaponModel evolutionWeaponModel = new();
            evolutionWeaponModel.evolution_weapon_id = int.Parse(dr["evolution_weapon_id"].ToString());
            evolutionWeaponModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            evolutionWeaponModel.weapon_category = int.Parse(dr["weapon_category"].ToString());
            evolutionWeaponModel.weapon_name = dr["weapon_name"].ToString();
            EvolutionWeaponList.Add(evolutionWeaponModel);
        }
        return EvolutionWeaponList.ToArray();
    }
}