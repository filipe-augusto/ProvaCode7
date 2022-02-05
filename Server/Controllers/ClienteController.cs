using AutoMapper;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProvaCode7.Shared;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaCode7.Server
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {

        private readonly AppDbContext _context;
        private IMapper _mapper;
        public ClienteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("PegaLista")]
        public async Task<ActionResult<List<ClienteListModelView>>> PegaLista()
        {
            try
            {
                //AsNoTracking busca mais rapida
                var listaClientes = _context.Cliente.Take(20).AsNoTracking().ToList();

                List<ClienteListModelView> ListaClientesModel = new List<ClienteListModelView>();
                //O MAPER PASSA A ENTIDADE PARA MODEL SEM A NECESSIDADE DE PASSAR VIA FOREACH
                ListaClientesModel = _mapper.Map<List<ClienteListModelView>>(listaClientes);


                //pegar  o nome do status
                foreach (var item in ListaClientesModel)
                {
                    ListaClientesModel.First(x => x.Id == item.Id).Status = RetornaStatusPorId(item.IdStatus);
                }


                return Ok(_mapper.Map<List<ClienteListModelView>>(ListaClientesModel));
            }
            catch (Exception ex)
            {
                return BadRequest(new RetornoRequisicao { Mensagem = ex.Message, Sucesso = false });
            }
        }


        [HttpGet("{idCliente}")]
        public async Task<ActionResult<ClienteViewModel>> Get(int idCliente)
        {
            try
            {
                //AsNoTracking busca mais rapida
                var cliente = _context.Cliente.FirstOrDefault(x => x.Id == idCliente);
                var endereco = _context.Endereco.FirstOrDefault(x => x.Id == cliente.IdEndereco);
                var listaProdutos = _context.Produto.AsNoTracking().ToList();
                var listaStatus = _context.StatusCliente.AsNoTracking().ToList();

                ClienteViewModel viewModel = new ClienteViewModel();
                viewModel.ListProdutos = new List<ProdutoListModelView>();
                viewModel.ListaStatus = new List<StatusClienteListModelView>();
                viewModel = _mapper.Map<ClienteViewModel>(cliente);
                viewModel.ListProdutos = _mapper.Map<List<ProdutoListModelView>>(listaProdutos);
                var produtosVendidos = _context.ProdutoOfertadoCliente.Where(x => x.IdCliente == cliente.Id).ToList();
                if (produtosVendidos.Count() > 0)
                {
                    foreach (var item in produtosVendidos)
                    {
                        viewModel.ListProdutos.First(x => x.Id == item.IdProduto).isSelect = true;
                    }
                }

                viewModel.Endereco = _mapper.Map<EnderecoViewModel>(endereco);
                //O MAPER PASSA A ENTIDADE PARA MODEL SEM A NECESSIDADE DE PASSAR VIA FOREACH
                viewModel.NomeStatus = RetornaStatusPorId(viewModel.IdStatus);
                viewModel.ListaStatus = _mapper.Map<List<StatusClienteListModelView>>(listaStatus);

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new RetornoRequisicao { Mensagem = ex.Message, Sucesso = false });
            }
        }


        [HttpPut]
        public async Task<ActionResult<RetornoRequisicao>> Put(ClienteViewModel clienteViewModel)
        {
            try
            {
                using (IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction())
                {
                    var registroCliente = _context.Cliente.FirstOrDefault(x => x.Id == clienteViewModel.Id);
                    registroCliente.Nome = clienteViewModel.Nome;
                    registroCliente.Cpf = clienteViewModel.Cpf;
                    registroCliente.Telefone = clienteViewModel.Telefone;

                    #region Endereco
                    Endereco endereco = _mapper.Map<Endereco>(clienteViewModel.Endereco);
                    _context.Entry(endereco).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    #endregion

                    if (registroCliente.IdStatus == null | registroCliente.IdStatus < 1)
                    {
                        registroCliente.IdStatus = (byte)EnumStatus.NomeLivre;
                    }

                    registroCliente.Credito = 1500;
                    _context.Entry(registroCliente).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    #region RegistroAtendimento
                    RegistroAtendimentos registro = new RegistroAtendimentos()
                    {
                        IdCliente = registroCliente.Id,
                        Descricao = "Ateração de dados do cliente.",
                        NomeDaMaquinaOuIP = Environment.MachineName

                    };

                    await _context.AddAsync(registro);
                    await _context.SaveChangesAsync();
                    #endregion
                    await dbContextTransaction.CommitAsync();
                    return Ok(new RetornoRequisicao { Sucesso = true, Mensagem = "Registro alterado com sucesso" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new RetornoRequisicao() { Mensagem = ex.Message, Sucesso = false });
            }
        }

        [HttpPut("AlteracaoOferta")]
        public async Task<ActionResult<RetornoRequisicao>> AlteracaoOferta(ClienteViewModel clienteViewModel)
        {
            try
            {
                using (IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction())
                {
                    var registroCliente = _context.Cliente.FirstOrDefault(x => x.Id == clienteViewModel.Id);
                    registroCliente.Nome = clienteViewModel.Nome;
                    registroCliente.Cpf = clienteViewModel.Cpf;
                    registroCliente.Telefone = clienteViewModel.Telefone;
                    //  registroCliente.
                    switch (clienteViewModel.IdStatus)
                    {
                        case (byte)EnumStatus.CaiuAligacao:
                            registroCliente.IdStatus = (byte)EnumStatus.CaiuAligacao;
                            await registroAtendimento("ligação caiu.", registroCliente.Id);
                            break;
                        case (byte)EnumStatus.Falecido:
                            registroCliente.IdStatus = (byte)EnumStatus.Falecido;
                            await registroAtendimento("Alteração cliente falceu.", registroCliente.Id);
                            break;
                        case (byte)EnumStatus.Viajou:
                            registroCliente.IdStatus = (byte)EnumStatus.Viajou;
                            await registroAtendimento("Alteração cliente viajou.", registroCliente.Id);
                            break;
                        case (byte)EnumStatus.NaoDesejaSerContado:
                            registroCliente.IdStatus = (byte)EnumStatus.NaoDesejaSerContado;

                            await registroAtendimento("Alteração cliente nao deseja ser contado.", registroCliente.Id);
                            break;
                        case (byte)EnumStatus.ClienteACeitouOferta:
                            registroCliente.IdStatus = (byte)EnumStatus.ClienteACeitouOferta;
                            registroCliente.Credito = registroCliente.Credito
                                - (decimal)clienteViewModel.ListProdutos.Where(x => x.isSelect).Sum(x => x.Valor);


                            #region RegistroAtendimento
                            RegistroAtendimentos registro = new RegistroAtendimentos()
                            {
                                IdCliente = registroCliente.Id,
                                Descricao = "Venda de produtos.",
                                NomeDaMaquinaOuIP = Environment.MachineName,
                                     DataCadastro = DateTime.Now
                            };

                            await _context.AddAsync(registro);
                            await _context.SaveChangesAsync();
                            #endregion

                            List<ProdutoListModelView> produtosSelecionados = clienteViewModel.ListProdutos.Where(x => x.isSelect).ToList();
                            foreach (var item in produtosSelecionados)
                            {
                                #region RegistroOFerta
                                ProdutoOfertadoCliente venda = new ProdutoOfertadoCliente();
                                venda.IdCliente = clienteViewModel.Id;
                                venda.IdProduto = item.Id;
                                venda.DiaDaOferta = DateTime.Now;
                                venda.IdRegistroAtendimento = registro.Id;
                                await _context.AddAsync(venda);
                                await _context.SaveChangesAsync();
                                #endregion
                            }
                            break;
                    }

                    #region Endereco
                    Endereco endereco = _mapper.Map<Endereco>(clienteViewModel.Endereco);
                    _context.Entry(endereco).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    #endregion

                    _context.Entry(registroCliente).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();
                    return Ok(new RetornoRequisicao { Sucesso = true, Mensagem = "Registro alterado com sucesso" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new RetornoRequisicao() { Mensagem = ex.Message, Sucesso = false });
            }
        }

        private async Task registroAtendimento(string descricao, int idCliente)
        {
            #region RegistroAtendimento
            RegistroAtendimentos registro = new RegistroAtendimentos()
            {
                IdCliente = idCliente,
                Descricao = descricao,
                NomeDaMaquinaOuIP = Environment.MachineName,
                DataCadastro  = DateTime.Now

            };

            await _context.AddAsync(registro);
            await _context.SaveChangesAsync();
            #endregion

            return;
        }

        [HttpPost]
        public async Task<ActionResult<RetornoRequisicao>> Post(ClienteViewModel clienteViewModel)
        {
            try
            {

                using (IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction())
                {
                    Cliente cliente = new Cliente();
                    Endereco endereco = new Endereco();
                    cliente = _mapper.Map<Cliente>(clienteViewModel);
                    endereco = _mapper.Map<Endereco>(clienteViewModel.Endereco);

                    try
                    {
                        await _context.AddAsync(endereco);
                        await _context.SaveChangesAsync();

                        cliente.IdEndereco = endereco.Id;
                        cliente.IdStatus = (byte)EnumStatus.NomeLivre;
                        cliente.Credito = 5;
                        await _context.AddAsync(cliente);
                        await _context.SaveChangesAsync();

                        await dbContextTransaction.CommitAsync();
                        return Ok(new RetornoRequisicao { Sucesso = true, Mensagem = "Cliente cadastrado com sucesso!" });
                    }
                    catch (Exception ex)
                    {
                        await dbContextTransaction.RollbackAsync();
                        return BadRequest(new RetornoRequisicao { Mensagem = ex.Message, Sucesso = false });
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new RetornoRequisicao() { Mensagem = ex.Message, Sucesso = false });
            }
        }

        [HttpGet("BuscaCliente")]
        public async Task<ActionResult<List<ClienteListModelView>>> BuscaCliente([FromQuery] string palavra)
        {
            List<ClienteListModelView> listaClientes = new List<ClienteListModelView>();
            try
            {

                //trazer somente cliente que nao esteja com status finalizado cliente
                var registroClientes = _context.Cliente
                        .Where(x => x.IdStatus == (byte)EnumStatus.NomeLivre || x.IdStatus == (byte)EnumStatus.CaiuAligacao || x.IdStatus == (byte)EnumStatus.Viajou)
                        .Where(x => x.Nome.ToLower().Contains(palavra.ToLower()) ||
              x.Cpf.Replace(".", "").Replace("-", "").ToLower()
              .Contains(palavra.Replace(".", "").Replace("-", "").ToLower())).ToList();
                listaClientes = _mapper.Map<List<ClienteListModelView>>(registroClientes);

                return Ok(listaClientes);
            }
            catch (Exception ex)
            {
                return BadRequest(new RetornoRequisicao { Mensagem = ex.Message, Sucesso = false });
            }
        }


        string RetornaStatusPorId(byte id)
        {
            if (id == 0)
            {
                return "";
            }
            var nomeStatus = _context.StatusCliente.FirstOrDefault(x => x.IdStatus == id).Descricao;


            return nomeStatus == null ? "" : nomeStatus;
        }
    }
}
