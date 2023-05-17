using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.Servico;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit.Abstractions;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTeste
    {
        private readonly IAgenciaRepositorio _agenciaRepositorio;
        public ITestOutputHelper SaidaConsoleTeste { get; set; }

        public AgenciaRepositorioTeste(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor Invocado");

            // Injetando dependências no construtor;
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _agenciaRepositorio = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            var agencia = _agenciaRepositorio.ObterPorId(1);

            Assert.NotNull(agencia);
        }


        // Exceções
        [Fact]
        public void TestaExcecaoObterAgenciaPorId()
        {
            // Act
            // Assert
            Assert.Throws<Exception>(
                () => _agenciaRepositorio.ObterPorId(33)
            );
        }

        [Fact]
        public void TestaAdicionarAgenciaMock()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agencia Amaral",
                Identificador = Guid.NewGuid(),
                Id = 3,
                Endereco = "Rua Aru Beizao",
                Numero = 5234
            };

            var repositorioMock = new ByteBankRepositorio();

            // Act
            var adicionado = repositorioMock.AdicionarAgencia(agencia);

            // Assert
            Assert.True(adicionado);

        }

        [Fact]
        public void TestaObterAgenciasMock()
        {
            // Arrange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            // Act
            var lista = mock.BuscarAgencias();

            // Assert
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());
        }


    }
}
