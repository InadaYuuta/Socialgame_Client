using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Networking;

public class NewsManager : MonoBehaviour
{
    [SerializeField] GameObject newsPrefab, Content, newsPanel;
    GameObject newsClone;
    [SerializeField] TextMeshProUGUI newsContentText, newsDateText;

    enum DisplayNews { HELP, GACHA, EVENT, DEFECT }

    DisplayNews currentState = DisplayNews.HELP;

    NewsMasterModel[] helpNews, gachaNews, eventNews, defectNews;

    List<GameObject> helpNewsObj = new(), gachaNewsObj = new(), eventNewsObj = new(), defectNewsObj = new();
    List<NewsMasterModel> helpNewsModel = new(), gachaNewsModel = new(), eventNewsModel = new(), defectNewsModel = new();

    bool isUpdate = false; // 情報を更新したかどうか、連続で呼ばないように開くたびに一度ずつ更新

    Vector3 generatePos = new Vector3(0, 0, 0);

    void Start()
    {
        newsPanel.SetActive(false);
        GetMission();
    }

    void Update()
    {
        if (newsPanel.activeInHierarchy == true)
        {
            if (!isUpdate)
            {
                GetMission();
                isUpdate = true;
            }
        }
        else
        {
            isUpdate = false;
        }
    }

    // クローン作成
    void GenerateNews()
    {
        helpNews = NewsMaster.GetNewsCategory(1);
        gachaNews = NewsMaster.GetNewsCategory(2);
        eventNews = NewsMaster.GetNewsCategory(3);
        defectNews = NewsMaster.GetNewsCategory(4);

        CreateNews(1);
        CreateNews(2);
        CreateNews(3);
        CreateNews(4);
    }

    // 追加するものが重複していないかチェック
    bool CheckDuplication(NewsMasterModel target)
    {
        foreach (var news in helpNewsModel)
        {
            if (news.news_id == target.news_id)
            {
                return true;
            }
        }

        foreach (var news in gachaNewsModel)
        {
            if (news.news_id == target.news_id)
            {
                return true;
            }
        }

        foreach (var news in eventNewsModel)
        {
            if (news.news_id == target.news_id)
            {
                return true;
            }
        }

        foreach (var news in defectNewsModel)
        {
            if (news.news_id == target.news_id)
            {
                return true;
            }
        }

        return false;
    }

    void CreateNews(int category)
    {
        var News = NewsMaster.GetNewsCategory(category);
        foreach (var target in News)
        {
            if (CheckDuplication(target)) { continue; } // 重複していたら無視
            newsClone = Instantiate(newsPrefab, generatePos, Quaternion.identity);
            newsClone.transform.parent = Content.transform;
            NewsCloneManager cloneManager = newsClone.GetComponent<NewsCloneManager>();
            cloneManager.SetParamater(target.news_id);

            switch (category)
            {
                case 1:
                    helpNewsObj.Add(newsClone);
                    helpNewsModel.Add(target);
                    break;
                case 2:
                    gachaNewsObj.Add(newsClone);
                    gachaNewsModel.Add(target);
                    break;
                case 3:
                    eventNewsObj.Add(newsClone);
                    eventNewsModel.Add(target);
                    break;
                case 4:
                    defectNewsObj.Add(newsClone);
                    defectNewsModel.Add(target);
                    break;
                default:
                    break;
            }
        }
    }

    // お知らせを取得
    void GetMission()
    {
        List<IMultipartFormSection> newsFrom = new();
        Action afterAction = new(() => GenerateNews());
        StartCoroutine(CommunicationManager.ConnectServer(GameUtil.Const.GET_NEWS_URL, newsFrom, afterAction));
    }

    // お知らせの本文設定
    public void DisplayNewsContent(string context, string date)
    {
        newsContentText.text = context;
        newsDateText.text = date;
    }

    // 全てのお知らせを一旦非表示にする
    void HideNewsAll()
    {
        foreach (var target in helpNewsObj)
        {
            target.SetActive(false);
        }

        foreach (var target in gachaNewsObj)
        {
            target.SetActive(false);
        }

        foreach (var target in eventNewsObj)
        {
            target.SetActive(false);
        }

        foreach (var target in defectNewsObj)
        {
            target.SetActive(false);
        }
    }

    void SelectDisplayNews()
    {
        HideNewsAll();
        switch (currentState)
        {
            case DisplayNews.HELP:
                foreach (var target in helpNewsObj)
                {
                    target.SetActive(true);
                }
                break;
            case DisplayNews.GACHA:
                foreach (var target in gachaNewsObj)
                {
                    target.SetActive(true);
                }
                break;
            case DisplayNews.EVENT:
                foreach (var target in eventNewsObj)
                {
                    target.SetActive(true);
                }
                break;
            case DisplayNews.DEFECT:
                foreach (var target in defectNewsObj)
                {
                    target.SetActive(true);
                }
                break;
        }
    }

    public void PushOpenButton()
    {
        newsPanel.SetActive(true);
        currentState = DisplayNews.HELP;
        SelectDisplayNews();
    }

    public void PushBackButton()
    {
        newsPanel.SetActive(false);
    }

    public void OnPushHelpButton()
    {
        currentState = DisplayNews.HELP;
        SelectDisplayNews();
    }

    public void OnPushGachaButton()
    {
        currentState = DisplayNews.GACHA;
        SelectDisplayNews();
    }

    public void OnPushEventButton()
    {
        currentState = DisplayNews.EVENT;
        SelectDisplayNews();
    }

    public void OnPushDefectButton()
    {
        currentState = DisplayNews.DEFECT;
        SelectDisplayNews();
    }
}
