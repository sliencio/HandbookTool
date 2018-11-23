using UnityEngine;
using UnityEngine.UI;

public class BaseLetter : MonoBehaviour {

	//字母索引
	public int m_LetterIndex = 0;
	//二维坐标
	public Int2 m_IndexPos = new Int2(0,0);
	//子母
	public string m_LetterStr = string.Empty;
	//左侧字母
	public BaseLetter m_LeftLetter = null;
	//右侧字母
	public BaseLetter m_RightLetter = null;
	//上部字母
	public BaseLetter m_TopLetter = null;
	//下部字母
	public BaseLetter m_BottomLetter = null;
	
	/// <summary>
	/// 设置子母
	/// </summary>
	/// <param name="letterStr"></param>
	public virtual void SetLetter(string letterStr)
	{
		m_LetterStr = letterStr;
	}

	public virtual string GetLetterStr()
	{
		return m_LetterStr;
	}

	public virtual int GetLetterIndex()
	{
		return m_LetterIndex;
	}

	public virtual void SetData(int index, string letterStr,Int2 indexPos)
	{
		m_LetterIndex = index;
		SetLetter(letterStr);
		m_IndexPos = indexPos;
		SetLetter(letterStr);
	}

	public virtual void Reset()
	{
		m_LetterIndex = -1;
		SetLetter(string.Empty);
	}
}
