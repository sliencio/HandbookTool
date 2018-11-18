using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class WordList : MonoBehaviour
{
	public Transform m_Content;

	public Color m_CellColor;

	public GameObject m_WordModelObj;

	public void AddWord(string wordStr)
	{
		if (string.IsNullOrEmpty(wordStr))
			return;
		GameObject word = Instantiate(m_WordModelObj);
		word.transform.SetParent(m_Content);
		word.GetComponentInChildren<Text>().text = wordStr;
		word.GetComponent<Image>().color = m_CellColor;
		word.transform.localPosition = Vector3.zero;
		word.transform.localScale = Vector3.one;
	}
	
	public void AddWordList(List<string> wordList)
	{
		foreach (string wordStr in wordList)
		{
			GameObject word = Instantiate(m_WordModelObj);
			word.transform.SetParent(m_Content);
			word.GetComponentInChildren<Text>().text = wordStr;
			word.GetComponent<Image>().color = m_CellColor;
			word.transform.localPosition = Vector3.zero;
			word.transform.localScale = Vector3.one;
		}
	}

	public void Reset()
	{
		foreach (Transform word in m_Content)
		{
			Destroy(word.gameObject);
		}
	}
}
