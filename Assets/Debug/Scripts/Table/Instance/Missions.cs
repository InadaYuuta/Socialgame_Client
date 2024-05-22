using System;
using System.Collections.Generic;

[Serializable]
public class MissionsModel
{
    public int mission_id;       // ミッションID
    public int achieved;         // 達成
    public int receipt;          // 受取
    public int progress;         // 進捗
    public string term;          // 期限
    public string validity_term; // 達成日
}

public class Missions : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists missions(user_id varchar,mission_id bigint,achieved tinyint,receipt tinyint,progress smallint,term varchar,validity_term varchar,primary key(user_id,mission_id))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(MissionsModel[] missions_model, string user_id)
    {
        foreach (MissionsModel mission in missions_model)
        {
            setQuery = "insert or replace into missions(user_id,mission_id ,achieved ,receipt ,progress ,term ,validity_term) values(\"" + user_id + "\"," + mission.mission_id + "," + mission.achieved + "," + mission.receipt + "," + mission.progress + "\"," + mission.term + "\"," + mission.validity_term + ")";
            RunQuery(setQuery);
        }
    }

    // 全てのプレゼントボックスデータを取得
    public static MissionsModel[] GetMissionDataAll()
    {
        List<MissionsModel> MissionList = new();
        getQuery = "select * from missions";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionsModel MissionModel = new();
            MissionModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionModel.achieved = int.Parse(dr["achieved"].ToString());
            MissionModel.receipt = int.Parse(dr["receipt"].ToString());
            MissionModel.progress = int.Parse(dr["progress"].ToString());
            MissionModel.term = dr["term"].ToString(); // TODO: 今後ミッション関連を作るときに日時で取得できるメソッドを追加する
            MissionModel.validity_term = dr["validity_term"].ToString(); // TODO: 今後ミッション関連を作るときに日時で取得できるメソッドを追加する
            MissionList.Add(MissionModel);
        }
        return MissionList.ToArray();
    }

    // 指定された進化後武器データを取得
    public static MissionsModel[] GetPresentBoxData(int mission_id)
    {
        List<MissionsModel> MissionList = new();
        getQuery = string.Format("select * from missions where mission_id={0}", mission_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionsModel MissionModel = new();
            MissionModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionModel.achieved = int.Parse(dr["achieved"].ToString());
            MissionModel.receipt = int.Parse(dr["receipt"].ToString());
            MissionModel.progress = int.Parse(dr["progress"].ToString());
            MissionModel.term = dr["term"].ToString(); // TODO: 今後ミッション関連を作るときに日時で取得できるメソッドを追加する
            MissionModel.validity_term = dr["validity_term"].ToString(); // TODO: 今後ミッション関連を作るときに日時で取得できるメソッドを追加する
            MissionList.Add(MissionModel);
        }
        return MissionList.ToArray();
    }
}