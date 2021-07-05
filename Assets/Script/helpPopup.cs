using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helpPopup : MonoBehaviour
{
    public GameObject[] helpImages;
    public Button[] helpButton;

    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    private int flickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Flick();
    }

    /// <summary>
    /// 左右フリック操作
    /// </summary>
    private void Flick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x,
                                        Input.mousePosition.y,
                                        Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
                                      Input.mousePosition.y,
                                      Input.mousePosition.z);
            GetDirection();
        }
        

    }
    /// <summary>
    /// 左右フリック管理
    /// </summary>
    private void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        string Direction ="";

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        }
        switch (Direction)
        {
            case "right":
                //右フリックされた時の処理
                Debug.Log("右フリック入った");
                if (flickCount > 0)
                {
                    flickCount--;
                    Debug.Log(string.Format("フリックカウント:{0} ヘルプページ数:{1}", flickCount, helpImages.Length - 1));
                    for (var i = 0; i < helpImages.Length-1; i++)
                    {
                        if (helpImages[flickCount+1] == helpImages[i+1])
                        {
                            helpImages[i+1].SetActive(true);
                            helpButton[i+1].enabled = false;
                            GameObject.Find(string.Format("helpPopup/buttonArea/button{0}",i+1)).GetComponent<Image>().color = Color.green;
                        }
                        else
                        {
                            helpImages[i+1].SetActive(false);
                            helpButton[i+1].enabled = true;
                            GameObject.Find(string.Format("helpPopup/buttonArea/button{0}",i+1)).GetComponent<Image>().color = Color.white;
                        }
                    }
                }
                break;

            case "left":
                //左フリックされた時の処理
                Debug.Log("左フリック入った");
                if (flickCount < helpImages.Length - 2)
                {
                    flickCount++;
                    Debug.Log(string.Format("フリックカウント:{0} ヘルプページ数:{1}", flickCount, helpImages.Length - 1));
                    for (var i = 0; i < helpImages.Length-1; i++)
                    {
                        if (helpImages[flickCount+1] == helpImages[i+1])
                        {
                            helpImages[i+1].SetActive(true);
                            helpButton[i+1].enabled = false;
                            GameObject.Find(string.Format("helpPopup/buttonArea/button{0}",i+1)).GetComponent<Image>().color = Color.green;
                        }
                        else
                        {
                           // GameObject.Find("helpPopup/buttonArea/button" + i + 1).GetComponent<Image>().color = Color.white;
                            helpImages[i+1].SetActive(false);
                            helpButton[i+1].enabled = true;
                            GameObject.Find(string.Format("helpPopup/buttonArea/button{0}",i+1)).GetComponent<Image>().color = Color.white;
                        }
                    }
                }
                break;
        }
    }
    /// <summary>
    /// パネルを押した時の挙動
    /// </summary>
    public void OnclickBtn()
    {
        // 初期化する
        for (var i = 1; i < helpImages.Length; i++)
        {
            if (i == 1)
            {
                helpImages[i].SetActive(true);
                GameObject.Find(string.Format("helpPopup/buttonArea/button{0}", i)).GetComponent<Image>().color = Color.green;
            }
            else
            {
                helpImages[i].SetActive(false);
                GameObject.Find(string.Format("helpPopup/buttonArea/button{0}",i)).GetComponent<Image>().color = Color.white;
            }
        }
        this.gameObject.SetActive(false);
        // フリックカウントも初期化
        flickCount = 0;
    }
    /// <summary>
    /// helppopup下のボタン制御
    /// </summary>
    /// <param name="iNum"></param>
    public void OnClickCircle(int iNum)
    {
        for(var i = 1; i < helpButton.Length; i++)
        {
            if (i == iNum)
            {
                helpImages[i].SetActive(true);
                helpButton[i].enabled = false;
                GameObject.Find(string.Format("helpPopup/buttonArea/button{0}", i)).GetComponent<Image>().color = Color.green;
                flickCount = i-1;
            }
            else
            {
                helpImages[i].SetActive(false);
                helpButton[i].enabled = true;
                GameObject.Find(string.Format("helpPopup/buttonArea/button{0}", i)).GetComponent<Image>().color = Color.white;
            }
        }
        
    }
}
