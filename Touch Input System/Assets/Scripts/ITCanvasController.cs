using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Testing;
using TMPro;
using UnityEngine.AddressableAssets;

public class ITCanvasController : MonoBehaviour
{

    private static ITCanvasController _instance;
    public static ITCanvasController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Button nextCheckPointButton;
    public Button previousCheckPointButton;
    

    public Button nextLevelButton;
    public Button previousLevelButton;



    public Button testLevelButton;


    public Button canvasSwitchButton;

    public Button levelStartButton;

    public TMP_InputField levelNumberInputField;

    public GameObject canvas;


    public Button infiniteButton;
    public Button spikesButton;

    private MoveToNextCheckPoint NextCheckPoint = new MoveToNextCheckPoint();

  

    private void Start()
    {
        nextCheckPointButton.onClick.AddListener(() => 
        {
            
            var checkPoints = FindAnyObjectByType<CheckPointManager>();

            NextCheckPoint.maxPoints = checkPoints.allCheckPoints.Count - 1;
            var currentCheckPOint = NextCheckPoint.MoveToCheckPoint(1);

            var checkPTransform = checkPoints.allCheckPoints[currentCheckPOint];
            var ball = FindAnyObjectByType<BallCollisions>()._lastCheckPoint = checkPTransform.transform;

        } );

        previousCheckPointButton.onClick.AddListener(() =>
        {
           


            var checkPoints = FindAnyObjectByType<CheckPointManager>();

            NextCheckPoint.maxPoints = checkPoints.allCheckPoints.Count - 1;
            var currentCheckPOint = NextCheckPoint.MoveToCheckPoint(-1);

            var checkPTransform = checkPoints.allCheckPoints[currentCheckPOint];


            var ball = FindAnyObjectByType<BallCollisions>()._lastCheckPoint = checkPTransform.transform;

        });

        nextLevelButton.onClick.AddListener(() =>
        {
            LevelLoader.Instance.LoadNextLevel();
            MenuManager.Instance.CloseMenu(GameMenu.Instance);
        });

        previousLevelButton.onClick.AddListener(() =>
        {
            LevelLoader.Instance.LoadPreviousLevel();
            MenuManager.Instance.CloseMenu(GameMenu.Instance);

        });

        canvasSwitchButton.onClick.AddListener(() => 
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
        });

        testLevelButton.onClick.AddListener(() =>
        {
            LevelLoader.Instance.LoadTestScene();
        });

        levelStartButton.onClick.AddListener(() =>
        {

            MenuManager.Instance.CloseMenu(MainMenu.Instance);
            MenuManager.Instance.CloseMenu(GameMenu.Instance);


            if (string.IsNullOrEmpty(levelNumberInputField.text)) return;

            int.TryParse(levelNumberInputField.text, out int levelNumber);
            AssetReference level = LevelLoader.Instance.levelHolder.GetLevelByNumber(levelNumber);

            if (level == null) return;

            SceneTransitionManager.Instance.OnSceneTransitionStarted.Invoke(() => LevelLoader.Instance.LoadLevel(level));

          


        });

        infiniteButton.onClick.AddListener(() =>
        {
            var lives = FindAnyObjectByType<LivesControl>();
            var time = FindAnyObjectByType<TimeTrialControl>();

            if (lives != null)
                lives.isTesting = !lives.isTesting;

            if (time != null)
                time.isTesting = !time.isTesting;
        });


        spikesButton.onClick.AddListener(() =>
        {
            var obstacleManger = FindAnyObjectByType<ObstacleManager>();

            if (obstacleManger != null)
                obstacleManger.gameObject.SetActive(!obstacleManger.gameObject.activeInHierarchy);
        });
    }






}
