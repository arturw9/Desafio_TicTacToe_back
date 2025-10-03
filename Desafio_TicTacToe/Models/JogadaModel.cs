namespace Desafio_TicTacToe.Models
{
    public class JogadaModel
    {
        public int Id { get; set; }
        public string Jogador { get; set; } // "X" ou "O"
        public string Nome { get; set; }    // Nome do jogador
        public int Posicao { get; set; }    // Índice do quadrado (0-8)
        public DateTime DataHora { get; set; }
    }
}