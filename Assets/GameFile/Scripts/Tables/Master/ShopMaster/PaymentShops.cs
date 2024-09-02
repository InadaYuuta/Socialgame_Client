using System;
using System.Collections.Generic;

[Serializable]
public class PaymentShopModel
{
    public int product_id;         // ���iID
    public string product_name;    // ���i��
    public int price;              // �̔����i
    public int paid_currency;      // �L���ʉݐ�
    public int bonus_currency;     // ���܂������ʉݐ�
}

public class PaymentShops : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists payment_shops(product_id bigint,product_name varchar,price smallint,paid_currency smallint,bonus_currency smallint,primary key(product_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(PaymentShopModel[] payment_model_list)
    {
        foreach (PaymentShopModel paymentModel in payment_model_list)
        {
            setQuery = "insert or replace into payment_shops(product_id,product_name,price,paid_currency,bonus_currency) values(" + paymentModel.product_id + ",\"" + paymentModel.product_name + "\"," + paymentModel.price + "," + paymentModel.paid_currency + "," + paymentModel.bonus_currency + ")";
            RunQuery(setQuery);
        }
    }

    // �S�Ă̏��i���擾
    public static PaymentShopModel[] GetPaymentShopDataAll()
    {
        List<PaymentShopModel> list = new();
        getQuery = "select * from payment_shops";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PaymentShopModel paymentShopsModel = new();
            paymentShopsModel.product_id = int.Parse(dr["product_id"].ToString());
            paymentShopsModel.product_name = dr["product_name"].ToString();
            paymentShopsModel.price = int.Parse(dr["price"].ToString());
            paymentShopsModel.paid_currency = int.Parse(dr["paid_currency"].ToString());
            paymentShopsModel.bonus_currency = int.Parse(dr["bonus_currency"].ToString());
            list.Add(paymentShopsModel);
        }
        return list.ToArray(); // List��z��ɕϊ����ĕԂ�
    }

    // �w�肳�ꂽ���iID�̏��i�������擾
    public static PaymentShopModel GetPaymentShopData(int product_id)
    {
        PaymentShopModel paymentShopsModel = new();
        getQuery = string.Format("select * from payment_shops where product_id = {0}", product_id);

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