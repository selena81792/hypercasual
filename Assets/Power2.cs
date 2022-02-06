using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.Dialogs;
using UnityEngine.Events;

public class Power2 : MonoBehaviour
{
    private Image myIMGcomponent;
    [SerializeField] SpriteRenderer circleAround ;

    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickPower);
        myIMGcomponent = this.GetComponent<Image> ();
    }
    
    private void OnClickPower()
    {
        int currentGold = PlayerPrefs.GetInt("gold", 0);
        int currentVacuum = PlayerPrefs.GetInt("vacuum", 0);

        if (currentVacuum == 2)
        {
            DialogUI.Instance
            .SetTitle ( "Upgrade Vacuum Cleaner" )
            .SetMessage ( "Your Vacuum Cleaner is already the best :)" )
            .SetButtonColor ( DialogButtonColor.Blue )
            .SetButtonText ( "OK" )
            .Show ( );
        } else if (currentVacuum == 1)
        {
            if (currentGold < 100)
            {
                DialogUI.Instance
                .SetTitle ( "Upgrade Vacuum Cleaner" )
                .SetMessage ( "Upgrade Vacuum Cleaner for 100 Gold?\nYou don't have enough gold!" )
                .SetButtonColor ( DialogButtonColor.Blue )
                .SetButtonText ( "Not enough gold" )
                .SetButtonText2 ( "No" )
                .Show ( );
            } else {
                DialogUI.Instance
                .SetTitle ( "Upgrade Vacuum Cleaner" )
                .SetMessage ( "Upgrade Vacuum Cleaner for 100 Gold?" )
                .SetButtonColor ( DialogButtonColor.Blue )
                .SetButtonText ( "Yes" )
                .SetButtonText2 ( "No" )
                .OnClose ( UpgradeAgain )
                .Show ( );
            }
        } else {
            if (currentGold < 20)
            {
                DialogUI.Instance
                .SetTitle ( "Upgrade Vacuum Cleaner" )
                .SetMessage ( "Upgrade Vacuum Cleaner for 20 Gold?\nYou don't have enough gold!" )
                .SetButtonColor ( DialogButtonColor.Blue )
                .SetButtonText ( "Not enough gold" )
                .SetButtonText2 ( "No" )
                .Show ( );
            } else {
                DialogUI.Instance
                .SetTitle ( "Upgrade Vacuum Cleaner" )
                .SetMessage ( "Upgrade Vacuum Cleaner for 20 Gold?" )
                .SetButtonColor ( DialogButtonColor.Blue )
                .SetButtonText ( "Yes" )
                .SetButtonText2 ( "No" )
                .OnClose ( Upgrade )
                .Show ( );
            }
        }

        

    }

    private void Upgrade()
    {
        int currentGold = PlayerPrefs.GetInt("gold", 0);
        PlayerPrefs.SetInt("gold", currentGold - 20);
        Level.Instance.UpdateLevelGold();
        Level.Instance.Effects.PlayOneShot(Level.Instance.effect3);
        Debug.Log("upgrade vacuum 0->1");
        PlayerPrefs.SetInt("vacuum", 1);
        Magnet.Instance.SetForce(3000);
    }

    private void UpgradeAgain()
    {
        int currentGold = PlayerPrefs.GetInt("gold", 0);
        PlayerPrefs.SetInt("gold", currentGold - 100);
        Level.Instance.UpdateLevelGold();
        Level.Instance.Effects.PlayOneShot(Level.Instance.effect3);
        Debug.Log("upgrade vacuum 1->2");
        PlayerPrefs.SetInt("vacuum", 2);
        Magnet.Instance.SetForce(6000);
    }

}
