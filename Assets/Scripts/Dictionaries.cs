using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1502 // Element should not be on a single line
#pragma warning disable SA1516 // Elements should be separated by blank line
#pragma warning disable SA1402 // File may only contain a single type
[Serializable] public class ResourceIntSerializedDictionary : SerializableDictionary<Resource, int> { }
[Serializable] public class ResourceTextSerializedDictionary : SerializableDictionary<Resource, TextMeshProUGUI> { }
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1516 // Elements should be separated by blank line
#pragma warning restore SA1502 // Element should not be on a single line
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1600 // Elements should be documented