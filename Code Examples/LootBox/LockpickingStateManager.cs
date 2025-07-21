using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockpickingStateManager : MonoBehaviour
{
    [Header("Prefabs")]

    public LockpinkSettings Prefab;
    public Transform Camera { get; set; }
    public GameObject LootBox { get; set; }
    public MovementStateManager MSM { get; set; }
    public CameraFollow Cf { get; set; }
    public GrapplingGunStateManager GGSM { get; set; }
    public InputManager Input { get; set; }
    
    public Transform CameraLootBoxPos;
    public Transform Player;
    public Transform LockpickNumber1;
    public Transform LockpickNumber2;
    public GameObject Lock;

    [Header("Text Settings")]

    [SerializeField]private Text _text;
    public Text Text { get; set; }
    public string DifficultyString { get; set; }

    [Header("Durability Settings")]
    public float LockpicksDurability { get; set; }

    [Header("Sweet Spot Settings")]
    public float SweetSpotTolerance { get; set; }

    [Header("LockPicking Difficulty")]
    [SerializeField] protected Difficulty difficulty;
    protected enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Extreme
    }

    [Header("States")]
    LockpickingIdleState _currentState;
    public LockpickingMovingState _movingState = new();
    public LockpickingEnter _interactState = new();
    public LockpickingState _lockpicking = new();
    public LockpickingExitState _exitState = new();
    private List<LockpickingIdleState> _lockpickState;
    void Start()
    {
        ChooseDifficulty();
        Camera = UnityEngine.Camera.main.transform;
        MSM = Player.gameObject.GetComponent<MovementStateManager>();
        GGSM = Player.gameObject.GetComponent<GrapplingGunStateManager>();
        Cf = Camera.gameObject.GetComponent<CameraFollow>();
        Input = FindAnyObjectByType<InputManager>();
        if (Input == null)
        {
            Debug.LogError("NOOO InputManager in the scene");
        }
        _lockpickState = new List<LockpickingIdleState>()
        {
            _movingState,
            _interactState,
            _lockpicking,
            _exitState
        };
        foreach (var state in _lockpickState)
        {
            state.SetupState(this);
        }
        _currentState = _interactState;
        _currentState.StartState(this);

    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }
    public void CreateText()
    {
        Text = Instantiate(_text, _text.transform.position, _text.transform.localRotation);
        Text.color = _text.color;
        Text.transform.SetParent(_text.transform.parent);
        Text.text = DifficultyString.ToString();
        Debug.Log("created");
    }   
    public void DeleteText()
    {
        Destroy(Text.gameObject);
    }

    public void SwitchingState(LockpickingIdleState state)
    { 
      _currentState = state;
       state.StartState(this);
    }
    private void ChooseDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                SweetSpotTolerance = Prefab.SweetSpotToleranceAtEasiestDifficulty;
                LockpicksDurability = Prefab.DurabilityAtEasiestDifficulty;
                Dif(Difficulty.Easy);
                break;
            case Difficulty.Normal:
                SweetSpotTolerance = Prefab.SweetSpotToleranceAtEasiestDifficulty - Prefab.SweetSpotToleranceByDifficultyDecrease;
                LockpicksDurability = Prefab.DurabilityAtEasiestDifficulty - Prefab.DurabilityByDifficultyDecrease;
                Dif(Difficulty.Normal);
                break;
            case Difficulty.Hard:
                SweetSpotTolerance = Prefab.SweetSpotToleranceAtEasiestDifficulty - (2 * Prefab.SweetSpotToleranceByDifficultyDecrease);
                LockpicksDurability = Prefab.DurabilityAtEasiestDifficulty - (2 * Prefab.DurabilityByDifficultyDecrease);
                Dif(Difficulty.Hard);
                break;
            case Difficulty.Extreme:
                SweetSpotTolerance = Prefab.SweetSpotToleranceAtEasiestDifficulty - (3 * Prefab.SweetSpotToleranceByDifficultyDecrease);
                LockpicksDurability = Prefab.DurabilityAtEasiestDifficulty - (3 * Prefab.DurabilityByDifficultyDecrease);
                Dif(Difficulty.Extreme);
                break;

        }
    }
    private void Dif(Difficulty state)
    {
        DifficultyString = "Difficulty : " + state.ToString();
        Debug.Log(DifficultyString);
    }
}
