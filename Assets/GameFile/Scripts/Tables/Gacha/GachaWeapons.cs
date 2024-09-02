using System;
using System.Collections.Generic;

[Serializable]
public class GachaWeaponModel
{
    public int gacha_id;  // �K�`��ID
    public int weapon_id; // ����ID
    public int weight;    // �d��
}

public class GachaWeapons : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists gacha_weapons(gacha_id bigint,weapon_id bigint,weight int,primary key(weapon_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(GachaWeaponModel[] gacha_weapon_list)
    {
        foreach (GachaWeaponModel gacha_weapons in gacha_weapon_list)
        {
            setQuery = "insert or replace into gacha_weapons(gacha_id,weapon_id,weight) values(" + gacha_weapons.gacha_id + "," + gacha_weapons.weapon_id + "," + gacha_weapons.weight + ")";
            RunQuery(setQuery);
        }
    }

    // �S�ẴK�`������f�[�^���擾
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

    // �m���\�L�p�ɕ��ёւ��Ă���S�Ẵf�[�^���擾
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

    // �w�肳�ꂽ�K�`���̕���f�[�^�̂ݎ擾
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
