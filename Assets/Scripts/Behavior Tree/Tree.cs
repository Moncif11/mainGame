using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

    // Start is called before the first frame update
       protected void Start()
        {
            _root =SetupTree();
        }

    // Update is called once per frame
        void Update()
        {
            if(_root != null){
                _root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }
}