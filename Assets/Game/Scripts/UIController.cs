using MidniteOilSoftware;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button startBtn, nextMapBtn;
    [SerializeField] private GameObject nextBtnLayout;
    [SerializeField] private MapController mapController;

    private void Start()
    {
        mapController.GetDoneTravelAc += ActiveNextMapLayout;
        nextBtnLayout.gameObject.SetActive(false);
        startBtn.onClick.AddListener(StartGameButtonClick);
        nextMapBtn.onClick.AddListener(NextGameButtonClick);
    }
    public void StartGameButtonClick()
    {
        startBtn.gameObject.SetActive(false);
        mapController.IntitMapAndNpc();
    }
    public void NextGameButtonClick()
    {
        nextBtnLayout.gameObject.SetActive(false);
        mapController.ChangLstObjectSpawned(false);
        mapController.IntitMapAndNpc();
    }
    void ActiveNextMapLayout()
    {
        nextBtnLayout.gameObject.SetActive(true);
    }
}
