using NativeWebSocket;
using System;
using System.Text;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class WebsocketController : MonoBehaviour
{
    public static WebsocketController instance;

    [SerializeField] private WebToGrid _webToGrid;

    private WebSocket websocket;

    private bool test = false;

    [SerializeField] private string serverUrl = "ws://v2202601328887424492.powersrv.de:3000/";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    async void Start()
    {
        websocket = new WebSocket(serverUrl);

        websocket.OnOpen += async () =>
        {
            Debug.Log("Connection open!");
            string registerMessage = "{\"type\":\"register\",\"role\":\"unity\"}";
            await websocket.SendText(registerMessage);
            Debug.Log("Registered as Unity client");
        };

        websocket.OnError += (e) => Debug.LogError("WebSocket Error: " + e);
        websocket.OnClose += (e) => Debug.Log("Connection closed!");

        websocket.OnMessage += (bytes) =>
        {
            string json = Encoding.UTF8.GetString(bytes);

            if (json.Trim().ToLower() == "ping") return;

            try
            {

                var root = JObject.Parse(json);
                string type = root["type"].Value<string>();

                if (type == "colorData")
                {
                    var payload = root["payload"].ToObject<PayloadColorData>();
                    _webToGrid.UpdateWebDataToGrid(payload);
                }
                else if (type == "clientData")
                {
                    var payload = root["payload"].ToObject<PayloadFaceData>();
                }

                //WebData update = JsonUtility.FromJson<WebData>(json);

                    
                
             //   if (update.type == "update" && update.client?.blobs != null)
              //  {
                        //test = true;
                      //  _webToGrid.UpdateWebDataToGrid(update);
                       // Debug.Log("blaaaa"+update);

                //}
                Debug.Log("one message");
                }
           
            catch (Exception e)
            {
                Debug.LogWarning($"Invalid JSON: {e.Message} -> {json}");
            }
        };


        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    private async void OnApplicationQuit()
    {
        if (websocket != null)
            await websocket.Close();
    }
}
