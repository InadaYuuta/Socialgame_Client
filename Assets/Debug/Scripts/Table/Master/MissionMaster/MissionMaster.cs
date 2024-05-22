using System;
using System.Collections.Generic;

[Serializable]
public class MissionMasterModel
{
    public int mission_id;                // ミッションID
    public int next_mission_id;           // 次のミッションID
    public string mission_name;           // ミッション名
    public string mission_content;        // ミッションの内容
    public int mission_category;          // ミッションのカテゴリー
    public int reward_category;           // 報酬のカテゴリー
    public string mission_reward;         // ミッションの報酬
    public string achievement_condition;  // 達成条件(と数値)
    public string period_end;             // 終了日時
}

public class MissionMaster : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists mission_masters(mission_id bigint,next_mission_id bigint,mission_name text,mission_content text,mission_category tinyint,reward_category tinyint,mission_reward text,achievement_condition text,period_end varchar,primary key(mission_id))";
        RunQuery(createQuery);
        // インデックス作成
        createQuery = "CREATE INDEX IF NOT EXISTS mission_category_index ON mission_masters(mission_category);";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(MissionMasterModel[] mission_master_model)
    {
        foreach (MissionMasterModel mission_master in mission_master_model)
        {
            setQuery = "insert or replace into mission_masters(mission_id,next_mission_id,mission_name,mission_content,mission_category ,reward_category ,mission_reward ,achievement_condition ,period_end ) values(\"" + mission_master.mission_id + "," + mission_master.next_mission_id + "\"," + mission_master.mission_name + "\"," + mission_master.mission_content + "\"," + mission_master.mission_category + "," + mission_master.reward_category + "\"," + mission_master.mission_reward + "\"," + mission_master.achievement_condition + "\"," + mission_master.period_end + ")";
            RunQuery(setQuery);
        }
    }

    // 全てのミッションデータを取得
    public static MissionMasterModel[] GetMissionMasterDataAll()
    {
        List<MissionMasterModel> MissionMasterList = new();
        getQuery = "select * from mission_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionMasterModel MissionMasterModel = new();
            MissionMasterModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionMasterModel.next_mission_id = int.Parse(dr["next_mission_id"].ToString());
            MissionMasterModel.mission_name = dr["mission_name"].ToString();
            MissionMasterModel.mission_content = dr["mission_content"].ToString();
            MissionMasterModel.mission_category = int.Parse(dr["mission_category"].ToString());
            MissionMasterModel.reward_category = int.Parse(dr["reward_category"].ToString());
            MissionMasterModel.mission_reward = dr["mission_reward"].ToString();
            MissionMasterModel.achievement_condition = dr["achievement_condition"].ToString();
            MissionMasterModel.period_end = dr["period_end"].ToString(); // TODO: 今後ミッション関連を作るときに日時で取得できるメソッドを追加する
            MissionMasterList.Add(MissionMasterModel);
        }
        return MissionMasterList.ToArray();
    }

    // 指定されたミッションデータを取得
    public static MissionMasterModel[] GetMissionMasterData(int mission_id)
    {
        List<MissionMasterModel> MissionMasterList = new();
        getQuery = string.Format("select * from mission_masters where mission_id={0}", mission_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionMasterModel MissionMasterModel = new();
            MissionMasterModel.mission_id = int.Parse(dr["mission_id"].ToString());
            MissionMasterModel.next_mission_id = int.Parse(dr["next_mission_id"].ToString());
            MissionMasterModel.mission_name = dr["mission_name"].ToString();
            MissionMasterModel.mission_content = dr["mission_content"].ToString();
            MissionMasterModel.mission_category = int.Parse(dr["mission_category"].ToString());
            MissionMasterModel.reward_category = int.Parse(dr["reward_category"].ToString());
            MissionMasterModel.mission_reward = dr["mission_reward"].ToString();
            MissionMasterModel.achievement_condition = dr["achievement_condition"].ToString();
            MissionMasterModel.period_end = dr["period_end"].ToString(); // TODO: 今後ミッション関連を作るときに日時で取得できるメソッドを追加する
            MissionMasterList.Add(MissionMasterModel);
        }
        return MissionMasterList.ToArray();
    }
}