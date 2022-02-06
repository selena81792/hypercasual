using UnityEngine;
using EasyUI.Dialogs;

public class Demo : MonoBehaviour {

	void Start ( ) {

		// First Dialog -----------------------------
		DialogUI.Instance
		.SetTitle ( "Restart Game" )
		.SetMessage ( "Clear All Progress and Restart from Level 1?" )
		.SetButtonColor ( DialogButtonColor.Red )
		.SetButtonText ( "Yes" )
		.SetButtonText2 ( "No" )
		.OnClose ( ( ) => Debug.Log ( "Closed 1" ) )
		.OnClose2 ( ( ) => Debug.Log ( "Closed 2" ) )
		.Show ( );


		// Second Dialog ----------------------------
		// DialogUI.Instance
		// .SetTitle ( "Message 2" )
		// .SetMessage ( "Hello Again :)" )
		// .SetButtonColor ( DialogButtonColor.Magenta )
		// .SetButtonText ( "ok" )
		// .OnClose ( ( ) => Debug.Log ( "Closed 2" ) )
		// .Show ( );


		// // Third Dialog -----------------------------
		// DialogUI.Instance
		// .SetTitle ( "Message 3" )
		// .SetMessage ( "Bye!" )
		// .SetFadeInDuration ( 1f )
		// .SetButtonColor ( DialogButtonColor.Red )
		// .OnClose ( ( ) => Debug.Log ( "Closed 3" ) )
		// .Show ( );

	}

}
