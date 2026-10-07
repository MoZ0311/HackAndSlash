using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour, DebugInputActions.IDebugActions
{
    [Header("Scripts")]
    [SerializeField] PlayerWeapon playerWeapon;
    [SerializeField] InfoUIManager infoUIManager;

    [Header("Barrels")]
    [SerializeField] BarrelBase riflePrefab;
    [SerializeField] BarrelBase shotgunPrefab;
    [SerializeField] BarrelBase gatlingPrefab;
    [SerializeField] BarrelBase handgunPrefab;
    [SerializeField] BarrelBase vRiflePrefab;

    [Header("Modules")]
    [SerializeField] ModuleBase penetrationModule;
    [SerializeField] ModuleBase ricochetModule;
    [SerializeField] ModuleBase explosionModule;
    [SerializeField] ModuleBase noneModule;

    DebugInputActions debugInput;
    DebugInputActions.DebugActions debugActions;

    int sceneIndex;

    void Awake()
    {
        debugInput = new();
        debugActions = debugInput.Debug;
        debugActions.AddCallbacks(this);

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnEnable()
    {
        debugActions.Enable();
    }

    void OnDisable()
    {
        debugActions.Disable();
    }

    void OnDestroy()
    {
        debugInput.Dispose();
    }

    public void OnWeapon1(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerWeapon.SetBarrel(riflePrefab);
            infoUIManager.OnBarrelChanged("制式射撃ユニット");
        }
    }

    public void OnWeapon2(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerWeapon.SetBarrel(shotgunPrefab);
            infoUIManager.OnBarrelChanged("散弾ユニット");
        }
    }

    public void OnWeapon3(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerWeapon.SetBarrel(gatlingPrefab);
            infoUIManager.OnBarrelChanged("ガトリングユニット");
        }
    }

    public void OnWeapon4(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerWeapon.SetBarrel(vRiflePrefab);
            infoUIManager.OnBarrelChanged("V-ライフル");
        }
    }

    public void OnModule1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerWeapon.SetModule(penetrationModule);
            infoUIManager.OnModuleChanged("貫通モジュール");
        }
    }

    public void OnModule2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerWeapon.SetModule(ricochetModule);
            infoUIManager.OnModuleChanged("跳弾モジュール");
        }
    }

    public void OnModule3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerWeapon.SetModule(explosionModule);
            infoUIManager.OnModuleChanged("爆発モジュール");
        }
    }

    public void OnModule4(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerWeapon.SetModule(noneModule);
            infoUIManager.OnModuleChanged("ダミーモジュール");
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Debug:Scene Reload");
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
