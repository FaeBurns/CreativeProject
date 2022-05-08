using System;
using TMPro;
using UnityEditor;

#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1502 // Element should not be on a single line
[CustomPropertyDrawer(typeof(ResourceIntSerializedDictionary))]
[CustomPropertyDrawer(typeof(ResourceTextSerializedDictionary))]
public class MyCustomDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
#pragma warning restore SA1502 // Element should not be on a single line
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1600 // Elements should be documented