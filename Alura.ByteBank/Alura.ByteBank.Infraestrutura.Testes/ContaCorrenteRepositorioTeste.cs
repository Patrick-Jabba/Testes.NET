using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.DTO;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ContaCorrenteRepositorioTeste
    {
        private readonly IContaCorrenteRepositorio _contaCorrenteRepositorio;

        public ContaCorrenteRepositorioTeste()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();


            var provider = servico.BuildServiceProvider();
            _contaCorrenteRepositorio = provider.GetService<IContaCorrenteRepositorio>();
        }

        [Fact]
        public void TestarObterTodasAsContasCorrentes()
        {
            List<ContaCorrente> listaContas = _contaCorrenteRepositorio.ObterTodos();

            Assert.NotNull(listaContas);
            //Assert.Equal(3, listaContas.Count); 
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterVariasContasCorrentesPorId(int id)
        {
            var contaCorrente = _contaCorrenteRepositorio.ObterPorId(id);

            Assert.NotNull(contaCorrente);
            Assert.Equal(id, contaCorrente.Id);
        }

        [Fact]
        public void TestaAtualizaSaldoDeterminadaConta()
        {
            // Arrange
            var contaCorrente = _contaCorrenteRepositorio.ObterPorId(1);

            // Act
            double saldoNovo = 15;
            contaCorrente.Saldo = saldoNovo;

            var atualizado = _contaCorrenteRepositorio.Atualizar(1, contaCorrente);

            // Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaCriaUmaNovaContaCorrenteNoBancoDeDados()
        {
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Numero = 2760,
                Cliente = new Cliente()
                {
                    Nome = "Sidnelson Rainha",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Tenista Profissional"
                },
                Agencia = new Agencia()
                {
                    Identificador = Guid.NewGuid(),
                    Nome = "Agência Banco Central",
                    Id = 1,
                    Endereco = "Rua das Flores, 25",
                    Numero = 146

                }
            };

            // Act 
            var retorno = _contaCorrenteRepositorio.Adicionar(conta);

            // Assert
            Assert.True(retorno);

        }

        //[Theory]
        //[InlineData(3)]
        //[InlineData(4)]
        //[InlineData(5)]
        //[InlineData(6)]
        //public void TestaExclusaoDeVariasContasCorrentesNoBancoDeDados(int id)
        //{
        //    var retorno = _contaCorrenteRepositorio.Excluir(id);

        //    Assert.True(retorno);
        //}

        [Fact]
        public void TestaConsultaPix()
        {
            // Arrange
            var guid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a");
            var pix = new PixDTO() { Chave = guid, Saldo = 10 };

            var pixRepositorioMock = new Mock<IPixRepositorio>();

            pixRepositorioMock.Setup(x => x.consultaPix(It.IsAny<Guid>())).Returns(pix);

            var mock = pixRepositorioMock.Object;

            // Act
            var saldo = mock.consultaPix(guid).Saldo;

            // Assert
            Assert.Equal(10, saldo);

        }
        
    }
}
