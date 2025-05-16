using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBibliotecario.Helpers
{
    public static class Validador
    {
        public static bool Validar(object obj, out List<String> erros)
        {
            erros = new List<string>();
            ValidationContext contexto = new ValidationContext(obj);
            List<ValidationResult> resultados = new List<ValidationResult>();
            bool ehValido = Validator.TryValidateObject(obj, contexto, resultados, true);

            if (obj == null)
            {
                erros.Add("O objeto não pode ser nulo.");
                return false;
            }

            if (!ehValido)
            {
                foreach (var erro in resultados)
                {
                    erros.Add(erro.ErrorMessage);
                }
            }

            return ehValido;
        }
    }
}
