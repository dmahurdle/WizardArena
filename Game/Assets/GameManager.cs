using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public float startingTime;
	
	private float currentTime;
	private GUIStyle timerStyle, counterStyle;
	public GameObject[] players;
	private static GameManager me;
	// Use this for initialization
	void Start () {
		me = this;
		startingTime *= 60;
		currentTime = startingTime;
		timerStyle = new GUIStyle();
		counterStyle = new GUIStyle();
		timerStyle.fontSize = 20;
		counterStyle.fontSize = 15;
		timerStyle.normal.textColor = Color.white;
		counterStyle.normal.textColor = Color.red;
	}
	
	public static Character getPlayer(int playerId)
	{
		Character ret = null;
		foreach(GameObject g in me.players)
		{
			Character p = (Character)(g.GetComponent("Character"));
			if(p.getId() == playerId)
			{
				ret = p;
			}
		}
		return ret;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentTime > 0)
		{
			currentTime -= Time.deltaTime;
		}
		else
		{
			foreach (GameObject o in players) {
				Character c = o.GetComponent<Character>();
				c.setInvincible(true);
			}
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Restart();
		}
	}
	
	void Restart () 
	{
		currentTime = startingTime;
		foreach(GameObject o in players)
		{
			o.GetComponent<Character>().setInvincible(false);
			((Character)o.GetComponent("Character")).Restart();
		}
	}
	
	int nabBestPlayer()
	{
		int highestKills = -100;
		int index = -1;
		foreach(GameObject o in players)
		{
			
			Character c = ((Character)o.GetComponent("Character"));
			if(c.getNumKills() > highestKills)
			{
				highestKills = c.getNumKills();
				index = c.getId()+1;
			}
		}
		return index;
	}
	
	void OnGUI()
	{
		int minutes = (int)(currentTime/60);
		int seconds = (int)(currentTime - minutes*60);
		string secondsString = (seconds < 10)?"0"+seconds:""+seconds;
		
		GUI.TextField(new Rect((3*Screen.width/8),10,100,20), "Time Remaining "+minutes+":"+secondsString, timerStyle);
		int width = 120;
		int height = 20;
		if(players.Length > 0)
		{
		int numKills = GameManager.getPlayer(0).getNumKills();
		GUI.TextField(new Rect(0,0,width,height), "Player 1 Kills: "+numKills, counterStyle);
			if(players.Length > 1)
			{
				numKills = GameManager.getPlayer(1).getNumKills();
				GUI.TextField(new Rect(Screen.width-width, 0, width, height), "Player 2 Kills: "+numKills, counterStyle);
				if(players.Length > 2)
				{
					numKills = GameManager.getPlayer(2).getNumKills();
					GUI.TextField(new Rect(0, Screen.height-height, width, height), "Player 3 Kills: "+numKills, counterStyle);
					if(players.Length > 3)
					{
						numKills = GameManager.getPlayer(3).getNumKills();
						GUI.TextField(new Rect(Screen.width-width, Screen.height-height, width, height), "Player 4 Kills: "+numKills, counterStyle);
					}
				}
			}
		}
		if(currentTime <= 0)
		{
			GUI.TextField(new Rect((int)(3*Screen.width/8), (int)(3*Screen.height/8), 100, 20), "Player "+nabBestPlayer()+" Wins!!!", timerStyle);
		}
	}
}
