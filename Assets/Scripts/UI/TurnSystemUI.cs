using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private TextMeshProUGUI turnNumberText;

    private void Start()
    {
        endTurnBtn.onClick.AddListener(() => 
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEndTurnButtonVisibility();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEndTurnButtonVisibility();
    }

    private void UpdateTurnText()
    {
        turnNumberText.text = "Turn " + TurnSystem.Instance.GetTurnNumber();
    }

    private void UpdateEndTurnButtonVisibility()
    {
        endTurnBtn.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
