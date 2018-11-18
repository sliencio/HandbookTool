using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class UIDepthControl : MonoBehaviour
{
	/// <summary>
  	///开始启动
	/// </summary>
	public bool StartEnble = true;
    /// <summary>
    /// 深度，默认为零
    /// </summary>
    public int depth;
	/// <summary>
	/// 排序层名称
	/// </summary>
	public string sortLayerName = "UI";
    /// <summary>
    /// 是否是UI
    /// </summary>
    public bool isUI = true;

    // Use this for initialization
    void Start()
    {
		if (StartEnble) {
			this.refreshDepth ();
		}
    }

    private void refreshDepth()
    {
        if (isUI)
        {
            Canvas canvas = this.GetComponent<Canvas>();
            GraphicRaycaster raycaster = this.GetComponent<GraphicRaycaster>();
			if (canvas == null) canvas = this.gameObject.AddComponent<Canvas>();
            if (raycaster == null) raycaster = this.gameObject.AddComponent<GraphicRaycaster>();
			canvas.overrideSorting = true;
			canvas.sortingLayerName = sortLayerName;
            canvas.sortingOrder = this.depth;
        }
        else
        {
            Renderer[] renders = GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders)
            {
				render.sortingLayerName = sortLayerName;
                render.sortingOrder = this.depth;
            }
        }
    }

	public void RemoveCanvas()
	{
		if (isUI) {
			GraphicRaycaster raycaster = this.GetComponent<GraphicRaycaster>();
			if (raycaster != null) {
				Destroy (raycaster);
			}
			Canvas canvas = this.GetComponent<Canvas> ();
			if (canvas != null) {
				Destroy (canvas);
			}
		}
	}

	public int Depth {
		set {
			depth = value;
			refreshDepth ();
		} 
		get {
			return depth;
		}
	}
}
