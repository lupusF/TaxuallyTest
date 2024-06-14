using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxually.technicalTest;

namespace Taxually.TechnicalTest.Infrastructure;

public interface IVatRegistrationService
{
    public Task RegisterVATAsync(VatRegistrationRequest request);
}
