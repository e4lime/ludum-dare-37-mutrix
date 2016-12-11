using UnityEngine;
using System.Collections;
using Mutrix.WorldObjects;

namespace Mutrix.Management {
    public class MutrixGame : MonoBehaviour {
        public static MutrixGame instance = null;

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
        }

       
        public void RegisterDifferenceObject(WorldObject wo) {
            if (wo.IsBoth()) {
                Debug.LogError("World object: " + wo.name + " is already in both worlds.");
            }
            else {
                m_TotalDifferences++;
            }
        }

        public void DifferenceFound() {
            m_DifferencesFound++;
        }
        
        public void ShotMade() {
            m_TotalShotsMade++;
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

    }
}
