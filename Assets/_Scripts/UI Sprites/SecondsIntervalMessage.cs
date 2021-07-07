using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SecondsIntervalMessage : MonoBehaviour
{
    private Timer _timer;
    public TextMeshProUGUI timeText;
    public Animator timeTextAnimator;
    private IEnumerator showTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer._currentSecond % 10 == 0 && _timer._currentSecond != 0 && showTime == null)
        {
            showTime = ShowTimeText();
            StartCoroutine(ShowTimeText());
        }
    }

    IEnumerator ShowTimeText()
    {
        // Activate animation for time text
        timeText.text = _timer._currentSecond.ToString();
        timeTextAnimator.SetTrigger("ShowText");
        yield return new  WaitForSecondsRealtime(1f);
        showTime = null;
    }
}
