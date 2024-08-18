using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using FI.WebAtividadeEntrevista.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {

        public ActionResult Index()
        {
            return View();
            //return Json(beneficiarios, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult Incluir(BeneficiarioModel model)
        //{
        //    BoBeneficiario bo = new BoBeneficiario();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            Beneficiario beneficiario = new Beneficiario()
        //            {
        //                CPF = model.CPF.NumeroCPF,
        //                IdCliente = model.IdCliente,
        //                Nome = model.Nome
        //            };

        //            bo.Incluir(beneficiario);

        //            return Json(new { success = true });
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { success = false, message = ex.Message });
        //        }
        //    }
        //    return Json(new { success = false, message = "Dados inválidos." });
        //}

        //[HttpPost]
        //public JsonResult Listar(int idCliente)
        //{
        //    try
        //    {
        //        BoBeneficiario bo = new BoBeneficiario();
        //        List<Beneficiario> beneficiarios = bo.Listar(idCliente);
        //        return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = beneficiarios.Count });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Result = "ERROR", Message = ex.Message });
        //    }

        //}
    }
}