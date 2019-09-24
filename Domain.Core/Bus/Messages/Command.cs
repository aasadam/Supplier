using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core.Models.ValueObjects;
using FluentValidation.Results;

namespace Domain.Core.Bus
{
    public abstract class Command : Message
    {
        public ValidationResult _validationResult { get; protected set; }

        public abstract ValidationResult SetValidationResult();

        public virtual bool IsValid()
        {
            _validationResult = SetValidationResult();
            return _validationResult?.IsValid ?? true;
        }

        public virtual IEnumerable<string> GetErrorMessages()
        {
            if (_validationResult == null) yield break;

            foreach (var error in _validationResult.Errors) yield return error.ErrorMessage;
        }

        public virtual IEnumerable<ValidationError> GetErrors()
        {
            if (_validationResult == null) yield break;

            foreach (var error in _validationResult.Errors) yield return new ValidationError
            {
                PropertyName = error.PropertyName,
                Message = error.ErrorMessage
            };
        }
    }
}
