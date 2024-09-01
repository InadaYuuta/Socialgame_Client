using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemCategoryModel
{
    public int item_category;    // �A�C�e���J�e�S���[
    public string category_name; // �J�e�S���[�̖��O
}

public class ItemCategories : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists item_categories (item_category tinyint, category_name varchar,primary key(item_category))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(ItemCategoryModel[] item_category_model_list)
    {
        foreach (ItemCategoryModel itemCategoryModel in item_category_model_list)
        {
            setQuery = "insert or replace into item_categories (item_category,category_name) values(" + itemCategoryModel.item_category + ",\"" + itemCategoryModel.category_name + "\") ";
            RunQuery(setQuery);
        }
    }

    // �S�ẴJ�e�S���[���擾
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
        return list.ToArray(); // List��z��ɕϊ����ĕԂ�
    }

    // �w�肵���J�e�S���[�������擾
    public static ItemCategoryModel GetItemCategory(int item_category)
    {
        ItemCategoryModel itemCategoryModel = new();
        getQuery = string.Format("select * from item_categories where item_category = {0}", item_category);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemCategoryModel.item_category = int.Parse(dr["item_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
        }
        return itemCategoryModel;
    }
}