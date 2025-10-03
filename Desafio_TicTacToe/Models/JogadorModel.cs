using Desafio_TicTacToe.Enum;

namespace Desafio_TicTacToe.Models
{
    public class JogadorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ResultadoEnum Status { get; set; }
    }
}