using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models.ValueObjects;
using FI.WebAtividadeEntrevista.Models;
using System.Web.Services.Description;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            if (bo.VerificarExistencia(model.CPF))
            {
                this.ModelState.AddModelError("CPF", "CPF já cadastrado para cliente.");
            }
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select $"{error.ErrorMessage}").ToList();
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.Id = bo.Incluir(new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF.NumeroCPF
                });
                if (model.Beneficiarios != null && model.Beneficiarios.Any())
                {
                    BoBeneficiario boBeneficiario = new BoBeneficiario();
                    List<Beneficiario> beneficiarios = model.Beneficiarios.Select(x => new Beneficiario()
                    {
                        CPF = x.CPF.NumeroCPF,
                        IdCliente = model.Id,
                        Nome = x.Nome
                    }).ToList();
                    boBeneficiario.IncluirAlterarExcluir(beneficiarios, model.Id);
                }
                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            var cli = bo.Consultar(model.Id);

            if (cli.CPF != model.CPF && bo.VerificarExistencia(model.CPF))
            {
                this.ModelState.AddModelError("CPF", "CPF já cadastrado para cliente.");
            }

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF.NumeroCPF,
                });

                List<Beneficiario> beneficiarios = model.Beneficiarios == null ? new List<Beneficiario>() :
                    model.Beneficiarios.Select(x => new Beneficiario()
                    {
                        Id = x.Id,
                        CPF = x.CPF.NumeroCPF,
                        IdCliente = model.Id,
                        Nome = x.Nome
                    }).ToList();
                boBeneficiario.IncluirAlterarExcluir(beneficiarios, model.Id);
            }

            return Json("Cadastro alterado com sucesso");
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;
            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF,
                    Beneficiarios = boBeneficiario.Listar(id).Select(x => new BeneficiarioModel()
                    {
                        CPF = x.CPF,
                        Id = x.Id,
                        IdCliente = x.IdCliente,
                        Nome = x.Nome
                    }).ToList()
                };
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}