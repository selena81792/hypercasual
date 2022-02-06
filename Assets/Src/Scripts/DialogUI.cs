﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace EasyUI.Dialogs {

	public enum DialogButtonColor {
		Black,
		Purple,
		Magenta,
		Blue,
		Green,
		Yellow,
		Orange,
		Red
	}

	public class Dialog {
		public string Title = "Title";
		public string Message = "Message goes here.";
		public string ButtonText = "OK";
		public string ButtonText2 = "Close";
		public float FadeInDuration = .3f;
		public DialogButtonColor ButtonColor = DialogButtonColor.Black;
		public DialogButtonColor ButtonColor2 = DialogButtonColor.Black;
		public UnityAction OnClose = null;
		public UnityAction OnClose2 = null;
	}

	public class DialogUI : MonoBehaviour {
		[SerializeField] GameObject canvas;
		[SerializeField] Text titleUIText;
		[SerializeField] Text messageUIText;
		[SerializeField] Button closeUIButton;
		[SerializeField] Button closeUIButton2;

		Image closeUIButtonImage;
		Image closeUIButtonImage2;
		Text closeUIButtonText;
		Text closeUIButtonText2;
		CanvasGroup canvasGroup;

		[Space ( 20f )]
		[Header ( "Close button colors" )]
		[SerializeField] Color[] buttonColors;

		Queue<Dialog> dialogsQueue = new Queue<Dialog> ( );
		Dialog dialog = new Dialog ( );
		Dialog tempDialog;

		[HideInInspector] public bool IsActive = false;

		//Singleton pattern
		public static DialogUI Instance;



		void Awake ( ) {
			Instance = this;

			closeUIButtonImage = closeUIButton.GetComponent <Image> ( );
			closeUIButtonImage2 = closeUIButton2.GetComponent <Image> ( );
			closeUIButtonText = closeUIButton.GetComponentInChildren <Text> ( );
			closeUIButtonText2 = closeUIButton2.GetComponentInChildren <Text> ( );
			canvasGroup = canvas.GetComponent <CanvasGroup> ( );

			//Add close event listener
			closeUIButton.onClick.RemoveAllListeners ( );
			closeUIButton2.onClick.RemoveAllListeners ( );
			closeUIButton.onClick.AddListener ( Hide );
			closeUIButton2.onClick.AddListener ( Hide2 );
		}

		
		public DialogUI SetTitle ( string title ) {
			dialog.Title = title;
			return Instance;
		}

		
		public DialogUI SetMessage ( string message ) {
			dialog.Message = message;
			return Instance;
		}

		
		public DialogUI SetButtonText ( string text ) {
			dialog.ButtonText = text;
			return Instance;
		}

		public DialogUI SetButtonText2 ( string text ) {
			dialog.ButtonText2 = text;
			return Instance;
		}

		
		public DialogUI SetButtonColor ( DialogButtonColor color ) {
			dialog.ButtonColor = color;
			return Instance;
		}

		public DialogUI SetButtonColor2 ( DialogButtonColor color ) {
			dialog.ButtonColor2 = color;
			return Instance;
		}

		
		public DialogUI SetFadeInDuration ( float duration ) {
			dialog.FadeInDuration = duration;
			return Instance;
		}

		
		public DialogUI OnClose ( UnityAction action ) {
			dialog.OnClose = action;
			return Instance;
		}

		public DialogUI OnClose2 ( UnityAction action ) {
			dialog.OnClose2 = action;
			return Instance;
		}

		//-------------------------------------
		
		public void Show ( ) {
			dialogsQueue.Enqueue ( dialog );
			//Reset Dialog
			dialog = new Dialog ( );

			if ( !IsActive )
				ShowNextDialog ( );
		}


		void ShowNextDialog ( ) {
			tempDialog = dialogsQueue.Dequeue ( );

			titleUIText.text = tempDialog.Title;
			messageUIText.text = tempDialog.Message;
			closeUIButtonText.text = tempDialog.ButtonText.ToUpper ( );
			closeUIButtonImage.color = buttonColors [ ( int )tempDialog.ButtonColor ];

			canvas.SetActive ( true );
			IsActive = true;
			StartCoroutine ( FadeIn ( tempDialog.FadeInDuration ) );
		}


		// Hide dialog
		public void Hide ( ) {
			canvas.SetActive ( false );
			IsActive = false;

			// Invoke OnClose Event
			if ( tempDialog.OnClose != null )
				tempDialog.OnClose.Invoke ( );

			StopAllCoroutines ( );

			if ( dialogsQueue.Count != 0 )
				ShowNextDialog ( );
		}

		// Hide dialog
		public void Hide2 ( ) {
			canvas.SetActive ( false );
			IsActive = false;

			// Invoke OnClose Event
			if ( tempDialog.OnClose2 != null )
				tempDialog.OnClose2.Invoke ( );

			StopAllCoroutines ( );

			if ( dialogsQueue.Count != 0 )
				ShowNextDialog ( );
		}


		//-------------------------------------

		IEnumerator FadeIn ( float duration ) {
			float startTime = Time.time;
			float alpha = 0f;

			while ( alpha < 1f ) {
				alpha = Mathf.Lerp ( 0f, 1f, (Time.time - startTime) / duration );
				canvasGroup.alpha = alpha;

				yield return null;
			}
		}
	}

}