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

    IEnumerator fadeCoroutine = null;   // �R���[�`���̊i�[�p

    private FadeManager() { }

    private static void Init()
    {
        // Canvas�쐬
        GameObject canvasObject = new GameObject("CanvasFade");
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;

        // Image�쐬(Canvas�̎q�ɂ���)
        image = new GameObject("ImageFade").AddComponent<Image>();
        image.transform.SetParent(canvas.transform, false);

        // ��ʒ������A���J�[�Ƃ��AImage�̃T�C�Y���X�N���[���T�C�Y�ɍ��킹��
        image.rectTransform.anchoredPosition = Vector3.zero;
        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        // �J�ڐ�V�[���ł��I�u�W�F�N�g��j�����Ȃ�
        DontDestroyOnLoad(canvas.gameObject);

        // �V���O���g���I�u�W�F�N�g��ێ�
        canvasObject.AddComponent<FadeManager>();
        instance = canvasObject.GetComponent<FadeManager>();
    }

    // �t�F�[�h�t���V�[���J�ڂ��s��
    public void LoadScene(string sceneName, float interval = 1f)
    {
        // �t�F�[�h���Ƀt�F�[�h���Ă΂�Ă����Ȃ��悤�ɑΏ�
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

        // �t�F�[�h�A�E�g
        while (time <= interval)
        {
            float fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            image.color = new Color(0f, 0f, 0f, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        // �V�[���񓯊����[�h
        yield return SceneManager.LoadSceneAsync(sceneName);

        // �t�F�[�h�C��
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
