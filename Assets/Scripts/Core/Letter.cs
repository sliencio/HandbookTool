using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : BaseLetter, IDragHandler,IBeginDragHandler, IEndDragHandler
{
    public Text m_LetterText;
    private GameObject target = null;
    private string m_Tag = string.Empty;
    private Vector3 offset;
    private int m_NormalLayer = 110;
    private int m_SelectLayer = 111;
    private bool m_bIsMoveParent = false;

    private UIDepthControl m_UIDepthControlScript;

    private RectTransform m_RecTransform;

    // Use this for initialization
    void Start()
    {
        m_RecTransform = gameObject.GetComponent<RectTransform>();
        m_UIDepthControlScript = GetComponent<UIDepthControl>();
        m_UIDepthControlScript.Depth = m_NormalLayer;
        CalOffset(target);
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
            m_bIsMoveParent = true;
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

    public void OnDrag(PointerEventData eventData)
    {
//        transform.position = Input.mousePosition; //当前位置为鼠标所在位置
        Vector3 globalMousePos;
        if (m_bIsMoveParent && RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RecTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
        }
    }
    
    /// <summary>
    /// 开始拖动
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag (PointerEventData eventData) {
        Vector3 globalMousePos;
        if (m_bIsMoveParent && RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RecTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
        }
        //抬起
        EventManager.Instance.LetterPointDown(gameObject);
        m_UIDepthControlScript.Depth = m_SelectLayer;
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        m_bIsMoveParent = false;
        EventManager.Instance.LetterPointUp(gameObject);
        m_UIDepthControlScript.Depth = m_NormalLayer;
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
        if (gameObject == go)
        {
            m_bIsMoveParent = false;
        }
        CalOffset(go);
        target = go;
        
    }

    /// <summary>
    /// 设置偏移量
    /// </summary>
    void CalOffset(GameObject go)
    {
        if (null != go)
        {
            offset = transform.position- go.transform.position;
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