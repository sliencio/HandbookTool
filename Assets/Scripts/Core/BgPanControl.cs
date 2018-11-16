using UnityEngine;
using UnityEngine.UI;

public class BgPanControl : MonoBehaviour {
	//背景展示
	public Transform m_BgPan;
	private GridLayoutGroup m_BgGridLayoutGroup;
	private float m_fMargin = 0;
	public GameObject m_PanBgCell;
	private bool isDisplayEffect = false;

	// Use this for initialization
	void Start() {
		m_BgGridLayoutGroup = m_BgPan.GetComponent<GridLayoutGroup>();
	}

	public void SetData(int row,int col,Vector2 size,float cellSideLength)
	{
		m_BgPan.GetComponent<RectTransform>().sizeDelta = size;
		if (!isDisplayEffect)
		{
			return;
		}
		m_BgGridLayoutGroup.cellSize = new Vector2(cellSideLength, cellSideLength);
		FlushPanBgCell(row,col);
	}

	//初始化背景格子
	void FlushPanBgCell(int row,int col) {
		if (m_BgPan.childCount < (row * col)) {
			for (int i = m_BgPan.childCount; i <= (row * col); i++) {
				GameObject cell = Instantiate(m_PanBgCell);
				cell.transform.SetParent(m_BgPan);
				cell.transform.localPosition = Vector3.zero;
				cell.transform.localScale = Vector3.one;

			}
		}
		for (int i = 0; i < m_BgPan.childCount; i++) {
			m_BgPan.GetChild(i).gameObject.SetActive(i < (row * col));
		}


		for (int i = 0; i <= (row * col); i++) {
			int index = i + 1;
			int curRow = (index % col != 0) ? (index / col + 1) : (index / col);
			int curCol = (index % col == 0) ? (col) : (index % col);
			m_BgPan.GetChild(i).GetComponent<Image>().color = ((curRow + curCol) % 2 == 0) ? Color.gray : Color.clear;
		}
	}
}
