using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    public Profile profile;
    public InputField login;
    public InputField password;
    public Text message;

    public static string UserName;
    public static string UserMark;
    public static Dictionary<string, int> LevelMarks = Marks.StartMarks();

    private IEnumerator TryGetIn(bool toRegister)
    {
        var form = new WWWForm();
        form.AddField("name", login.text);
        form.AddField("password", password.text);
        var www = UnityWebRequest.Post(toRegister ? ConnectionString.RegisterUrl : ConnectionString.LoginUrl, form);
        //www.SetRequestHeader(ConnectionString.Header.Item1, ConnectionString.Header.Item2);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            message.text = www.error;
            yield break;
        }
        switch (www.downloadHandler.text)
        {
            case "success":
            {
                StartCoroutine(GetIn(toRegister));
                break;
            }
            default:
            {
                message.text = www.downloadHandler.text;
                break;
            }
        }
    }

    private IEnumerator GetIn(bool toRegister)
    {
        UserName = login.text;
        if (toRegister)
        {
            foreach (var level in LevelMarks.Where(l => l.Value > 0))
            {
                StartCoroutine(WriteMark(level.Key, level.Value));
            }
        }
        else
        {
            var form = new WWWForm();
            form.AddField("name", UserName);
            var www = UnityWebRequest.Post(ConnectionString.GetMarkUrl, form);
            //www.SetRequestHeader(ConnectionString.Header.Item1, ConnectionString.Header.Item2);
            yield return www.SendWebRequest();
            LevelMarks = Marks.GetFromString(www.downloadHandler.text);
        }
        profile.Load();
    }
    
    public static IEnumerator WriteMark(string level, int mark)
    {
        if (string.IsNullOrEmpty(UserName)) yield break;
        var form = new WWWForm();
        form.AddField("name", UserName);
        form.AddField("mark", mark);
        form.AddField("level", level);
        var www = UnityWebRequest.Post(ConnectionString.WriteMarkUrl, form);
        //www.SetRequestHeader(ConnectionString.Header.Item1, ConnectionString.Header.Item2);
        yield return www.SendWebRequest();
    }

    public void RegisterButtonClick()
    {
        StartCoroutine(TryGetIn(true));
    }

    public void LoginButtonClick()
    {
        StartCoroutine(TryGetIn(false));
    }

    public void Start()
    {
        profile.LoadLevelMarks();
        if(string.IsNullOrEmpty(UserName))
            return;
        profile.Load();
    }
}