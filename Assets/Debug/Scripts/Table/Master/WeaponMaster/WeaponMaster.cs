using System;
using System.Collections.Generic;

[Serializable]
public class WeaponMasterModel
{
    public int weapon_id;
    public int rarity_id;
    public int weapon_category;
    public string weapon_name;
    public int evolution_weapon_id;
    public int special_attack_id;
    public int evolution_special_attack_id;
}

public class WeaponMaster : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapon_masters(weapon_id bigint,rarity_id bigint,weapon_category tinyint,weapon_name varchar,evolution_weapon_id bigint,special_attack_id bigint,evolution_special_attack_id bigint,primary key(weapon_id))";
        RunQuery(createQuery);
        // インデックス作成
        createQuery = "CREATE INDEX IF NOT EXISTS weapon_category_index ON weapon_masters(weapon_category);";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(WeaponMasterModel[] weapon_master_model_list)
    {
        foreach (WeaponMasterModel weaponsMaster in weapon_master_model_list)
        {
            setQuery = "insert or replace into weapon_masters(weapon_id,rarity_id,weapon_category,weapon_name,evolution_weapon_id,special_attack_id,evolution_special_attack_id) values(" + weaponsMaster.weapon_id + "," + weaponsMaster.rarity_id + "," + weaponsMaster.weapon_category + ",\"" + weaponsMaster.weapon_name + "\"," + weaponsMaster.evolution_weapon_id + "," + weaponsMaster.special_attack_id + "," + weaponsMaster.evolution_special_attack_id + ")";
            RunQuery(setQuery);
        }
    }

    // 全ての武器マスターデータを取得
    public static WeaponMasterModel[] GetWeaponMasterDataAll()
    {
        List<WeaponMasterModel> weaponMasterList = new();
        getQuery = "select * from weapon_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            WeaponMasterModel weaponMasterModel = new();
            weaponMasterModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            weaponMasterModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponMasterModel.weapon_category = int.Parse(dr["weapon_category"].ToString());
            weaponMasterModel.weapon_name = dr["weapon_name"].ToString();
            weaponMasterModel.evolution_weapon_id = int.Parse(dr["evolution_weapon_id"].ToString());
            weaponMasterModel.special_attack_id = int.Parse(dr["special_attack_id"].ToString());
            weaponMasterModel.evolution_special_attack_id = int.Parse(dr["evolution_special_attack_id"].ToString());
            weaponMasterList.Add(weaponMasterModel);
        }
        return weaponMasterList.ToArray();
    }

    // 指定された武器IDのマスターデータのみを取得
    public static WeaponMasterModel GetWeaponMasterData(int weapon_id)
    {
        WeaponMasterModel weaponMasterModel = new();
        getQuery = "select * from weapon_masters where weapon_id=" + weapon_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            weaponMasterModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            weaponMasterModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponMasterModel.weapon_category = int.Parse(dr["weapon_category"].ToString());
            weaponMasterModel.weapon_name = dr["weapon_name"].ToString();
            weaponMasterModel.evolution_weapon_id = int.Parse(dr["evolution_weapon_id"].ToString());
            weaponMasterModel.special_attack_id = int.Parse(dr["special_attack_id"].ToString());
            weaponMasterModel.evolution_special_attack_id = int.Parse(dr["evolution_special_attack_id"].ToString());
        }
        return weaponMasterModel;
    }
}
