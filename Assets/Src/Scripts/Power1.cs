using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.Dialogs;
using UnityEngine.Events;

public class Power1 : MonoBehaviour
{
    private Image myIMGcomponent;
    [SerializeField] SpriteRenderer circleAround ;
    [SerializeField] Sprite fireImage ;
    [SerializeField] Sprite originalImage ;

    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickPower);
        myIMGcomponent = this.GetComponent<Image> ();
    }
    
    private void OnClickPower()
    {
        int currentGold = PlayerPrefs.GetInt("gold", 0);

        if (currentGold >= 5)
        {
            DialogUI.Instance
            .SetTitle ( "Speed Boost" )
            .SetMessage ( "Buy Speed Boost for 5 Gold?" )
            .SetButtonColor ( DialogButtonColor.Blue )
            .SetButtonText ( "Yes" )
            .SetButtonText2 ( "No" )
            .OnClose ( ActualAction )
            .Show ( );
        } else {
            DialogUI.Instance
            .SetTitle ( "Speed Boost" )
            .SetMessage ( "Buy Speed Boost for 5 Gold?\nYou don't have enough gold!" )
            .SetButtonColor ( DialogButtonColor.Blue )
            .SetButtonText ( "Not enough gold" )
            .SetButtonText2 ( "No" )
            .Show ( );
        }

    }

    private void ActualAction()
    {
        int currentGold = PlayerPrefs.GetInt("gold", 0);
        PlayerPrefs.SetInt("gold", currentGold - 5);
        Level.Instance.UpdateLevelGold();
        Debug.Log("power 1 clicked!");
        Level.Instance.Effects.PlayOneShot(Level.Instance.speedup);
        HoleMovement.Instance.SetSpeed(10);
        circleAround.color = new Color(255f, 0f, 0f, 1f);
        myIMGcomponent.sprite = fireImage;

        Invoke("StopAction", 10);
    }

    private void StopAction()
    {
        Level.Instance.Effects.PlayOneShot(Level.Instance.speeddown);
        HoleMovement.Instance.SetSpeed(4);
        circleAround.color = new Color(255f, 255f, 255f, 1f);
        myIMGcomponent.sprite = originalImage;
    }
}
