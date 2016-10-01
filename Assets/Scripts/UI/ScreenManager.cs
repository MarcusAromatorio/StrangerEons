using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

/*
    This class is borrowed from the unity3d manual and slightly altered to fit the needs of this project
    The source can be found here https://docs.unity3d.com/Manual/HOWTO-UIScreenTransition.html

    This script is a simple controller for the menus of a 2D game project.
    To be visible, the game must have a canvas object in the hierarchy at least as the root of all screen children

    Expanded to adding runtime user controlled viewing of a tree-style data structure with "FirstOpen" as the root
*/

public class ScreenManager : MonoBehaviour {


    // First menu to open at the beginning of the scene
    // Menus are controlled via their animator states, ensuring only one is open while all others are closed
    public GameObject firstOpenMenu;

    // Member variables used in menu switching
    private Animator m_CurrentMenu;
    private int m_OpenParameterId;              // ID of the parameter to set when changing open/closed state
    private GameObject m_PreviouslySelected;    // Object that caused the current menu to open, used when returning to previous menus

    [SerializeField]
    private int m_LevelToLoad;

    // Constants used for checking against menu state
    const string k_OpenStateName = "Open";
    const string k_ClosedStateName = "Closed";

	public void OnEnable()
    {
        // Save the ID of the open parameter to set later
        m_OpenParameterId = Animator.StringToHash(k_OpenStateName);
        m_LevelToLoad = 0;

        // Open the first menu if it is initialized
        if (firstOpenMenu != null)
            OpenMenu(firstOpenMenu);
    }
    
	
    // This method closes the current menu and opens the provided one
    // Handles navigation by setting the EventManager's Selected element
    // @param menuToOpen: GameObject container of the desired menu. Must have an Animator component with "Open" and "Closed" state
	public void OpenMenu(GameObject menuObject)
    {
        Animator menuToOpen = menuObject.GetComponent<Animator>();

        if (m_CurrentMenu == menuObject)
            return; // Exit if the desired menu is already open

        // Set the new menu to active
        menuObject.SetActive(true);
        GameObject newPreviousSelected = EventSystem.current.currentSelectedGameObject;

        CloseCurrentMenu();

        // Set the proper member variables to their new reference values
        m_PreviouslySelected = newPreviousSelected;
        m_CurrentMenu = menuToOpen;
        m_CurrentMenu.SetBool(m_OpenParameterId, true);

        // Set a selectable element in the new menu to be used for navigation as selected
        GameObject g = FindFirstEnabledSelectable(menuObject);
        SetSelected(g);

    }

    // Method to go back to the previously opened screen
    public void GoBack()
    {
        if (m_PreviouslySelected == null)
            return;

        // Open the menu parented to the m_PreviouslyOpened object
        GameObject previousMenu = m_PreviouslySelected.transform.parent.gameObject;
        OpenMenu(previousMenu);
    }

    // Method to close the current menu, handling navigation
    private void CloseCurrentMenu()
    {
        if (m_CurrentMenu == null)
            return;

        // Start the close animation
        m_CurrentMenu.SetBool(m_OpenParameterId, false);
        StartCoroutine(DisableMenuDelayed(m_CurrentMenu));

        // Set the selected gameobject to the previous selection
        SetSelected(m_PreviouslySelected);

        // Reflect the closing of the menu to the member variable
        m_CurrentMenu = null;
    }

    //Finds the first Selectable element in the providade hierarchy.
    static GameObject FindFirstEnabledSelectable(GameObject gameObject)
    {
        GameObject go = null;
        var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
        foreach (var selectable in selectables)
        {
            if (selectable.IsActive() && selectable.IsInteractable())
            {
                go = selectable.gameObject;
                break;
            }
        }
        return go;
    }

    // Coroutine to deactivate the closing menu after the animation has finished
    IEnumerator DisableMenuDelayed(Animator closingMenu)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while(!closedStateReached && wantToClose)
        {
            // If the menu ISN'T transitioning, check if the current state is "Closed"
            if (!closingMenu.IsInTransition(0))
                closedStateReached = closingMenu.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

            // only want to close if the open parameter is false (given we haven't reached the closed state yet)
            wantToClose = !closingMenu.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            closingMenu.gameObject.SetActive(false);
    }

    // Make the provided GameObject selected
    // When using the mouse/touch we actually want to set it as the previously selected and 
    // set nothing as selected for now.
    private void SetSelected(GameObject go)
    {
        // Select the GameObject.
        EventSystem.current.SetSelectedGameObject(go);

        // Since we are using a pointer device, we don't want anything selected. 
        // But if the user switches to the keyboard, we want to start the navigation from the provided game object.
        // So here we set the current Selected to null, so the provided gameObject becomes the Last Selected in the EventSystem.
        EventSystem.current.SetSelectedGameObject(null);
    }

    // The screen manager should have the ability to load a new game scene given the appropriate button press
    public void LoadNewLevel(int level)
    {
        m_LevelToLoad = level;
        SceneManager.LoadScene("Level" + m_LevelToLoad);
    }
}
