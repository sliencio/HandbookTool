using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorLetter : BaseLetter, IPointerDownHandler
{
    public Text letter;

    Image bg;

    //是否被选中
    private bool m_IsSelect = false;

    //是否被捡起
    private bool m_IsPickUp = false;

    //bool isDrag
    private bool isDrag = false;

    //取消选中的颜色
    Color UnSelectColor = new Color(1, 1, 1, 0);

    //选中颜色
    Color SelectColor = new Color(0, 0.5f, 0.8f, 1);
    private RectTransform m_RecTransform;


    // Use this for initialization
    void Start()
    {
        m_RecTransform = gameObject.GetComponent<RectTransform>();
        bg = GetComponent<Image>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!m_IsPickUp)
            SetSelect();
    }

    /// <summary>
    /// 设置子母
    /// </summary>
    /// <param name="letterStr"></param>
    public override void SetLetter(string letterStr)
    {
        m_LetterStr = letterStr;
        if (null != letter)
        {
            letter.text = letterStr;
        }
    }

    /// <summary>
    /// 设置大小
    /// </summary>
    /// <param name="size"></param>
    public void SetSize(Vector2 size)
    {
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void SetPickUpStatus(bool isPicked)
    {
        m_IsPickUp = isPicked;
    }

    public bool GetPickUpStatus()
    {
        return m_IsPickUp;
    }

    /// <summary>
    /// 获取选中状态
    /// </summary>
    /// <returns></returns>
    public bool GetSelectStatus()
    {
        return m_IsSelect;
    }


    /// <summary>
    /// 设置选中状态
    /// </summary>
    void SetSelect()
    {
        m_IsSelect = !m_IsSelect;
        bg.color = m_IsSelect ? SelectColor : UnSelectColor;
    }

    /// <summary>
    /// 设置颜色
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        bg.color = color;
    }

    /// <summary>
    /// 重置
    /// </summary>
    public override void Reset()
    {
        SetColor(UnSelectColor);
        m_IsSelect = false;
        m_IsPickUp = false;
    }
}