using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	//行
	private int m_nRowNum = 5;
	//列
	private int m_nColNum = 4;
	private int m_nMaxRow = 5;
	//背景展示
	public Transform m_BgPan;
	private GridLayoutGroup m_BgGridLayoutGroup;
	//Cell
	public Transform m_CellPan;
	private GridLayoutGroup m_CellGridLayoutGroup;
	
	
	private float m_PanMaxHeight;
	private float m_fMargin = 0;

	public GameObject m_PanBgCell;
	public GameObject m_Cell;

	List<string> WordList = new List<string> { "abcde", "dgdsa", "gsads", "sdfgd" };

	// Use this for initialization
	void Start() {
		m_BgGridLayoutGroup = m_BgPan.GetComponent<GridLayoutGroup>();
		m_CellGridLayoutGroup = m_CellPan.GetComponent<GridLayoutGroup>();
		m_PanMaxHeight = m_BgPan.GetComponent<RectTransform>().sizeDelta.y;
		m_fMargin = m_BgGridLayoutGroup.padding.top;
		InitLayoutBg();
	}

	//初始化布局
	void InitLayoutBg() {
		var cellSideLenth = (m_PanMaxHeight - m_fMargin * 2) / m_nRowNum;
		Vector2 setSize = new Vector2(cellSideLenth * m_nColNum + m_fMargin * 2, m_PanMaxHeight);
		m_BgPan.GetComponent<RectTransform>().sizeDelta = setSize;
		m_CellPan.GetComponent<RectTransform>().sizeDelta = setSize;
		m_BgGridLayoutGroup.cellSize = new Vector2(cellSideLenth, cellSideLenth);
		m_CellGridLayoutGroup.cellSize = new Vector2(cellSideLenth, cellSideLenth);
		FlushPanBgCell();
		FlushLetterCell();
	}

	
	//初始化背景格子
	void FlushLetterCell() {
		if (m_CellPan.childCount < (m_nRowNum * m_nColNum)) {
			for (int i = m_CellPan.childCount; i <= (m_nRowNum * m_nColNum); i++) {
				GameObject cell = Instantiate(m_Cell);
				cell.transform.SetParent(m_CellPan);
				cell.transform.localPosition = Vector3.zero;
				cell.transform.localScale = Vector3.one;

			}
		}

		var wordList = WordList.ToString();
		for (int i = 0; i < m_CellPan.childCount; i++) {
			m_CellPan.GetChild(i).gameObject.SetActive(i < (m_nRowNum * m_nColNum));
			m_CellPan.GetChild(i).GetComponent<Letter>().SetData(i,wordList.Substring(i,1));
		}
	}

	//初始化背景格子
	void FlushPanBgCell() {
		if (m_BgPan.childCount < (m_nRowNum * m_nColNum)) {
			for (int i = m_BgPan.childCount; i <= (m_nRowNum * m_nColNum); i++) {
				GameObject cell = Instantiate(m_PanBgCell);
				cell.transform.SetParent(m_BgPan);
				cell.transform.localPosition = Vector3.zero;
				cell.transform.localScale = Vector3.one;

			}
		}

		for (int i = 0; i < m_BgPan.childCount; i++) {
			m_BgPan.GetChild(i).gameObject.SetActive(i < (m_nRowNum * m_nColNum));
		}


		for (int i = 0; i <= (m_nRowNum * m_nColNum); i++) {
			int index = i + 1;
			int row = (index % m_nColNum != 0) ? (index / m_nColNum + 1) : (index / m_nColNum);
			int col = (index % m_nColNum == 0) ? (m_nColNum) : (index % m_nColNum);
			m_BgPan.GetChild(i).GetComponent<Image>().color = ((row + col) % 2 == 0) ? Color.gray : Color.clear;
		}
	}

	public void DropDownListener(int index) {
		m_nRowNum = m_nMaxRow + index;
		m_nColNum = m_nRowNum - 1;
		InitLayoutBg();
	}
}