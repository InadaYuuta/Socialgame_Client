using System;
using System.Collections.Generic;

[Serializable]
public class ItemsModel
{
    public int item_id;  // アイテムID
    public int item_num; // アイテムの所持数
    public int used_num; // アイテムの使用回数
}

public class Items : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists items(item_id bigint,item_num mediumint,used_num mediumint,primary key(item_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(ItemsModel[] items_model_list)
    {
        foreach (ItemsModel itemsModel in items_model_list)
        {
            setQuery = "insert or replace into items(item_id,item_num,used_num) values(" + itemsModel.item_id + "," + itemsModel.item_num + "," + itemsModel.used_num + ")";
            RunQuery(setQuery);
        }
    }

    // アイテムのデータ全取得
    public static ItemsModel[] GetItemDataAll()
    {
        List<ItemsModel> list = new();
        getQuery = "select * from items";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            ItemsModel itemsModel = new();
            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
            itemsModel.item_num = int.Parse(dr["item_num"].ToString());
            itemsModel.used_num = int.Parse(dr["used_num"].ToString());
            list.Add(itemsModel);
        }
        return list.ToArray(); // Listを配列に変換して返す
    }

    // 指定された商品IDの商品だけを取得
    public static ItemsModel GetItemData(int item_id)
    {
        ItemsModel itemsModel = new();
        getQuery = string.Format("select * from items where item_id = {0}", item_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
            itemsModel.item_num = int.Parse(dr["item_num"].ToString());
            itemsModel.used_num = int.Parse(dr["used_num"].ToString());
        }
        return itemsModel;
    }

    // 指定された武器IDのアイテムを取得
    public static ItemsModel GetWeaponItemData(int weapon_id)
    {
        int item_id = CheckWeaponIdItem(weapon_id);
        ItemsModel itemsModel = new();
        getQuery = string.Format("select * from items where item_id = {0}", item_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            itemsModel.item_id = int.Parse(dr["item_id"].ToString());
            itemsModel.item_num = int.Parse(dr["item_num"].ToString());
            itemsModel.used_num = int.Parse(dr["used_num"].ToString());
        }
        return itemsModel;
    }

    // 武器ごとに凸アイテムのIDを返す
    // TODO:これ不便だから今後諸々の実装が終わったら、テーブル設計を見直す。　具体的にはWeaponsテーブルに対応する凸アイテムのID情報を入れる
    static int CheckWeaponIdItem(int weapon_id)
    {
        int item_id = 0;

        switch (weapon_id)
        {
            case 1010001:
                item_id = 40002; // 普通の剣凸アイテム
                break;
            case 1020001:
                item_id = 40003; // 普通の弓凸アイテム
                break;
            case 1030001:
                item_id = 40004; // 普通の槍凸アイテム
                break;
            case 2020001:
                item_id = 40005; // 強い弓凸アイテム
                break;
            case 3010001:
                item_id = 40006; // めっちゃ強い剣凸アイテム
                break;
        }

        return item_id;
    }

}
