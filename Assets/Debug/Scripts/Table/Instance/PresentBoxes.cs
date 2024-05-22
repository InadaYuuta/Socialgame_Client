using System;
using System.Collections.Generic;

[Serializable]
public class PresentBoxModel
{
    public int present_id;             // プレゼントID
    public int reward_category;        // 報酬のカテゴリー
    public string present_box_reward;  // 報酬の内容
    public string receive_reason;      // 受け取った理由
    public int receipt;                // 受取
    public string display;             // 表示期限
}

public class PresentBoxes : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists present_boxes(user_id varchar,present_id bigint,reward_category tinyint,present_box_reward text,receive_reason varchar,receipt tinyint,display varchar,primary key(user_id,present_id))";
        RunQuery(createQuery);
        //// インデックス作成 TODO:今後サーバー側でリワードカテゴリーをインデックスにする場合はコメントアウト解除
        //createQuery = "CREATE INDEX IF NOT EXISTS reward_category_index ON present_boxes(reward_category);";
        //RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(PresentBoxModel[] present_box_model, string user_id)
    {
        foreach (PresentBoxModel present_box in present_box_model)
        {
            setQuery = "insert or replace into present_boxes(user_id,reward_category,present_box_reward,receive_reason,receipt,display) values(\"" + user_id + "\"," + present_box.present_id + "," + present_box.reward_category + "\"," + present_box.present_box_reward + "\"," + present_box.receive_reason + "," + present_box.receipt + "\"," + present_box.display + ")";
            RunQuery(setQuery);
        }
    }

    // 全てのプレゼントボックスデータを取得
    public static PresentBoxModel[] GetPresentBoxDataAll()
    {
        List<PresentBoxModel> PresentBoxList = new();
        getQuery = "select * from present_boxes";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PresentBoxModel PresentBoxModel = new();
            PresentBoxModel.present_id = int.Parse(dr["present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.display = dr["display"].ToString(); // TODO: 今後プレゼント関連を作るときに日時で取得できるメソッドを追加する
            PresentBoxList.Add(PresentBoxModel);
        }
        return PresentBoxList.ToArray();
    }

    // 指定された進化後武器データを取得
    public static PresentBoxModel GetPresentBoxData(int present_id)
    {
        PresentBoxModel PresentBoxModel = new();
        getQuery = string.Format("select * from present_boxes where present_id={0}", present_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PresentBoxModel.present_id = int.Parse(dr["present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.display = dr["display"].ToString(); // TODO: 今後プレゼント関連を作るときに日時で取得できるメソッドを追加する
        }
        return PresentBoxModel;
    }
}