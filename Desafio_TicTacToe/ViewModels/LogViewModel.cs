namespace Desafio_TicTacToe.ViewModels
{
    public class LogViewModel
    {
        public List<JogadorViewModel> Jogadores { get; set; } = new List<JogadorViewModel>();
        public string Vencedor { get; set; }
        public DateTime DataHoraPartida { get; set; }
        public List<JogadaViewModel> Log { get; set; } = new List<JogadaViewModel>();
    }
}