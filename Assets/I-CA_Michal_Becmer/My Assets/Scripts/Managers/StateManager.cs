using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private bool gameEnded = false;

        private void Awake()
        {
            //Initialization logic
        }

        private void Start()
        {
            //Start logic
        }

        private void Update()
        {
            //Evaluation logic
        }

        private void HandleWin()
        {
            //Handle win logic
        }

        private void HandleLoss()
        {
            //Handle loss logic
        }
    }
}
