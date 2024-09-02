using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ExchangeItemCategoryModel
{
    public int exchange_item_category;
    public string category_name;
}

public class ExchangeShopCategories : TableBase
{
    public static void CreateTable()
    {
        createQuery = "create table if not exists exchange_item_categories (exchange_item_category tinyint, category_name varchar,primary key(exchange_item_category))";
        RunQuery(createQuery);
    }

    public static void Set(ExchangeItemCategoryModel[] exchangeShopCategoryModelList)
    {
        foreach (ExchangeItemCategoryModel exchageShopCategoryModel in exchangeShopCategoryModelList)
        {
            setQuery = "insert or replace into exchange_item_categories (exchange_item_category,category_name) values(" + exchageShopCategoryModel.exchange_item_category + ",\"" + exchageShopCategoryModel.category_name + "\") ";
            RunQuery(setQuery);
        }
    }

    // �S�ẴJ�e�S���[���擾
    public static ExchangeItemCategoryModel[] GetExchangeShopCategoryAll()
    {
        List<ExchangeItemCategoryModel> exchangeShopCategoryList = new();
        getQuery = "select * from exchange_item_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ExchangeItemCategoryModel itemCategoryModel = new();
            itemCategoryModel.exchange_item_category = int.Parse(dr["exchange_item_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
            exchangeShopCategoryList.Add(itemCategoryModel);
        }
        return exchangeShopCategoryList.ToArray(); // List��z��ɕϊ����ĕԂ�
    }

    // �w�肵���J�e�S���[�������擾
    public static ExchangeItemCategoryModel GetExchangeModelCategory(int exchangeShopCategory)
    {
        ExchangeItemCategoryModel itemCategoryModel = new();
        getQuery = string.Format("select * from exchange_item_categories where exchange_item_category = {0}", exchangeShopCategory);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemCategoryModel.exchange_item_category = int.Parse(dr["exchange_item_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
        }
        return itemCategoryModel;
    }
}