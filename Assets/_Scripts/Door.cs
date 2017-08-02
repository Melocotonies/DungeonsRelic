using UnityEngine;

public class Door : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.gameObject.SetActive(false);
    }

    public void ShowWaveCanvas()
    {
        _canvas.gameObject.SetActive(true);
    }
}
