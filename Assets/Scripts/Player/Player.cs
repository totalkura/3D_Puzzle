using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        public PlayerController controller;
        public PlayerCondition condition;

        public ConsoleKeypad consoleKeypad; // �ܼ� Ű�е� ����

    private void Awake()
        {
            CharacterManager.Instance.Player = this;
            controller = GetComponent<PlayerController>();
            condition = GetComponent<PlayerCondition>();
        }
       
}
