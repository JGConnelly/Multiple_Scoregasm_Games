using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

/// <summary>
/// Input handler.
/// Tash
/// Tuesday 29 October 2013
/// use InputHandler.leftPressed, InputHandler.rightPressed, InputHandler.confirmPressed, etc
/// individual keyboard and xbox buttons are also usable
/// </summary>

namespace OpenglTester
{
	public class InputHandler
	{
		//use these!!
		public static bool leftPressed = false; //user has pressed 'a' or left arrow, or left stick
		public static bool rightPressed = false; //etc
		public static bool downPressed = false; //user has pressed 's' or down arrow, or left stick
		public static bool upPressed = false;
		public static bool confirmPressed = false; //user has pressed space or A
		public static bool escPressed = false; //user has pressed esc or start
		public static bool sprintPressed = false; // shift or B
		public static bool punchPressed = false; // right ctrl or e or X
		public static bool switchPressed = false;//'r' or Num0 or Y
		public static bool dlg1Pressed = false; //Up on DPad or 1 on keyboard
		public static bool dlg2Pressed = false; //left on DPad or 2 on keyboard
		public static bool dlg3Pressed = false; //down on DPad or 3 on keyboard
		public static bool dlg4Pressed = false; //right on DPad or 4 on keyboard


		//individual xbox buttons
		public static bool btnA = false;
		public static bool btnB = false;
		public static bool btnX = false;
		public static bool btnY = false;
		public static bool btnBack = false;
		public static bool btnStart = false;
		public static bool btnBig = false;
		public static bool btnDPadDown = false;
		public static bool btnDPadUp = false;
		public static bool btnDPadLeft = false;
		public static bool btnDPadRight = false;
		public static bool btnLeftShoulder = false;
		public static bool btnLeftTrigger = false;
		public static bool btnRightShoulder = false;
		public static bool btnRightTrigger = false;
		public static float stickLeftX = 0;
		public static float stickLeftY = 0;
		public static float stickRightX = 0;
		public static float stickRightY = 0;
		public static bool stickLeft = false;
		public static bool stickRight = false;

		//individual keyboard buttons
		public static bool keySpace = false;
		public static bool keyUp = false;
		public static bool keyDown = false;
		public static bool keyLeft = false;
		public static bool keyRight = false;
		public static bool keyW = false;
		public static bool keyA = false;
		public static bool keyS = false;
		public static bool keyD = false;
		public static bool keyE = false;
		public static bool keyEnter = false;
		public static bool keyEsc = false;
		public static bool keyCtrl = false;
		public static bool keyShift = false;

		public static bool keyR = false;
		public static bool keyNum0 = false;

		//Keyboard buttons for dialogue
		public static bool key1 = false;
		public static bool key2 = false;
		public static bool key3 = false;
		public static bool key4 = false;




		public InputHandler()
		{

		}

		/// <summary>
		/// This function is called every loop (from Game1.cs)
		/// It calls the UpdateController() and UpdateKeyboard() function, and sets leftPressed, confirmPressed, punchPressed, etc.
		/// </summary>
		public void Update ()
		{
			UpdateController ();
			UpdateKeyboard ();

			//moveLeft is true if the stick or the dPad or a or the left arrow are pressed
			if ((stickLeftX < 0) || keyA || keyLeft)
				leftPressed = true;
			else
				leftPressed = false;

			//moveRight is true if the stick or the dpad or d or the right arrow are pressed
			if ((stickLeftX > 0) || keyD || keyRight)
				rightPressed = true;
			else
				rightPressed = false;

			//moveUp is true if stick or dpad or w or up arrow are pressed
			if ((stickLeftY > 0) || keyW || keyUp)
				upPressed = true;
			else
				upPressed = false;

			//moveDown is true if stick or dpad or s or down arrow are pressed
			if ((stickLeftY < 0) || keyS || keyDown)
				downPressed = true;
			else
				downPressed = false;

			//confirmPressed is true if enter or space or A
			if (btnA || keySpace)
				confirmPressed = true;
			else
				confirmPressed = false;

			//escPressed is true if esc or start
			if (keyEsc || btnStart)
				escPressed = true;
			else
				escPressed = false;

			//sprintPressed is true if B or shift
			if (keyShift || btnB)
				sprintPressed = true;
			else
				sprintPressed = false;

			//punchPressed is true if X or ctrl or e
			if (btnX || keyE || keyCtrl)
				punchPressed = true;
			else
				punchPressed = false;
			//
			if (keyR || keyNum0 || btnY) 
				switchPressed = true;
			else
				switchPressed = false;

			dlg1Pressed = btnDPadUp   ||key1;

			dlg2Pressed = btnDPadLeft ||key2;
				//dlg2Pressed = true;
			dlg3Pressed = btnDPadDown ||key3;
				//dlg3Pressed = true;
			dlg4Pressed = btnDPadRight||key4; 
				//dlg4Pressed = true;

		}

		public void UpdateController()
		{
			GamePadState padState = GamePad.GetState(PlayerIndex.One);
			if(padState.IsConnected)
			{
				if (padState.IsButtonDown(Buttons.A))
				    btnA = true;
				else
					btnA = false;

				if (padState.IsButtonDown(Buttons.B))
					btnB = true;
				else
					btnB = false;

				if (padState.IsButtonDown(Buttons.X))
					btnX = true;
				else
					btnX = false;

				if (padState.IsButtonDown(Buttons.Y))
					btnY = true;
				else
					btnY = false;

				if (padState.IsButtonDown(Buttons.Back))
					btnBack = true;
				else
					btnBack = false;

				if (padState.IsButtonDown(Buttons.Start))
					btnStart = true;
				else
					btnStart = false;

				if (padState.IsButtonDown(Buttons.BigButton))
					btnBig = true;
				else
					btnBig = false;

				if (padState.IsButtonDown(Buttons.DPadDown))
					btnDPadDown = true;
				else
					btnDPadDown = false;
				
				if (padState.IsButtonDown(Buttons.DPadUp))
					btnDPadUp = true;
				else
					btnDPadUp = false;
				
				if (padState.IsButtonDown(Buttons.DPadLeft))
					btnDPadLeft = true;
				else
					btnDPadLeft = false;
				
				if (padState.IsButtonDown(Buttons.DPadRight))
					btnDPadRight = true;
				else
					btnDPadRight = false;

				if (padState.IsButtonDown(Buttons.LeftShoulder))
					btnLeftShoulder = true;
				else
					btnLeftShoulder = false;
				
				if (padState.IsButtonDown(Buttons.LeftTrigger))
					btnLeftTrigger = true;
				else
					btnLeftTrigger = false;
				
				if (padState.IsButtonDown(Buttons.RightShoulder))
					btnRightShoulder = true;
				else
					btnRightShoulder = false;
				
				if (padState.IsButtonDown(Buttons.RightTrigger))
					btnRightTrigger = true;
				else
					btnRightTrigger = false;

				if (padState.IsButtonDown(Buttons.LeftStick))
					stickLeft = true;
				else
					stickLeft = false;

				if (padState.IsButtonDown(Buttons.RightStick))
					stickRight = true;
				else
					stickRight = false;

				stickLeftX = padState.ThumbSticks.Left.X;
				stickLeftY = padState.ThumbSticks.Left.Y;
				stickRightX = padState.ThumbSticks.Right.X;
				stickRightY = padState.ThumbSticks.Right.Y;
			}
		}

		public void UpdateKeyboard()
		{
			KeyboardState kb = Keyboard.GetState();
			//space
			if (kb.IsKeyDown(Keys.Space))
				keySpace = true;
			else
				keySpace = false;
			//enter
			if (kb.IsKeyDown(Keys.Enter))
				keyEnter = true;
			else
				keyEnter = false;
			//escape
			if (kb.IsKeyDown(Keys.Escape))
				keyEsc = true;
			else
				keyEsc = false;
			//up arrow
			if (kb.IsKeyDown(Keys.Up))
				keyUp = true;
			else
				keyUp = false;
			//down arrow
			if (kb.IsKeyDown(Keys.Down))
				keyDown = true;
			else
				keyDown = false;
			//left arrow
			if (kb.IsKeyDown(Keys.Left))
				keyLeft = true;
			else
				keyLeft = false;
			//right arrow
			if (kb.IsKeyDown(Keys.Right))
				keyRight = true;
			else
				keyRight = false;
			//w
			if (kb.IsKeyDown(Keys.W))
				keyW = true;
			else
				keyW = false;
			//a
			if (kb.IsKeyDown(Keys.A))
				keyA = true;
			else
				keyA = false;
			//s
			if (kb.IsKeyDown(Keys.S))
				keyS = true;
			else
				keyS = false;
			//d
			if (kb.IsKeyDown(Keys.D))
				keyD = true;
			else
				keyD = false;
			//e
			if (kb.IsKeyDown(Keys.E))
				keyE = true;
			else
				keyE = false;

			//shift
			if (kb.IsKeyDown(Keys.LeftShift) || kb.IsKeyDown(Keys.RightShift))
				keyShift = true;
			else
				keyShift = false;

			//ctrl
			if (kb.IsKeyDown(Keys.RightControl) || kb.IsKeyDown(Keys.LeftControl))
				keyCtrl = true;
			else
				keyCtrl = false;

			if (kb.IsKeyDown(Keys.R))
				keyR = true;
			else 
				keyR = false;

			if (kb.IsKeyDown(Keys.NumPad0))
				keyNum0 = true;
			else 
				keyNum0 = false;


			if (kb.IsKeyDown(Keys.D1))
				key1 = true;
			else
				key1 =false;
			if (kb.IsKeyDown(Keys.D2))
				key2 = true;
			else
				key2 =false;
			if (kb.IsKeyDown(Keys.D3))
				key3 = true;
			else
				key3 =false;
			if (kb.IsKeyDown(Keys.D4))
				key4 = true;
			else
				key4 =false;


		}
	}
}

