using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ItemCategoryModel
{
    public int item_category;
    public string category_name;
}


public class ItemCategories : TableBase
{
    public enum CategoryType
    {
        Stamina = 1, // �X�^�~�i
        ReInForce,   // ����
        ExChange,    // ����
        Convex,      // ��
    }

    public static void CreateTable()
    {
        createQuery = "create table if not exists item_categories (item_category tinyint, category_name varchar,primary key(item_category))";
        RunQuery(createQuery);
    }

    public static void Set(ItemCategoryModel[] item_category_model_list)
    {
        foreach (ItemCategoryModel itemCategoryModel in item_category_model_list)
        {
            setQuery = "insert or replace into item_categories (item_category,category_name) values(" + itemCategoryModel.item_category + ", " + itemCategoryModel.category_name + " ) ";
            RunQuery(setQuery);
        }
    }

    public static Dictionary<int, ItemCategoryModel> GetItemCategory()
    {
        Dictionary<int, ItemCategoryModel> itemCategoryListModel = new();
        getQuery = "select * from item_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ItemCategoryModel itemCategoryModel = new();
            itemCategoryModel.item_category = int.Parse(dr["item_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
            itemCategoryListModel.Add(itemCategoryModel.item_category, itemCategoryModel);
        }
        return itemCategoryListModel;
    }

    public static ItemCategoryModel GetItemCategory(string item_category)
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
