
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IPointerDownHandler
{

	public Text letter;
	//字母索引
	public int letterIndex = 0;
	Image bg;

	private bool isSelect = false;
	// Use this for initialization
	void Start ()
	{
		bg = GetComponent<Image>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		SetSelect();
	}
	
	/// <summary>
	/// 设置子母
	/// </summary>
	/// <param name="letterStr"></param>
	public void SetLetter(string letterStr)
	{
		letter.text = letterStr;
	}
	/// <summary>
	/// 设置大小
	/// </summary>
	/// <param name="size"></param>
	public void SetSize(Vector2 size)
	{
		GetComponent<RectTransform>().sizeDelta = size;
	}
	
	/// <summary>
	/// 获取选中状态
	/// </summary>
	/// <returns></returns>
	public bool GetSelectStatus()
	{
		return isSelect;
	}

	public int GetLetterIndex()
	{
		return letterIndex;
	}
	
	/// <summary>
	/// 设置选中状态
	/// </summary>
	void SetSelect()
	{
		isSelect = !isSelect;
		bg.color = isSelect ? Color.red : Color.white;
	}

	public void SetData(int index, string letterStr)
	{
		letterIndex = index;
		SetLetter(letterStr);
	}
}
