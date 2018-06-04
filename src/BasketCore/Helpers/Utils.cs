using System;
using System.Collections.Generic;
using System.Text;
using BasketCore.Entities;
using BasketCore.Exceptions;

namespace BasketCore.Helpers
{
    public static class Utils
    {
        public static void ParameterNotNull(object input, string parameterName)
        {
            if (null == input)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        public static void EntityNotNull(int entityId, BaseEntity entity)
        {
            if (entity == null)
                throw new EntityNotFoundException(entityId);
        }
        public static void ParameterNotNullOrEmpty( string input, string parameterName)
        {
            ParameterNotNull(input, parameterName);
            if (input == String.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }
    }
}
