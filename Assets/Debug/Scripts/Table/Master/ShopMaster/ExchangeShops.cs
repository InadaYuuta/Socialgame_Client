using System;
using System.Collections.Generic;

[Serializable]
public class ExchangeShopModel
{
    public int exchange_product_id;       // 交換アイテムの商品ID
    public int exchange_item_category;    // 交換アイテムのカテゴリー
    public string exchange_item_name;     // 交換アイテムの名前(商品名)
    public int exchange_item_amount;      // 交換でもらえるアイテムの量
    public int exchange_price;            // 交換に必要なアイテム数
}

public class ExchangeShops : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists exchange_item_shops(exchange_product_id bigint,exchange_item_category tinyint,exchange_item_name varchar,exchange_item_amount smallint,exchange_price smallint,primary key(exchange_product_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(ExchangeShopModel[] exchange_shop_model_list)
    {
        foreach (ExchangeShopModel exchangeShopModel in exchange_shop_model_list)
        {
            setQuery = "insert or replace into exchange_item_shops(exchange_product_id,exchange_item_category,exchange_item_name,exchange_item_amount,exchange_price) values(" + exchangeShopModel.exchange_product_id + "," + exchangeShopModel.exchange_item_category + ",\"" + exchangeShopModel.exchange_item_name + "\"," + exchangeShopModel.exchange_item_amount + "," + exchangeShopModel.exchange_price + ")";
            RunQuery(setQuery);
        }
    }

    // 全ての商品を取得
    public static ExchangeShopModel[] GetExchangeShopAll()
    {
        List<ExchangeShopModel> list = new();
        getQuery = "select * from exchange_item_shops";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ExchangeShopModel exchangeShopModel = new();
            exchangeShopModel.exchange_product_id = int.Parse(dr["exchange_product_id"].ToString());
            exchangeShopModel.exchange_item_category = int.Parse(dr["exchange_item_category"].ToString());
            exchangeShopModel.exchange_item_name = dr["exchange_item_name"].ToString();
            exchangeShopModel.exchange_item_amount = int.Parse(dr["exchange_item_amount"].ToString());
            exchangeShopModel.exchange_price = int.Parse(dr["exchange_price"].ToString());
            list.Add(exchangeShopModel);
        }
        return list.ToArray(); // Listを配列に変換して返す
    }

    // 指定された商品IDの商品だけを取得
    public static ExchangeShopModel GetExchangeShop(int exchange_product_id)
    {
        ExchangeShopModel exchangeShopModel = new();
        getQuery = "select * from exchange_item_shops where exchange_product_id =" + exchange_product_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            exchangeShopModel.exchange_product_id = int.Parse(dr["exchange_product_id"].ToString());
            exchangeShopModel.exchange_item_category = int.Parse(dr["exchange_item_category"].ToString());
            exchangeShopModel.exchange_item_name = dr["exchange_item_name"].ToString();
            exchangeShopModel.exchange_item_amount = int.Parse(dr["exchange_item_amount"].ToString());
            exchangeShopModel.exchange_price = int.Parse(dr["exchange_price"].ToString());
        }
        return exchangeShopModel;
    }
}