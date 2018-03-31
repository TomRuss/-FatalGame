using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;

public class SteamMenu : MonoBehaviour {

	[Header("Current User Data")]
	[Space]
	public string steamName;
	public EPersonaState localState;
	public Texture2D picture;

	[Space]
	[Header("Friends Data")]

	public List<Friend> friends = new List<Friend>();

	[Space]
	[Header("UI")]
	public Text userName;
	public RawImage profilePicture;
	public List<State> status = new List<State>();
	public Text stateText;
	public Image stateIcon;
	public GameObject friendPrefab;
	public Transform friendParent;
	public GameObject manager;

	void Awake ()
	{
		if (GameObject.FindGameObjectWithTag("Manager") == null)
		{
			GameObject man = Instantiate(manager);
			DontDestroyOnLoad(man);
		}
	}

	void Start ()
	{
		steamName = SteamFriends.GetPersonaName();
		//PhotonNetwork.playerName = steamName;
		profilePicture.texture = GetSmallAvatar(SteamUser.GetSteamID());
		profilePicture.rectTransform.localScale = new Vector3(1, -1, 1);

		InvokeRepeating("UpdateState", 0, 2);

		int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
		for (int i = 0; i < friendCount; ++i)
		{
			CSteamID friendSteamId = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
			string friendName = SteamFriends.GetFriendPersonaName(friendSteamId);
			EPersonaState friendState = SteamFriends.GetFriendPersonaState(friendSteamId);

			FriendGameInfo_t info;
			SteamFriends.GetFriendGamePlayed(friendSteamId, out info);

			Friend friend = new Friend();
			friend.name = friendName;
			friend.state = friendState;
			friend.picture = GetSmallAvatar(friendSteamId);

			if (SteamFriends.GetFriendPersonaState(friendSteamId) == EPersonaState.k_EPersonaStateOnline)
			{
				friends.Add(friend);
				GameObject f = Instantiate(friendPrefab, friendParent, false);

				f.GetComponent<FriendHelper>().image.texture = friend.picture;

				f.GetComponent<FriendHelper>().image.rectTransform.localScale = new Vector3(1, -1, 1);

				f.GetComponent<FriendHelper>().name.text = friendName;

				FriendGameInfo_t gameInfo;
				SteamFriends.GetFriendGamePlayed(friendSteamId, out gameInfo);

				if (gameInfo.m_gameID.AppID().m_AppId == 480)
					f.GetComponent<FriendHelper>().currentlyIn.text = "In-game";

				SetState(Color.black, null, true, f.GetComponent<FriendHelper>().stateColor, friendState, f.GetComponent<FriendHelper>().currentlyIn);
			}		
		}
	}
	
	void Update ()
	{
		
	}

	public void UpdateState ()
	{
		localState = SteamFriends.GetPersonaState();
		userName.text = steamName;
		CheckPersonalState();
	}

	public void CheckPersonalState ()
	{
		for (int i = 0; i < status.Count; i++)
		{
			if (localState == EPersonaState.k_EPersonaStateOffline)
				if (status[i].stateName == "Offline")
					SetState(status[i].stateColor, status[i].stateName, false, null, EPersonaState.k_EPersonaStateOnline, null);
			if (localState == EPersonaState.k_EPersonaStateOnline)
				if (status[i].stateName == "Online")
					SetState(status[i].stateColor, status[i].stateName, false, null, EPersonaState.k_EPersonaStateOnline, null);
			if (localState == EPersonaState.k_EPersonaStateAway)
				if (status[i].stateName == "Away")
					SetState(status[i].stateColor, status[i].stateName, false, null, EPersonaState.k_EPersonaStateOnline, null);
			if (localState == EPersonaState.k_EPersonaStateBusy)
				if (status[i].stateName == "Busy")
					SetState(status[i].stateColor, status[i].stateName, false, null, EPersonaState.k_EPersonaStateOnline, null);
		}
	}

	public void SetState(Color stateColor, string stateName, bool isFriend, Image state, EPersonaState curState, Text currentlyIn)
	{
		if (!isFriend)
		{
			stateText.text = stateName;
			stateIcon.color = stateColor;
		}
		else if (isFriend)
		{
			for (int i = 0; i < status.Count; i++)
			{
				if (curState == EPersonaState.k_EPersonaStateOffline)
					if (status[i].stateName == "Offline")
						state.color = status[i].stateColor;
				if (curState == EPersonaState.k_EPersonaStateOnline)
					if (status[i].stateName == "Online")
						state.color = status[i].stateColor;
				if (curState == EPersonaState.k_EPersonaStateAway)
					if (status[i].stateName == "Away")
						state.color = status[i].stateColor;
				if (curState == EPersonaState.k_EPersonaStateBusy)
					if (status[i].stateName == "Busy")
						state.color = status[i].stateColor;
				if (status[i].stateName == "Offline" || status[i].stateName == "Away" || status[i].stateName == "Busy")
					currentlyIn.text = string.Empty;
			}
		}
	}

	public Texture2D GetSmallAvatar(CSteamID user)
	{
		int FriendAvatar = SteamFriends.GetMediumFriendAvatar(user);
		uint ImageWidth;
		uint ImageHeight;
		bool success = SteamUtils.GetImageSize(FriendAvatar, out ImageWidth, out ImageHeight);

		if (success && ImageWidth > 0 && ImageHeight > 0)
		{
			byte[] Image = new byte[ImageWidth * ImageHeight * 4];
			Texture2D returnTexture = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
			returnTexture.filterMode = FilterMode.Bilinear;
			success = SteamUtils.GetImageRGBA(FriendAvatar, Image, (int)(ImageWidth * ImageHeight * 4));
			if (success)
			{
				returnTexture.LoadRawTextureData(Image);
				returnTexture.Apply();
			}
			return returnTexture;
		}
		else
		{
			Debug.LogError("Couldn't get avatar.");
			return new Texture2D(0, 0);
		}
	}
}

[System.Serializable]
public class State
{
	public string stateName;
	public Color stateColor;
}

[System.Serializable]
public class Friend
{
	public string name;
	public Texture2D picture;
	public EPersonaState state;
}
