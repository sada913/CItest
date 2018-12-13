using UnityEngine;
using System.Collections;

public class PhotonConnect : MonoBehaviour
{
    [SerializeField] GameObject Player,Atacker,debugEnemy;
    int RoomCount = 0;
 
    void Start()
    {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings(null);

        //今回オートでロビーにJoinする
    }

    // ロビーに入ると呼ばれる
    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入りました。");

        // ルームに入室する
        PhotonNetwork.JoinRandomRoom();
    }

    // ルームに入室すると呼ばれる
    void OnJoinedRoom()
    {
        Debug.Log("ルームへ入室しました。");

        // Player生成
        if (PhotonNetwork.countOfPlayersInRooms == 0)
        {
            var player = PhotonNetwork.Instantiate(Player.name, new Vector3(0, 0, 0), transform.rotation, 0);
            GameMaster.Instance.Player = player;

            //Resourcesの中にある生成プレハブの名前
            // デバッグの敵
            //PhotonNetwork.Instantiate(debugEnemy.name, new Vector3(0, 0, 0), transform.rotation, 0);
        }


        else if(PhotonNetwork.countOfPlayersInRooms == 1)
        {
            PhotonNetwork.Instantiate(Atacker.name, new Vector3(0, 0, 0), transform.rotation, 0);

        }
        else
        {
            Debug.Log("満員です");
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.CreateRoom("ARKitShare" + RoomCount.ToString());
            RoomCount++;
        }




    }

    // ルームの入室に失敗すると呼ばれる
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("ルームの入室に失敗しました。");

        // ルームがないと入室に失敗するため、その時は自分で作る
        // 引数でルーム名を指定できる
        PhotonNetwork.CreateRoom("ARKitShare" + RoomCount.ToString());
        RoomCount++;
    }
}