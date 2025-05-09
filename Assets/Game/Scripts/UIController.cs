using MidniteOilSoftware;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button startBtn, nextMapBtn;
    [SerializeField] private GameObject unAvailLayout;
    [SerializeField] private MapController mapController;

    private void Start()
    {
        mapController.GetDoneTravelAc += ActiveNextMapLayout;
        mapController.GetNullPathAc += ActiveUnAvailPathLayout;
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
        nextMapBtn.interactable = false;
        mapController.ChangLstObjectSpawned(false);
        mapController.IntitMapAndNpc();
    }
    void ActiveNextMapLayout()
    {
        nextMapBtn.interactable = true; 
    }
    void ActiveUnAvailPathLayout()
    {
        unAvailLayout.gameObject.SetActive(true);
        StartCoroutine(DeactiveUnAvailPathLayout());
    }
    IEnumerator DeactiveUnAvailPathLayout()
    {
        yield return new WaitForSeconds(1f);
        unAvailLayout.gameObject.SetActive(false);
        mapController.ChangLstObjectSpawned(false);
        mapController.IntitMapAndNpc();
    }
}
