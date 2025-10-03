using Desafio_TicTacToe.Data;
using Desafio_TicTacToe.Helpers;
using Desafio_TicTacToe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartidaController : ControllerBase
    {
        private readonly ILogger<PartidaController> _logger;
        private readonly AppDbContext _context;
        public PartidaController(ILogger<PartidaController> logger,
        AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// - Inseri os dados do resultado da partida no banco de dados.
        /// </summary>
        /// <param name="partidaViewModel">Objeto PartidaViewModel contendo os dados do resultado da partida a ser inserida.</param>
        /// <remarks>
        /// Significado dos status: Ganhou = 0, Perdeu = 1, Empatou = 2
        /// 
        /// 
        /// Exemplo de requisição a ser usado:
        /// 
        /// 
        ///{
        ///  "jogadores": [
        ///    {
        ///      "nome": "felipe",
        ///      "status": 0
        ///    },
        ///{
        ///      "nome": "maria",
        ///      "status": 1
        ///    }
        ///  ],
        ///  "dataHoraPartida": "2025-10-03T18:42:00.785Z"
        ///}
        /// </remarks>
        /// <returns>Inseri os dados do resultado da partida no banco de dados.</returns>
        /// <response code="200">Resultado da partida inserido com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar inserir o resultado da partida.</response>
        [HttpPost, Route("Gravar")]
        public async Task<IActionResult> Gravar(PartidaViewModel partidaViewModel)
        {
            try
            {
                var partidasHelper = new PartidasHelper();
                var partida = partidasHelper.PartidaViewModelToPartidaModel(partidaViewModel);
                await _context.Partidas.AddAsync(partida);
                _context.SaveChanges();

                return Ok(partidaViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// - Obtem o historico de partidas no banco de dados.
        /// </summary>
        /// <returns>Obtem o historico de partidas no banco de dados.</returns>
        /// <response code="200">Historico de partidas retornado com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar obter o historico de partidas.</response>
        [HttpGet, Route("HistoricoPartidas")]
        public async Task<IActionResult> HistoricoPartidas()
        {
            try
            {
                var historicoPartidas = await _context.Partidas
                    .Include(p => p.Jogadores)
                    .OrderByDescending(x => x.DataHoraPartida)
                    .ToListAsync();

                if (!historicoPartidas.Any())
                {
                    return Ok("Nenhum partida encontrada");
                }

                var jogadoresViewModel = new List<PartidaViewModel>();

                var partidasHelper = new PartidasHelper();
                jogadoresViewModel = partidasHelper.PartidaModelToPartidaViewModel(historicoPartidas);

                return Ok(jogadoresViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// - Obtem os ultimos vencedores no banco de dados.
        /// </summary>
        /// <returns>Obtem os ultimos vencedores no banco de dados.</returns>
        /// <response code="200">Ultimos vencedores retornados com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar obter ultimos vencedores.</response>
        [HttpGet, Route("UltimosVencedores")]
        public async Task<IActionResult> UltimosVencedores()
        {
            try
            {
                var ultimosVencedores = await _context.Partidas
                    .Include(p => p.Jogadores)
                    .OrderByDescending(x => x.DataHoraPartida)
                    .SelectMany(p => p.Jogadores)
                    .Where(j => j.Status == 0)
                    .Take(5)
                    .ToListAsync();

                if (!ultimosVencedores.Any())
                {
                    return Ok("Nenhum vencedor encontrado");
                }

                var jogadoresViewModel = new List<JogadorViewModel>();


                var partidasHelper = new PartidasHelper();

                jogadoresViewModel = partidasHelper.ListJogadorModelToListJogadorViewModel(ultimosVencedores);

                return Ok(jogadoresViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// - Obtem os ranking dos vencedores no banco de dados.
        /// </summary>
        /// <returns>Obtem os ranking dos vencedores no banco de dados.</returns>
        /// <response code="200">Ranking de vencedores retornado com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar obter ranking de vencedores.</response>
        [HttpGet, Route("RankingVencedores")]
        public async Task<IActionResult> RankingVencedores()
        {
            try
            {
                var ranking = await _context.Partidas
                    .Include(p => p.Jogadores)
                    .SelectMany(p => p.Jogadores)
                    .GroupBy(j => j.Nome.Trim().ToLower())
                    .Select(g => new
                    {
                        Nome = g.Select(x => x.Nome).FirstOrDefault(),
                        Status = 0,
                        Vitoria = g.Count(x => x.Status == 0)
                    })
                    .Where(r => r.Vitoria > 0)
                    .OrderByDescending(r => r.Vitoria)
                    .ToListAsync();

                return Ok(ranking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// - Inseri os dados de log da partida no banco de dados.
        /// </summary>
        /// <param name="logViewModel">Objeto LogViewModel contendo os dados de log da partida a ser inserido.</param>
        /// <remarks>
        /// Significado dos status: Ganhou = 0, Perdeu = 1, Empatou = 2
        /// 
        /// 
        /// Exemplo de requisição a ser usado:
        /// 
        /// 
        ///{
        ///  "jogadores": [
        ///    {
        ///      "nome": "Artur",
        ///      "status": 0
        ///    },
        ///{
        ///      "nome": "Maria",
        ///      "status": 1
        ///    }
        ///  ],
        ///  "vencedor": "Artur",
        ///  "dataHoraPartida": "2025-10-03T18:55:10.394Z",
        ///  "log": [
        ///    {
        ///      "jogador": "X",
        ///      "nome": "Artur",
        ///      "posicao": 0,
        ///      "dataHora": "2025-10-03T18:55:10.394Z"
        ///    },
        ///{
        ///    "jogador": "O",
        ///      "nome": "Maria",
        ///      "posicao": 2,
        ///      "dataHora": "2025-10-03T18:55:10.394Z"
        ///    },
        ///{
        ///    "jogador": "X",
        ///      "nome": "Artur",
        ///      "posicao": 8,
        ///      "dataHora": "2025-10-03T18:55:10.394Z"
        ///    }
        ///  ]
        ///}
        /// </remarks>
        /// <returns>Inseri os dados de log da partida no banco de dados.</returns>
        /// <response code="200">Dados de log da partida inserido com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar inserir os dados de log.</response>
        [HttpPost, Route("Logs")]
        public async Task<IActionResult> Logs(LogViewModel logViewModel)
        {
            try
            {
                var partidasHelper = new PartidasHelper();
                var partida = partidasHelper.LogViewModelToLogModel(logViewModel);
                await _context.Logs.AddAsync(partida);
                _context.SaveChanges();

                return Ok(logViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// - Obtem as melhores estratégias dos que venceram.
        /// </summary>
        /// <returns>Obtem as melhores estratégias dos que venceram no banco de dados.</returns>
        /// <response code="200">Melhores estratégias dos que venceram retornado com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar obter melhores estratégias dos que venceram.</response>
        [HttpGet, Route("MelhoresEstrategias")]
        public async Task<IActionResult> MelhoresEstrategias()
        {
            try
            {
                var partidasVencedoras = await _context.Logs
                    .Where(l => !string.IsNullOrEmpty(l.Vencedor))
                    .Include(l => l.Log)
                    .ToListAsync();

                var sequencias = new List<string[]>();

                foreach (var partida in partidasVencedoras)
                {
                    var jogadasVencedor = partida.Log
                        .Where(j => j.Nome == partida.Vencedor)
                        .OrderBy(j => j.DataHora)
                        .Select(j => j.Posicao.ToString())
                        .ToArray();

                    if (jogadasVencedor.Length >= 3)
                    {
                        sequencias.Add(jogadasVencedor.Take(3).ToArray());
                    }
                }

                var estatisticas = sequencias
                     .GroupBy(s => string.Join(",", s))
                     .Select(g => new
                     {
                         Movimentos = g.Key.Split(','),
                         Quantidade = g.Count()
                     })
                     .OrderByDescending(x => x.Quantidade)
                     .Take(5)
                     .ToList();

                return Ok(estatisticas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}