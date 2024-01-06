using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using TMPro;
using System.Collections.Generic;

public class TestScript : MonoBehaviour
{
    [SerializeField] TMP_InputField input;

    string url = "http://localhost/api/view";
    [SerializeField] string name = "ユウタ";

    private void Awake()
    {
        Debug.Log("ユニークID" + SystemInfo.deviceUniqueIdentifier);
    }

    private void Update()
    {
        var a = Keyboard.current;
        var b = a.digit1Key.wasPressedThisFrame;
        var c = a.digit2Key.wasPressedThisFrame;
        if (b)
        {
            StartCoroutine(Resist());
        }
        if (c)
        {
            StartCoroutine(PostTest());
        }
    }

    public void TestResist()
    {
        name = input.text;
        StartCoroutine(PostTest());
    }

    IEnumerator PostTest()
    {
       // var deviceId = SystemInfo.deviceUniqueIdentifier;

        List<IMultipartFormSection> form = new List<IMultipartFormSection>(); // WWWFormの新しいやり方
        form.Add(new MultipartFormDataSection("un", name));
      //  form.Add(new MultipartFormDataSection("did", deviceId));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            webRequest.timeout = 10; // 10秒でタイムアウト
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.downloadHandler != null)
                {
                    string text = webRequest.downloadHandler.text;
                    Debug.Log(text);
                }
            }
        }
    }

    IEnumerator Resist()
    {
        // UnityWebRequestを作る
        UnityWebRequest request = UnityWebRequest.Get(url);

        // SendWebREquestで送受信する
        yield return request.SendWebRequest();

        string text = request.downloadHandler.text; // 結果を保存

        // isNetworkErrorとisHttpErrorでエラーを判定する
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log(request.error); // エラーの内容を表示
        }
        else
        {
            Debug.Log(text); // 結果を返す
        }
    }
}
