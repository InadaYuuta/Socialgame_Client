using System;
using System.Collections.Generic;

[Serializable]
public class GachaLogModel
{
    public int gacha_log_id;
    public int gacha_id;
    public int weapon_id;
    public string created;
}

public class GachaLogs : TableBase
{
    // テーブル作成
    public static void Createtable()
    {
        createQuery = "create table if not exists gacha_logs( gacha_log_id bigint,gacha_id bigint, weapon_id bigint,created varchar, primary key(gacha_log_id))";
        RunQuery(createQuery);
    }

    // レコード登録
    public static void Set(GachaLogModel[] gacha_log_model_list)
    {
        if (gacha_log_model_list == null) { return; }
        foreach (GachaLogModel gacha_logs in gacha_log_model_list)
        {
            setQuery = "insert or replace into gacha_logs(gacha_log_id,gacha_id,weapon_id,created) values(" + gacha_logs.gacha_log_id + "," + gacha_logs.gacha_id + "," + gacha_logs.weapon_id + ",\"" + gacha_logs.created + "\")";
            RunQuery(setQuery);
        }
    }

    // 指定したユーザーIDのガチャ履歴をすべて取得
    public static GachaLogModel[] GetGacaLogDataAll()
    {
        List<GachaLogModel> gachaLogList = new();
        getQuery = string.Format("select * from gacha_logs");

        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            GachaLogModel gachaWeaponModel = new();
            gachaWeaponModel.gacha_log_id = int.Parse(dr["gacha_log_id"].ToString());
            gachaWeaponModel.gacha_id = int.Parse(dr["gacha_id"].ToString());
            gachaWeaponModel.weapon_id = int.Parse(dr["weapon_id"].ToString());
            DateTime dateTime = DateTime.Parse(dr["created"].ToString());
            string formattedDateString = dateTime.ToString("yyyy年MM月dd日-HH時mm分");
            gachaWeaponModel.created = formattedDateString;
            gachaLogList.Add(gachaWeaponModel);
        }
        return gachaLogList.ToArray();
    }
}
