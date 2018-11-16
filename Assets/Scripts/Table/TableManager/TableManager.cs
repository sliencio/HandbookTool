public class TableManager
{	
	private static TableManager _instance = null;
	public static TableManager Instance {
		get {
			if(_instance == null) {
				_instance = new TableManager ();
			}
			return _instance;
		}
	}
	
	private BT_Table_jigsaw mTableJigsaw = null;
	private bool mTableInitializeAchievement = false;
	private BT_Table_jigsaw CheckTableJigsaw {
		get {
			if (mTableJigsaw == null) {
				mTableJigsaw = new BT_Table_jigsaw ("jigsaw");
				mTableJigsaw.CheckVersion();
			}
			return mTableJigsaw;
		}
	}
	public BT_Table_jigsaw TableJigsaw {
		get {
			if (mTableInitializeAchievement == false) {
				mTableInitializeAchievement = true;
				CheckTableJigsaw.Initialize ();
			}
			return CheckTableJigsaw;
		}
	}
}

