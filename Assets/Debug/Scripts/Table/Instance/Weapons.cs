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
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapons(user_id varchar,weapon_id bigint,rarity_id bigint,level tinyint,level_max tinyint,current_exp int,limit_break tinyint,limit_break_max tinyint,evolution tinyint,primary key(user_id,weapon_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(WeaponModel[] weapons_model_list, string user_id)
    {
        if (weapons_model_list == null || user_id == null) { return; }
        foreach (WeaponModel weapons in weapons_model_list)
        {
            setQuery = "insert or replace into weapons(user_id,weapon_id,rarity_id,level,level_max,current_exp,limit_break,limit_break_max,evolution) values(\"" + user_id + "\"," + weapons.weapon_id + "," + weapons.rarity_id + "," + weapons.level + "," + weapons.level_max + "," + weapons.current_exp + "," + weapons.limit_break + "," + weapons.limit_break_max + "," + weapons.evolution + ")";
            RunQuery(setQuery);
        }
    }

    // �S�Ă̕���f�[�^�̎擾
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

    // �S�Ă̕���f�[�^�̎擾
    public static WeaponModel[] GetWeaponDataAll()
    {
        WeaponModel[] weaponsList;
        getQuery = "select * from weapons";
        weaponsList = GetWeaponDataDefault(getQuery);
        return weaponsList;
    }

    /// <summary>
    /// ���A���e�B���ɕ��ёւ��ăf�[�^���擾
    /// isDesc��true�Ȃ珸���Afalse�Ȃ�~��
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

    // �w�肳�ꂽ����ID�̕��킾�����擾
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


    // �w�肳�ꂽ����̍폜(�i�����ɃN���C�A���g�������i���O�̕��킪�c���Ă��܂�����)
    // �Ԉ���Ă��V�C�O�̏ꏊ�ŌĂ΂Ȃ��悤�ɒ��ӁA�_���폜�ł͂Ȃ������폜�̂���
    public static void DeleteWeapon(int weapon_id)
    {
        getQuery = string.Format("delete from weapons where weapon_id = {0}", weapon_id);
        RunQuery(getQuery);
    }
}
