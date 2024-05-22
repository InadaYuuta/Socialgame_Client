using System;
using System.Collections.Generic;

[Serializable]
public class ExchangeShopModel
{
    public int exchange_product_id;       // �����A�C�e���̏��iID
    public int exchange_item_category;    // �����A�C�e���̃J�e�S���[
    public string exchange_item_name;     // �����A�C�e���̖��O(���i��)
    public int exchange_item_amount;      // �����ł��炦��A�C�e���̗�
    public int exchange_price;            // �����ɕK�v�ȃA�C�e����
}

public class ExchangeShops : TableBase
{
    // �e�[�u���쐬
    public static void CreateTable()
    {
        createQuery = "create table if not exists exchange_item_shops(exchange_product_id bigint,exchange_item_category tinyint,exchange_item_name varchar,exchange_item_amount smallint,exchange_price smallint,primary key(exchange_product_id))";
        RunQuery(createQuery);
    }

    // ���R�[�h�o�^����
    public static void Set(ExchangeShopModel[] exchange_shop_model_list)
    {
        foreach (ExchangeShopModel exchangeShopModel in exchange_shop_model_list)
        {
            setQuery = "insert or replace into exchange_item_shops(exchange_product_id,exchange_item_category,exchange_item_name,exchange_item_amount,exchange_price) values(" + exchangeShopModel.exchange_product_id + "," + exchangeShopModel.exchange_item_category + ",\"" + exchangeShopModel.exchange_item_name + "\"," + exchangeShopModel.exchange_item_amount + "," + exchangeShopModel.exchange_price + ")";
            RunQuery(setQuery);
        }
    }

    // �S�Ă̏��i���擾
    public static ExchangeShopModel[] GetExchangeShopDataAll()
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
        return list.ToArray(); // List��z��ɕϊ����ĕԂ�
    }

    // �w�肳�ꂽ���iID�̏��i�������擾
    public static ExchangeShopModel GetExchangeShopData(int exchange_product_id)
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