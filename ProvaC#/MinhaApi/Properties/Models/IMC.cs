using System;
using System.ComponentModel.DataAnnotations;

public class Imc
{
    public int Id { get; set; }

    public int AlunoId { get; set; }

    [Required(ErrorMessage = "O campo Peso é obrigatório.")]
    public double Peso { get; set; }

    [Required(ErrorMessage = "O campo Altura é obrigatório.")]
    public double Altura { get; set; }

    private double _valor;

    public double Valor
    {
        get
        {
            // Calcula o valor do IMC apenas se não estiver definido ainda
            if (_valor == 0 && Altura != 0)
            {
                _valor = Math.Round(Peso / (Altura * Altura), 2);
            }
            return _valor;
        }
        set
        {
            _valor = value;
        }
    }
}