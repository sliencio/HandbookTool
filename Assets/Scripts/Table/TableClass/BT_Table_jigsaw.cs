using System;
using System.IO;
using System.Collections.Generic;
public class BT_Data_jigsaw { 
	private Int32 m_nLevel;				  //// 关卡 
	public Int32 ID { get { return m_nLevel; } } 
	public Int32 Level { get { return m_nLevel; } } 
	private Int32 m_nSection;				  //// 章节 
	public Int32 Section { get { return m_nSection; } } 
	private string m_sWords;				  //// 单词 
	public string Words { get { return m_sWords; } } 
	private Int2 m_n2Size;				  //// 尺寸(行：列) 
	public Int2 Size { get { return m_n2Size; } } 
	private string m_sLayout;				  //// 布局 
	public string Layout { get { return m_sLayout; } } 
	private Int32 m_nPrizeID1;				  //// 奖励1 
	public Int32 PrizeID1 { get { return m_nPrizeID1; } } 
	private Int32 m_nPrizeCount1;				  //// 个数1 
	public Int32 PrizeCount1 { get { return m_nPrizeCount1; } } 
	private Int32 m_nPrizeID2;				  //// 奖励2 
	public Int32 PrizeID2 { get { return m_nPrizeID2; } } 
	private Int32 m_nPrizeCount2;				  //// 个数2 
	public Int32 PrizeCount2 { get { return m_nPrizeCount2; } } 
	private Int32 m_nPrizeID3;				  //// 奖励3 
	public Int32 PrizeID3 { get { return m_nPrizeID3; } } 
	private Int32 m_nPrizeCount3;				  //// 个数3 
	public Int32 PrizeCount3 { get { return m_nPrizeCount3; } } 
	public void ReadMemory ( BinaryReader reader ) { 
		m_nLevel = reader.ReadInt32();
		m_nSection = reader.ReadInt32();
		m_sWords = TableTools.ReadString(reader);
		m_n2Size = new Int2(reader.ReadInt32(),reader.ReadInt32());
		m_sLayout = TableTools.ReadString(reader);
		m_nPrizeID1 = reader.ReadInt32();
		m_nPrizeCount1 = reader.ReadInt32();
		m_nPrizeID2 = reader.ReadInt32();
		m_nPrizeCount2 = reader.ReadInt32();
		m_nPrizeID3 = reader.ReadInt32();
		m_nPrizeCount3 = reader.ReadInt32();
	}
};
public class BT_Table_jigsaw{ 
	const string FILE_MD5_CODE = "F2FEFE109E6F585BCB93273E62760433";
	private string fileName = "";
	private Dictionary<Int32,BT_Data_jigsaw> m_dataArray = new Dictionary<Int32,BT_Data_jigsaw>();
	public BT_Table_jigsaw (string fileName) {
		this.fileName = fileName;
	}
	public void CheckVersion() {
		byte[] buffer = TableTools.GetZipBuffer(fileName);
		MemoryStream stream = new MemoryStream(buffer);
		BinaryReader reader = new BinaryReader(stream);
		reader.ReadInt32();
		reader.ReadInt32();
		string strMD5Code = TableTools.ReadString(reader);
		if (strMD5Code != FILE_MD5_CODE) { 
			throw new System.Exception("文件" + fileName + "版本验证失败");
		}
	}
	public void Initialize() {
		m_dataArray.Clear();
		byte[] buffer = TableTools.GetZipBuffer(fileName);
		MemoryStream stream = new MemoryStream(buffer);
		BinaryReader reader = new BinaryReader(stream);
		Int32 iRow = reader.ReadInt32();
		Int32 iColums = reader.ReadInt32();
		string strMD5Code = TableTools.ReadString(reader);
		if (strMD5Code != FILE_MD5_CODE) 
			throw new System.Exception("文件" + fileName + "版本验证失败");
		Int32[] arrayType = new Int32[iColums];
		Int32[] arrayArray = new Int32[iColums];
		for (Int32 i = 0; i < iColums; ++i) {
			arrayType[i] = reader.ReadInt32();
			arrayArray[i] = reader.ReadInt32();
		}
		for (Int32 i = 0; i < iRow; ++i) {
			BT_Data_jigsaw pData = new BT_Data_jigsaw();
			pData.ReadMemory(reader);
			if (Contains(pData.ID))
				throw new System.Exception("文件" + fileName + "有重复项 ID : " + pData.ID);
			else
				m_dataArray.Add (pData.ID,pData);
		}
	}
	public bool Contains(Int32 ID) {
		return m_dataArray.ContainsKey(ID);
	}
	public BT_Data_jigsaw GetElement(Int32 ID) {
		if (m_dataArray.ContainsKey(ID))
			return m_dataArray[ID];
		return null;
	}
	public int Count() {
		return m_dataArray.Count;
	}
	public Dictionary<Int32,BT_Data_jigsaw> Datas() {
		return m_dataArray;
	}
};
