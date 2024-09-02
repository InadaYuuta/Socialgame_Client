using System;
using System.Collections.Generic;

[Serializable]
public class WeaponExpModel
{
    public int rarity_id;       // ����̃��A���e�BID
    public int level;           // ���x��
    public int use_reinforce_point; // �K�v�o���l
}

public class WeaponExps : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapon_exps(user_id varchar, rarity_id bigint,level tinyint,reinforce_point smallint,primary key(user_id,rarity_id,level))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(WeaponExpModel[] weapon_exps_list, string user_id)
    {
        foreach (WeaponExpModel weapon_exp in weapon_exps_list)
        {
            setQuery = "insert or replace into weapon_exps(user_id,rarity_id,level,reinforce_point) values(\"" + user_id + "\"," + weapon_exp.rarity_id + "," + weapon_exp.level + "," + weapon_exp.use_reinforce_point + ")";
            RunQuery(setQuery);
        }
    }

    // �S�Ă̕���o���l�f�[�^���擾
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
            weaponExpModel.use_reinforce_point = int.Parse(dr["reinforce_point"].ToString());
            weaponExpList.Add(weaponExpModel);
        }
        return weaponExpList.ToArray();
    }

    // �w�肳�ꂽ����̎��ɕK�v�ȋ����|�C���g�f�[�^���擾
    public static WeaponExpModel GetWeaponExpData(int weapon_id)
    {
        WeaponExpModel weaponExpModel = new();
        WeaponModel weaponData = Weapons.GetWeaponData(weapon_id);
        int rarity_id = WeaponMaster.GetWeaponMasterData(weapon_id).rarity_id;
        int level = weaponData.level + 1;
        getQuery = string.Format("select * from weapon_exps where rarity_id={0} and level={1}", rarity_id, level);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            weaponExpModel.rarity_id = int.Parse(dr["rarity_id"].ToString());
            weaponExpModel.level = int.Parse(dr["level"].ToString());
            weaponExpModel.use_reinforce_point = int.Parse(dr["reinforce_point"].ToString());
        }
        return weaponExpModel;
    }
}
