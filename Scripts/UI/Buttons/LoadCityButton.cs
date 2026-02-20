using UnityEngine;
using Core;

namespace UI.Buttons
{
	public class LoadCityButton : MonoBehaviour
	{
		public void OnClick()
		{
			SceneFlowController.Instance.LoadCity();
		}
	}	
}

