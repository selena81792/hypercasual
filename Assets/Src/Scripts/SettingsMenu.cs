using UnityEngine ;
using UnityEngine.UI ;
using DG.Tweening ;
using EasyUI.Dialogs;
using UnityEngine.Events;

public class SettingsMenu : MonoBehaviour {
   [Header ("space between menu items")]
   [SerializeField] Vector2 spacing ;
   [SerializeField] Sprite image0 ;
   [SerializeField] Sprite image0clicked ;
   [SerializeField] Sprite image1 ;
   [SerializeField] Sprite image1clicked ;

   [Space]
   [Header ("Main button rotation")]
   [SerializeField] float rotationDuration ;
   [SerializeField] Ease rotationEase ;

   [Space]
   [Header ("Animation")]
   [SerializeField] float expandDuration ;
   [SerializeField] float collapseDuration ;
   [SerializeField] Ease expandEase ;
   [SerializeField] Ease collapseEase ;

   [Space]
   [Header ("Fading")]
   [SerializeField] float expandFadeDuration ;
   [SerializeField] float collapseFadeDuration ;

   Button mainButton ;
   SettingsMenuItem[] menuItems ;

   //is menu opened or not
   bool isExpanded = false ;

   private UnityAction RestartFromLevelOneAction;

   Vector2 mainButtonPosition ;
   int itemsCount ;

   void Start () {
      //add all the items to the menuItems array
      itemsCount = transform.childCount - 1 ;
      menuItems = new SettingsMenuItem[itemsCount] ;
      for (int i = 0; i < itemsCount; i++) {
         // +1 to ignore the main button
         menuItems [ i ] = transform.GetChild (i + 1).GetComponent <SettingsMenuItem> () ;
      }

      mainButton = transform.GetChild (0).GetComponent <Button> () ;
      mainButton.onClick.AddListener (ToggleMenu) ;
      //SetAsLastSibling () to make sure that the main button will be always at the top layer
      mainButton.transform.SetAsLastSibling () ;

      mainButtonPosition = mainButton.GetComponent <RectTransform> ().anchoredPosition ;

      //set all menu items position to mainButtonPosition
      ResetPositions () ;
   }

   void ResetPositions () {
      for (int i = 0; i < itemsCount; i++) {
         menuItems [ i ].rectTrans.anchoredPosition = mainButtonPosition ;
      }
   }

   void ToggleMenu () {
      isExpanded = !isExpanded ;

      if (isExpanded) {
         //menu opened
         for (int i = 0; i < itemsCount; i++) {
            menuItems [ i ].rectTrans.DOAnchorPos (mainButtonPosition + spacing * (i + 1), expandDuration).SetEase (expandEase) ;
            //Fade to alpha=1 starting from alpha=0 immediately
            menuItems [ i ].img.DOFade (1f, expandFadeDuration).From (0f) ;
         }
      } else {
         //menu closed
         for (int i = 0; i < itemsCount; i++) {
            menuItems [ i ].rectTrans.DOAnchorPos (mainButtonPosition, collapseDuration).SetEase (collapseEase) ;
            //Fade to alpha=0
            menuItems [ i ].img.DOFade (0f, collapseFadeDuration) ;
         }
      }

      //rotate main button arround Z axis by 180 degree starting from 0
      mainButton.transform
			.DORotate (Vector3.forward * 180f, rotationDuration)
			.From (Vector3.zero)
			.SetEase (rotationEase) ;
   }

   public void OnItemClick (int index) {
      //here you can add you logic 
      switch (index) {
         case 0: 
				//first button
            // Debug.Log ("Music") ;
            if (menuItems[0].GetComponent<Image>().sprite == image0){
               menuItems[0].GetComponent<Image>().sprite = image0clicked;
            } else {
               menuItems[0].GetComponent<Image>().sprite = image0;
            }
            Level.Instance.Music.mute = !Level.Instance.Music.mute;
            break ;
         case 1: 
				//second button
            if (menuItems[1].GetComponent<Image>().sprite == image1){
               menuItems[1].GetComponent<Image>().sprite = image1clicked;
            } else {
               menuItems[1].GetComponent<Image>().sprite = image1;
            }
            Level.Instance.Effects.mute = !Level.Instance.Effects.mute;
            break ;
         case 2: 
				//third button
            RestartFromLevelOneAction += Level.Instance.RestartFromLevelOne;
            Debug.Log ("Vibration") ;
            DialogUI.Instance
            .SetTitle ( "Restart Game" )
            .SetMessage ( "Clear All Progress and Restart from Level 1?" )
            .SetButtonColor ( DialogButtonColor.Red )
            .SetButtonText ( "Yes" )
            .SetButtonText2 ( "No" )
            .OnClose ( RestartFromLevelOneAction )
            .OnClose2 ( ( ) => Debug.Log ( "Closed 2" ) )
            .Show ( );
            break ;

         case 3: 
            //info button
            DialogUI.Instance
            .SetTitle ( "Information" )
            .SetMessage ( "This game is made by Ka Po Chau, for the Epitech Hyper Casual module project. Team name: Happy. Enjoy :)" )
            .SetButtonColor ( DialogButtonColor.Blue )
            .SetButtonText ( "OK" )
            .SetButtonText2 ( "No" )
            .Show ( );
            break ;
      }
   }

   void OnDestroy () {
      //remove click listener to avoid memory leaks
      mainButton.onClick.RemoveListener (ToggleMenu) ;
   }
}
