using System;
using System.Collections.Generic;
using System.Text;

namespace BasketApi.Web.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(int entityId) : base($"No entity found with id {entityId}")
        {
        }

        protected EntityNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
