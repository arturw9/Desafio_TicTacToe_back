namespace Desafio_TicTacToe.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public List<JogadorModel> Jogadores { get; set; } = new List<JogadorModel>();
        public string Vencedor { get; set; }
        public DateTime DataHoraPartida { get; set; }
        public List<JogadaModel> Log { get; set; } = new List<JogadaModel>();
    }
}