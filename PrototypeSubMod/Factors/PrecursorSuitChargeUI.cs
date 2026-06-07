using System;
using System.Collections;
using PrototypeSubMod.Prefabs;
using UnityEngine;
using UnityEngine.UI;

namespace PrototypeSubMod.Factors;

public class PrecursorSuitChargeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image chargeBar;
    [SerializeField] private Image backgroundShadow;
    [SerializeField] private Sprite survivalShadow;
    [SerializeField] private Sprite freedomShadow;
    [SerializeField] private float fillAmountMin;
    [SerializeField] private float fillAmountMax;

    private FactorIonManager factorIonManager;
    
    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        UpdateUIVisibility();
        Inventory.main.equipment.onAddItem += OnAddItem;
        Inventory.main.equipment.onRemoveItem += OnRemoveItem;
    }

    private IEnumerator CheckIfNewGameMode(GameModePresetId CurrentGameMode)
    {
        if (GameModeManager.currentPresetId.Value == CurrentGameMode)
        {
            yield return RestartCheck(CurrentGameMode);
        }
        else
        {
            OnGameModeChanged(GameModeManager.currentPresetId.Value);
        }
        yield return RestartCheck(CurrentGameMode);
    }

    private IEnumerator RestartCheck(GameModePresetId CurrentGameMode)
    {
        yield return new WaitForSeconds(10f);
        yield return CheckIfNewGameMode(GameModeManager.currentPresetId.Value);
    }

    private void OnAddItem(InventoryItem item)
    {
        UpdateUIVisibility();
    }
    
    private void OnRemoveItem(InventoryItem item)
    {
        UpdateUIVisibility();
    }

    private void OnGameModeChanged(GameModePresetId option)
    {
        if ((option & GameModePresetId.Freedom) != 0 || (option & GameModePresetId.Creative) == GameModePresetId.Creative)
        {
            backgroundShadow.sprite = freedomShadow;
        }
        else
        {
            backgroundShadow.sprite = survivalShadow;
        }
    }

    private void UpdateUIVisibility()
    {
        var itemInSlot = Inventory.main.equipment.GetItemInSlot("Body");
        bool hasSuit = itemInSlot?.techType == PrecursorSuit.prefabInfo.TechType;
        canvasGroup.alpha = hasSuit ? 1 : 0;

        if (itemInSlot == null) return;
        
        factorIonManager = itemInSlot.item.GetComponent<FactorIonManager>();
    }

    private void Update()
    {
        if (factorIonManager == null) return;

        chargeBar.fillAmount = Mathf.Lerp(fillAmountMin, fillAmountMax, factorIonManager.GetNormalizedCharge());
    }

    private void OnDestroy()
    {
        Inventory.main.equipment.onAddItem -= OnAddItem;
        Inventory.main.equipment.onRemoveItem -= OnRemoveItem;
    }
}