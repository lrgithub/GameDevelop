using UnityEngine;
using System.Collections;

public class StartMenuController : MonoBehaviour
{

    public TweenScale startpanelTween;
    public TweenScale loginpanelTween;
    public TweenScale registerpanelTween;
    public TweenScale severpanelTween;

    public UIInput usernameInputLogin;
    public UIInput passwordInputLogin;

    public UIInput usernameInputRegister;
    public UIInput passwardInputRegister;
    public UIInput repasswardInputRegister;


    public UILabel usernameLabelStart;
    public UILabel serverNameLabelStart;
    public static string username;
    public static string password;
    public static ServerProperty sp;

    private bool haveInitSeverlist;
    public UIGrid serverlistGrid;
    public GameObject serveritemRed;
    public GameObject serveritemGreen;

    public GameObject serverSelectGo;
    private void Start()
    {
        InitServerlist();//初始化服务器
    }

    /// <summary>
    /// 输入账号进行登录
    /// </summary>
    public void OnUsernameClick()
    {
        startpanelTween.PlayForward();
        StartCoroutine(HidePanel(startpanelTween.gameObject));
        loginpanelTween.gameObject.SetActive(true);
        loginpanelTween.PlayForward();

    }
    /// <summary>
    /// 选择服务器
    /// </summary>
    public void OnSeverClick()
    {
        startpanelTween.PlayForward();
        StartCoroutine(HidePanel(startpanelTween.gameObject));

        severpanelTween.gameObject.SetActive(true);
        severpanelTween.PlayForward();

        
    }
    /// <summary>
    /// 1.连接服务器，验证用户名和服务器
    /// 2.进入角色选择界面
    /// </summary>
    public void OnEnterGameClick()
    {

    }
    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }

    /// <summary>
    /// 1.验证用户名和密码
    /// Todo
    /// 2.验证成功
    /// 返回开始界面
    /// 3.验证失败
    /// 提示信息返回第一步
    /// 
    /// 
    /// 
    /// </summary>
    public void OnLoginClick()
    {
        //得到用户名和密码存储起来
        username = usernameInputLogin.value;
        password = passwordInputLogin.value;
        //返回开始界面
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        usernameLabelStart.text = username;
    }
    public void OnRegisterShowClick()
    {
        //隐藏当前面板，显示注册面板
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        registerpanelTween.gameObject.SetActive(true);
        registerpanelTween.PlayForward();

    }
    public void OnLoginCloseClick()
    {
        //返回开始界面
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
    }

    public void OnCancelClick()
    {
        //隐藏注册面板
        registerpanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpanelTween.gameObject));
        //显示登录面板
        loginpanelTween.gameObject.SetActive(true);
        loginpanelTween.PlayForward();

    }
    public void OnRegisterCloseClick()
    {
        OnCancelClick();
    }
    public void OnRegisterAndLoginClick()
    {
        //1.本地校正，连接服务器进行校正

        //2.连接失败

        //3.连接成功
        //保存用户名和密码
        username = usernameInputRegister.value;
        password = passwardInputRegister.value;
        //返回开始界面
        registerpanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();


        //usernamestart
        usernameLabelStart.text = username;
    }

    public void InitServerlist()
    {
        if (haveInitSeverlist)

            return;

        //1.连接服务器取得游戏服务器列表信息

        //TODO

        //2.根据上面的信息，添加服务器列表
        for (int i = 0; i < 20; i++)
        {
            string ip = "127.0.0.1:9080";
            string name = (i + 1) + "区 马达加斯加";
            int count = Random.Range(0, 100);
            GameObject go = null;
            if (count > 50)
            {
                //火爆 
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemRed);

            }
            else
            {
                //流畅
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemGreen);
            }
            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.Name = name;
            sp.count = count;
            serverlistGrid.AddChild(go.transform);
        }
        haveInitSeverlist = true;
    }
    public void OnServerSelect(GameObject serverGo)
    {
        sp = serverGo.GetComponent<ServerProperty>();
        serverSelectGo.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;
        serverSelectGo.transform.Find("Label").GetComponent<UILabel>().text = sp.Name;
    }
    public void OnServerPanelClose()
    {
        severpanelTween.PlayReverse();
        StartCoroutine(HidePanel(severpanelTween.gameObject));

        //显示开始界面
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
        serverNameLabelStart.text= sp.Name;
    }
}
