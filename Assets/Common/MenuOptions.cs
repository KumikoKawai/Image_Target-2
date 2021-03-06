/*===============================================================================
Copyright (c) 2015-2016 PTC Inc. All Rights Reserved.

Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;
using System.Linq;

public class MenuOptions : MonoBehaviour
{
    #region PRIVATE_MEMBERS
    private CameraSettings mCamSettings = null;
    private TrackableSettings mTrackableSettings = null;
    private MenuAnimator mMenuAnim = null;

		////////////////
		private StrategySettings mStrtSettings = null;
		private ToggleGroup mToggleGroup;
		private Toggle prev_tgl;
		///////////////

    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS
    protected virtual void Start()
    {
        mCamSettings = FindObjectOfType<CameraSettings>();
        mTrackableSettings = FindObjectOfType<TrackableSettings>();
        mMenuAnim = FindObjectOfType<MenuAnimator>();

				////////////////
				mStrtSettings = FindObjectOfType<StrategySettings>();
				mToggleGroup = GetComponentInChildren<ToggleGroup>();
				prev_tgl = mToggleGroup.ActiveToggles().FirstOrDefault();
				///////////////
    }
    #endregion //MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void ActivateDataset(string datasetName)
    {
        if (mTrackableSettings)
            mTrackableSettings.ActivateDataSet(datasetName);
    }

		public void SelectStrategy()
		{

			Toggle tgl = mToggleGroup.ActiveToggles().FirstOrDefault();

			if (tgl.name.CompareTo("1Toggle")==0 && tgl.name != prev_tgl.name && mStrtSettings)
			{
					mStrtSettings.SetStrategy(1);
			}
			else if(tgl.name.CompareTo("2Toggle")==0 && tgl.name != prev_tgl.name && mStrtSettings)
			{
			 	  mStrtSettings.SetStrategy(2);
			}
			else if(tgl.name.CompareTo("3Toggle")==0 && tgl.name != prev_tgl.name && mStrtSettings)
			{
					mStrtSettings.SetStrategy(3);
			}

			prev_tgl = tgl;


		}

    public void UpdateUI()
    {
        Toggle extTrackingToggle = FindUISelectableWithText<Toggle>("Extended");
        if (extTrackingToggle && mTrackableSettings)
            extTrackingToggle.isOn = mTrackableSettings.IsExtendedTrackingEnabled();

        Toggle flashToggle = FindUISelectableWithText<Toggle>("Flash");
        if (flashToggle && mCamSettings)
            flashToggle.isOn = mCamSettings.IsFlashTorchEnabled();

        Toggle autofocusToggle = FindUISelectableWithText<Toggle>("Autofocus");
        if (autofocusToggle && mCamSettings)
            autofocusToggle.isOn = mCamSettings.IsAutofocusEnabled();
    }

    public void CloseMenu()
    {
        if (mMenuAnim)
            mMenuAnim.Hide();
    }
    #endregion //PUBLIC_METHODS


    #region PROTECTED_METHODS
    protected T FindUISelectableWithText<T>(string text) where T : UnityEngine.UI.Selectable
    {
        T[] uiElements = GetComponentsInChildren<T>();
        foreach (var uielem in uiElements) {
            string childText = uielem.GetComponentInChildren<Text>().text;
            if (childText.Contains(text))
                return uielem;
        }
        return null;
    }
    #endregion //PROTECTED_METHODS
}
