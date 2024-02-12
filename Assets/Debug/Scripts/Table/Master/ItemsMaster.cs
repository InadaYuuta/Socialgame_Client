using System;
using System.Collections.Generic;

[Serializable]
public class ItemMasterModel
{
    public int item_id;
    public string item_name;
    public int item_category;
}

public class ItemsMaster : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists item_masters(item_id bigint,item_name varchar,item_category tinyint,primary key(item_id),index(item_category))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(ItemMasterModel[] item_master_model_list)
    {
        foreach (ItemMasterModel itemMasterModel in item_master_model_list)
        {
            setQuery = "insert or replace into item_masters(item_id,item_name,item_category) values(" + itemMasterModel.item_id + ",\"" + itemMasterModel.item_name + "," + itemMasterModel.item_category + ")";
            RunQuery(createQuery);
        }
    }

    // 全てのアイテムデータを取得
    public static ItemMasterModel[] GetItemMasterDataAll()
    {
        List<ItemMasterModel> itemMasterList = new();
        getQuery = "select * from item_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ItemMasterModel itemMasterModel = new();
            itemMasterModel.item_id = int.Parse(dr["item_id"].ToString());
            itemMasterModel.item_name = dr["Name"].ToString();
            itemMasterModel.item_category = int.Parse(dr["item_category"].ToString());
            itemMasterList.Add(itemMasterModel);
        }
        return itemMasterList.ToArray();
    }

    // 指定されたアイテムのデータのみを取得
    public static ItemMasterModel GetItemMasterData(int item_id)
    {
        ItemMasterModel itemMasterModel = new();
        getQuery = "select * from item_masters where item_id =" + item_id;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemMasterModel.item_id = int.Parse(dr["item_id"].ToString());
            itemMasterModel.item_name = dr["item_name"].ToString();
            itemMasterModel.item_category = int.Parse(dr["item_category"].ToString());
        }
        return itemMasterModel;
    }
}
