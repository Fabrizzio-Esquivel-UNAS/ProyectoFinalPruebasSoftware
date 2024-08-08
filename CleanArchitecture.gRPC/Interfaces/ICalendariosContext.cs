using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Calendarios;

namespace CleanArchitecture.gRPC.Interfaces;

public interface ICalendariosContext
{
    Task<IEnumerable<CalendarioViewModel>> GetCalendariosByIds(IEnumerable<Guid> ids);
}