using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Zenject;

namespace View.Input
{
    public class GameBoardInput : IGameBoardInput, ITickable
    {
        private readonly Match3ActionMap actionMap;
        private readonly Dictionary<Type, IInputMode> inputModes;

        private IInputMode currentMode;
        private bool isEnabled;
        private bool isDragging;

        public GameBoardInput(Match3ActionMap actionMap, Dictionary<Type, IInputMode> inputModes)
        {
            this.actionMap = actionMap;
            this.inputModes = inputModes;
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            isEnabled = true;
            actionMap.GameBoard.Enable();
            actionMap.GameBoard.Tap.performed += Tap;
            actionMap.GameBoard.Click.started += DragStarted;
            actionMap.GameBoard.Click.canceled += DragEnded;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            isEnabled = false;
            actionMap.GameBoard.Disable();
            actionMap.GameBoard.Tap.performed -= Tap;
            actionMap.GameBoard.Click.started -= DragStarted;
            actionMap.GameBoard.Click.canceled -= DragEnded;
        }

        public void Tick()
        {
            if (!isEnabled)
                return;
            if (!isDragging)
                return;

            currentMode?.Drag();
        }

        public void SetCurrentMode<T>() where T : IInputMode
        {
            T mode = GetInputMode<T>();
            if (mode == null)
                return;

            currentMode = mode;
            //Debug.Log($"Current Input mode: {typeof(T).Name}");
        }

        public T GetInputMode<T>() where T : IInputMode
        {
            if (!inputModes.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Input doesn't contain {typeof(T).Name}");
                return default;
            }

            return (T)inputModes[typeof(T)];
        }

        private void Tap(InputAction.CallbackContext context)
        {
            currentMode?.Tap(context);
        }

        private void DragStarted(InputAction.CallbackContext context)
        {
            isDragging = true;
            currentMode?.DragStarted(context);
        }

        private void DragEnded(InputAction.CallbackContext context)
        {
            if (actionMap.GameBoard.Tap.inProgress)
                return;

            isDragging = false;
            currentMode?.DragEnded(context);
        }
    }
}