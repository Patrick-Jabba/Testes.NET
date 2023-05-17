using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public ClienteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient < IClienteRepositorio, ClienteRepositorio > ();
            var provedor = servico.BuildServiceProvider();
            _clienteRepositorio = provedor.GetService<IClienteRepositorio>();
        }

        [Fact]
        public void TestarObterTodosClientes()
        {
            // Arrange
            //var _repositorio = new ClienteRepositorio();

            // Act
            List<Cliente> lista = _clienteRepositorio.ObterTodos();

            // Assert
            Assert.NotNull(lista);
            //Assert.Equal(8, lista.Count);
        }

        [Fact]
        public void TestarObterClientePorId()
        {
            // Arrange
            var id = 1;

            // Act
            var cliente = _clienteRepositorio.ObterPorId(id);

            // Assert
            Assert.NotNull(cliente);
            Assert.True(cliente.Id == id);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestarObterClientePorVariosId(int id)
        {
            // Act
            var cliente = _clienteRepositorio.ObterPorId(id);

            // Assert
            Assert.NotNull(cliente);
            Assert.True(cliente.Id == id);
        }


    }
}
