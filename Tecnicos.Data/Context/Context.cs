using Microsoft.EntityFrameworkCore;
using Tecnicos.Data.Models;

namespace Tecnicos.Data.Context;

public class Context : DbContext
{
	public Context(DbContextOptions<Context> options) : base(options) { }

	public DbSet<Clientes> Clientes { get; set; }
}

