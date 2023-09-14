using CManager.Infrastructure.Context.CManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CManager.Tests.Infrastructure
{
    public class AnuncioRepositoryTest
    {
        [Fact]
        public void Test()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CManagerDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new CManagerDBContext(optionsBuilder.Options);

            context.Opcional.Add(new ("teste"));
            context.SaveChanges();
            Assert.Single(context.Opcional);
        }
    }
}
