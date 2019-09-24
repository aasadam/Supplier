using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Models.ValueObjects
{
    public class ValidationError : ValueObject<ValidationError>
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }

        protected override int GetHashCodeHandle()
        {
            var hashCode = -455586387;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PropertyName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            return hashCode;
        }

        protected override bool EqualsHandle(ValidationError other)
        => this.PropertyName == other.PropertyName
            && this.Message == other.Message;

    }
}
