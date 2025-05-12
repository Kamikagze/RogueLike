using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProcedureOfLevelingUp : MonoBehaviour
{
    [SerializeField] Canvas controllerCanvas;
    [SerializeField] Canvas lVLcanvas;
    [SerializeField] Canvas artefactCanvas;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] FullFillButtons fullFillButtons;

    // изменена логика, теперь способности выбираются до показа табличек и запускаются после
    // события которое отключает канвас с плашками
    public static event Action ChooseAbil;
   

    // Update is called once per frame
    void Update()
    {

    }
    private void StopAndChoseAnAbility()
    {
        fullFillButtons.Reroll();
        Time.timeScale = 0;
        StartCoroutine(TimeForRerollKostyl());
    }
    public void Activate()
    {
        Time.timeScale = 1;
        fullFillButtons.Reroll();
        controllerCanvas.enabled = true;
        lVLcanvas.enabled = false;
        menuCanvas.enabled = false;
        ChooseAbil?.Invoke();

    }
    private IEnumerator TimeForRerollKostyl()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        controllerCanvas.enabled = false;
        menuCanvas.enabled= false;
        lVLcanvas.enabled = true;
    }
    private void OnDisable()
    {
        EXPManager.LevelingUp -= StopAndChoseAnAbility;
        UI.GainedAnArtifact -= ArtefactHasBeenChoosen;
        Collector.CollectArtifact -= StopAndChooseAnArtifact;
    }
    private IEnumerator TimeForRerollArtsKostyl()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        controllerCanvas.enabled = false;
        menuCanvas.enabled= false;
        artefactCanvas.enabled = true;
    }
    private void StopAndChooseAnArtifact()
    {
        Time.timeScale = 0;
        StartCoroutine(TimeForRerollArtsKostyl());
    }
    private void ArtefactHasBeenChoosen()
    {
        Time.timeScale = 1;
        controllerCanvas.enabled = true;
        artefactCanvas.enabled = false;
        menuCanvas.enabled = false;
    }

    public void StopAndChooseMenu()
    {
        Time.timeScale = 0;
        controllerCanvas.enabled = false;
        artefactCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
    public void ActivateAfterMenu()
    {
        Time.timeScale = 1;
        controllerCanvas.enabled = true;
        artefactCanvas.enabled = false;
        menuCanvas.enabled = false;
    }

    private void OnEnable()
    {
        UI.GainedAnArtifact += ArtefactHasBeenChoosen;
        EXPManager.LevelingUp += StopAndChoseAnAbility;
        Collector.CollectArtifact += StopAndChooseAnArtifact;
    }
   
}
