using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField] private Camera _mainCam;
    [SerializeField] private Camera _cameraGreenPlayer;
    [SerializeField] private Camera _cameraPinkPlayer;

    private bool _mainCameraActive = true;

    private void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
    private void ShowMainCamera()
    {
        _mainCam.enabled = true;
        _cameraGreenPlayer.enabled = false;
        _cameraPinkPlayer.enabled = false;
    }

    private void ShowSplitScreen()
    {
        _mainCam.enabled = false;
        _cameraGreenPlayer.enabled = true;
        _cameraPinkPlayer.enabled = true;
    }

    public void SwitchSplitMainCamera()
    {
        if(_mainCameraActive)
        {
            ShowSplitScreen();
            _mainCameraActive=false;
        }
        else
        {
            ShowMainCamera();
            _mainCameraActive = true;
        }
    }
}
