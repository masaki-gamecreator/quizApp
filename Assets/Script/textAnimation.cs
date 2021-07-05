using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textAnimation : MonoBehaviour
{
    private const int ANCHOREDPOSITION_Y = -50;
    private const int WIDTH = 440;
    private const int HEIGHT = 170;
    // rectTransform
    public RectTransform rect;
    // 1fで動かす距離
    public float anchorPosition_y;
    // text拡大表示の％
    public int parsent;

    private bool isAminEnd;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(textAnim());
    }
    void Update()
    {
        if (quizView.getIsNextQueztion())
        {
            StartCoroutine(textAnim());
        }
    }
    /// <summary>
    /// 第問数テキストアニメ
    /// </summary>
    /// <returns></returns>
    private IEnumerator textAnim()
    {
        quizView.setIsNextQuestion(false);
        // 文字を消す。
        rect.sizeDelta = new Vector2(0, 0);
        rect.anchoredPosition = new Vector2(0, -300);
        // 少し待ってから文字を表示
        yield return new WaitForSeconds(0.5f);
        // 第問数のテキスト移動処理
        while (rect.sizeDelta.x < WIDTH && rect.sizeDelta.y < HEIGHT)
        {
            rect.sizeDelta += new Vector2(WIDTH / parsent, HEIGHT / parsent);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2.0f);
        // 第問数のテキスト移動処理
        while (rect.anchoredPosition.y < ANCHOREDPOSITION_Y)
        {
            rect.anchoredPosition += new Vector2(0, anchorPosition_y);//毎フレームy座標を10ずつプラス
            rect.sizeDelta += new Vector2(0, -(HEIGHT / 34));
            yield return new WaitForSeconds(0.01f);
        }
    }
    // アニメ完了かどうか取得
    public static float getEndTextAnimTime()
    {
        return 3.5f;
    }
}
