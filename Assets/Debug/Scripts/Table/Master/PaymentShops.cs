using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class PaymentShopModel
{
    public int product_id;         // 商品ID
    public string product_name;    // 商品名
    public int price;              // 販売価格
    public int paid_currency;      // 有償通貨数
    public int bonus_currency;     // おまけ無償通貨数
}

public class PaymentShops : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists payment_shops(product_id bigint,product_name varchar,price smallint,paid_currency smallint,bonus_currency smallint,unique(product_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(PaymentShopModel[] payment_model_list)
    {
        foreach (PaymentShopModel paymentModel in payment_model_list)
        {
            setQuery = "insert or replace into payment_shops(product_id,product_name,price,paid_currency,bonus_currency) values(" + paymentModel.product_id + ",\"" + paymentModel.product_name + "\"," + paymentModel.price + "," + paymentModel.paid_currency + "," + paymentModel.bonus_currency + ")";
            RunQuery(setQuery);
        }
    }

    // レコード取得処理
    //public static Dictionary<int, PaymentShopModel> GetPaymentShops()
    //{
    //    Dictionary<int, PaymentShopModel> list = new();
    //    getQuery = "select * from payment_shops";
    //    DataTable dataTable = RunQuery(getQuery);
    //    PaymentShopModel paymentShopsModel = new();
    //    foreach (DataRow dr in dataTable.Rows)
    //    {
    //        paymentShopsModel.product_id = int.Parse(dr["product_id"].ToString());
    //        paymentShopsModel.product_name = dr["product_name"].ToString();
    //        paymentShopsModel.price = int.Parse(dr["price"].ToString());
    //        paymentShopsModel.paid_currency = int.Parse(dr["paid_currency"].ToString());
    //        paymentShopsModel.bonus_currency = int.Parse(dr["bonus_currency"].ToString());
    //        list.Add(paymentShopsModel.product_id, paymentShopsModel);
    //    }
    //    return list;
    //}


    public static PaymentShopModel[] GetPaymentShops()
    {
        List<PaymentShopModel> list = new List<PaymentShopModel>();
        getQuery = "select * from payment_shops";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PaymentShopModel paymentShopsModel = new PaymentShopModel();
            paymentShopsModel.product_id = int.Parse(dr["product_id"].ToString());
            paymentShopsModel.product_name = dr["product_name"].ToString();
            paymentShopsModel.price = int.Parse(dr["price"].ToString());
            paymentShopsModel.paid_currency = int.Parse(dr["paid_currency"].ToString());
            paymentShopsModel.bonus_currency = int.Parse(dr["bonus_currency"].ToString());
            list.Add(paymentShopsModel);
        }
        return list.ToArray(); // Listを配列に変換して返す
    }


    public static PaymentShopModel GetPaymentShop(string product_id)
    {
        PaymentShopModel paymentShopsModel = new();
        getQuery = "select * from payment_shops where product_id =" + product_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            paymentShopsModel.product_id = int.Parse(dr["product_id"].ToString());
            paymentShopsModel.product_name = dr["product_name"].ToString();
            paymentShopsModel.price = int.Parse(dr["price"].ToString());
            paymentShopsModel.paid_currency = int.Parse(dr["paid_currency"].ToString());
            paymentShopsModel.bonus_currency = int.Parse(dr["bonus_currency"].ToString());
        }
        return paymentShopsModel;
    }
}
