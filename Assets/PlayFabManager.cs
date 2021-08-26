using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    public string titleid;
    public InputField passwordinput, emailinput, creatUserNameinput, createmailinput, creatpass1input, creatpass2input, resetEmailinput;
    [Header("ui")]
    public GameObject gamePanel, signupPanel, loginPanel;
    void OnError(PlayFabError error)
    {
        Debug.Log("Account Creating Faild");
        Debug.Log(error.ErrorMessage);
        Debug.Log(error.GenerateErrorReport());
    }
    //signUp
    public void SignupButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = creatUserNameinput.text,
            Email = createmailinput.text,
            Password = creatpass1input.text,
            //RequireBothUsernameAndEmail = true
        };
        if (creatpass1input.text == creatpass2input.text)
        {
            Debug.Log("test");
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterDone, OnError);
        }
        else
        {
            Debug.Log("your password not match please confrim again with same password");
        }
    }
    void OnRegisterDone(RegisterPlayFabUserResult result)
    {
        Debug.Log("Signup in done");
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
    }
    //login
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailinput.text,
            Password = passwordinput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    void OnLoginSuccess(LoginResult result)
    {
        loginPanel.SetActive(false);
        gamePanel.SetActive(true);
        Debug.Log("Success full login");
    }
    //Reset password 
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = resetEmailinput.text,
            TitleId = titleid
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("reset password link mail you");
    }
    // send leadboard
    public void SendLeadBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
              new StatisticUpdate{
                  StatisticName="GameScore",Value=score
              }
          }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }
    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard send");
    }
    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "GameScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position + "" + item.PlayFabId + "" + item.StatValue);
        }
    }
}

