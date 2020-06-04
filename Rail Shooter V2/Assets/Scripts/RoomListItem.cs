using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour {

 //callback function
 //delegate allows to make a reference to functions
 //these funcions will be executed when JoinRoom is called
 public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
 private JoinRoomDelegate joinRoomCallback;

//  private GameObject[] button = new GameObject[1];

 [SerializeField]
 private Text roomNameText;
 private MatchInfoSnapshot match;

 public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
 {
  match = _match;
  joinRoomCallback = _joinRoomCallback;
  roomNameText.text = _match.name + " (" + match.currentSize + "/" + match.maxSize + ") ";
 }

 public void JoinRoom()
 {
  joinRoomCallback.Invoke(match);   
  GameObject.FindGameObjectWithTag("HostGame").SetActive(false);
 }

    // Update is called once per frame
    // void Update()
    // {
    //     print(match.currentSize);
    //     if(match.currentSize >= 1){
    //         GameObject.FindGameObjectWithTag("ButtonStart").SetActive(true);
    //         print("Hello");
    //     }
        
    // }

}