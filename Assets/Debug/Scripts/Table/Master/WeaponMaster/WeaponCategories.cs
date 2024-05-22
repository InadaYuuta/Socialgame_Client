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
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists weapon_categories(weapon_category tinyint,category_name varchar,primary key(weapon_category))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(WeaponCategoryModel[] weapon_categories_list)
    {
        foreach (WeaponCategoryModel weapon_category in weapon_categories_list)
        {
            setQuery = "insert or replace into weapon_categories(weapon_category,category_name) values(" + weapon_category.weapon_category + ",\"" + weapon_category.category_name + "\")";
            RunQuery(setQuery);
        }
    }

    // 全てのガチャカテゴリーデータを取得
    public static WeaponCategoryModel[] GetWeaponCategoryDataAll()
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

    // 指定された武器カテゴリーのデータのみ取得
    public static WeaponCategoryModel GetWeaponCategoryData(int weapon_category)
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