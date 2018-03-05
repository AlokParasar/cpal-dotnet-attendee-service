using DataAccessLayer.Models;
using System;

[assembly: CLSCompliant(true)]

namespace DataAccessLayer.Repository
{
    public class AttendeeRepository : Repository<AttendeeModel>, IAttendeeRepository
    {
    }
}
