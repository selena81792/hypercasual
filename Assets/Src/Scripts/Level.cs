using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using EasyUI.Dialogs;

public class Level : MonoBehaviour
{
    #region Singleton class: Level

    public static Level Instance;

    void Awake ()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] ParticleSystem winFx;

    [Space]
    //remaining objects
    [HideInInspector] public int objectsInScene;
    //total objects at the beginning
    [HideInInspector] public int totalObjects;

    //the Objects parent
    [SerializeField] Transform objectsParent;
    [SerializeField] TMP_Text currentGoldText;

    [Space]
    [Header ("Audio")]
    [SerializeField] public AudioSource Music;
    [SerializeField] public AudioSource Effects;
    [SerializeField] public AudioClip effect1;
    [SerializeField] public AudioClip effect2;
    [SerializeField] public AudioClip effect3;
    [SerializeField] public AudioClip win;
    [SerializeField] public AudioClip lose;
    [SerializeField] public AudioClip speedup;
    [SerializeField] public AudioClip speeddown;

    [Space]
    [Header ("Materials & Sprites")]
    [SerializeField] Material groundMaterial;
    [SerializeField] Material objectMaterial;
    [SerializeField] Material obstacleMaterial;
    [SerializeField] SpriteRenderer groundBorderSprite;
    [SerializeField] SpriteRenderer groundSideSprite;
    [SerializeField] Image progressFillImage;

    [SerializeField] SpriteRenderer bgFadeSprite;

    [Space]
    [Header ("Level Colors-------")]
    [Header ("Ground")]
    [SerializeField] Color groundColor;
    [SerializeField] Color bordersColor;
    [SerializeField] Color sideColor;

    [Header ("Objects & Obstacles")]
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;

    [Header ("UI (progress)")]
    [SerializeField] Color progressFillColor;

    [Header ("Background")]
    [SerializeField] Color cameraColor;
    [SerializeField] Color fadeColor;

    private UnityAction RestartFromLevelOneAction;


    void Start ()
    {
        int currentLevel = PlayerPrefs.GetInt("level", 0);
        if (currentLevel != SceneManager.GetActiveScene ().buildIndex){
            Debug.Log("woahhhhh already played");
            SceneManager.LoadScene (currentLevel);
        }
        int currentVacuum = PlayerPrefs.GetInt("vacuum", 0);
        if (currentVacuum == 2){
            Magnet.Instance.SetForce(6000);
        } else if (currentVacuum == 1){
            Magnet.Instance.SetForce(3000);
        } else {
            Magnet.Instance.SetForce(1000);
        }
        RestartFromLevelOneAction += Level.Instance.RestartFromLevelOne;
        UpdateLevelGold ();
        CountObjects ();
        UpdateLevelColors ();
        YsoCorp.GameUtils.YCManager.instance.OnGameStarted(SceneManager.GetActiveScene ().buildIndex);
    }

    void CountObjects ()
    {
        //Count collectable white objects
        totalObjects = objectsParent.childCount;
        objectsInScene = totalObjects;
    }

    public void PlayWinFx ()
    {
        winFx.Play ();
    }

    public void LoadNextLevel ()
    {
        YsoCorp.GameUtils.YCManager.instance.OnGameFinished(true);
        if (SceneManager.GetActiveScene ().buildIndex == 9) // last level
        {
            Debug.Log("pog win");
            DialogUI.Instance
            .SetTitle ( "You win!" )
            .SetMessage ( "Great job, you've finished this game! Now you can restart from level 1 to earn more gold :D" )
            .SetButtonColor ( DialogButtonColor.Red )
            .SetButtonText ( "OK" )
            .SetButtonText2 ( "No" )
            .OnClose ( RestartFromLevelOneAction )
            .OnClose2 ( RestartFromLevelOneAction )
            .Show ( );
        } else {
            PlayerPrefs.SetInt("level", SceneManager.GetActiveScene ().buildIndex + 1);
            SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        }
        // Debug.Log("going to next level, setting new level: " + PlayerPrefs.GetInt("level", 0));
    }

    public void RestartLevel ()
    {
        YsoCorp.GameUtils.YCManager.instance.OnGameFinished(false);
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene ().buildIndex);
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

    public void RestartFromLevelOne ()
    {
        Debug.Log("restarted from 1");
        YsoCorp.GameUtils.YCManager.instance.OnGameFinished(false);
        PlayerPrefs.SetInt("level", 0);
        SceneManager.LoadScene (0);
    }

    public void UpdateLevelGold()
    {
        currentGoldText.text = PlayerPrefs.GetInt("gold", 0).ToString ();
    }

    void UpdateLevelColors ()
    {
        groundMaterial.color = groundColor;
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = bordersColor;

        obstacleMaterial.color = obstacleColor;
        objectMaterial.color = objectColor;

        progressFillImage.color = progressFillColor;

        Camera.main.backgroundColor = cameraColor;
        bgFadeSprite.color = fadeColor;
    }

    void OnValidate ()
    {
        //This method will exeute whenever you change something of this script in the inspector
        //this method won't be included in the final Build (Editor only)

        UpdateLevelColors ();
    }
}
