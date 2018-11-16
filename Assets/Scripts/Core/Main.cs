using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    //行
    private int m_nRowNum = 4;

    //列
    private int m_nColNum = 5;

    //最小行数
    private int m_nMinRow = 4;

    //Cell
    public Transform m_CellPan;

    private GridLayoutGroup m_CellGridLayoutGroup;

    //背景控制脚本
    public BgPanControl BgPanControlScript;

    //pan 最大宽度
    private float m_PanMaxWidth;

    //边距
    private float m_fMargin = 0;

    //字母格子模板
    public GameObject m_Cell;

    List<string> WordList = new List<string> {"abcde", "dgdsa", "gsads", "sdfgd"};

    List<List<int>> WordSplitList = new List<List<int>>();

    public GameObject m_NextBtnObj;
    public GameObject m_ResetBtnObj;
    public GameObject m_GenerateBtnObj;

    // Use this for initialization
    void Start()
    {
        m_NextBtnObj.GetComponent<Button>().onClick.AddListener(NextBtnClick);
        m_ResetBtnObj.GetComponent<Button>().onClick.AddListener(ResetBtnClick);
        m_GenerateBtnObj.GetComponent<Button>().onClick.AddListener(GenerateBtnClick);
        m_CellGridLayoutGroup = m_CellPan.GetComponent<GridLayoutGroup>();
        m_PanMaxWidth = m_CellPan.GetComponent<RectTransform>().sizeDelta.x;
        m_fMargin = m_CellGridLayoutGroup.padding.top;
        InitLayoutBg();


        var datalist = DataManager.Instance().GetWordListBySectionId(1);
        foreach (string works in datalist)
        {
            Debug.Log(works);
        }
    }

    /// <summary>
    /// 初始化布局
    /// </summary>
    void InitLayoutBg()
    {
        var cellSideLenth = (m_PanMaxWidth - m_fMargin * 2) / m_nColNum;
        Vector2 setSize = new Vector2(m_PanMaxWidth, cellSideLenth * m_nRowNum + m_fMargin * 2);
        m_CellPan.GetComponent<RectTransform>().sizeDelta = setSize;
        m_CellGridLayoutGroup.cellSize = new Vector2(cellSideLenth, cellSideLenth);
        FlushLetterCell();
        BgPanControlScript.SetData(m_nRowNum, m_nColNum, setSize, cellSideLenth);
    }


    /// <summary>
    /// 初始化背景格子
    /// </summary>
    void FlushLetterCell()
    {
        if (m_CellPan.childCount < (m_nRowNum * m_nColNum))
        {
            for (int i = m_CellPan.childCount; i < (m_nRowNum * m_nColNum); i++)
            {
                GameObject cell = Instantiate(m_Cell);
                cell.transform.SetParent(m_CellPan);
                cell.name = "letter_" + i;
                cell.transform.localPosition = Vector3.zero;
                cell.transform.localScale = Vector3.one;
            }
        }

        for (int i = 0; i < m_CellPan.childCount; i++)
        {
            m_CellPan.GetChild(i).gameObject.SetActive(i < (m_nRowNum * m_nColNum));
            m_CellPan.GetChild(i).GetComponent<Letter>().SetData(i, GetLetterStr(i));
        }
    }

    /// <summary>
    /// 获取单词字母
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    string GetLetterStr(int index)
    {
        Int2 wordIndex = GetRowCol(index);
//        Debug.Log("index:" + index + "行列：" + wordIndex.field1 + ":" + wordIndex.field2);
//        return wordIndex.field1 + ":" + wordIndex.field2;
        if (WordList.Count > wordIndex.field1)
        {
            string word = WordList[wordIndex.field1];
            return word.Substring(wordIndex.field2, 1);
        }

        return string.Empty;
    }

    /// <summary>
    /// 根据位置，获取行列
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    Int2 GetRowCol(int index)
    {
        int curRow = index / m_nColNum;
        int curCol = index % m_nColNum;
        return new Int2(curRow, curCol);
    }

    /// <summary>
    /// 下拉框回调
    /// </summary>
    /// <param name="index"></param>
    public void DropDownListener(int index)
    {
        m_nRowNum = m_nMinRow + index;
        m_nColNum = m_nRowNum + 1;
        InitLayoutBg();
    }

    /// <summary>
    /// 下一步按钮回调
    /// </summary>
    void NextBtnClick()
    {
        Color cellColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        var tempWordIndexList = new List<int>();
        foreach (Transform cell in m_CellPan)
        {
            Letter letter = cell.GetComponent<Letter>();
            if (letter.GetSelectStatus() && !letter.GetPickUpStatus())
            {
                letter.SetColor(cellColor);
                letter.SetPickUpStatus(true);
                tempWordIndexList.Add(letter.GetLetterIndex());
            }
        }

        WordSplitList.Add(tempWordIndexList);
    }

    /// <summary>
    /// 重置按钮回调
    /// </summary>
    void ResetBtnClick()
    {
        foreach (Transform cell in m_CellPan)
        {
            cell.GetComponent<Letter>().Reset();
        }

        WordSplitList.Clear();
    }

    /// <summary>
    /// 生成数据
    /// </summary>
    void GenerateBtnClick()
    {
        Debug.Log(WordSplitList);
    }
}