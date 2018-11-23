using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitWord
{
	public List<Letter> LetterList = new List<Letter>();
	//最大行
	public int MaxRow = 0;
	//最小行
	public int MinRow = 0;
	//最大列
	public int MaxCol = 0;
	//最小列
	public int MinCol = 0;
}

public class GamePlay : MonoBehaviour
{
	//返回按钮
	public GameObject m_BackBtnObj;
	//目标字母盘
	public LetterPan m_TargetPan;
	//字母子母盘
	public LetterPan m_LetterPan;
	
	List<List<int>> m_WordSplitList = new List<List<int>>();
	List<string> m_WordList = new List<string>();
	Dictionary<string,List<Letter>> m_WordLetterDic = new Dictionary<string,List<Letter>>();
	// Use this for initialization
	void Start () {
		m_BackBtnObj.GetComponent<Button>().onClick.AddListener(BackBtnClick);
	}
	
	public void SetData(List<string> wordList, List<List<int>> wordSplitList)
	{
		m_WordLetterDic.Clear();
		m_LetterPan.SetLayoutComponentEnable(true);
		m_WordList = wordList;
		m_WordSplitList = wordSplitList;
		m_LetterPan.SetLayoutComponentEnable(true);
		gameObject.SetActive(true);
		//底板创建cell
		m_TargetPan.SetData(wordList);
		//创建拖动cell
		m_LetterPan.SetData(wordList);
		SplitLetter();
	}
	
	/// <summary>
	/// 分割字母
	/// </summary>
	void SplitLetter()
	{
		int tempIndex = 0;
		foreach (var letterIndexList in m_WordSplitList)
		{
			foreach (var index in letterIndexList)
			{
				string tagName = "tag_" + tempIndex;
				Letter letter = m_LetterPan.transform.GetChild(index).GetComponent<Letter>();
				letter.SetTag(tagName);
				if (!m_WordLetterDic.ContainsKey(tagName))
				{
					m_WordLetterDic[tagName] = new List<Letter>();
				}
				m_WordLetterDic[tagName].Add(letter);
			}

			tempIndex++;
		}
		
		Invoke("CancelLayoutGroup",0.1f);
	}

	void CancelLayoutGroup()
	{
		m_LetterPan.SetLayoutComponentEnable(false);
	}

	void BackBtnClick()
	{
		gameObject.SetActive(false);
	}
}
