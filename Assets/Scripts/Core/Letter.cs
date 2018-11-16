using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Letter : MonoBehaviour, IPointerDownHandler
{
    public Text letter;

    //字母索引
    public int letterIndex = 0;

    Image bg;

    //是否被选中
    private bool m_IsSelect = false;

    //是否被捡起
    private bool m_IsPickUp = false;

    //取消选中的颜色
    Color UnSelectColor = new Color(1, 1, 1, 0);

    //选中颜色
    Color SelectColor = new Color(0, 0.5f, 0.8f, 1);

    // Use this for initialization
    void Start()
    {
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
    public void SetLetter(string letterStr)
    {
        letter.text = letterStr;
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

    public int GetLetterIndex()
    {
        return letterIndex;
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

    public void SetData(int index, string letterStr)
    {
        letterIndex = index;
        SetLetter(letterStr);
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        SetColor(UnSelectColor);
        m_IsSelect = false;
        m_IsPickUp = false;
    }
}