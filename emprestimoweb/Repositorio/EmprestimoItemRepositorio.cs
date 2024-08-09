using emprestimoweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emprestimoweb.Repositorio
{
    public class EmprestimoItemRepositorio
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        public void InserirItem(ItensEmprestimo objdados)
        {
            db.ItensEmprestimo.Add(objdados);
            db.SaveChanges();


        }
    }
}