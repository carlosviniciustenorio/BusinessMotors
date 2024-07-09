using CManager.Infrastructure.Context.CManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CManager.Domain.Models;

namespace CManager.Tests.Infrastructure
{
    public class AnuncioRepositoryTest
    {
        [Theory]
        [InlineData("teste1")]
        [InlineData("teste2")]
        [InlineData("teste3")]
        public void Test(string value)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CManagerDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new CManagerDBContext(optionsBuilder.Options);

            context.Opcional.Add(new (value));
            context.SaveChanges();
            Assert.Single(context.Opcional);
        }
    }
}
