using GestionEntradasInventario.Data;
using GestionEntradasInventario.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionEntradasInventario.Services;

public class ProductosService(IDbContextFactory<ApplicationDbContext> dbContext)
{
    public async Task<bool> Existe(int productoId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Productos.AnyAsync(p => p.ProductoId == productoId); 
    }

    public async Task<bool> Insertar(Productos producto)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Productos.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Productos producto)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        contexto.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Productos producto)
    {
        if(!await Existe(producto.ProductoId))
        {
            return await Insertar(producto);
        }
        else
        {
            return await Modificar(producto);
        }
    }

    public async Task<bool> Eliminar(int productoId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Productos.AsNoTracking().Where(p => p.ProductoId == productoId).ExecuteDeleteAsync() > 0;
    }

    public async Task<Productos?> Buscar(int productoId)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Productos.FirstOrDefaultAsync(p => p.ProductoId == productoId);
    }

    public async Task<List<Productos>> Listar(Expression<Func<Productos, bool>> criterio)
    {
        await using var contexto = await dbContext.CreateDbContextAsync();
        return await contexto.Productos.Where(criterio).AsNoTracking().ToListAsync();
    }
}
