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

                return Ok(_mapper.Map<List<ClienteListModelView>>(listaClientes));
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
                ClienteViewModel viewModel = new ClienteViewModel();

                //O MAPER PASSA A ENTIDADE PARA MODEL SEM A NECESSIDADE DE PASSAR VIA FOREACH
                viewModel = _mapper.Map<ClienteViewModel>(cliente);


                return Ok(_mapper.Map<ClienteViewModel>(cliente));
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
                    Endereco endereco = _mapper.Map<Endereco>(clienteViewModel.Endereco);
                    _context.Entry(endereco).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    _context.Entry(registroCliente).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    await dbContextTransaction.CommitAsync();
                    return Ok(new RetornoRequisicao { Sucesso = true, Mensagem = "Registro alterado com sucesso" });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new RetornoRequisicao() { Mensagem = ex.Message, Sucesso = false });
            }
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
                    // cliente.Cpf = clienteViewModel.Cpf


                    endereco = _mapper.Map<Endereco>(clienteViewModel.Endereco);

                    try
                    {

                        await _context.AddAsync(endereco);
                        await _context.SaveChangesAsync();

                        cliente.IdEndereco = endereco.Id;
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




    }
}
