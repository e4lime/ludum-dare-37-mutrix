﻿using UnityEngine;
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
        [SerializeField]
        private Text m_EndText;

        private int m_TotalShotsMade = 0;
        private int m_TotalDifferences = 0;
        private int m_DifferencesFound = 0;

        private bool m_GameCompleted = false;


        void Awake() {
            if (instance == null) {
                instance = this;
            }
            else {
                Debug.LogError("Double MutrixGame", this);
            }
            m_EndText.gameObject.SetActive(false);
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
            if (m_GameCompleted) return;
            m_DifferencesFound++;
            UpdateDifferenceDisplay();
            if (m_DifferencesFound == m_TotalDifferences) {
                m_GameCompleted = true;
                m_EndText.gameObject.SetActive(true);
            }
        }
        
        public void ShotMade() {
            if (m_GameCompleted) return;
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
