using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	
	public string gameName = "BB_Rogue_1";
	public GameObject playerPrefab;
	public Transform spawnPoint_1;
	public Transform spawnPoint_2;
	
	private bool hostsRefreshing;
	private bool hostsUpdated;
	private HostData[] hostDataList;
	
	void Start()
	{
		hostDataList = new HostData[] {};
	}
	
	void OnGUI()
	{
		if (!Network.isServer && !Network.isClient)
		{
			if(GUILayout.Button("Start Server"))
			{
				Debug.Log("Starting Server");
				StartServer();
			}
			
			if(GUILayout.Button("Refresh Hosts"))
			{
				Debug.Log("Requesting Hosts...");
				RefreshHostsList();
			}
			
			foreach(HostData host in hostDataList)
			{
				if(GUILayout.Button(host.comment))
				{
					Network.Connect(host);
				}
			}
		}
	}
	
	void StartServer()
	{
		Network.InitializeServer(2, 9001, !Network.HavePublicAddress());
		MasterServer.RegisterHost(gameName, "Rogue BB Test", "poem's game");
	}

	void RefreshHostsList()
	{
		MasterServer.RequestHostList(gameName);
		hostsRefreshing = true;
		hostsUpdated = false;
	}
	
	void Update()
	{
		if (hostsRefreshing && hostsUpdated)
		{
			hostsRefreshing = false;
			hostsUpdated = false;
			Debug.Log("Found " + MasterServer.PollHostList().Length);
			hostDataList = MasterServer.PollHostList();
		}
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent) 
	{
		if(msEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log("Server Registered");
		
		if(msEvent == MasterServerEvent.HostListReceived)
			hostsUpdated = true;
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
		SpawnPlayer(1);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Player Connected");
		SpawnPlayer(2);
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