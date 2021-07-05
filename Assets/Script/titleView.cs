using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleView : MonoBehaviour
{
    public Text titleText;
    public Text btnText;

    public GameObject helpPopup;
    
    private void Start()
    {
        // タイトルテキスト表示
        titleText.GetComponent<Text>().text = string.Format("アニメクイズ");
        // ボタンテキスト表示
        btnText.GetComponent<Text>().text = string.Format("スタート");
        helpPopup.SetActive(false);
    }

    // クイズ画面へ遷移
    public void OnClickButton( int iNum )
    {
        if(iNum == 0)
        {
            SceneManager.LoadScene("Scenes/quizScenes");
        }
        else
        {
            helpPopup.SetActive(true);
        }
    }
}
