using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Roebi.Common.Context;
using Roebi.Common.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Roebi.Tests.Common.UnitOfWorkTests
{
    public class UnitOfWorkTests
    {
        private readonly RoebiContext _roebiContext;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkTests() {
            var options = new DbContextOptionsBuilder<RoebiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
            _roebiContext = new RoebiContext(options);
            _unitOfWork = new UnitOfWork(_roebiContext);
        }

        [Fact]
        public void Will_call_save_changes()
        {
            var result = _unitOfWork.Save();
            Assert.IsType<int>(result);
        }
    }
}
