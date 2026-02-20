using UnityEngine;
using Core;

namespace UI.Buttons
{
	public class LoadCharacterCreationButton : MonoBehaviour
	{
		public void OnClick()
		{
			SceneFlowController.Instance.LoadCharacterCreation();
		}
	}
}

