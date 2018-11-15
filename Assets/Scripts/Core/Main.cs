using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    //行
    private int m_nRowNum = 5;
    //列
    private int m_nColNum = 4;
    private int m_nMaxRow = 5;

    public Transform m_Pan;
    private GridLayoutGroup m_GridLayoutGroup;
    private float m_PanMaxHeight;
    private float m_fMargin = 0;
    
    public GameObject m_PanBgCell;
    private List<GameObject> m_PanBgCellList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        m_GridLayoutGroup = m_Pan.GetComponent<GridLayoutGroup>();
        m_PanMaxHeight = m_Pan.GetComponent<RectTransform>().sizeDelta.y;
        m_fMargin = m_GridLayoutGroup.padding.top;
        InitLayout();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitLayout()
    {
        var cellSideLenth = (m_PanMaxHeight - m_fMargin * 2) / m_nRowNum;
        m_Pan.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSideLenth * m_nColNum + m_fMargin * 2 ,m_PanMaxHeight);
        m_GridLayoutGroup.cellSize = new Vector2(cellSideLenth,cellSideLenth);
        FlushPanBgCell();
    }

    void FlushPanBgCell()
    {
        if (m_Pan.childCount < (m_nRowNum * m_nColNum))
        {
            for (int i = m_Pan.childCount; i <= (m_nRowNum * m_nColNum); i++)
            {
                GameObject cell = Instantiate(m_PanBgCell) as GameObject;
                cell.transform.SetParent(m_Pan);
                cell.transform.localPosition = Vector3.zero;
                cell.transform.localScale = Vector3.one;
                
            }
        }
        
        for (int i = 0; i < m_Pan.childCount;  i++)
        {
            m_Pan.GetChild(i).gameObject.SetActive(i<(m_nRowNum * m_nColNum));
        }


        for (int i = 0; i <= (m_nRowNum * m_nColNum); i++)
        {
            int index = i + 1;
            int row = (index % m_nColNum != 0)?(index/m_nColNum +1): (index / m_nColNum);
            int col = (index % m_nColNum == 0)?(m_nColNum): (index % m_nColNum);
            m_Pan.GetChild(i).GetComponent<Image>().color = ((row + col) % 2 == 0)?Color.gray:Color.clear;
        }
    }

    public void DropDownListener(int index)
    {
        m_nRowNum = m_nMaxRow + index;
        m_nColNum = m_nRowNum - 1;
        InitLayout();
    }
}