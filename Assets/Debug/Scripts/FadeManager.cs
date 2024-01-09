using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    static Canvas canvas;
    static Image image;

    static FadeManager instance;
    public static FadeManager Instance
    {
        get
        {
            if (instance == null) { Init(); }
            return instance;
        }
    }

    IEnumerator fadeCoroutine = null;   // コルーチンの格納用

    private FadeManager() { }

    private static void Init()
    {
        // Canvas作成
        GameObject canvasObject = new GameObject("CanvasFade");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;

        // Image作成(Canvasの子にする)
        image = new GameObject("ImageFade").AddComponent<Image>();
        image.transform.SetParent(canvas.transform, false);

        // 画面中央をアンカーとし、Imageのサイズをスクリーンサイズに合わせる
        image.rectTransform.anchoredPosition = Vector3.zero;
        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        // 遷移先シーンでもオブジェクトを破棄しない
        DontDestroyOnLoad(canvas.gameObject);

        // シングルトンオブジェクトを保持
        canvasObject.AddComponent<FadeManager>();
        instance = canvasObject.GetComponent<FadeManager>();
    }

    // フェード付きシーン遷移を行う
    public void LoadScene(string sceneName, float interval = 1f)
    {
        // フェード中にフェードが呼ばれても問題ないように対処
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = null;

        fadeCoroutine = Fade(sceneName, interval);
        StartCoroutine(fadeCoroutine);
    }
    IEnumerator Fade(string sceneName, float interval)
    {
        float time = 0f;
        canvas.enabled = true;

        // フェードアウト
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            image.color = new Color(0f, 0f, 0f, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        // シーン非同期ロード
        yield return SceneManager.LoadSceneAsync(sceneName);

        // フェードイン
        time = 0f;
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            image.color = new Color(0f, 0f, 0f, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        canvas.enabled = false;
    }
}
