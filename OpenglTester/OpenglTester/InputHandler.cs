using System;using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

/// <summary>
/// Input handler.
/// Tash
/// Tuesday 29 October 2013
/// use InputHandler.leftPressed, InputHandler.rightPressed, InputHandler.downPressed, InputHandler.upPressed, InputHandler.confirmPressed, InputHandler.escPressed
/// individual keyboard and xbox buttons are also usable
/// </summary>

namespace OpenglTester
{
	public class InputHandler
	{
		//use these!!
		public static bool leftPressed = false; //user has pressed 'a' or left arrow, or left stick or dpad left
		public static bool rightPressed = false; //etc
		public static bool downPressed = false; //user has pressed 's' or down arrow, or left stick or dpad down
		public static bool upPressed = false;
		public static bool confirmPressed = false; //user has pressed enter or space or A
		public static bool escPressed = false; //user has pressed esc or start
		//public static bool backPressed = false; // user has pressed B or ???

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
		public static bool keyEnter = false;
		public static bool keyEsc = false;




		public InputHandler()
		{

		}

		public void Update()
		{
			UpdateController();
			UpdateKeyboard();

			//moveLeft is true if the stick or the dPad or a or the left arrow are pressed
			if (btnDPadLeft || (stickLeftX < 0) || keyA || keyLeft)
				leftPressed = true;
			else
				leftPressed = false;

			//moveRight is true if the stick or the dpad or d or the right arrow are pressed
			if (btnDPadRight || (stickLeftX > 0) || keyD || keyRight)
				rightPressed = true;
			else
				rightPressed = false;

			//moveUp is true if stick or dpad or w or up arrow are pressed
			if (btnDPadUp || (stickLeftY < 0) || keyW || keyUp)
				upPressed = true;
			else
				upPressed = false;

			//moveDown is true if stick or dpad or s or down arrow are pressed
			if (btnDPadDown || (stickLeftY > 0) || keyS || keyDown)
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
				keyDown = false;
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
		}
	}
}

