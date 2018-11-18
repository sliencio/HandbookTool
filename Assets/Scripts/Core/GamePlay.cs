using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Core;
using UnityEngine;
using UnityEngine.UI;

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
	// Use this for initialization
	void Start () {
		m_BackBtnObj.GetComponent<Button>().onClick.AddListener(BackBtnClick);
	}
	
	public void SetData(List<string> wordList, List<List<int>> wordSplitList)
	{
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

	void SplitLetter()
	{
		int tempIndex = 0;
		foreach (var letterIndexList in m_WordSplitList)
		{
			foreach (var index in letterIndexList)
			{
				m_LetterPan.transform.GetChild(index).GetComponent<Letter>().SetData(index,m_LetterPan.GetLetterStr(index));
				m_LetterPan.transform.GetChild(index).GetComponent<Letter>().SetTag("tag_"+tempIndex);
				
			}

			tempIndex++;
		}
		
		//m_LetterPan.SetLayoutComponentEnable(false);
	}

	void BackBtnClick()
	{
		gameObject.SetActive(false);
	}
}
