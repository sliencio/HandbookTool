using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<int, List<string>> SectionWordData = new Dictionary<int, List<string>>();

    private static DataManager m_Instance;

    public static DataManager Instance()
    {
        if (null == m_Instance)
        {
            InitData();
            m_Instance = new DataManager();
        }

        return m_Instance;
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    static void InitData()
    {
        SectionWordData.Clear();
        foreach (var jigsawData in TableManager.Instance.TableJigsaw.Datas().Values)
        {
            if (!SectionWordData.ContainsKey(jigsawData.Section))
            {
                SectionWordData[jigsawData.Section] = new List<string>();
            }

            SectionWordData[jigsawData.Section].Add(jigsawData.Words);
        }
    }

    /// <summary>
    /// 返回该场景所有的word列表
    /// </summary>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    public List<string> GetWordListBySectionId(int sectionId)
    {
        if (SectionWordData.ContainsKey(sectionId))
            return SectionWordData[sectionId];
        return new List<string>();
    }
}