﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Practices.Unity.Configuration.Tests.TestObjects.MyGenericTypes
{
    public interface IGenericService<T>
    {
        string ServiceStatus { get; }
    }
}
