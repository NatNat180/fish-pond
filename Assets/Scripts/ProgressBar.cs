using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float progress;
    public float minProgress;
    public float maxProgress;
    private Slider progressBar;

    private void Start()
    {
        progressBar = GetComponent<Slider>();
    }
	
    void Update()
    {
        UpdateProgressBar();
        progress = Mathf.Clamp(progress, minProgress, maxProgress);
    }

    void UpdateProgressBar()
    {
        progressBar.value = progress;
    }

}
