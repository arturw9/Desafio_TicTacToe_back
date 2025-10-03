using Desafio_TicTacToe.Controllers;
using Desafio_TicTacToe.Data;
using Desafio_TicTacToe.Enum;
using Desafio_TicTacToe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Desafio_TicTacToe.Teste.testeControllers
{
    public class PartidaControllerTests
    {
        private readonly AppDbContext _context;
        private readonly PartidaController _controller;

        public PartidaControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "DbTest")
            .Options;

            _context = new AppDbContext(options);

            var mockLogger = new Mock<ILogger<PartidaController>>();
            _controller = new PartidaController(mockLogger.Object, _context);
        }

        [Fact]
        public async Task Gravar_DeveRetornarOk_QuandoInserirPartida()
        {
            var partidaViewModel = new PartidaViewModel
            {
                DataHoraPartida = DateTime.Now,
                Jogadores = new List<JogadorViewModel>
                {
                    new JogadorViewModel { Nome = "Artur", Status = (ResultadoEnum)0 },
                    new JogadorViewModel { Nome = "Maria", Status = (ResultadoEnum)1 }
                }
            };

            var result = await _controller.Gravar(partidaViewModel);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var retorno = Assert.IsType<PartidaViewModel>(okResult.Value);
            Assert.Equal("Artur", retorno.Jogadores.First().Nome);
        }

        [Fact]
        public async Task HistoricoPartidas_DeveRetornarLista()
        {
            var result = await _controller.HistoricoPartidas();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UltimosVencedores_DeveRetornarLista()
        {
            var result = await _controller.UltimosVencedores();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RankingVencedores_DeveRetornarLista()
        {
            var result = await _controller.RankingVencedores();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}