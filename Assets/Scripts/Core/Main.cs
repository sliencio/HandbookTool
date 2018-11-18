using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    //bg LetterPan
    public LetterPan m_BgLetterPan;

    //分割letter Pan
    public LetterPan m_EditorLetterPan;

    //单词列表脚本
    public WordList m_SrcWordListScript;

    //已分割单词列表脚本
    public WordList m_SplitWordListScript;

    List<string> WordList = new List<string> {"abcde", "dgdsa", "gsads", "sdfgd"};
    List<List<int>> WordSplitList = new List<List<int>>();

    public GameObject m_NextBtnObj;
    public GameObject m_ResetBtnObj;
    public GameObject m_GenerateBtnObj;
    public GameObject m_PlayBtnObj;

    public GamePlay m_PlayPanel;

    // Use this for initialization
    void Start()
    {
        //设置按钮回调
        m_NextBtnObj.GetComponent<Button>().onClick.AddListener(NextBtnClick);
        m_ResetBtnObj.GetComponent<Button>().onClick.AddListener(ResetBtnClick);
        m_GenerateBtnObj.GetComponent<Button>().onClick.AddListener(GenerateBtnClick);
        m_PlayBtnObj.GetComponent<Button>().onClick.AddListener(PlayBtnClick);

        m_BgLetterPan.SetData(WordList);
        m_EditorLetterPan.SetData(WordList);
        m_SrcWordListScript.AddWordList(WordList);
    }


    /// <summary>
    /// 下一步按钮回调
    /// </summary>
    void NextBtnClick()
    {
        List<int> wordIndexList;
        string splitWord;
        m_EditorLetterPan.ConfirmWord(out wordIndexList, out splitWord);

        WordSplitList.Add(wordIndexList);
        m_SplitWordListScript.AddWord(splitWord);
    }

    /// <summary>
    /// 重置按钮回调
    /// </summary>
    void ResetBtnClick()
    {
        m_EditorLetterPan.Reset();
        m_SplitWordListScript.Reset();
        WordSplitList.Clear();
    }

    /// <summary>
    /// 生成数据
    /// </summary>
    void GenerateBtnClick()
    {
        Debug.Log(WordSplitList);
    }

    /// <summary>
    /// 生成数据
    /// </summary>
    void PlayBtnClick()
    {
        m_PlayPanel.SetData(WordList,WordSplitList);
    }
}