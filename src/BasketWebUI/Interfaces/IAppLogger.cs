using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketWebUI.Interfaces
{
    /// <summary>
    /// Logging supported methods by the application.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}
