using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;
using System.Linq;
using UnityEngine.Events;
// C:\Users\johny\AppData\LocalLow\DefaultCompany\UnityProject4\FungusSaves
public class MultiSaveManager : MonoBehaviour {

    [SerializeField]
    [Range(1,9)]
    int _slotsMax = 9, _autoSaveSlotNum=1;
    [SerializeField]
    int screenScaleCompressFactor = 5;

    Flowchart _flowchart;
    public Flowchart flowchart
    {
        get
        {
            if(_flowchart==null)
            {
                var obj = GameObject.Find("Flowchart");
                if (obj != null)
                    _flowchart = obj.GetComponent<Flowchart>();
            }
            return _flowchart;
        }
    }
    [SerializeField]
    List<string> lockedToOpenMenuBlocks = new List<string>();
    [SerializeField]
    List<string> lockedToSaveBlocks = new List<string>();

    public float blockMenuTime=3;
    public float autosaveDelayTime = 2;
    float _blockMenuTimeLeft = 0;
    
    public const string defaultSaveDataKey = "savedGame";
    public bool menuIsForcedBlocking { get; private set; }
	public static MultiSaveManager Instance { get; private set; }
    public static string STORAGE_DIRECTORY { get { return Application.persistentDataPath + "/FungusSaves/"; } }
    private static string GetFullFilePath(string saveDataKey)
    {
        return STORAGE_DIRECTORY + saveDataKey + ".json";
    }
    

    public Dictionary<int,SaveGameInfo> gameInfoData { get; private set; }
    public SaveGameInfo loadedGameInfo { get; private set; }
    public SaveGameInfo autosaveGameInfo { get; private set; }
    public SaveGameInfo FindGameInfoBySlotId(int id)
    {
        if (gameInfoData.ContainsKey(id))
            return gameInfoData[id];

        return null;
    }
    public Block GetExecutingBlock()
    {
        if (flowchart == null) return null;
        return flowchart.GetExecutingBlocks().FirstOrDefault();
    }
    public int GetSlotIdFromDataKey(string dataKey)
    {
        string[] arr = dataKey.Split('_');
        if (arr.Length <= 1) return -1;
        int lastIndex = arr.Length - 1;
        int id = -1;
        int.TryParse(arr[lastIndex], out id);
        return id;
    }
    public int slotsMax
    {
        get { return _slotsMax; }
    }
    public int autosaveSlotNum
    {
        get { return _autoSaveSlotNum; }
    }
    public bool isValidSlotId(int id)
    {
        return id > 0 && id <= slotsMax ? true : false;
    }
    public bool isCanSaveGame()
    {
        Block selectedBlock = GetExecutingBlock();
        if (MenuDialog.ActiveMenuDialog != null && MenuDialog.ActiveMenuDialog.gameObject.activeInHierarchy)
        {
            Debug.Log("dialog is actived");
            return true;
        }
        if (selectedBlock == null) return false;
        Debug.Log("selected block name: "+selectedBlock.BlockName);
        foreach (var block in lockedToSaveBlocks)
            if (block == selectedBlock.BlockName)
                return false;

        return true;
    }
    public bool isBlockMenu()
    {
        var block = GetExecutingBlock();
        if (block == null)
        {
            Debug.Log("block is null");
            return false;
        }
        if (menuIsForcedBlocking)
        {
            Debug.Log("menuIsForcedBlocking");
            return true;
        }
        foreach (var locked in lockedToOpenMenuBlocks)
            if (locked == block.BlockName)
            {
                Debug.Log("locked: "+ locked);
                return true;
            }

        return false;
    }
    public bool isMainMenu()
    {
        if (flowchart == null)
        {
            Debug.Log("flowchart is null");
            return false;
        }
        if (flowchart.SelectedBlock != null)
        {
            Debug.Log("flowchart.SelectedBlock Name: " + flowchart.SelectedBlock.BlockName);
        }

        return false;
    }
    
    public static string CreateGameDataKey(int slotId)
    {
        return defaultSaveDataKey + "_" + slotId;
    }
    public static string CreateGameInfoKey(int slotId)
    {
        return defaultSaveDataKey + "_info_" + slotId;
    }
    public SaveGameInfo GetLastOrNewFirstGameInfo()
    {
        if (gameInfoData == null)
        {
            Debug.Log("gameInfoData is null");
            gameInfoData = new Dictionary<int, SaveGameInfo>();
        }

        if (gameInfoData.Count==0 || (gameInfoData.Count==1 &&
            gameInfoData.ContainsKey(autosaveSlotNum)))
        {
            int slotNum = autosaveSlotNum + 1;
            string infoKey = CreateGameInfoKey(slotNum);
            string dataKey = CreateGameDataKey(slotNum);
            return new SaveGameInfo(infoKey, dataKey);
        }
        else
        {
            Dictionary<DateTime, SaveGameInfo> handleSaveData =
                new Dictionary<DateTime, SaveGameInfo>();
            foreach(var info in gameInfoData)
            {
                if (info.Key != autosaveSlotNum)
                {
                    var dataKey = DateTime.Parse(info.Value.time);
                    var dataVal = info.Value;
                    handleSaveData.Add(dataKey,dataVal);
                }
            }
            var dateList = handleSaveData.Keys.ToList();
            dateList.Sort();
            //int index = dateList.Count - 1;
            var dateLastKey = dateList[0];
            var infoLast = handleSaveData[dateLastKey];
            Debug.Log("info last. time: " + infoLast.time);
            return infoLast;
        }
    }

    public UnityEvent OnSavePointLoaded;

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        gameInfoData = new Dictionary<int, SaveGameInfo>();

        StartCoroutine(HandleSaveMenuTimer());
    }
    void Start()
    {
        
    }
    void Update()
    {
        
        if (menuIsForcedBlocking)
        {
            _blockMenuTimeLeft -= Time.deltaTime;
            if (_blockMenuTimeLeft <= 0)
                menuIsForcedBlocking = false;
        }
        
    }
    protected virtual void OnEnable()
    {
        SaveManagerSignals.OnSavePointAdded += OnSavePointAdded;
        SaveManagerSignals.OnSavePointLoaded += CallOnSavePointLoaded;
        BlockSignals.OnBlockStart += OnStartNewBlock;
    }
    protected virtual void OnDisable()
    {
        SaveManagerSignals.OnSavePointAdded -= OnSavePointAdded;
        SaveManagerSignals.OnSavePointLoaded -= CallOnSavePointLoaded;
        BlockSignals.OnBlockStart -= OnStartNewBlock;
        menuIsForcedBlocking = false;
    }
    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
    void OnSavePointAdded(string savePointKey, string savePointDescription)
    {
        Debug.Log("autoSave");
        ForcedBlockMenu(blockMenuTime);
        AutoSaveGameDelayed(autosaveDelayTime);
    }
    void CallOnSavePointLoaded(string savePointKey)
    {
        OnSavePointLoaded?.Invoke();
    }
    void OnStartNewBlock(Block block)
    {
        ForcedBlockMenu(blockMenuTime);
    }
    public void ForcedBlockMenu(float time)
    {
        _blockMenuTimeLeft = time;
        menuIsForcedBlocking = true;
    }
       
    public void LoadSaveGameInfoData()
    {
        gameInfoData = new Dictionary<int, SaveGameInfo>();
        for (int i = 1; i < (slotsMax + 1); i++)
        {
            var key = CreateGameInfoKey(i);
            if (SaveDataExists(key))
            {
                var data = ReadSaveGameInfo(key);
                gameInfoData.Add(i, data);
            }
        }
        UIGameMenu.Instance.UpdateGameDataSlots();
    }    
    public void SaveGame(int slot, SaveGameInfo oldInfo)
    {
        if (!isValidSlotId(slot)) return;
        StartCoroutine(SaveGameTimer(slot, oldInfo));
    }
    public void QuickSaveGame()
    {        
        if (loadedGameInfo == null || loadedGameInfo==autosaveGameInfo ||
            (loadedGameInfo.dataKey==string.Empty || loadedGameInfo.infoKey==string.Empty))
        {
            Debug.Log("GetLastOrNewFirstGameInfo");
            loadedGameInfo = GetLastOrNewFirstGameInfo();
        }
        Debug.Log("QuickSaveGame. loadedGameInfo key: "+loadedGameInfo.dataKey);
        var slotId = GetSlotIdFromDataKey(loadedGameInfo.dataKey);
        SaveGame(slotId, loadedGameInfo);
    }
    public void AutoSaveGame()
    {
        if (!isCanSaveGame()) return;
        Debug.Log("AutoSaveGame success");
        string dataKey = CreateGameDataKey(autosaveSlotNum);
        string infoKey = CreateGameInfoKey(autosaveSlotNum);
        autosaveGameInfo = new SaveGameInfo(infoKey, dataKey);
        // temporarily replace the current key with the autosave key
        var saveMenu = SaveMenu.instance;
        string tempKey = saveMenu.saveDataKey;
        saveMenu.saveDataKey = dataKey;
        SaveGame(autosaveSlotNum,autosaveGameInfo);
        // return the previous key
        saveMenu.saveDataKey = tempKey;
    }
    protected void AutoSaveGameDelayed(float time)
    {
        if (!isCanSaveGame()) return;
        StartCoroutine(AutoSaveDelayTimer(time));
    }
    public void LoadGame(SaveGameInfo info)
    {
        if (info == null) return;

        var menu = SaveMenu.instance;
        if (menu == null) return;
        // block the menu
        ForcedBlockMenu(blockMenuTime);
        // resetting current data
        flowchart.Reset(false, true);
        // clear history
        SaveManagerSignals.DoSaveReset();

        menu.saveDataKey = info.dataKey;
        menu.Load();
        //loadedGameInfo = info.Clone();
        //TEST
        loadedGameInfo = info;
        
        //FungusManager.Instance.NarrativeLog.Clear();
    }
    public void QuickLoadGame()
    {
        if (loadedGameInfo != null)
            LoadGame(loadedGameInfo);
    }
    public void DeleteSavedGame(SaveGameInfo info)
    {
        DeleteSavedGame(info.dataKey, info.infoKey);
    }
    public void DeleteSavedGame(string dataKey, string infoKey)
    {
        var saveManager = FungusManager.Instance.SaveManager;

        if (saveManager.SaveDataExists(dataKey))
            SaveManager.Delete(dataKey);
        if (saveManager.SaveDataExists(infoKey))
            SaveManager.Delete(infoKey);
    }
    public void DeleteAllSavedGames()
    {
        if(gameInfoData!=null)
            foreach (var info in gameInfoData)
                DeleteSavedGame(info.Value);

        for (int i = 1; i < (slotsMax + 1); i++)
        {            
            var dataKey = CreateGameDataKey(i);
            var infoKey = CreateGameInfoKey(i);

            DeleteSavedGame(dataKey, infoKey);
        }
        loadedGameInfo = null;

        gameInfoData = new Dictionary<int, SaveGameInfo>();

        UIGameMenu.Instance.UpdateGameDataSlots();
    }
    protected virtual SaveGameInfo ReadSaveGameInfo(string key)
    {
        var jsonData = string.Empty;
        SaveGameInfo gameData =null;
#if UNITY_WEBPLAYER || UNITY_WEBGL
        jsonData = PlayerPrefs.GetString(key);
#else
        var fullFilePath = GetFullFilePath(key);
        if (System.IO.File.Exists(fullFilePath))
        {
            jsonData = System.IO.File.ReadAllText(fullFilePath);
        }
#endif//UNITY_WEBPLAYER
        if (!string.IsNullOrEmpty(jsonData))
        {
            gameData = JsonUtility.FromJson<SaveGameInfo>(jsonData);
        }
        return gameData;
    }
    protected virtual bool WriteSaveGameInfo(SaveGameInfo info)
    {
        var jsonData = JsonUtility.ToJson(info, true);
        Debug.Log("jsonData length: "+ jsonData.Length);
        if (!string.IsNullOrEmpty(jsonData))
        {
#if UNITY_WEBPLAYER || UNITY_WEBGL
                PlayerPrefs.SetString(info.infoKey, jsonData);
                PlayerPrefs.Save();
#else
            var fileLoc = GetFullFilePath(info.infoKey);

            //make sure the dir exists
            System.IO.FileInfo file = new System.IO.FileInfo(fileLoc);
            file.Directory.Create();

            System.IO.File.WriteAllText(fileLoc, jsonData);
#endif//UNITY_WEBPLAYER
            return true;
        }
        return false;
    }
    public static bool SaveDataExists(string key)
    {
#if UNITY_WEBPLAYER || UNITY_WEBGL
            return PlayerPrefs.HasKey(key);
#else
        var fullFilePath = GetFullFilePath(key);
        return System.IO.File.Exists(fullFilePath);
#endif//UNITY_WEBPLAYER
    }
    IEnumerator HandleSaveMenuTimer()
    {
        while (SaveMenu.instance == null)
        {
            yield return new WaitForSeconds(1);
        }
        var menu = SaveMenu.instance;

        menu.loadOnStart = false;
        menu.saveDataKey = CreateGameDataKey(autosaveSlotNum);

        LoadSaveGameInfoData();

        yield break;
    }
    IEnumerator SaveGameTimer(int slot, SaveGameInfo oldInfo)
    {
        SaveGameInfo info = oldInfo;
        var saveMenu = SaveMenu.instance;

        if (saveMenu == null) yield break;
        if (info == null || GetSlotIdFromDataKey(oldInfo.dataKey) != slot)
        {
            var infoKey = CreateGameInfoKey(slot);
            var dataKey = CreateGameDataKey(slot);
            info = new SaveGameInfo(infoKey, dataKey);
        }
        saveMenu.saveDataKey = info.dataKey;       
        saveMenu.Save();

        // make a screen
        bool isShowed = UIGameMenu.Instance.menuIsShowed;
        /*
        if (isShowed)
            UIGameMenu.Instance.ShowMenu(false);
        */
        yield return new WaitForEndOfFrame();     
        Texture2D screenTex = Camera.main.CreateScreenShot();
        //var saveTexture = Instantiate(screenTex);
        TextureScale.Bilinear(screenTex, screenTex.width / screenScaleCompressFactor, 
            screenTex.height / screenScaleCompressFactor);
        info.UpdateScreenShot(screenTex);        
        info.UpdateTime();
        //Debug.Log("screenTex.height: " + screenTex.height + "; screenTex.width: " + screenTex.width);
        if (isShowed)
            UIGameMenu.Instance.ShowMenu(true);

        // update information about the saved game
        int id = GetSlotIdFromDataKey(info.dataKey);
        if (gameInfoData.ContainsKey(id))
            gameInfoData[id] = info;
        else gameInfoData.Add(id, info);
        // we display it in the slots interface
        UIGameMenu.Instance?.UpdateGameDataSlots();
        // save info
        //Debug.Log("game saved");
        WriteSaveGameInfo(info);
        yield return new WaitForEndOfFrame();
        
        yield break;
    }
    IEnumerator AutoSaveDelayTimer(float time)
    {
        yield return new WaitForSeconds(time);

        AutoSaveGame();
        menuIsForcedBlocking = false;
        yield break;
    }
    
}
