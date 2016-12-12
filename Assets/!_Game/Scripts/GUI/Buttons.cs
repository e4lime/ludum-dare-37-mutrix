using UnityEngine;
using System.Collections;
namespace Mutrix.GUI {
    public class Buttons : MonoBehaviour {

        public RectTransform prepareButton;
      //  public RectTransform startButton;

        public RectTransform instructions;

        /// <summary>
        /// On prepare click
        /// </summary>
        public void OnInstructionClick() {
              prepareButton.gameObject.SetActive(false);
              instructions.gameObject.SetActive(true);
        }


        public void OnStartGame() {

           Management.MutrixGame.instance.StartGame();
        }
    }
}
