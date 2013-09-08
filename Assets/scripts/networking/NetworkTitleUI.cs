using UnityEngine;
using System.Collections;

public class NetworkTitleUI : MonoBehaviour {
	
	public string gameName = "BB_Rogue_1";
	public GameObject playerPrefab;
	public Transform spawnPoint_1;
	public Transform spawnPoint_2;
	
	private bool hostsRefreshing;
	private bool hostsUpdated;
	private HostData[] hostDataList;

    public bool debugSinglePlayer = false;

	void Start()
	{
		hostDataList = new HostData[] {};

        if (debugSinglePlayer)
            StartServer();
	}

    // clicked 2P Host
	void StartServer()
	{
        Debug.Log("Hosting...");
		Network.InitializeServer(2, 9001, !Network.HavePublicAddress());
		MasterServer.RegisterHost(gameName, "Rogue BB Test", "poem's game");
	}

    // clicked 2P Join
    void StartClient()
    {
        Debug.Log("Joining...");
        RefreshHostsList();
    }

    // starts the refresh hosts process
	void RefreshHostsList()
	{
		MasterServer.RequestHostList(gameName);
		hostsRefreshing = true;
		hostsUpdated = false;
        Debug.Log("Refreshing Hosts");
	}

    // if we found hosts, connect to first
	void Update()
	{
		if (hostsRefreshing && hostsUpdated)
		{
			hostsRefreshing = false;
			hostsUpdated = false;
			hostDataList = MasterServer.PollHostList();
            if (hostDataList.Length > 0)
            {
                Debug.Log ("Connecting to Host");
                Network.Connect(hostDataList[0]);
            }
            else
                Debug.Log("No Hosts Found");
		}
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) 
	{
		if(msEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log("Server Registered");
		
		if(msEvent == MasterServerEvent.HostListReceived)
            Debug.Log("Hosts Refreshed");
			hostsUpdated = true;
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
        if (debugSinglePlayer)
            SpawnPlayer(1);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Player Connected");
		//SpawnPlayer(2);
	}
	
	void SpawnPlayer(int playerNum)
	{
		if (playerNum == 1)
			Network.Instantiate(playerPrefab, spawnPoint_1.position, Quaternion.identity, 0);
		else if (playerNum == 2)
			Network.Instantiate(playerPrefab, spawnPoint_2.position, Quaternion.identity, 0);
	}
	
	void OnPlayerDisconnected (NetworkPlayer player)
	{
		Debug.Log("player disconnected");
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
	}
}