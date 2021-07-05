using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class quizView : MonoBehaviour
{
    // 何問目のテキストを入れる
    public Text quizNumber;
    // 残り時間を計測
    public Text limitTimer;
    // 問題テキスト
    public Text questionText;
    // 問題文１文字の表示スピード
    public float questionTextSpeed;
    // マスターデータ
    [SerializeField] MstItems mstDatas;
    // 回答１
    public Text answer1Text;
    // 回答２
    public Text answer2Text;
    // 回答３
    public Text answer3Text;
    // 回答４
    public Text answer4Text;
    // 回答後パネル
    public GameObject panel;
    // 正解表示
    public GameObject correctAnswer;
    // 不正解表示
    public GameObject inCorrectAnswer;
    // 正解テキスト
    public Text correntAnserText;
    // 次へボタン文言
    public Text nextBtnText;
    // 残り時間表記
    private double timer;
    // 時間切れかどうかフラグ
    private bool isLimit;
    // 回答ボタンを押したかどうか
    private bool isAnserBtn;
    // 何問目かをカウントする変数
    private int questionCount;
    // 問題文を全て表示したかどうか
    private bool isAllText;
    // 正解数
    private static int correntAnsCount;

    private static bool isNext;

    private static bool isNextQuestion;
    
    // 初期化
    void Start()
    {
        questionCount = 0;
        correntAnsCount = 0;
        // 表示物初期化
        viewInit();
        setIsNext(true);
        Debug.Log(getIsNext());
    }
    private IEnumerator Novel()
    {
        questionText.text = "";
        ansBtnCtrl(false);
        yield return new WaitForSeconds(textAnimation.getEndTextAnimTime());
        ansBtnCtrl(true);
        for (var i = 0; i < mstDatas.Entities[questionCount].question.Length; i++)
        {
            if(isAnserBtn)
            {
                break;
            }
            questionText.text += string.Format("{0}", mstDatas.Entities[questionCount].question[i]);
            yield return new WaitForSeconds(questionTextSpeed);
        }
        isAllText = true;
    }
    // カウントタイマー
    private void Update()
    {
        // todo問題文が全て表示されるまではタイマーをスタートさせない処理
        if(isAllText)
        {
            if (timer > 0)
            {
                // 回答ボタンを押していない間カウントダウンする
                if (!isAnserBtn)
                {
                    timer -= Time.deltaTime;
                }
                limitTimer.text = string.Format("残り：{0}秒", timer.ToString("F2"));
            }
            // 時間切れのときは不正解の文言をそれぞれ表示
            if (timer <= 0 && !isLimit)
            {
                timer = 0;
                isLimit = true;
                // 正解不正解文言管理
                ansctrl(isAnserBtn, false);
            }
        }
        Debug.Log(getIsNext());
    }
    /// <summary>
    /// 回答ボタン押した時の処理
    /// </summary>
    /// <param name="iNum">回答ボタンナンバー</param>
    public void onclickBtn( int iNum )
    {
        var is_answer = false;
        isAnserBtn = true;
        if (iNum <= 0)
        {
            Debug.Log("1以上の数値を入れてください"); return;
        }
        if (questionCount+1 < mstDatas.Entities.Count()) setIsNext(true);
        else setIsNext(false);

        switch (iNum)
        {
            case 1:
                if (answer1Text.text == mstDatas.Entities[questionCount].corrent_answer)
                {
                    is_answer = true;
                    correntAnsCount++;
                }
                break;
            case 2:
                if (answer2Text.text == mstDatas.Entities[questionCount].corrent_answer)
                {
                    is_answer = true;
                    correntAnsCount++;
                }
                break;
            case 3:
                if (answer3Text.text == mstDatas.Entities[questionCount].corrent_answer)
                {
                    is_answer = true;
                    correntAnsCount++;
                }
                break;
            default:
                if (answer4Text.text == mstDatas.Entities[questionCount].corrent_answer)
                {
                    is_answer = true;
                    correntAnsCount++;
                }
                break;
        }
        // 正解不正解文言管理
        ansctrl(isAnserBtn, is_answer);
    }
    /// <summary>
    /// 次へ行くボタン制御
    /// </summary>
    public void onClickNextBtn()
    {
        if (questionCount == mstDatas.Entities.Count-1)
        {
            SceneManager.LoadScene("Scenes/resultScenes");
        }
        else
        {
            questionCount++;
            setIsNextQuestion(true);
            // 表示物初期化
            viewInit();
        }
    }

    /// <summary>
    /// quizViewの諸々初期化
    /// </summary>
    private void viewInit()
    {
        timer = 10.00;
        quizNumber.text = string.Format("第{0}問", mstDatas.Entities[questionCount].mst_id);
        StartCoroutine(Novel());
        answer1Text.text = string.Format("{0}", mstDatas.Entities[questionCount].answer1);
        answer2Text.text = string.Format("{0}", mstDatas.Entities[questionCount].answer2);
        answer3Text.text = string.Format("{0}", mstDatas.Entities[questionCount].answer3);
        answer4Text.text = string.Format("{0}", mstDatas.Entities[questionCount].answer4);
        limitTimer.text = string.Format("残り：{0:0.00}秒", timer);
        isLimit = false;
        isAnserBtn = false;
        isAllText = false;
        panel.SetActive(isAnserBtn);
        Debug.Log(string.Format("何問目？：{0}", questionCount+1));
        Debug.Log(questionCount);
        Debug.Log(mstDatas.Entities.Count());
    }
    /// <summary>
    /// 正解不正解文言管理
    /// </summary>
    /// <param name="is_iBtn">回答ボタンを押したかどうか</param>
    /// <param name="is_iAns">正解かどうか</param>
    private void ansctrl(bool is_iBtn, bool is_iAns)
    {
        if (!is_iBtn)
        {
            limitTimer.text = string.Format("残り：0.00秒");
        }
        panel.SetActive(true);
        correctAnswer.SetActive(is_iAns);
        inCorrectAnswer.SetActive(!is_iAns);
        // 不正解なら正解の回答を表示
        if (inCorrectAnswer.activeSelf)
        {
            correntAnserText.text = string.Format("正解：{0}", mstDatas.Entities[questionCount].corrent_answer);
        }  
        if (getIsNext()) nextBtnText.text = string.Format("次の問題へ");
        else nextBtnText.text = string.Format("結果へ");
    }

    /// <summary>
    /// 回答ボタンの有効無効処理
    /// </summary>
    /// <param name="is_iEndTextAnim">テキストアニメが終わったかどうか</param>
    private void ansBtnCtrl( bool is_iEndTextAnim)
    {
        for (var i = 0; i < 4; i++)
        {
            if (is_iEndTextAnim)
            {
                GameObject.Find(string.Format("quizView/answerBtnZone/answerBtn{0}", i + 1)).GetComponent<Button>().enabled = true;
            }
            else
            {
                GameObject.Find(string.Format("quizView/answerBtnZone/answerBtn{0}", i + 1)).GetComponent<Button>().enabled = false;
            }
        }
    }
    /// <summary>
    /// 正解数の取得
    /// </summary>
    /// <returns></returns>
    public static int getCorrentAns()
    {
        return correntAnsCount;
    }

    /// <summary>
    /// 次の問題があるかどうかをセット
    /// </summary>
    /// <param name="is_iNext"></param>
    /// <returns></returns>
    public static bool setIsNext( bool is_iNext )
    {
        isNext = is_iNext;
        return isNext;
    }

    /// <summary>
    /// 次の問題があるかどうかを取得
    /// </summary>
    /// <returns></returns>
    public static bool getIsNext()
    {
        return isNext;
    }

    /// <summary>
    /// 次の問題に行くかどうかをセット
    /// </summary>
    /// <param name="is_iNext"></param>
    /// <returns></returns>
    public static bool setIsNextQuestion(bool is_iNext)
    {
        isNextQuestion = is_iNext;
        return isNextQuestion;
    }
    /// <summary>
    /// 次の問題に行くかどうかを取得
    /// </summary>
    /// <returns></returns>
    public static bool getIsNextQueztion()
    {
        return isNextQuestion;
    }
}
