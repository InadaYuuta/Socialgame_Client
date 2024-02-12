using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ExchangeShopCategoryModel
{
    public int exchange_shop_category;
    public string category_name;
}

public class ExchangeShopCategories : TableBase
{
    public enum CategoryType
    {
        Stamina = 1, // スタミナ
        ReInForce,   // 強化
        ExChange,    // 交換
        Convex,      // 凸
    }

    public static void CreateTable()
    {
        createQuery = "create table if not exists exchange_shop_categories (exchange_item_category tinyint, category_name varchar,primary key(exchange_item_category))";
        RunQuery(createQuery);
    }

    public static void Set(ExchangeShopCategoryModel[] exchangeShopCategoryModelList)
    {
        foreach (ExchangeShopCategoryModel exchageShopCategoryModel in exchangeShopCategoryModelList)
        {
            setQuery = "insert or replace into exchange_shop_categories (exchange_item_category,category_name) values(" + exchageShopCategoryModel.exchange_shop_category + ", \"" + exchageShopCategoryModel.category_name + "\") ";
            RunQuery(setQuery);
        }
    }

    // 全てのカテゴリーを取得
    public static ExchangeShopCategoryModel[] GetExchangeShopCategoryAll()
    {
        List<ExchangeShopCategoryModel> exchangeShopCategoryList = new();
        getQuery = "select * from exchange_shop_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ExchangeShopCategoryModel itemCategoryModel = new();
            itemCategoryModel.exchange_shop_category = int.Parse(dr["exchange_shop_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
            exchangeShopCategoryList.Add(itemCategoryModel);
        }
        return exchangeShopCategoryList.ToArray(); // Listを配列に変換して返す
    }

    // 指定したカテゴリーだけを取得
    public static ExchangeShopCategoryModel GetExchangeModelCategory(string exchangeShopCategory)
    {
        ExchangeShopCategoryModel itemCategoryModel = new();
        getQuery = "select * from exchange_shop_categories where exchange_shop_category" + exchangeShopCategory;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemCategoryModel.exchange_shop_category = int.Parse(dr["exchange_shop_category"].ToString());
            itemCategoryModel.category_name = dr["category_name"].ToString();
        }
        return itemCategoryModel;
    }
}