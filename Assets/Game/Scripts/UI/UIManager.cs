using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private UIShootModule _uiShootModule;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _uiShootModule.Initialize(this, () =>
        {

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCrossfade(Vector2 target)
    {
        _uiShootModule.MoveCrossfade(target);
    }
}
