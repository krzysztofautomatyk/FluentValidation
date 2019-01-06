using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ModelLibrary;

namespace DashboardUI.Validators
{
    public class PersonValidator:AbstractValidator<PersonModel>      
    {
        public PersonValidator()
        {
            //Sprawdzam czy Imię jest:
            // - puste,
            // - ma więcej niż 2 znaki a mniej niż 50
            // Jesli wystąpi jakiś błąd to wyświetl tylko pierwszy
            RuleFor(p => p.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Nic nie wpisałeś w polu  {PropertyName} ") // {PropertyName} wyświelta nazwę pola
                .Length(2, 50).WithMessage("Proszę wpisać prawidłowe imię ({TotalLength}) ")  // {TotalLength} => wyświetli w (ilość znaków) np. (32)
                .Must(BeValidName).WithMessage("Imię zawiera niedozowlone znaki proszę użyj liter");
        }

        protected bool BeValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }
    }
}
