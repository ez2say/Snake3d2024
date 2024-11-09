using UnityEngine;

namespace Root.GUI
{
    public class GUIInspector : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;

        [SerializeField] private HUDManager _hudManager;

        [SerializeField] private PauseMenuManager _pauseManager;

        [SerializeField] private DeathScreenManager _deathScreenManager;

        public void Construct()
        {
            _mainMenu.Construct();

            _hudManager.Construct();

            _pauseManager.Construct();

            _deathScreenManager.Construct();
        }

        public void OpenMainMenu()
        {
            _mainMenu.Open();
            
            _hudManager.Close();

            _pauseManager.Close();
            
            _deathScreenManager.Close();
        }

        public void OpenGameplayMenu()
        {
            _mainMenu.Close();

            _hudManager.Open();

            _pauseManager.Open();

            _deathScreenManager.Close();
        }
    }
}