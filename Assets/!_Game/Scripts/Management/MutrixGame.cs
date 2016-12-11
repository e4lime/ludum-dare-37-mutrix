using UnityEngine;
using System.Collections;
using Mutrix.WorldObjects;
using UnityEngine.UI;

namespace Mutrix.Management {
    public class MutrixGame : MonoBehaviour {
        public static MutrixGame instance = null;

        [SerializeField]
        private Text m_DifferencesDisplay;
        [SerializeField]
        private Text m_ShotsDisplay; 

        private int m_TotalShotsMade = 0;
        private int m_TotalDifferences = 0;
        private int m_DifferencesFound = 0;



        void Awake() {
            if (instance == null) {
                instance = this;
            }
            else {
                Debug.LogError("Double MutrixGame", this);
            }
            UpdateDifferenceDisplay();
            UpdateShotsDisplay();
        }

       
        public void RegisterDifferenceObject(WorldObject wo) {
            if (wo.IsBoth()) {
                Debug.LogError("World object: " + wo.name + " is already in both worlds.");
            }
            else {
                m_TotalDifferences++;
            }

            UpdateDifferenceDisplay();
        }

        public void DifferenceFound() {
            m_DifferencesFound++;
            UpdateDifferenceDisplay();
        }
        
        public void ShotMade() {
            m_TotalShotsMade++;
            UpdateShotsDisplay();
        }

        public int GetDifferencesLeft() {
            return m_TotalDifferences - m_DifferencesFound;
        }

        public int GetShotsMade() {
            return m_TotalShotsMade;
        }

        public int GetDifferencesFound() {
            return m_DifferencesFound;
        }

        public int GetTotalDifferences() {
            return m_TotalDifferences;
        }


        private void UpdateDifferenceDisplay() {
            m_DifferencesDisplay.text = m_DifferencesFound + " / " + m_TotalDifferences;
        }

        private void UpdateShotsDisplay() {
            m_ShotsDisplay.text = m_TotalShotsMade + "";

        }
    }
}
