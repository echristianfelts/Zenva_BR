using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject createRoomScreen;
    public GameObject lobbyScreen;
    public GameObject lobbyBrowserScreen;

    [Header("Main Screen")]
    public Button createRoomButton;
    public Button findRoomButton;

    [Header("Lobby")]
    public TextMeshProUGUI playerListText;
    public TextMeshProUGUI roomInfoText;
    public Button startGameButton;

    [Header("Lobby Browser")]
    public RectTransform roomListContainer;
    public GameObject roomButtonPrefab;

    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomList = new List<RoomInfo>();


    // Start is called before the first frame update
    void Start()
    {
        // disable the menu buttons at the start
        createRoomButton.interactable = false;
        findRoomButton.interactable = false;

        // enable the cursor since we hide it when we play the game
        Cursor.lockState = CursorLockMode.None;

        // are we in a game?
        if (PhotonNetwork.InRoom)
        {
            // go to the lobby  
            
            // make the room visible
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }

    // changes the currently visible screen
    void SetScreen(GameObject screen)
    {
        // disable all other screens
        mainScreen.SetActive(false);
        createRoomScreen.SetActive(false);
        lobbyScreen.SetActive(false);
        lobbyBrowserScreen.SetActive(false);

        // activate the requested screen
        screen.SetActive(true);
    }

    // called when the "Back" button gets pressed
    public void OnBackButton()
    {
        SetScreen(mainScreen);
    }

    // MAIN SCREEN

    public void OnPlayerNameValueChanged(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text;
    }

    public override void OnConnectedToMaster()
    {
        // enable the menu buttons once we connect to the server
        createRoomButton.interactable = true;
        findRoomButton.interactable = true;
    }

    // called when the "Create Room" button has been pressed.
    public void OnCreateRoomButton()
    {
        SetScreen(createRoomScreen);
    }

    // called when the "Find Room" button has been pressed
    public void OnFindRoomButton()
    {
        SetScreen(lobbyBrowserScreen);
    }

    //  CREATE ROOM SCREEN

    public void OnCreateButton(TMP_InputField roomNameInput)
    {
        NetworkManager.instance.CreateRoom(roomNameInput.text);
    }

}
