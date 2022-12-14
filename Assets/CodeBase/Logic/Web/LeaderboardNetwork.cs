using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Logic.Web
{
    public class LeaderboardNetwork : MonoBehaviour
    {
        private const string JsonApi = "https://taptics.b-cdn.net/files/leaderboard.json";
        private Players _players = null;

        public Players GetPlayers => _players;

        private void Start()
        {
            StartCoroutine(GetPlayersJson(ParseData));
        }

        private void ParseData(string data)
        {
            _players = JsonUtility.FromJson<Players>("{\"PlayerStats\":" + data + "}");
        }

        private IEnumerator GetPlayersJson(Action<string> callback) => 
            CallAPI(JsonApi, callback);

        private IEnumerator CallAPI(string url, Action<string> callback) {
            using (UnityWebRequest request = UnityWebRequest.Get(url)) { 
                yield return request.SendWebRequest(); 
                if (request.result == UnityWebRequest.Result.ConnectionError) { 
                    Debug.Log("network problem: " + request.error);
                } else if (request.responseCode != (long)System.Net.HttpStatusCode.OK) { 
                    Debug.Log("response error: " + request.responseCode);
                } else {
                    callback(request.downloadHandler.text); 
                }
            }
        }
    }
}