using DrugsMicroservice.BusinessLogic.Validators;
using FluentValidation.AspNetCore;
using MedicationMicroservice.BusinessLogic.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace MedicationMicroservice.BusinessLogic.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddRequestValidations(this IServiceCollection services)
    {
       services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DiseaseCreateDTOValidator>());
       services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DiseaseUpdateDTOValidator>());
       services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DrugCreateDTOValidator>());
       services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SubstanceCreateDTOValidator>());
        return services;
    }
}