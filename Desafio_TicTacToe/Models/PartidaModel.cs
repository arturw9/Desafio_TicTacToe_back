namespace Desafio_TicTacToe.Models
{
    public class PartidaModel
    {
        public int Id { get; set; }
        public List<JogadorModel> Jogadores { get; set; }
        public DateTime DataHoraPartida { get; set; }
    }
}