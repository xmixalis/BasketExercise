﻿using System;
using BasketApi.Infrastructure.Entities;
using BasketApi.Web.Exceptions;

namespace BasketApi.Web.Helpers
{
    public static class Guard
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
