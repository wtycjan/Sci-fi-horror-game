﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace VHS
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Variables    
            [Space,Header("Interactable Settings")]

            [SerializeField] private bool holdInteract = true;
            [ShowIf("holdInteract")][SerializeField] private float holdDuration = 1f;
            
            [Space] 
            [SerializeField] private bool multipleUse = false;
            [SerializeField] public bool isInteractable = true;

            [SerializeField] private string tooltipMessage = "interact";
        #endregion

        #region Properties    
            public float HoldDuration => holdDuration; 

            public bool HoldInteract => holdInteract;
            public bool MultipleUse => multipleUse;
            public bool IsInteractable => isInteractable;

            public string TooltipMessage => tooltipMessage;
        #endregion

        #region Methods
        public virtual void OnInteract()
            {
            SendMessage("Interact");
                Debug.Log("INTERACTED: " + gameObject.name);
            }
        #endregion
    }
}
