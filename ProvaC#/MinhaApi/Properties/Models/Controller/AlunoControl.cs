using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MinhaEmpresa.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {
        private static List<Aluno> alunos = new List<Aluno>();

        [HttpPost("cadastrar")]
        public ActionResult CadastrarAluno([FromBody] Aluno aluno)
        {
            var alunoExistente = alunos.FirstOrDefault(a => a.Nome == aluno.Nome && a.Sobrenome == aluno.Sobrenome);
            if (alunoExistente != null)
            {
                return Conflict("Já existe um aluno cadastrado com o mesmo nome e sobrenome.");
            }

            aluno.Id = alunos.Count + 1;
            alunos.Add(aluno);

            return CreatedAtAction(nameof(GetAlunoById), new { id = aluno.Id }, aluno);
        }

        private ActionResult<Aluno> GetAlunoById(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return aluno;
        }
    }
}