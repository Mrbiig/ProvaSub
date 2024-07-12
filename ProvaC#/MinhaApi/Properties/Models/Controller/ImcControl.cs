using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinhaEmpresa.Controllers
{
    [ApiController]
    [Route("api/imc")]
    public class ImcController : ControllerBase
    {
        private static List<Imc> imcs = new List<Imc>();

        [HttpPost("cadastrar")]
        public ActionResult CadastrarImc([FromBody] Imc imc)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            imc.Id = imcs.Count + 1;
            imcs.Add(imc);

            return CreatedAtAction(nameof(GetImcById), new { id = imc.Id }, imc);
        }

        [HttpGet("listar")]
        public ActionResult<List<Imc>> ListarImcs()
        {
            return imcs;
        }

        [HttpGet("listarporaluno/{alunoId}")]
        public ActionResult<List<Imc>> ListarImcsPorAluno(int alunoId)
        {
            var imcsDoAluno = imcs.FindAll(i => i.AlunoId == alunoId);
            if (imcsDoAluno.Count == 0)
            {
                return NotFound();
            }
            return imcsDoAluno;
        }

        [HttpPut("alterar/{imcId}")]
        public IActionResult AlterarImc(int imcId, [FromBody] Imc imc)
        {
            var existingImc = imcs.FirstOrDefault(i => i.Id == imcId);
            if (existingImc == null)
            {
                return NotFound();
            }

            // Verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Atualiza os valores do IMC existente
            existingImc.Peso = imc.Peso;
            existingImc.Altura = imc.Altura;

            // Recalcula o Valor (IMC) com base nos novos valores
            existingImc.Valor = Math.Round(imc.Peso / (imc.Altura * imc.Altura), 2);

            return NoContent();
        }

        private ActionResult<Imc> GetImcById(int id)
        {
            var imc = imcs.FirstOrDefault(i => i.Id == id);
            if (imc == null)
            {
                return NotFound();
            }
            return imc;
        }
    }
}
