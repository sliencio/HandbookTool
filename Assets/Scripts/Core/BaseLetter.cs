using UnityEngine;
public class BaseLetter : MonoBehaviour {

	//字母索引
	public int m_LetterIndex = 0;

	//子母
	public string m_LetterStr = string.Empty;

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

	public virtual void SetData(int index, string letterStr)
	{
		m_LetterIndex = index;
		SetLetter(letterStr);
	}

	public virtual void Reset()
	{
		m_LetterIndex = -1;
		SetLetter(string.Empty);
	}
}
