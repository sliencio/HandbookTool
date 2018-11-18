using System;
using UnityEngine;

public class EventManager
{
	/// <summary>
	/// 点击字母
	/// </summary>
	public event Action<GameObject> LetterPointDownEvent;
	public event Action<GameObject> LetterPointUpEvent;
	
	private static EventManager instance;
	public static EventManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new EventManager();
			}
			return instance;
		}
	}
	/// <summary>
	/// 字母按下
	/// </summary>
	/// <param name="go"></param>
	public void LetterPointDown(GameObject go)
	{
		if (null != LetterPointDownEvent)
		{
			LetterPointDownEvent(go);
		}
	}
	/// <summary>
	/// 字母抬起
	/// </summary>
	/// <param name="go"></param>
	public void LetterPointUp(GameObject go)
	{
		if (null != LetterPointUpEvent)
		{
			LetterPointUpEvent(go);
		}
	}

	
}