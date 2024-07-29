using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : MonoBehaviour {

    public static UIGameMenu Instance { get; private set; }

    public string actionTypeString_save;
    public string actionTypeString_load;
    public string slotName=string.Empty;
    public string autosaveSlotName = string.Empty;
    public bool addSlotIdToName = false;
    public string emptySlotName;
    public Sprite emptySlotIcon;

    public bool menuIsShowed { get; set; }
    [SerializeField]
    GameObject menu_panel;
    [SerializeField]
    Button save_button, exitToMenu_button;
    [SerializeField]
    Text actionType_txt;
    MenuDialog menuDialogTemp;
    public bool menuByEsc = true;
    public GameDataActionType actionType { get; set; }
    public List<SaveGameSlot> GetSaveGameSlots()
    {
        return GetComponentsInChildren<SaveGameSlot>(true).ToList();
    }
    public SaveGameSlot SelectedGameSlot { get; private set; }
    protected SaveGameSlot FindGameSlot(int id)
    {
        foreach (var slot in GetSaveGameSlots())
            if (slot.id == id) return slot;

        return null;
    }
    protected SaveGameSlot GetAutosaveGameSlot()
    {
        foreach (var slot in GetSaveGameSlots())
            if (slot.isAutoSaveSlot) return slot;

        return null;
    }
    public string GetAutosaveDataKey()
    {
        foreach (var slot in GetSaveGameSlots())
            if (slot.isAutoSaveSlot) return GetGameDataKey(slot.id);

        return "unknownKey";
    }
    protected string GetGameDataKey(int slotId)
    {
        return FungusConstants.DefaultSaveDataKey + "_" + slotId;
    }
    
    protected int GetSlotIdFromDataKey(string dataKey)
    {
        string[] arr = dataKey.Split('_');
        if (arr.Length <= 1) return -1;
        int lastIndex = arr.Length - 1;
        return int.Parse(arr[lastIndex]);
    }
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

        
    }
    void OnEnable()
    {
        UpdateGameDataSlots();
        UpdateActionType();
    }
    void Start()
    {
        UpdateGameDataSlots();
    }
    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
    void Update()
    {
        if(menuByEsc == true) { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MultiSaveManager.Instance.isMainMenu())
                ShowFromMainMenu();
            else
                ShowMenu(!menu_panel.activeSelf, MultiSaveManager.Instance.isCanSaveGame());
        }
        }
    }
    public void TryOpenMenu(bool open)
    {

    }
    public void SetActionTypeToSave()
    {
        actionType = GameDataActionType.Save;
        actionType_txt.text = actionTypeString_save;
    }
    public void SetAtionTypeToLoad()
    {
        actionType = GameDataActionType.Load;
        actionType_txt.text = actionTypeString_load;
    }
    protected void UpdateActionType()
    {
        switch (actionType)
        {
            case GameDataActionType.Load:
                actionType_txt.text = actionTypeString_load;
                break;
            case GameDataActionType.Save:
                actionType_txt.text = actionTypeString_save;
                break;
        }
    }
    public void UpdateGameDataSlots()
    {
        var saveManager = FungusManager.Instance.SaveManager;
        var msm = MultiSaveManager.Instance;
        if (msm == null) return;
        foreach (var slot in GetSaveGameSlots())
        {
            var info = msm.FindGameInfoBySlotId(slot.id);
            //Debug.Log("info: "+ info);
            UpdateGameInfoSlot(slot, info);
        }
    }
    public void UpdateGameInfoSlot(SaveGameSlot slot, SaveGameInfo info)
    {
        if (slot == null) return;
        string slotName;
        Sprite slotIcon=null;

        slot.isAutoSaveSlot = MultiSaveManager.Instance.autosaveSlotNum == slot.id ?
            true : false;
        if (info == null)
        {
            slotName = emptySlotName;
            slotIcon = emptySlotIcon;            
        }
        else
        {
            string nameFirstPart = !slot.isAutoSaveSlot ? this.slotName :
                autosaveSlotName;
            string nameSecondPart = string.Empty;
            if (addSlotIdToName)
                nameSecondPart = nameFirstPart == string.Empty ? slot.id.ToString() :
                    " " + slot.id.ToString();
            string nameThirdPart =(nameFirstPart==string.Empty && nameSecondPart==string.Empty)?
                info.time:"\n"+info.time;
            slotName = nameFirstPart + nameSecondPart + nameThirdPart;
            var tex = info.GetScreenshot();
            slotIcon = tex.ToSprite();
        }

        slot.SetName(slotName);
        slot.SetIcon(slotIcon);
    }
    public void RemoveAllData()
    {
        MultiSaveManager.Instance?.DeleteAllSavedGames();
        
        UpdateGameDataSlots();
    }    
    public void Save(int slotId)
    {
        var slot = FindGameSlot(slotId);
        // autosave slot cannot be re-saved
        if (slot == GetAutosaveGameSlot()) return;
        var msm= MultiSaveManager.Instance;
        var infoOld = msm.FindGameInfoBySlotId(slotId);        
        msm.SaveGame(slotId,infoOld);
                
        //UpdateGameInfoSlot(slot, infoNew);
    }
    public void Load(int slotId)
    {
        var msm = MultiSaveManager.Instance;
        var info = msm.FindGameInfoBySlotId(slotId);
        msm.LoadGame(info);

        Return();
    }
    public void OpenSaveMenu()
    {
        SetActionTypeToSave();
        ShowMenu(true, MultiSaveManager.Instance.isCanSaveGame());
    }
    public void OpenLoadMenu()
    {
        SetAtionTypeToLoad();
        ShowMenu(true, MultiSaveManager.Instance.isCanSaveGame());
    }
    public void SaveOrLoad(int slotId)
    {
        switch (actionType)
        {
            case GameDataActionType.Load:
                Load(slotId);
                break;
            case GameDataActionType.Save:
                Save(slotId);
                break;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Return()
    {
        ShowMenu(false);
    }
    public void ShowFromMainMenu()
    {
        ShowMenu(!menu_panel.activeSelf, false, false);
    }
    public void ShowMenu(bool show, bool enableSave=true, bool enableExit=true)
    {        
        if (show && MultiSaveManager.Instance.isBlockMenu()) return;
        if (!MultiSaveManager.Instance.isCanSaveGame())
            SetAtionTypeToLoad();
        // костыль
        if (menuDialogTemp==null)
            menuDialogTemp= FindObjectOfType<MenuDialog>();
        if (menuDialogTemp != null)
            menuDialogTemp.gameObject.SetActive(!show);

        if (show)
        {
            UpdateGameDataSlots();
            UpdateActionType();
        }
        if (save_button != null)
            save_button.gameObject.SetActive(enableSave);
        if(exitToMenu_button!=null)
            exitToMenu_button.gameObject.SetActive(enableExit);
        menu_panel?.SetActive(show);
        menuIsShowed = show;
        int timeScale = show ? 0 : 1;
        //пауза
        Time.timeScale = timeScale;
    }
    
    public enum GameDataActionType
    {
        Save,
        Load
    }
}