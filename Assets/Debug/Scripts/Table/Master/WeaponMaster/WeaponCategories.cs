using System;
using System.Collections.Generic;

[Serializable]
public class WeaponCategoryModel
{
    public int weapon_category;
    public string category_name;
}

public class WeaponCategories : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapon_categories(weapon_category bigint,category_name varchar,primary key(weapon_category))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(WeaponCategoryModel[] weapon_categories_list)
    {
        foreach (WeaponCategoryModel weapon_category in weapon_categories_list)
        {
            setQuery = "insert or replace into weapon_categories(weapon_category,category_name) values(" + weapon_category.weapon_category + ",\"" + weapon_category.category_name + "\")";
            RunQuery(setQuery);
        }
    }

    // �S�ẴK�`���J�e�S���[�f�[�^���擾
    public static WeaponCategoryModel[] GetGachaWeaponDataAll()
    {
        List<WeaponCategoryModel> weaponCategoryList = new();
        getQuery = "select * from weapon_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            WeaponCategoryModel weaponCategoryModel = new();
            weaponCategoryModel.weapon_category = int.Parse(dr["weapon_category"].ToString());
            weaponCategoryModel.category_name = dr["category_name"].ToString();
            weaponCategoryList.Add(weaponCategoryModel);
        }
        return weaponCategoryList.ToArray();
    }

    // �w�肳�ꂽ����J�e�S���[�̃f�[�^�̂ݎ擾
    public static WeaponCategoryModel GetGachaWeaponData(int weapon_category)
    {
        WeaponCategoryModel weaponCategoryModel = new();
        getQuery = "select * from weapon_categories where weapon_category =" + weapon_category;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            weaponCategoryModel.weapon_category = int.Parse(dr["weapon_category"].ToString());
            weaponCategoryModel.category_name = dr["category_name"].ToString();
        }
        return weaponCategoryModel;
    }
}