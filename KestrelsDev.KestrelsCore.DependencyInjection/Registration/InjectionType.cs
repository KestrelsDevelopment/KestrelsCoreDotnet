using System;
using System.Collections.Generic;
using System.Text;

namespace KestrelsDev.KestrelsCore.DependencyInjection.Registration;

public enum InjectionType
{
    Transient = 0, Scoped = 1, Singleton = 2
}
