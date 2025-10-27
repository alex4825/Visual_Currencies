﻿using Assets._Project.Develop.Runtime.UI.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.CommonViews
{
    public class ElementsListView<TElement> : MonoBehaviour, IView where TElement : MonoBehaviour, IView
    {
        [SerializeField] private Transform _parent;

        private List<TElement> _elements = new();

        public IReadOnlyList<TElement> Elements => _elements;

        public void Add(TElement element)
        {
            element.transform.SetParent(_parent, false);
            _elements.Add(element);
        }

        public void Remove(TElement element)
        {
            if (element == null)
                return;

            element.transform.SetParent(null, false);
            _elements.Remove(element);
        }
    }
}
