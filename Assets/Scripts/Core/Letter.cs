using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : BaseLetter, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //顶部条留的高度
    private float m_fTopBarHeight = 50f;
    private bool m_bIsInit = false;
    public Text m_LetterText;
    private GameObject target = null;
    private string m_Tag = string.Empty;

    private Vector3 offset;

    //没有选中，或者别的字母选中过层
    private int m_NormalLayer = 110;

    //选中并放下的层级
    private int m_CommonLayer = 111;

    //选中层级
    private int m_SelectLayer = 112;
    private bool m_bIsMoveParent = false;

    private RectTransform m_RecTransform;

    // Use this for initialization
    void Start()
    {
        if (!m_bIsInit)
        {
            m_RecTransform = gameObject.GetComponent<RectTransform>();
            CalOffset(target);
            m_bIsInit = true;
        }
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

    /// <summary>
    /// 重置
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        m_LetterIndex = -1;
        target = null;
    }

    /// <summary>
    /// 字母按下通知
    /// </summary>
    /// <param name="go"></param>
    void LetterPointDown(GameObject go)
    {
        if (go.GetComponent<Letter>().GetTag() != m_Tag)
        {
        }
        else
        {
            gameObject.transform.SetAsLastSibling();
            if (gameObject != go)
            {
                SetTarget(go);
            }
            //该字母随意父节点
            else if (gameObject == go)
            {
                m_bIsMoveParent = true;
                SetTarget(null);
            }
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
    }

    public void OnDrag(PointerEventData eventData)
    {
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (m_bIsMoveParent && RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RecTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
        }

        //抬起
        EventManager.Instance.LetterPointDown(gameObject);
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    /// <param name="eventData">Event data.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        m_bIsMoveParent = false;
        EventManager.Instance.LetterPointUp(gameObject);
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
    /// 设置大小
    /// </summary>
    /// <param name="size"></param>
    public void SetSize(Vector2 size)
    {
        if (!m_bIsInit)
        {
            Start();
        }

        m_RecTransform.sizeDelta = size;
    }

    /// <summary>
    /// 设置偏移量
    /// </summary>
    void CalOffset(GameObject go)
    {
        if (null != go)
        {
            offset = transform.position - go.transform.position;
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