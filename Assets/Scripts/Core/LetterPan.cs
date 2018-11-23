using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LetterPanType
{
    EDITOR,
    BACKGROUND,
    MAIN
}

public class LetterPan : MonoBehaviour
{
    private bool m_bIsInit = false;

    public LetterPanType m_PanType;

    //行
    private int m_nRowNum = 4;

    //列
    private int m_nColNum = 5;
    private GridLayoutGroup m_CellGridLayoutGroup;

    private RectTransform m_RectTransform;

    //pan 最大宽度
    private float m_PanMaxWidth;

    //边距
    private float m_fMargin = 0;

    //m_Cell
    public GameObject m_Cell;

    private List<string> m_WordList = new List<string>();

    //根据索引字典表
    private Dictionary<int, GameObject> m_LetterIndexDict = new Dictionary<int, GameObject>();
    public bool m_ShowEffect = false;

    // Use this for initialization
    void Start()
    {
        if (!m_bIsInit)
        {
            m_CellGridLayoutGroup = transform.GetComponent<GridLayoutGroup>();
            m_RectTransform = transform.GetComponent<RectTransform>();
            m_PanMaxWidth = m_RectTransform.sizeDelta.x;
            m_fMargin = m_CellGridLayoutGroup.padding.top;
            m_bIsInit = true;
        }
    }

    public void SetData(List<string> wordList)
    {
        if (!m_bIsInit)
        {
            Start();
        }

        m_WordList = wordList;
        InitLayoutBg(wordList);
    }

    /// <summary>
    /// 初始化布局
    /// </summary>
    void InitLayoutBg(List<string> wordList)
    {
        m_nRowNum = wordList.Count;
        m_nColNum = wordList[0].Length;
        var cellSideLenth = (m_PanMaxWidth - m_fMargin * 2) / m_nColNum;
        Vector2 setSize = new Vector2(m_PanMaxWidth, cellSideLenth * m_nRowNum + m_fMargin * 2);
        m_RectTransform.sizeDelta = setSize;
        m_CellGridLayoutGroup.cellSize = new Vector2(cellSideLenth, cellSideLenth);
        FlushLetterCell();
    }


    /// <summary>
    /// 初始化背景格子
    /// </summary>
    void FlushLetterCell()
    {
        if (transform.childCount < (m_nRowNum * m_nColNum))
        {
            for (int i = transform.childCount; i < (m_nRowNum * m_nColNum); i++)
            {
                GameObject cell = Instantiate(m_Cell);
                cell.transform.SetParent(transform);
                cell.name = "letter_" + i;
                cell.transform.localPosition = Vector3.zero;
                cell.transform.localScale = Vector3.one;
                m_LetterIndexDict[i] = cell;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < (m_nRowNum * m_nColNum));
            BaseLetter letter = transform.GetChild(i).GetComponent<BaseLetter>();
            letter.SetData(i, GetLetterStr(i), GetRowCol(i));
            SetLetterBrothers(letter);
        }

        //展示效果
        if (m_ShowEffect)
        {
            for (int i = 0; i < m_nColNum * m_nRowNum; i++)
            {
                int curRow = i / m_nColNum;
                int curCol = i % m_nColNum;
                transform.GetChild(i).GetComponent<Image>().color = ((curRow + curCol) % 2 == 0) ? Color.gray : Color.clear;
            }
        }
    }

    /// <summary>
    /// 设置字母的周围
    /// </summary>
    /// <param name="letter"></param>
    void SetLetterBrothers(BaseLetter letter)
    {
        int letterIndex = letter.m_LetterIndex;
        //左
        if (letter.m_IndexPos.field2 != 0)
        {
            letter.m_LeftLetter = GetBaseLetter(letterIndex - 1);
        }

        //右
        if (letter.m_IndexPos.field2 != m_nColNum - 1)
        {
            letter.m_RightLetter = GetBaseLetter(letterIndex + 1);
        }

        //上
        if (letter.m_IndexPos.field1 != 0)
        {
            letter.m_TopLetter = GetBaseLetter(letterIndex - m_nColNum);
        }

        //下
        if (letter.m_IndexPos.field1 != m_nRowNum - 1)
        {
            letter.m_BottomLetter = GetBaseLetter(letterIndex + m_nColNum);
        }
    }

    BaseLetter GetBaseLetter(int index)
    {
        if (m_LetterIndexDict.ContainsKey(index))
        {
            return m_LetterIndexDict[index].GetComponent<BaseLetter>();
        }
        Debug.Log("error:"+index);
        return null;
    }

    /// <summary>
    /// 获取单词字母
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetLetterStr(int index)
    {
        Int2 wordIndex = GetRowCol(index);
//        return wordIndex.field1 + ":" + wordIndex.field2;
        if (m_WordList.Count > wordIndex.field1)
        {
            string word = m_WordList[wordIndex.field1];
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

    public void Reset()
    {
        foreach (Transform cell in transform)
        {
            cell.GetComponent<EditorLetter>().Reset();

            switch (m_PanType)
            {
                case LetterPanType.EDITOR:
                    cell.GetComponent<EditorLetter>().Reset();
                    break;
                case LetterPanType.BACKGROUND:
                    cell.GetComponent<BaseLetter>().Reset();
                    break;
                case LetterPanType.MAIN:
                    cell.GetComponent<Letter>().Reset();
                    break;
            }
        }
    }

    /// <summary>
    /// 确认选中
    /// </summary>
    public void ConfirmWord(out List<int> wordIndexList, out string splitWord)
    {
        splitWord = string.Empty;
        wordIndexList = new List<int>();
        if (m_PanType != LetterPanType.EDITOR)
        {
            return;
        }

        Color cellColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        foreach (Transform cell in transform)
        {
            EditorLetter letter = cell.GetComponent<EditorLetter>();
            if (letter.GetSelectStatus() && !letter.GetPickUpStatus())
            {
                letter.SetColor(cellColor);
                letter.SetPickUpStatus(true);
                wordIndexList.Add(letter.GetLetterIndex());
                splitWord += letter.GetLetterStr();
            }
        }
    }

    /// <summary>
    /// 取消layout组件
    /// </summary>
    public void SetLayoutComponentEnable(bool isEnable)
    {
        if (!m_bIsInit)
        {
            Start();
        }

        m_CellGridLayoutGroup.enabled = isEnable;
    }

    /// <summary>
    /// 获取cell的大小
    /// </summary>
    /// <returns></returns>
    public Vector2 GetCellSize()
    {
        return m_CellGridLayoutGroup.cellSize;
    }
}