// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using CoronaTracker.Core.Models.ExternalCountryEvents;
using CoronaTracker.Core.Models.ExternalCountryEvents.Exceptions;

namespace CoronaTracker.Core.Services.Foundations.ExternalCountryEvents
{
    public partial class ExternalCountryEventService
    {
        private static void ValidateExternalCountryEvent(ExternalCountryEvent externalCountryEvent)
        {
            ValidateExternalCountryEventIsNotNull(externalCountryEvent);

            Validate(
                (Rule: IsInvalid(@object: externalCountryEvent.ExternalCountry),
                Parameter:nameof(externalCountryEvent.ExternalCountry)));
        }

        private static void ValidateExternalCountryEventIsNotNull(ExternalCountryEvent externalCountryEvent)
        {
            if( externalCountryEvent is null)
            {
                throw new NullExternalCountryEventException();
            }
        }

        private static dynamic IsInvalid(object @object) => new
        {
            Condition = @object is null,
            Message = "Object is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidExternalCountryEventException = new InvalidExternalCountryEventException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidExternalCountryEventException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidExternalCountryEventException.ThrowIfContainsErrors();
        }
    }
}

