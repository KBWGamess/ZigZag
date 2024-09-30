using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexApi : MonoBehaviour
{
    #region Internal
    [DllImport("__Internal", EntryPoint = "GetData")]
    private static extern void _GetData();
    [DllImport("__Internal", EntryPoint = "SetData")]
    private static extern void _SetData(string dataJson);
    [DllImport("__Internal", EntryPoint = "GetPlayerInfo")]
    private static extern string _GetPlayerInfo();
    [DllImport("__Internal", EntryPoint = "GetPlayer")]
    private static extern string _GetPlayer();
    [DllImport("__Internal", EntryPoint = "ShowFullscreenAdv")]
    private static extern void _ShowFullscreenAdv();
    [DllImport("__Internal", EntryPoint = "ShowRewardedVideo")]
    private static extern void _ShowRewardedVideo();
    [DllImport("__Internal", EntryPoint = "Init")]
    private static extern void _Init();
    [DllImport("__Internal", EntryPoint = "SendExitEvent")]
    private static extern void _SendExitEvent();
    [DllImport("__Internal", EntryPoint = "DeviceInfo")]
    private static extern int _DeviceInfo();
    #endregion

    #region Callbacks

    private static Action ShowFullscreenAdvSuccess = null;
    private static Action<string> ShowFullscreenAdvFail = null;
    private static Action<PlayerData> GetDataSuccess = null;
    private static Action<string> GetDataFail = null;
    private static Action SetDataSuccess = null;
    private static Action<string> SetDataFail = null;
    private static Action GetPlayerSuccess = null;
    private static Action<string> GetPlayerFail = null;
    private static Action InitSuccess = null;
    private static Action<string> InitFail = null;
    private static Action<RewardedVideoResult> ShowRewardedVideoSuccess = null;
    private static Action<string> ShowRewardedVideoFail = null;

    #endregion

    #region Events

    public void OnFullscreenAdvCallback(string dataJson)
    {
        Debug.Log("OnFullscreenAdvCallback");
        var response = JsonUtility.FromJson<CallbackResult<object>>(dataJson);
        if (response.isSuccessfull)
        {
            ShowFullscreenAdvSuccess.Invoke();
            return;
        }
        Debug.Log(response.error);
        ShowFullscreenAdvFail.Invoke(response.error);
    }

    public void OnRewardedVideoCallback(string dataJson)
    {
        Debug.Log("OnShowRewardedVideoCallback");
        var response = JsonUtility.FromJson<CallbackResult<RewardedVideoResult>>(dataJson);
        if (response.isSuccessfull)
        {
            ShowRewardedVideoSuccess.Invoke(response.data);
            return;
        }
        Debug.Log(response.error);
        ShowRewardedVideoFail.Invoke(response.error);
    }

    public void OnGetDataCallback(string dataJson)
    {
        Debug.Log("OnGetDataCallback");
        var response = JsonUtility.FromJson<CallbackResult<PlayerData>>(dataJson);
        if (response.isSuccessfull)
        {
            GetDataSuccess.Invoke(response.data);
            return;
        }
        Debug.Log(response.error);
        GetDataFail.Invoke(response.error);
    }

    public void OnGetPlayerCallback(string dataJson)
    {
        Debug.Log("OnGetPlayerCallback");
        var response = JsonUtility.FromJson<CallbackResult<object>>(dataJson);
        if (response.isSuccessfull)
        {
            GetPlayerSuccess.Invoke();
            return;
        }
        Debug.Log(response.error);
        GetPlayerFail.Invoke(response.error);
    }

    public void OnInitCallback(string dataJson)
    {
        Debug.Log("OnInitCallback");
        var response = JsonUtility.FromJson<CallbackResult<object>>(dataJson);
        if (response.isSuccessfull)
        {
            InitSuccess.Invoke();
            return;
        }
        Debug.Log(response.error);
        InitFail.Invoke(response.error);
    }

    public void OnSetDataCallback(string dataJson)
    {
        Debug.Log("OnSetDataCallback");
        var response = JsonUtility.FromJson<CallbackResult<object>>(dataJson);
        if (response.isSuccessfull)
        {
            SetDataSuccess.Invoke();
            return;
        }
        Debug.Log(response.error);
        SetDataFail.Invoke(response.error);
    }

    public void OnHistoryBackEvent()
    {
        //Здесь обработать нажатие кнопки "назад"
    }

    #endregion

    private static bool initIsFailed = false;

    /// <summary>
    /// Инициализирует работу с api
    /// </summary>
    /// <param name="success"></param>
    /// <param name="fail"></param>
    public static void Init(Action success = null, Action<string> fail = null)
    {
        InitSuccess = success;
        InitFail = fail;
        try
        {
            _Init();
        }
        catch
        {
            initIsFailed = true;
            Debug.LogError("Не удалось инициализировать YandexApi");
        }
    }

    /// <summary>
    /// Показвает полноэкранную рекламу
    /// </summary>
    public static void ShowFullscreenAdv(Action success = null, Action<string> fail = null)
    {
        if (initIsFailed)
        {
            ShowFullscreenAdvSuccess.Invoke();
            return;
        }
        ShowFullscreenAdvSuccess = success;
        ShowFullscreenAdvFail = fail;
        _ShowFullscreenAdv();
    }

    /// <summary>
    /// Показывает рекламу за вознаграждение
    /// </summary>
    /// <param name="success"></param>
    /// <param name="fail"></param>
    public static void ShowRewardedVideo(Action<RewardedVideoResult> success = null, Action<string> fail = null)
    {
        if (initIsFailed)
        {
            ShowRewardedVideoSuccess.Invoke(new RewardedVideoResult { action = RewardedVideoActionEnum.Rewarded });
            return;
        }
        ShowRewardedVideoSuccess = success;
        ShowRewardedVideoFail = fail;
        _ShowRewardedVideo();
    }

    /// <summary>
    /// Инициализирует работу с игроком
    /// </summary>
    /// <param name="callback"></param>
    public static void GetPlayer(Action success = null, Action<string> fail = null)
    {
        if (initIsFailed)
        {
            GetPlayerSuccess.Invoke();
            return;
        }
        GetPlayerSuccess = success;
        GetPlayerFail = fail;
        _GetPlayer();
    }

    /// <summary>
    /// Возвращает информацию об игроке
    /// </summary>
    /// <returns>Информация об игроке</returns>
    public static CallbackResult<PlayerInfo> GetPlayerInfo()
    {
        if (initIsFailed)
        {
            return new CallbackResult<PlayerInfo>
            {
                isSuccessfull = true,
                data = new PlayerInfo
                {
                    id = "testId",
                    name = "testName",
                    photoUrls = new string[3] { "testPhotoUrlSmall", "testPhotoUrlMed", "testPhotoUrlLarge" }
                }
            };
        }
        return JsonUtility.FromJson<CallbackResult<PlayerInfo>>(_GetPlayerInfo());
    }

    /// <summary>
    /// Запрашивает данные с сервера Яндекса
    /// </summary>
    public static void GetData(Action<PlayerData> success = null, Action<string> fail = null)
    {
        if (initIsFailed)
        {
            GetDataSuccess.Invoke(new PlayerData());
            return;
        }
        GetDataSuccess = success;
        GetDataFail = fail;
        _GetData();
    }

    /// <summary>
    /// Сохраняет данные на сервер Яндекса
    /// </summary>
    /// <param name="data">данные для сохранения</param>
    public static void SetData(PlayerData data)
    {
        if (initIsFailed)
        {
            SetDataSuccess.Invoke();
            return;
        }
        _SetData(JsonUtility.ToJson(data));
    }

    /// <summary>
    /// Отправляет событие закрытия приложения
    /// </summary>
    public static void SendExitEvent()
    {
        if (initIsFailed)
        {
            return;
        }
        _SendExitEvent();
    }

    public static DeviceTypeEnum DeviceInfo()
    {
        if (initIsFailed)
        {
            return DeviceTypeEnum.Desktop;
        }
        var res = (DeviceTypeEnum)_DeviceInfo();
        return res;
    }
}

/// <summary>
/// Информация об игроке
/// </summary>
[Serializable]
public class PlayerInfo
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public string id;
    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public string name;
    /// <summary>
    /// Три ссылки на картинки различных размеров (1 - маленькая, 2 - средняя, 3 - большая)
    /// </summary>
    public string[] photoUrls;
}

/// <summary>
/// Данные игрока, сохраняемые на сервере Яндекса. Структуру можно править
/// </summary>
[Serializable]
public class PlayerData
{
    public int playerScore;
    public string name;
}

[Serializable]
public class CallbackResult<T>
{
    public bool isSuccessfull;
    public T data;
    public string error;
    public string method;
}

/// <summary>
/// Результат показа рекламы с вознаграждением
/// </summary>
[Serializable]
public class RewardedVideoResult
{
    public RewardedVideoActionEnum action;
}

[Serializable]
public enum RewardedVideoActionEnum
{
    Rewarded = 1,
    Close = 2,
    Open = 3,
}

public enum DeviceTypeEnum
{
    Desktop = 1,
    Mobile = 2,
    Tablet = 3,
    Tv = 4
}