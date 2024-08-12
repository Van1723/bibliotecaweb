using emprestimoweb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public void FechaEmprestimo (Emprestimo objdados)
        {
            db.Entry(objdados).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}