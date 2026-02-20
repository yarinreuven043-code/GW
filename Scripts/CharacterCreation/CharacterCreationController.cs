using UnityEngine;
using UnityEngine.UI;
using AdvancedPeopleSystem;

namespace CharacterCreation
{
	public class CharacterCreationController : MonoBehaviour
	{
		private CharacterCustomization character;
		private GameObject characterObject;

		[Header("Gender Toggles")]
		[SerializeField] private Toggle maleToggle;
		[SerializeField] private Toggle femaleToggle;
		
		[Header("Skin Colors")]
		[SerializeField] private Color[] skinColors;
		
		[Header("Eye Colors")]
		[SerializeField] private Color[] eyeColors;

		[Header("Hair Selector")]
		[SerializeField] private int currentHairIndex = -1;

		[Header("Beard Selector")]
		[SerializeField] private int currentBeardIndex = -1;

		[Header("Shirt Selector")]
		[SerializeField] private int currentShirtIndex = 0;
		
		[Header("Pants Selector")]
		[SerializeField] private int currentPantsIndex = 0;
		
		[Header("Shoes Selector")]
		[SerializeField] private int currentShoesIndex = 0;
		
		[SerializeField] private GameObject playerPrefab;
		private GameObject playerInstance;
		
		private bool suppressToggleEvent = false;
		private bool isMale = true;

		private void Start()
		{
			GameObject characterObject = GameObject.Find("Player");
			character = characterObject.GetComponent<CharacterCustomization>();

			maleToggle.onValueChanged.AddListener(OnMaleSelected);
			femaleToggle.onValueChanged.AddListener(OnFemaleSelected);

			suppressToggleEvent = true;
			maleToggle.isOn = true;
			femaleToggle.isOn = false;
			suppressToggleEvent = false;

			SetMale();
		}

		private void OnMaleSelected(bool isOn)
		{
			if (suppressToggleEvent || !isOn)
				return;

			suppressToggleEvent = true;
			femaleToggle.isOn = false;
			suppressToggleEvent = false;

			SetMale();
		}

		private void OnFemaleSelected(bool isOn)
		{
			if (suppressToggleEvent || !isOn)
				return;

			suppressToggleEvent = true;
			maleToggle.isOn = false;
			suppressToggleEvent = false;

			SetFemale();
		}

		private void SetMale()
		{
			isMale = true;
			character.SwitchCharacterSettings("Male");
		}

	   private void SetFemale()
		{
			isMale = false;
			character.SwitchCharacterSettings("Female");

			currentBeardIndex = -1;
			character.SetElementByIndex(CharacterElementType.Beard, -1);
		}

		public void SetSkinColor(int index)
		{
			if (index < 0 || index >= skinColors.Length)
				return;

			character.SetBodyColor(BodyColorPart.Skin, skinColors[index]);
		}
		
		public void SetEyeColor(int index)
		{
			if (index < 0 || index >= eyeColors.Length)
				return;

			character.SetBodyColor(BodyColorPart.Eye, eyeColors[index]);
		}
		
		public void NextHair()
		{
			int count = character.Settings.hairPresets.Count;
			
			if (count == 0)
				return;

			currentHairIndex++;
			
			if (currentHairIndex >= count)
				currentHairIndex = -1;

			character.SetElementByIndex(CharacterElementType.Hair, currentHairIndex);
		}

		public void PreviousHair()
		{
			int count = character.Settings.hairPresets.Count;
			
			if (count == 0)
				return;

			currentHairIndex--;
			
			if (currentHairIndex < -1)
				currentHairIndex = count - 1;

			character.SetElementByIndex(CharacterElementType.Hair, currentHairIndex);
		}
		
		public void NextBeard()
		{
			if (!isMale)
				return;

			int count = character.Settings.beardPresets.Count;
			
			if (count == 0)
				return;

			currentBeardIndex++;
			
			if (currentBeardIndex >= count)
				currentBeardIndex = -1;

			character.SetElementByIndex(CharacterElementType.Beard, currentBeardIndex);
		}

		public void PreviousBeard()
		{
			if (!isMale)
				return;

			int count = character.Settings.beardPresets.Count;
			
			if (count == 0)
				return;

			currentBeardIndex--;
			
			if (currentBeardIndex < -1)
				currentBeardIndex = count - 1;

			character.SetElementByIndex(CharacterElementType.Beard, currentBeardIndex);
		}
		
		public void NextShirt()
		{
			int count = character.Settings.shirtsPresets.Count;
			
			if (count == 0)
				return;

			currentShirtIndex++;
			
			if (currentShirtIndex >= count)
				currentShirtIndex = 0;

			character.SetElementByIndex(CharacterElementType.Shirt, currentShirtIndex);
		}

		public void PreviousShirt()
		{
			int count = character.Settings.shirtsPresets.Count;
			
			if (count == 0)
				return;

			currentShirtIndex--;
			
			if (currentShirtIndex < 0)
				currentShirtIndex = count - 1;

			character.SetElementByIndex(CharacterElementType.Shirt, currentShirtIndex);
		}
		
		public void NextPants()
		{
			int count = character.Settings.pantsPresets.Count;
			
			if (count == 0)
				return;

			currentPantsIndex++;
			
			if (currentPantsIndex >= count)
				currentPantsIndex = 0;

			character.SetElementByIndex(CharacterElementType.Pants, currentPantsIndex);
		}

		public void PreviousPants()
		{
			int count = character.Settings.pantsPresets.Count;
			
			if (count == 0)
				return;

			currentPantsIndex--;
			
			if (currentPantsIndex < 0)
				currentPantsIndex = count - 1;

			character.SetElementByIndex(CharacterElementType.Pants, currentPantsIndex);
		}
		
		public void NextShoes()
		{
			int count = character.Settings.shoesPresets.Count;
			
			if (count == 0)
				return;

			currentShoesIndex++;
			
			if (currentShoesIndex >= count)
				currentShoesIndex = 0;

			character.SetElementByIndex(CharacterElementType.Shoes, currentShoesIndex);
		}

		public void PreviousShoes()
		{
			int count = character.Settings.shoesPresets.Count;
			
			if (count == 0)
				return;

			currentShoesIndex--;
			
			if (currentShoesIndex < 0)
				currentShoesIndex = count - 1;

			character.SetElementByIndex(CharacterElementType.Shoes, currentShoesIndex);
		}
		
		public void FinishCharacterCreation()
		{
			CharacterCustomizationSetup.CharacterFileSaveFormat format = CharacterCustomizationSetup.CharacterFileSaveFormat.Json;

			character.SaveCharacterToFile(format);
			UnityEngine.SceneManagement.SceneManager.LoadScene("Summerlake City");
		}
	}	
}

