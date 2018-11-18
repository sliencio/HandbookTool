using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : BaseLetter, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Text m_LetterText;
    private GameObject target = null;
    private string m_Tag = string.Empty;
    private Vector3 offset;
    private int m_NormalLayer = 110;
    private int m_SelectLayer = 111;

    private UIDepthControl m_UIDepthControlScript;

    private RectTransform m_RecTransform;

    // Use this for initialization
    void Start()
    {
        m_RecTransform = gameObject.GetComponent<RectTransform>();
        m_UIDepthControlScript = GetComponent<UIDepthControl>();
        m_UIDepthControlScript.Depth = m_NormalLayer;
        CalOffset();
    }

    // Update is called once per frame
    void Update()
    {
        if (null != target)
        {
            transform.position = target.transform.position + offset;
        }
    }


    private void OnEnable()
    {
        EventManager.Instance.LetterPointDownEvent += LetterPointDown;
        EventManager.Instance.LetterPointUpEvent += LetterPointUp;
    }

    private void OnDisable()
    {
        EventManager.Instance.LetterPointDownEvent -= LetterPointDown;
        EventManager.Instance.LetterPointUpEvent -= LetterPointUp;
    }


    public override void Reset()
    {
        m_LetterStr = string.Empty;
        m_LetterIndex = -1;
        m_Tag = string.Empty;
        target = null;
    }

    /// <summary>
    /// 字母按下通知
    /// </summary>
    /// <param name="go"></param>
    void LetterPointDown(GameObject go)
    {
        if (gameObject != go && go.GetComponent<Letter>().GetTag() == m_Tag)
        {
            SetTarget(go);
            m_UIDepthControlScript.Depth = m_SelectLayer;
        }
        //该字母随意父节点
        else if (gameObject == go)
        {
            SetTarget(null);
        }
    }

    /// <summary>
    /// 字母抬起通知
    /// </summary>
    /// <param name="go"></param>
    void LetterPointUp(GameObject go)
    {
        //按下
        SetTarget(null);
        m_UIDepthControlScript.Depth = m_NormalLayer;
    }

    /// <summary>
    /// 字母按下事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //抬起
        EventManager.Instance.LetterPointDown(gameObject);
        m_UIDepthControlScript.Depth = m_SelectLayer;
    }

    /// <summary>
    /// 字母抬起事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        EventManager.Instance.LetterPointUp(gameObject);
        m_UIDepthControlScript.Depth = m_NormalLayer;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RecTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            m_RecTransform.position = globalMousePos;
        }
    }

    /// <summary>
    /// 设置子母
    /// </summary>
    /// <param name="letterStr"></param>
    public override void SetLetter(string letterStr)
    {
        m_LetterStr = letterStr;
        if (null != m_LetterText)
        {
            m_LetterText.text = letterStr;
        }
    }

    /// <summary>
    /// 设置目标点
    /// </summary>
    /// <param name="go"></param>
    public void SetTarget(GameObject go)
    {
        target = go;
        CalOffset();
    }

    /// <summary>
    /// 设置偏移量
    /// </summary>
    void CalOffset()
    {
        if (null != target)
        {
            offset = target.transform.position - transform.position;
        }
    }

    /// <summary>
    /// 设置tag
    /// </summary>
    /// <param name="tag"></param>
    public void SetTag(string tag)
    {
        m_Tag = tag;
    }

    /// <summary>
    /// 获取tag
    /// </summary>
    /// <returns></returns>
    public string GetTag()
    {
        return m_Tag;
    }
}