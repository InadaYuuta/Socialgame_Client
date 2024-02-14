using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemCategoryModel
{
    public int item_category;    // アイテムカテゴリー
    public string category_name; // カテゴリーの名前
}

public class ItemCategories : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists item_categories (item_category tinyint, category_name varchar,primary key(item_category))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(ItemCategoryModel[] item_category_model_list)
    {
        foreach (ItemCategoryModel itemCategoryModel in item_category_model_list)
        {
            setQuery = "insert or replace into item_categories (item_category,category_name) values(" + itemCategoryModel.item_category + ",\"" + itemCategoryModel.category_name + "\") ";
            RunQuery(setQuery);
        }
    }

    // 全てのカテゴリーを取得
    public static ItemCategoryModel[] GetItemCategoryAll()
    {
        List<ItemCategoryModel> list = new();
        getQuery = "select * from item_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ItemCategoryModel itemCategoryModel = new();
            itemCategoryModel.item_category = int.Parse(dr["item_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
            list.Add(itemCategoryModel);
        }
        return list.ToArray(); // Listを配列に変換して返す
    }

    // 指定したカテゴリーだけを取得
    public static ItemCategoryModel GetItemCategory(int item_category)
    {
        ItemCategoryModel itemCategoryModel = new();
        getQuery = "select * from item_categories where item_category" + item_category;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemCategoryModel.item_category = int.Parse(dr["item_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
        }
        return itemCategoryModel;
    }
}