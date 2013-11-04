using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OpenglTester
{
	public class Button : Object
	{
		bool isSelected;
		Texture2D tex_SelectedImage;
		Texture2D tex_DeselectedImage;
		
		public Button(float xPos, float yPos, string deselectedImage, string selectedImage): base(deselectedImage)
		{
			v2_Position.X = xPos;
			v2_Position.Y = yPos;
			v2_Size.X = 400;
			v2_Size.Y = 100;

			tex_SelectedImage = Game1.contentManager.Load<Texture2D>(selectedImage);
			tex_DeselectedImage = Game1.contentManager.Load<Texture2D>(deselectedImage);
			tex_Image = tex_DeselectedImage;

			isSelected = false;
		}
		
		public void Select()
		{
			isSelected = true;
			tex_Image = tex_SelectedImage;
		}
		
		public void Deselect()
		{
			isSelected = false;
			tex_Image = tex_DeselectedImage;
		}

		public bool IsSelected()
		{
			return isSelected;
		}
	}
}