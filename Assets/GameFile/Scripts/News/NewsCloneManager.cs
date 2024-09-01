using TMPro;
using UnityEngine;

public class NewsCloneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    NewsManager newsManager;

    string title, context, date;

    NewsMasterModel newsModel;

    void Start()
    {
        newsManager = FindObjectOfType<NewsManager>();
    }

    // ê›íË
    public void SetParamater(int newsId)
    {
        newsModel = NewsMaster.GetNewsData(newsId);
        title = newsModel.news_name;
        context = newsModel.news_content;
        date = newsModel.created;

        titleText.text = title;
    }

    public void OnPushNewsButton()
    {
        newsManager.DisplayNewsContent(context, date);
    }
}
