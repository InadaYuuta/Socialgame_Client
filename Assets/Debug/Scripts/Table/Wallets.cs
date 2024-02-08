using System;
using System.Diagnostics;

[Serializable]
public class WalletsModel
{
    public int free_amount; // 無償通貨
    public int paid_amount; // 有償通貨
    public int max_amount;  // 通貨所持上限
}

public class Wallets : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists wallets(user_id varchar,free_amount mediumint,paid_amount mediumint,max_amount mediumint,primary key(user_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(WalletsModel walles, string user_id)
    {
        Debug.WriteLine("なんやこれ");
        if (walles == null || user_id == null) { return; }
        setQuery = "insert or replace into wallets(user_id,free_amount,paid_amount,max_amount) values(\"" + user_id + "\"," + walles.free_amount + "," + walles.paid_amount + "," + walles.max_amount + ")";
        RunQuery(setQuery);
    }

    // レコード取得処理
    public static WalletsModel Get()
    {
        getQuery = "select * from wallets";
        DataTable dataTable = RunQuery(getQuery);
        WalletsModel walletsModel = new();
        foreach (DataRow dr in dataTable.Rows)
        {
            walletsModel.free_amount = int.Parse(dr["free_amount"].ToString());
            walletsModel.paid_amount = int.Parse(dr["paid_amount"].ToString());
            walletsModel.max_amount = int.Parse(dr["max_amount"].ToString());
        }
        return walletsModel;
    }
}
