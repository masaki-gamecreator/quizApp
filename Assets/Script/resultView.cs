using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resultView : MonoBehaviour
{
    // 正解数テキスト
    public Text correnctAnsNum;
    // ボタンテキスト
    public Text btnText;

    // Start is called before the first frame update
    void Start()
    {
        correnctAnsNum.text = string.Format("正解数：{0}問", quizView.getCorrentAns());
        btnText.text = string.Format("タイトルへ戻る");
    }

    public void OnClickBtn()
    {
        SceneManager.LoadScene("Scenes/titleScenes");
    }
}
