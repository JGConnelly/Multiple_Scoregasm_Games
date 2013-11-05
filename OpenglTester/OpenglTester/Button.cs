
using System;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace OpenglTester
{
	public class Button : Object
	{
		Texture2D tex_selected, tex_deselected;
		public bool isSelected = false;
		
		public Button(float xPos, float yPos, string deselectedImage, string selectedImage):base (deselectedImage)
		{
			v2_Position.X = xPos;
			v2_Position.Y = yPos;
			
			tex_selected = Game1.contentManager.Load<Texture2D>(selectedImage);
			tex_deselected = Game1.contentManager.Load<Texture2D>(deselectedImage);
			tex_Image = tex_deselected;
			isSelected = false;
		}
		
		public void Select()
		{
			isSelected = true;
			tex_Image = tex_selected;
		}
		
		public void Deselect()
		{
			isSelected = false;
			tex_Image = tex_deselected;
		}
		
		void Update(double DeltaTime)
		{
			base.Update((float)DeltaTime);
		}
		void Draw()
		{
			base.Draw();
		}
	}
}

