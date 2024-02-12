//using System;

//[Serializable]
//public class ItemsModel
//{
//    public int item_id;               // アイテムID
//    public int has_stamina_item_num;  // スタミナアイテムの所持数
//    public int has_exchange_item_num; // 交換アイテムの所持数
//}

//public class Items : TableBase
//{
//    // テーブル作成
//    public static void CreateTable()
//    {
//        createQuery = "create table if not exists items(item_id bigint,has_stamina_item_num mediumint,has_exchange_item_num mediumint)";
//        RunQuery(createQuery);
//    }

//    // レコード登録処理
//    public static void Set(ItemsModel itemsModel)
//    {
//        setQuery = "insert or replace into wallets(item_id,has_stamina_item_num,has_exchange_item_num) values(" + itemsModel.item_id + "," + itemsModel.has_stamina_item_num + "," + itemsModel.has_exchange_item_num + ")";
//        RunQuery(setQuery);
//    }

//    // レコード取得処理
//    public static ItemsModel Get()
//    {
//        getQuery = "select * from wallets";
//        DataTable dataTable = RunQuery(getQuery);
//        ItemsModel itemsModel = new();
//        foreach (DataRow dr in dataTable.Rows)
//        {
//            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
//            itemsModel.has_stamina_item_num = int.Parse(dr["has_stamina_item_num"].ToString());
//            itemsModel.has_exchange_item_num = int.Parse(dr["has_exchange_item_num"].ToString());
//        }
//        return itemsModel;
//    }
//}
