using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject userNameScreen, connectScreen;
    [SerializeField]
    private GameObject usernameButton;
    [SerializeField]
    private InputField username_Input, room_Create_Input, room_join_Input;
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server!");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby!");
        userNameScreen.SetActive(true);
    }

    #region UICALLS
    public void OnClick_usernameButton()
    {
        PhotonNetwork.NickName = username_Input.text;
        userNameScreen.SetActive(false);
        connectScreen.SetActive(true);
    }

    public void OnNameField_Changed()
    {
        if (username_Input.text.Length >= 2)
        {
            usernameButton.SetActive(true);
        }
        else
        {
            usernameButton.SetActive(false);
        }

    }
    public override void OnJoinedRoom()
    {
        //start the game

        PhotonNetwork.LoadLevel(1);

    }
    #endregion
    public void OnClick_JoinRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(room_join_Input.text, ro, TypedLobby.Default);
    }
    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(room_Create_Input.text, new RoomOptions { MaxPlayers = 4 }, null);
    }

}
