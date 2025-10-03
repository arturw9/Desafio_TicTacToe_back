using Desafio_TicTacToe.Models;
using Desafio_TicTacToe.ViewModels;

namespace Desafio_TicTacToe.Helpers
{
    public class PartidasHelper
    {
        public PartidaModel PartidaViewModelToPartidaModel(PartidaViewModel partidaViewModel)
        {
            var jogadoresModel = new List<JogadorModel>();

            foreach (var jogador in partidaViewModel.Jogadores)
            {
                var jogadorModel = new JogadorModel
                {
                    Nome = jogador.Nome,
                    Status = jogador.Status
                };
                jogadoresModel.Add(jogadorModel);
            }

            var partidaModel = new PartidaModel
            {
                DataHoraPartida = partidaViewModel.DataHoraPartida,
                Jogadores = jogadoresModel
            };

            return partidaModel;
        }

        public List<JogadorViewModel> ListJogadorModelToListJogadorViewModel(List<JogadorModel> jogadormodel)
        {
            var jogadoresViewModel = new List<JogadorViewModel>();

            foreach (var jogador in jogadormodel)
            {
                var jogadorViewModel = new JogadorViewModel
                {
                    Nome = jogador.Nome,
                    Status = jogador.Status
                };
                jogadoresViewModel.Add(jogadorViewModel);
            }

            return jogadoresViewModel;
        }

        public List<PartidaViewModel> PartidaModelToPartidaViewModel(List<PartidaModel> partidas)
        {
            var partidasViewModel = new List<PartidaViewModel>();

            foreach (var partida in partidas)
            {
                var jogadoresViewModel = new List<JogadorViewModel>();

                foreach (var jogador in partida.Jogadores)
                {
                    var jogadorViewModel = new JogadorViewModel
                    {
                        Nome = jogador.Nome,
                        Status = jogador.Status
                    };
                    jogadoresViewModel.Add(jogadorViewModel);
                }

                var partidaViewModel = new PartidaViewModel
                {
                    DataHoraPartida = partida.DataHoraPartida,
                    Jogadores = jogadoresViewModel
                };

                partidasViewModel.Add(partidaViewModel);
            }

            return partidasViewModel;
        }

        public LogModel LogViewModelToLogModel(LogViewModel logViewModel)
        {
            var jogadoresModel = logViewModel.Jogadores.Select(j => new JogadorModel
            {
                Nome = j.Nome,
                Status = j.Status
            }).ToList();

            var jogadasModel = logViewModel.Log.Select(j => new JogadaModel
            {
                Jogador = j.Jogador,
                Nome = j.Nome,
                Posicao = j.Posicao,
                DataHora = j.DataHora
            }).ToList();

            var logModel = new LogModel
            {
                Vencedor = logViewModel.Vencedor,
                DataHoraPartida = logViewModel.DataHoraPartida,
                Jogadores = jogadoresModel,
                Log = jogadasModel
            };

            return logModel;
        }
    }
}