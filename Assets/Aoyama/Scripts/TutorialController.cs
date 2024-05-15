using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private IngameController _ingame;

    [SerializeField]
    private Image _wasdImage;

    [SerializeField]
    private Image _mouseImage;

    private bool _isMove = false;

    private bool _isMouse = false;

    private bool _isWASDFade = false;

    private bool _isMouseFade = false;

    private void Start()
    {
        _wasdImage.gameObject.SetActive(false);
        _mouseImage.gameObject.SetActive(false);

        StartCoroutine(EnableAnimation());
    }

    private IEnumerator EnableAnimation()
    {
        yield return new WaitForSeconds(4f);

        _wasdImage.gameObject.SetActive(true);
        _wasdImage.CrossFadeAlpha(0f, 0f, false);
        _wasdImage.CrossFadeAlpha(1f, 0.3f, false);

        yield return new WaitForSeconds(9f);

        _mouseImage.gameObject.SetActive(true);
        _mouseImage.CrossFadeAlpha(0f, 0f, false);
        _mouseImage.CrossFadeAlpha(1f, 0.3f, false);
    }

    void Update()
    {
        if (_ingame.State != InGameState.Game)
        {
            _wasdImage.gameObject.SetActive(false);
            _mouseImage.gameObject.SetActive(false);
        }
        else
        {
            float time = _ingame.GameTime;

            TutorialCheck();
            InputCheck(time);
        }    
    }

    private void TutorialCheck()
    {
        if (_isMove && !_isWASDFade)
        {
            _wasdImage.CrossFadeAlpha(0f, 0.5f, false);
            _isWASDFade = true;
        }

        if (_isMouse && !_isMouseFade)
        {
            _mouseImage.CrossFadeAlpha(0f, 0.5f, false);    
            _isMouseFade = true;
        }
    }

    private void InputCheck(float time)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        if (h != 0 && time > 2f || v != 0 && time > 2f) _isMove = true;

        if (mouse_x != 0 && time > 12f || mouse_y != 0 && time > 12f) _isMouse = true;
    }
}
